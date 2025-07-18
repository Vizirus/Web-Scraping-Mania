using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SearchParseFuncs
    {

        private MainWindowViewModel mainWindowViewModel;
        private FormatingFuncs formatingFuncs;
        public SearchParseFuncs(MainWindowViewModel mainWindowViewModel)
        {
            this.mainWindowViewModel = mainWindowViewModel;
            //formatingFuncs = new FormatingFuncs();
        }


        public HtmlNodeCollection GetCode(string tag, string link)
        {
            HtmlWeb htmlPage = new HtmlWeb();
            var htmlCode = htmlPage.Load(link);
            HtmlNodeCollection node = htmlCode.DocumentNode.SelectNodes(tag);
            return node;
        }
        public string GetAllCode(string link)
        {
            HtmlWeb htmlPage = new HtmlWeb();
            var htmlCode = htmlPage.Load(link);
            return htmlCode.DocumentNode.OuterHtml;
        }

        public async Task ParseByTag(string Tag, ParsePropModel selectedItem)
        {
            try
            {
                if (Tag != string.Empty)
                {
                    await selectedItem.SelectionFunc(mainWindowViewModel.SelectedItem.Link, Tag);
                    mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        /*public void FindText(string data, ObservableCollection<TabItemModel> tabItemModels, int searchIndex, List<TabItemModel> itemModelsSilent)
        {
            string[] tab = { "HTML", "CSS", "JS або PHP" };
            if (tabItemModels.Count > 0)
            {

                foreach (var tabItem in tabItemModels)
                {
                    tabItem.SelectionCollection.Clear();
                    foreach (Match match in Regex.Matches(tabItem.TabItemText, data))
                    {
                        tabItem.SelectionCollection.Add([match.Index, match.Value.Length]);
                    }
                    if (tabItem.SelectionCollection.Count > 0)
                    {
                        tabItem.IsTextBoxFocused = false;
                        tabItem.SearchCount[1] = tabItem.SelectionCollection.Count;
                        tabItem.SearchCount[2] = 1;
                        mainWindowViewModel.SelectionStart = tabItem.SelectionCollection[0][0];
                        mainWindowViewModel.SelectionLength = tabItem.SelectionCollection[0][1];
                        tabItem.IsTextBoxFocused = true;
                        mainWindowViewModel.IsEnabled[searchIndex] = true;
                    }
                    else
                    {
                        itemModelsSilent.Add(tabItem);
                        if (tabItemModels.Count == 0)
                        {
                            break;
                        }
                    }
                }
                if (itemModelsSilent.Count > 0)
                {
                    foreach (var i in itemModelsSilent)
                    {
                        tabItemModels.Remove(i);
                    }
                }
                if (tabItemModels.Count == 0)
                {
                    MessageBox.Show($"У блоці {tab[searchIndex]} не було знайдено жодного слова!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    mainWindowViewModel.IsEnabled[searchIndex] = false;
                }

            }
            else
            {
                MessageBox.Show("Не було обрано вкладку, у якій треба провести пошук!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
        public string GetTitle(string link)
        {
            string result = String.Empty;
            HtmlNodeCollection collection = GetCode("//title", link);  
            if(collection is not null)
            {
                result = collection[0].InnerText;
            }
            else
            {
                result = "Не вдалося знайти заголовок!";
            }
            return result;
        }

        /*public async Task ReturnObjAttribute(Microsoft.Web.WebView2.Wpf.WebView2 webView, OpenPreviewWindowViewModel viewModel)
        {
            FormatingFuncs formatingFuncs = new FormatingFuncs();

            string result = formatingFuncs.FormatHTML(await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.outerHTML;"));
            string id = await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.id;");
            string className = await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.className;");
            //result = formatingFuncs.FormatHTML(result);
            if (result != "" || result != "null")
            {
                ObservableCollection<TabItemModel> tabs = new ObservableCollection<TabItemModel>();
                tabs.Add(new TabItemModel("Результати парсингу", result));
                mainWindowViewModel.WebSites.Add(new Models.ComboBoxItem() { Link = webView.CoreWebView2.Source, Title = "Пошук з браузера id = " + id + " class = " + className, HtmlFiles = tabs });
                viewModel.ClassName = className;
                viewModel.IdName = id;
                mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.WebSites));
            }
            else
            {
                MessageBox.Show("Невдалося спарсити код активного елемента!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }*/
        /*public async Task ReturnSelectedTextAtributes(Microsoft.Web.WebView2.Wpf.WebView2 webView, OpenPreviewWindowViewModel viewModel)
        {
            string result = formatingFuncs.FormatHTML(await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.outerHTML;"));
            string id = await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.id;");
            string className = await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.className;");
            //result = formatingFuncs.FormatHTML(result);
            if (result != "" || result != "null")
            {
                ObservableCollection<TabItemModel> tabs = new ObservableCollection<TabItemModel>();
                tabs.Add(new TabItemModel("Результати парсингу", result));
                mainWindowViewModel.WebSites.Add(new Models.ComboBoxItem() { Link = webView.CoreWebView2.Source, Title = "Пошук з браузера id = " + id + " class = " + className, HtmlFiles = tabs });
                viewModel.ClassName = className;
                viewModel.IdName = id;
                mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.WebSites));
            }
            else
            {
                MessageBox.Show("Невдалося спарсити код активного елемента!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/
        public string LinkForamtion(string WebPageLink, string ResourceLink)
        {
            string ResultQueryString = String.Empty;
            if (ResourceLink.Contains("http") || ResourceLink.Contains("https"))
            {
                ResultQueryString = ResourceLink;
            }
            else
            {
                string[] CutedWebPageLink = WebPageLink.Split("/");
                if (ResourceLink.IndexOf("/") == 0)
                {
                    ResultQueryString = CutedWebPageLink[0] + "//" + CutedWebPageLink[2] + ResourceLink;
                    if (ResultQueryString.Contains("auth"))
                    {
                        ResultQueryString = ResultQueryString.Replace("www", "auth");
                    }
                }
                else
                {
                    ResultQueryString = CutedWebPageLink[0] + "//" + CutedWebPageLink[2] + "/" + ResourceLink;
                    if (ResultQueryString.Contains("auth"))
                    {
                        ResultQueryString = ResultQueryString.Replace("www", "auth");
                    }
                }
            }
            //GC.Collect();
            return ResultQueryString;
        }
        public string FindFileName(string link)
        {
            string[] CutedWebPageLink = link.Split("/");
            
            return CutedWebPageLink[CutedWebPageLink.Length-1];
        }
    }
}

