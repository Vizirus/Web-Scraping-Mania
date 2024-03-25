using HtmlAgilityPack;
using Microsoft.Win32;
using System;
using System.IO;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SearchParseFuncs
    {

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
        public async Task DownloadCodeAsync(string link)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|CSS File|*.css|Html File|*.html|JavaScript|*.js|PNG image|*.png|WEBP Image|*.webp|PHP|*.php";
            saveFileDialog.ShowDialog();
            if (link != null && link.Contains("http") && link.Contains("https"))
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
        public async Task ParseByTag(MainWindowViewModel mainWindowViewModel, string Tag, ParsePropModel selectedItem)
        {
            try
            {
                if (Tag != string.Empty)
                {
                    await selectedItem.SelectionFunc(mainWindowViewModel, mainWindowViewModel.SelectedItem.Link, Tag);
                    mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.SelectedItem));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
        public void FindText(MainWindowViewModel mainWindowViewModel, string data, string text)
        {
            if (text.Contains(data) == true)
            {
                mainWindowViewModel.SelectionDict.Clear();
                foreach (Match match in Regex.Matches(text, data))
                {
                    mainWindowViewModel.SelectionDict.Add(match.Index, match.Value.Length);
                }
                mainWindowViewModel.IsPMButActive = true;
                mainWindowViewModel.DictCount = mainWindowViewModel.SelectionDict.Count;
                mainWindowViewModel.OnPropertyChanged(nameof(mainWindowViewModel.IsPMButActive));
            }
            else
            {
                MessageBox.Show("Не вдалося знайти слово або символ в тексті!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
        private void AddFromBrowser(MainWindowViewModel viewModel, string id, string className, Microsoft.Web.WebView2.Wpf.WebView2 webView, string Node)
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
                    viewModel.WebSites.Add(new ComboBoxItem() { Code = result, Link = webView.CoreWebView2.Source, Title = "Пошук з браузера id = " + id + " class = " + className });
                    viewModel.OnPropertyChanged(nameof(viewModel.WebSites));
                }
                else
                {
                    MessageBox.Show("Не вдалося спарсити код елемента!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        public async Task ReturnObjAttribute(Microsoft.Web.WebView2.Wpf.WebView2 webView, MainWindowViewModel viewModel)
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
            AddFromBrowser(viewModel, id, className, webView, Node);

        }
        public async Task ReturnSelectedTextAtributes(Microsoft.Web.WebView2.Wpf.WebView2 webView, MainWindowViewModel viewModel)
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
            AddFromBrowser(viewModel, id, className, webView, Node);
        }
    }
}

