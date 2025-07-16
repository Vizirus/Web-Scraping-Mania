using HtmlAgilityPack;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Net.Http;
using System.Windows.Threading;
using System;
using System.Reflection.Metadata;
using System.Windows.Shapes;

namespace Web_Scraping_Mania.Commands.Functions
{
    internal class SaveFunctions
    {
        private MainWindowViewModel viewModel;
        private SearchParseFuncs searchParseFuncs;
        public SaveFunctions(MainWindowViewModel viewModel)
        {
            this.viewModel = viewModel;
            searchParseFuncs = new SearchParseFuncs(viewModel);
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
        public async Task AppendResourceToTabAsync(string link, string fileType, string XPath, string saveFilePath, int saveMode, ObservableCollection<TabItem> appendColection)
        {
            var HrefCollection = searchParseFuncs.GetCode(XPath, link);
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
                string code = await GetCodeStringAsync(downloadQuery);
                appendColection.Add(new TabItem() { Header = searchParseFuncs.FindFileName(downloadQuery, fileType), Content = new TextBox() { Text = code } });
                if (saveMode == 0)
                {
                    await DownloadCodeAsync(downloadQuery, filename);   
                }
            }
        }
        public void SaveFunc(TabItem tabItem)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|JavaScript|*.js|Html File|*.html|XML File|*.xml|PHP file|*.php";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            if (saveFileDialog.FileName != null)
            {
                if(tabItem is not null)
                {
                    TextBox? textBox = tabItem.Content as TextBox;
                    string code = textBox.Text;
                    using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                    {

                        if (saveFileDialog.FilterIndex == 1 || saveFileDialog.FilterIndex == 2 || saveFileDialog.FilterIndex == 3)
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
        private async Task saveEveryBlockAsync(string path, TabItem tabItem)
        {
            string? errorTabItemTitle = String.Empty;
            try
            {
                
                TextBox? textBox = tabItem.Content as TextBox;
                errorTabItemTitle = tabItem.Header as string;
                string fullPath = path + @"\\" +tabItem.Header;
                using (StreamWriter writer = new StreamWriter(fullPath))
                {
                    await writer.WriteAsync(textBox.Text);
                    writer.Close();
                }
                
            }
            catch
            {
                MessageBox.Show($"Під час завантаження файлу {errorTabItemTitle} сталася помилка!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            
        }
        private async Task CallSave(string foloderName, ObservableCollection<TabItem> filesCollection)
        {
            foreach (var file in filesCollection) 
            {
                await saveEveryBlockAsync(foloderName, file);
            }
        }
        public async Task SaveProjectOrSelected(int mode)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Multiselect = false;
            bool? isChoosed = openFolderDialog.ShowDialog();
            if (isChoosed == true)
            {
                if(mode == 0) 
                {
                    if (viewModel.WebSites.Count > 0)
                    {
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.HtmlFiles);
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.CssFiles);
                        await CallSave(openFolderDialog.FolderName, viewModel.SelectedItem.Scripts);
                        MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else if(mode == 1)
                {
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedHtmlTab);
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedCssTab);
                    await saveEveryBlockAsync(openFolderDialog.FolderName, viewModel.SelectedScriptTab);
                    MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                }

            }
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
            openFileDialog.Filter = "All Files|*.*|Text File|*.txt|CSS File|*.css|Html File|*.html|JavaScript|*.js";

            if (openFileDialog.ShowDialog() == true)
            {
                using (StreamReader stream = new StreamReader(openFileDialog.FileName))
                {
                    result = stream.ReadToEnd();
                    stream.Close();
                }
            }
            else
            {
                openFileDialog.FileName = "Не вдалося відкрити файл";
            }
            return new Models.ComboBoxItem() { /*Code = result,*/ Title = openFileDialog.FileName, Link = "No link" };
        }

        public async Task AddNewTab(string TabName, string code, string FilePath, int appendMode, string TabLink = "No Tab Link")
        {

            await Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                if (TabName == null || TabLink == null)
                {
                    MessageBox.Show("Не було введено назву вкладки, або посилання!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ObservableCollection<TabItem> htmlTabs = new ObservableCollection<TabItem>();
                    ObservableCollection<TabItem> cssTabs = new ObservableCollection<TabItem>();
                    ObservableCollection<TabItem> scriptTabs = new ObservableCollection<TabItem>();
                    htmlTabs.Add(new TabItem() { Header = TabName, Content= new TextBox() { Text = code} });
                    await AppendResourceToTabAsync(TabLink, ".css", "//link[contains(@href, '.css')]", FilePath, appendMode, cssTabs);
                    await AppendResourceToTabAsync(TabLink, ".js", "//script[contains(@src, '.js')]", FilePath, appendMode, scriptTabs);
                    Models.ComboBoxItem comboBoxItem = new Models.ComboBoxItem() { Link = TabLink, Title = TabName, HtmlFiles = htmlTabs, CssFiles = cssTabs, Scripts=scriptTabs};
                    viewModel.WebSites.Add(comboBoxItem);
                }
            });
        }//https://uk.wikipedia.org/wiki/%D0%A4%D1%80%D0%B0%D0%BA%D1%86%D1%96%D1%8F_%D0%B2%D0%B8%D0%BA%D0%B8%D0%B4%D1%83

    }
}
