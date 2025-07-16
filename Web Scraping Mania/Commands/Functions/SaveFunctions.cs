using HtmlAgilityPack;
using LanguageExt;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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

        public void SaveFunc(TabItemModel tabItem)
        {
            
        }//Rwrite this function
        private async Task SaveSelectedBlockAsync(TabItemModel selectedTab)
        {
            TextRange saveRange = new TextRange(selectedTab.TabDocument.ContentStart, selectedTab.TabDocument.ContentEnd);
            await using (StreamWriter fs = new StreamWriter(viewModel.SelectedItem.SavePath))
            {
                saveRange.Save(fs.BaseStream, DataFormats.Text);
                fs.Close();
            }
        }
        private async Task SaveEveryBlockAsync(ObservableCollection<TabItemModel> collectionModel)
        {
            foreach(var model in collectionModel)
            {
                await SaveSelectedBlockAsync(model);
            } 
        }
        public async Task SaveProjectOrSelected(int mode)
        {
            OpenFolderDialog openFolderDialog = new OpenFolderDialog();
            openFolderDialog.Multiselect = false;
            bool? isChoosed = openFolderDialog.ShowDialog();
            viewModel.SelectedItem.SavePath = openFolderDialog.FolderName;

            if (isChoosed == true)
            {
                try
                {
                    if (mode == 0)
                    {
                        if (viewModel.WebSites.Count > 0)
                        {
                            if(viewModel.SelectedItem == null)
                            {
                                MessageBox.Show("Не було обрано сайт, який потрібно зберегти!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                await SaveEveryBlockAsync(viewModel.SelectedItem.HtmlFiles);
                                await SaveEveryBlockAsync(viewModel.SelectedItem.CssFiles);
                                await SaveEveryBlockAsync(viewModel.SelectedItem.Scripts);
                                MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                            }   
                        }
                    }
                    else if (mode == 1)
                    {
                        if(viewModel.SelectedScriptTab is null && viewModel.SelectedCssTab is null && viewModel.SelectedHtmlTab is null)
                        {
                            MessageBox.Show("Один з типових файлів (CSS, HTML, JS) не було обрано!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            await SaveSelectedBlockAsync(viewModel.SelectedHtmlTab);
                            await SaveSelectedBlockAsync(viewModel.SelectedCssTab);
                            await SaveSelectedBlockAsync(viewModel.SelectedScriptTab);
                            MessageBox.Show("Файли було збереженно успішно!", "Інформація", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
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
        public async Task SaveGodeAsync(string link, string filePath)
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
        }//Rewrite this function
        private async Task AdditionalCodeParsing(ComboBoxItem comboBoxItems, string link, string fileType, string XPath)
        {
            var parsedTags = searchParseFuncs.GetCode(XPath, link);
            string downloadLink = string.Empty;
            HttpClient client = new HttpClient();
            foreach (var i in parsedTags)
            {
                if (fileType == "css")
                {
                    downloadLink = searchParseFuncs.LinkForamtion(link, i.Attributes["href"].Value);
                }
                else
                {
                    downloadLink = searchParseFuncs.LinkForamtion(link, i.Attributes["src"].Value);
                }
                
                var requestResult = await client.GetAsync(downloadLink);
                if (requestResult.IsSuccessStatusCode)
                { 
                        comboBoxItems.CssFiles.Add(new TabItemModel() { 
                            TabDocument = new FlowDocument(new Paragraph(new Run(await requestResult.Content.ReadAsStringAsync()))), 
                            Title = $"{searchParseFuncs.FindFileName(downloadLink)}.{fileType}"
                        });
                }
            }
        }
        public void AddNewTab(string TabName, string TabLink)
        {
            if (TabName == null || TabLink == null)
            {
                MessageBox.Show("Не було введено назву вкладки, або посилання!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                string Document = searchParseFuncs.GetAllCode(TabLink);
                if (Document == null)
                {
                    //HTML
                    string title = searchParseFuncs.GetTitle(TabLink);
                    ComboBoxItem comboBoxItem = new ComboBoxItem() { Link = TabLink,
                        Title = TabName,
                        HtmlFiles = new ObservableCollection<TabItemModel>(),
                        CssFiles = new ObservableCollection<TabItemModel>(),
                        Scripts = new ObservableCollection<TabItemModel>()
                    };
                    comboBoxItem.HtmlFiles.Add(new TabItemModel()
                    {
                        Title = searchParseFuncs.FindFileName(TabLink) + ".html",
                        TabDocument = new FlowDocument(new Paragraph(new Run(Document)))
                    });


                    App.Current.Dispatcher.Invoke(() =>
                    {
                        viewModel.WebSites.Add(comboBoxItem);
                    });
                }
            }
        }
        public void AddNewTab(string TabName, string TabLink, string FilePath)
        {
            if (TabName == null || TabLink == null)
            {
                MessageBox.Show("Не було введено назву вкладки, або посилання!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                
            }
        }
        

    }
}
