using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SearchParseFuncs
    {
        private MainWindowViewModel mainWindowViewModel;
        public SearchParseFuncs() //Only for test purposes, do not use in production code
        {
                
        }
        public SearchParseFuncs(MainWindowViewModel viewModel)
        {
          mainWindowViewModel = viewModel;
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
            string resultCode = String.Empty;
            try
            {
                HtmlWeb htmlPage = new HtmlWeb();
                var htmlCode = htmlPage.Load(link);
                HtmlNode node = htmlCode.DocumentNode.SelectSingleNode("//*");
                resultCode = node.OuterHtml;
            }
            catch (Exception ex)
            {
                resultCode = "Сталася помилка, тому не вдалось спарсити ";
                MessageBox.Show(ex.Message, "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return resultCode;
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
        public List<int[]> FindText(string data, TabItem tabItem, int[] dictCount, int boolIndex)
        {
            string[] tab = { "HTML", "CSS", "JS або PHP" };
            List<int[]> selectionCollection = new List<int[]>();
            if (tabItem is not null)
            {
                
                TextBox textBox = tabItem.Content as TextBox;
                string text = textBox.Text;
                if (text.Contains(data) == true)
                {
                    try
                    {
                        foreach (Match match in Regex.Matches(text, data))
                        {
                            selectionCollection.Add([match.Index, match.Value.Length]);
                        }
                        if(selectionCollection.Count == 0)
                        {
                            dictCount[1] = 0;
                            dictCount[0] = 0;
                            dictCount[2] = 0;
                            mainWindowViewModel.IsEnabled[boolIndex] = false;
                            throw new Exception();
                        }
                        dictCount[1] = selectionCollection.Count;
                        mainWindowViewModel.IsEnabled[boolIndex] = true;
                    }
                    catch
                    {
                        MessageBox.Show("Не вдалося провести пошук! Введений символ був неправильний!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show($"Не вдалося знайти слово або символ в тексті {tab[boolIndex]} вкладки!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                    
                    dictCount[1] = 0;
                    dictCount[0] = 0;
                    dictCount[2] = 0;
                    mainWindowViewModel.IsEnabled[boolIndex] = false;
                }
                
            }
            else
            {
                MessageBox.Show($"Вкладка {tab[boolIndex]} не була обрана", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return selectionCollection;
        }
        public string GetTitle(string link)
        {
            string result = String.Empty;
            try
            {
                HtmlNodeCollection collection = GetCode("//title", link);
                foreach (HtmlNode node in collection)
                {
                    result = node.InnerText;
                }
            }
            catch
            {
                result = "Сталася помилка, не вдалося знайти сторінку!";
            }
            return result;
        }
        private string formQuery(string id, string clas, string node)
        {
            string query = String.Empty;
            if (id == "\"\"")
            {
                query = $"//{node.Substring(1, node.Length - 2)}[@class={clas}]";
            }
            else if (clas == "\"\"")
            {
                query = $"//{node.Substring(1, node.Length - 2)}[@id={id}]";
            }
            else if (clas == "\"\"" && id == "\"\"")
            {
                query = String.Empty;
            }
            else
            {
                query = $"//{node.Substring(1, node.Length - 2)}[@class={clas} and @id={id}]";
            }
            return query;

        }
        private void AddFromBrowser(string id, string className, Microsoft.Web.WebView2.Wpf.WebView2 webView, string Node)
        {
            string Query = formQuery(id, className, Node);
            if (Query != String.Empty)
            {
                string result = String.Empty;
                HtmlNodeCollection collection = GetCode(Query, webView.CoreWebView2.Source);
                if (collection != null)
                {
                    foreach (HtmlNode node in collection)
                    {
                        result += node.OuterHtml + "\n";
                    }
                    ObservableCollection<TabItem> tabs = new ObservableCollection<TabItem>();
                    tabs.Add(new TabItem() { Header = "Code", Content = new TextBox() { Text = result } });
                    mainWindowViewModel.WebSites.Add(new Models.ComboBoxItem() { Link = webView.CoreWebView2.Source, Title = "Пошук з браузера id = " + id + " class = " + className, HtmlFiles = tabs});
                    mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.WebSites));
                }
                else
                {
                    MessageBox.Show("Не вдалося спарсити код елемента!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public async Task ReturnObjAttribute(Microsoft.Web.WebView2.Wpf.WebView2 webView, OpenPreviewWindowViewModel viewModel)
        {
            string id = await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.id;");
            string Node = await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.tagName;");
            string className = await webView.CoreWebView2.ExecuteScriptAsync("document.activeElement.className");
            if (id == "\"\"" && className == "\"\"")
            {
                string query = "document.activeElement";
                while (id == "\"\"" && className == "\"\"")
                {
                    query += ".parentNode";
                    string ID = query + ".id;";
                    string CLASS = query + ".className;";
                    string NODE = query + ".tagName";
                    id = await webView.CoreWebView2.ExecuteScriptAsync(ID);
                    className = await webView.CoreWebView2.ExecuteScriptAsync(CLASS);
                    Node = await webView.CoreWebView2.ExecuteScriptAsync(NODE);
                }
            }
            Node = Node.ToLower();
            viewModel.IdName = id;
            viewModel.ClassName = className;
            AddFromBrowser(id, className, webView, Node);

        }
        public async Task ReturnSelectedTextAtributes(Microsoft.Web.WebView2.Wpf.WebView2 webView, OpenPreviewWindowViewModel viewModel)
        {
            string id = await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.id;");
            string Node = await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.tagName;");
            string className = await webView.CoreWebView2.ExecuteScriptAsync("document.getSelection().anchorNode.parentNode.className;");
            if (id == "\"\"" && className == "\"\"")
            {
                string query = "document.getSelection().anchorNode.parentNode";
                while (id == "\"\"" && className == "\"\"")
                {
                    query += ".parentNode";
                    string ID = query + ".id;";
                    string CLASS = query + ".className;";
                    string NODE = query + ".tagName";
                    id = await webView.CoreWebView2.ExecuteScriptAsync(ID);
                    className = await webView.CoreWebView2.ExecuteScriptAsync(CLASS);
                    Node = await webView.CoreWebView2.ExecuteScriptAsync(NODE);
                }
            }
            viewModel.IdName = id;
            Node = Node.ToLower();
            viewModel.ClassName = className;
            AddFromBrowser(id, className, webView, Node);
        }
        public string LinkForamtion(string WebPageLink, string ResourceLink)
        {
            string ResultQueryString = String.Empty;
            if(ResourceLink.Contains("http")) 
            {
                ResultQueryString = ResourceLink;
            }
            else
            {
                string[] CutedWebPageLink = WebPageLink.Split("/");
                ResultQueryString = CutedWebPageLink[0] + "//" + CutedWebPageLink[2] + ResourceLink;
            }
            GC.Collect();
            return ResultQueryString;
        }
        public string FindFileName(string link, string fileType)
        {
            string[] CutedWebPageLink = link.Split("/");
            string ResultQueryString = CutedWebPageLink[CutedWebPageLink.Length - 1];
            int indexOfFileType = ResultQueryString.Length - fileType.Length;
            if (!ResultQueryString.Contains(fileType) || ResultQueryString.IndexOf(fileType) != indexOfFileType)
            {
                ResultQueryString += fileType;
            }
            return ResultQueryString;
        }
    }
}

