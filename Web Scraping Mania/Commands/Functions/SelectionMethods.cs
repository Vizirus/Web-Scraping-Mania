using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Windows;

using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SelectionMethods
    {
        private SearchParseFuncs searchParseFuncs { get; set; }
        public SelectionMethods()
        {
            searchParseFuncs = new SearchParseFuncs();
        }
        public async Task ParseByTagInnerHtml(MainWindowViewModel viewMoel, string link, string command)
        {
            try
            {
                HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
                string resultCode = string.Empty;
                if (HtmlNodeCol != null)
                {
                    foreach (var i in HtmlNodeCol)
                    {
                        if (resultCode.Contains(i.InnerHtml) == false)
                        {
                            resultCode += i.InnerHtml + "\n";
                        }
                    }
                    MainWindowViewModel mainViewModel = viewMoel;
                    mainViewModel.SelectedItem.Code = resultCode;
                    resultCode = null;
                }
                else
                {
                    MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Такого тегу немає на сайті!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task ParseByTagOuterHtml(MainWindowViewModel viewMoel, string link, string command)
        {
            try
            {
                HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
                string resultCode = string.Empty;
                if (HtmlNodeCol != null)
                {
                    foreach (var i in HtmlNodeCol)
                    {
                        if (resultCode.Contains(i.OuterHtml) == false)
                        {
                            resultCode += i.OuterHtml + "\n";
                        }

                    }
                    MainWindowViewModel mainViewModel = viewMoel;
                    mainViewModel.SelectedItem.Code = resultCode;
                    resultCode = null;
                }
                else
                {
                    MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Такого тегу немає на сайті!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task ParseByInnerText(MainWindowViewModel viewMoel, string link, string command)
        {
            try
            {
                HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
                string resultCode = string.Empty;
                if (HtmlNodeCol != null)
                {
                    foreach (var i in HtmlNodeCol)
                    {
                        if (resultCode.Contains(i.InnerText) == false)
                        {
                            resultCode += i.InnerText + "\n";
                        }
                    }
                    MainWindowViewModel mainViewModel = viewMoel;
                    mainViewModel.SelectedItem.Code = resultCode;
                    resultCode = null;
                }
                else
                {
                    MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Такого тегу немає на сайті!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task ParseId(MainWindowViewModel viewMoel, string link, string command)
        {

            try
            {
                HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
                string resultCode = string.Empty;
                if (HtmlNodeCol != null)
                {
                    foreach (var i in HtmlNodeCol)
                    {
                        if (resultCode.Contains(i.Id) == false)
                        {
                            resultCode += i.Id + "\n";
                        }
                    }
                    MainWindowViewModel mainViewModel = viewMoel;
                    mainViewModel.SelectedItem.Code = resultCode;
                    resultCode = null;
                }
                else
                {
                    MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch
            {
                MessageBox.Show("Такого тегу немає на сайті!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public async Task ParseAttributes(MainWindowViewModel viewMoel, string link, string command)
        {
            HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
            string resultAttribute = string.Empty;
            if (HtmlNodeCol != null)
            {
                foreach (var i in HtmlNodeCol)
                {
                    string[] nodes = i.OuterHtml.Split(">");
                    resultAttribute += nodes[0] + ">" + "\n\n";
                }
                MainWindowViewModel mainViewModel = viewMoel;
                mainViewModel.SelectedItem.Code = resultAttribute;
                resultAttribute = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        public async Task ParseParentalNode(MainWindowViewModel viewMoel, string link, string command)
        {
            HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
            string resultCode = string.Empty;
            if (HtmlNodeCol != null)
            {
                foreach (var i in HtmlNodeCol)
                {
                    if (resultCode.Contains(i.ParentNode.OuterHtml) == false)
                    {
                        resultCode += i.ParentNode.OuterHtml + "\n";
                    }
                }
                MainWindowViewModel mainViewModel = viewMoel;
                mainViewModel.SelectedItem.Code = resultCode;
                resultCode = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public async Task ParseXPath(MainWindowViewModel viewMoel, string link, string command)
        {
            HtmlNodeCollection HtmlNodeCol = searchParseFuncs.GetCode(command, link);
            string resultCode = string.Empty;
            if (HtmlNodeCol != null)
            {
                foreach (var i in HtmlNodeCol)
                {
                    string[] parentalNode = i.ParentNode.OuterHtml.Split('>');
                    string[] node = i.OuterHtml.Split('>');
                    if (resultCode.Contains(parentalNode[0] + '>' + node[0] + '>' + ": " + i.XPath) == false)
                    {
                        resultCode += parentalNode[0] + '>' + node[0] + '>' + ": " + i.XPath + "\n\n";
                    }

                }
                MainWindowViewModel mainViewModel = viewMoel;
                mainViewModel.SelectedItem.Code = resultCode;
                resultCode = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
