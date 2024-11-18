using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SaveFunctions
    {
        private MainWindowViewModel viewModel { get; set; }
        private SearchParseFuncs searchParseFuncs;
        public SaveFunctions(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
            searchParseFuncs = new SearchParseFuncs(viewModel);
        }

        public ObservableCollection<TabItemModel> AppendResourceToTabDAsync(string link, string fileType, string XPath, string saveFilePath, int saveMode)
        {
            ObservableCollection<TabItemModel> resultTabs = new ObservableCollection<TabItemModel>();
            var HrefCollection = searchParseFuncs.GetCode(XPath, link);
            FormatingFuncs formatingFuncs = new FormatingFuncs();
            foreach (var i in HrefCollection)
            {
                string downloadQuery = String.Empty;
                if (fileType == ".js")
                {
                    downloadQuery = searchParseFuncs.LinkForamtion(link, i.Attributes["src"].Value);
                }
                else
                {
                    downloadQuery = searchParseFuncs.LinkForamtion(link, i.Attributes["href"].Value);
                }
                string filename = saveFilePath + '\\' + searchParseFuncs.FindFileName(downloadQuery, fileType).Replace("?", "");
                if (saveMode == 0)
                {
                    Task task = new Task(async () =>
                    {
                        try
                        {
                            await DownloadCodeAsync(downloadQuery, filename);
                            using (StreamReader stream = new StreamReader(filename))
                            {
                                Application.Current.Dispatcher.Invoke(() =>
                                {
                                    string content = stream.ReadToEnd();
                                    resultTabs.Add(new TabItemModel(searchParseFuncs.FindFileName(downloadQuery, fileType).Replace("?", ""), formatingFuncs.FormatJS(content)));
                                    stream.Close();
                                });
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    });
                    task.Start();
                }
                else
                {
                    string code = String.Empty;
                    Task.Run(async () =>
                    {
                        code = await GetCodeStringAsync(downloadQuery);
                        App.Current.Dispatcher.Invoke(() => resultTabs.Add(new TabItemModel(searchParseFuncs.FindFileName(downloadQuery, fileType).Replace("?", ""), formatingFuncs.FormatJS(code))));
                    });
                }

            }
            //GC.Collect();
            return resultTabs;
        }
        public void SaveFunc(TabItemModel tabItem)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|JavaScript|*.js|Html File|*.html|XML File|*.xml|PHP file|*.php|CSS file|*.css|JS File|*.js";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != null)
            {
                if (tabItem is not null)
                {
                    string code = tabItem.TabItemText;
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {

                        if (saveFileDialog.FilterIndex == 1 || saveFileDialog.FilterIndex == 2 || saveFileDialog.FilterIndex == 3 || saveFileDialog.FilterIndex == 5 || saveFileDialog.FilterIndex == 6 || saveFileDialog.FilterIndex == 7)
                        {
                            writer.Write(code);
                        }
                        else if (saveFileDialog.FilterIndex == 4)
                        {
                            var htmlDoc = new HtmlDocument();
                            htmlDoc.LoadHtml(code);
                            htmlDoc.Save(writer);
                        }
                        writer.Close();
                        writer.Dispose();
                    }
                }
            }

        }
        private async Task saveEveryBlockAsync(string path, string header, string text)
        {
            string? errorTabItemTitle = String.Empty;
            try
            {

                errorTabItemTitle = header;
                string fullPath = path + @"\\" + header;
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    await writer.WriteAsync(text);
                    writer.Close();
                }

            }
            catch
            {
                MessageBox.Show($"Під час завантаження файлу {errorTabItemTitle} сталася помилка!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        private async Task CallSave(string foloderName, ObservableCollection<TabItemModel> filesCollection)
        {
            foreach (var file in filesCollection)
            {
                await saveEveryBlockAsync(foloderName, file.Header, file.TabItemText);
            }
        }
        public async Task SaveProjectOrSelected(int mode)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Multiselect = false;
            bool? isChoosed = openFolderDialog.ShowDialog();
            if (isChoosed == true)
            {
                if (mode == 0)
                {
                    if (viewModel.WebSites.Count > 0)
                    {
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.HtmlFiles);
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.CssFiles);
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.Scripts);
                        MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if (mode == 1)
                {
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedHtmlTab.Header, viewModel.SelectedHtmlTab.TabItemText);
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedCssTab.Header, viewModel.SelectedCssTab.TabItemText);
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedScriptTab.Header, viewModel.SelectedScriptTab.TabItemText);
                    MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
        }
        public async Task<string> GetCodeStringAsync(string link)
        {
            HttpClient client = new HttpClient();
            using HttpResponseMessage httpResponse = await client.GetAsync(link);
            string result = await httpResponse.Content.ReadAsStringAsync();
            client.Dispose();
            httpResponse.Dispose();
            return result;
        }
        public async Task DownloadCodeAsync(string link, string filePath)
        {
            HttpClient client = new HttpClient();

            var result = await client.GetStreamAsync(link);
            using (var sw = File.Create(filePath))
            {
                await result.CopyToAsync(sw);
                sw.Close();
                result.Close();
            }

            client.Dispose();
        }
        public async Task SaveResourceAsync(string link)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|CSS File|*.css|Html File|*.html|JavaScript|*.js|PNG image|*.png|WEBP Image|*.webp|PHP|*.php";
            saveFileDialog.ShowDialog();
            if (link != null && link.Contains("http") || link != null && link.Contains("https"))
            {
                try
                {
                    HttpClient client = new HttpClient();
                    var result = await client.GetStreamAsync(link);
                    using (var sw = File.Create(saveFileDialog.FileName))
                    {
                        await result.CopyToAsync(sw);
                        sw.Close();
                        result.Close();
                    }
                    client.Dispose();
                }
                catch
                {
                    MessageBox.Show("Такого ресурсу немає на сайті!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введе посилання написано не правильно!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public Models.ComboBoxItem OpenCode()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            string result = string.Empty;
            openFileDialog.Filter = "All Files|*.*|Text File|*.txt|CSS File|*.css|Html File|*.html|JavaScript|*.js|PHP|*.php";
            ObservableCollection<TabItemModel> tabItems = new ObservableCollection<TabItemModel>();
            Models.ComboBoxItem comboBoxItem = new Models.ComboBoxItem() { Link = "No link" };
            if (openFileDialog.ShowDialog() == true)
            {
                string fileName = openFileDialog.FileName;
                using (StreamReader stream = new StreamReader(fileName))
                {
                    result = stream.ReadToEnd();
                    var tabHeadera = fileName.Split(@"\");
                    string tabHeader = tabHeadera[tabHeadera.Length - 1];
                    comboBoxItem.Title = tabHeader;
                    if (tabHeader.Contains(".js"))
                    {
                        comboBoxItem.Scripts = new ObservableCollection<TabItemModel>() { new TabItemModel(tabHeader, result) };
                    }
                    else if (tabHeader.Contains(".css"))
                    {
                        comboBoxItem.CssFiles = new ObservableCollection<TabItemModel>() { new TabItemModel(tabHeader, result) };
                    }
                    else if (tabHeader.Contains(".html"))
                    {
                        comboBoxItem.HtmlFiles = new ObservableCollection<TabItemModel>() { new TabItemModel(tabHeader, result) };

                    }
                    stream.Close();
                }
            }
            else
            {
                comboBoxItem.HtmlFiles = new ObservableCollection<TabItemModel>() { new TabItemModel("Не вдалося відкрити файл!", "") };
                comboBoxItem.Title = "Помилка!";
            }

            return comboBoxItem;
        }
        private void doAction(string TabName, string TabLink, string code, string FilePath, int appendMode)
        {
            if (TabName == null || TabLink == null)
            {
                MessageBox.Show("Не було введено назву вкладки, або посилання!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ObservableCollection<TabItemModel> htmlTabs = new ObservableCollection<TabItemModel>() { new TabItemModel(TabName, code) };
                ObservableCollection<TabItemModel> cssTabs;
                ObservableCollection<TabItemModel> scriptTabs;
                cssTabs = AppendResourceToTabDAsync(TabLink, ".css", "//link[contains(@href, '.css')]", FilePath, appendMode);
                scriptTabs = AppendResourceToTabDAsync(TabLink, ".js", "//script[contains(@src, '.js')]", FilePath, appendMode);
                Models.ComboBoxItem comboBoxItem = new Models.ComboBoxItem() { Link = TabLink, Title = TabName, HtmlFiles = htmlTabs, CssFiles = cssTabs, Scripts = scriptTabs };
                viewModel.WebSites.Add(comboBoxItem);
                viewModel.SelectedItem = viewModel.WebSites[viewModel.WebSites.IndexOf(comboBoxItem)];
                viewModel.SelectedHtmlTab = htmlTabs[0];
                viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
            }
        }
        public void AddNewTab(string TabName, string code, string FilePath, int appendMode, MainWindowViewModel _viewModel, string TabLink = "No Tab Link")
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                 doAction(TabName, TabLink, code, FilePath, appendMode);
            });
            GC.Collect();
        }//https://uk.wikipedia.org/wiki/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D1%96%D1%8F_%D0%B2%D0%B8%D0%BA%D0%B8%D0%B4%D1%83

    }
}
