using HtmlAgilityPack;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SelectionMethods
    {
        private SearchParseFuncs searchParseFuncs { get; set; }
        private MainWindowViewModel viewModel { get; set; }
        public SelectionMethods(MainWindowViewModel viewModelMain)
        {
            viewModel = viewModelMain;
            searchParseFuncs = new SearchParseFuncs(viewModelMain);
        }
        public async Task ParseByTagInnerHtml(string link, string command)
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
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        if (viewModel.SelectedHtmlTab.Content is null)
                        {
                            MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                            textBox.Text = resultCode;
                            viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                        }
                    });
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
        public async Task ParseByTagOuterHtml(string link, string command)
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
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                    if (viewModel.SelectedHtmlTab.Content is null)
                        {
                            MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                            textBox.Text = resultCode;
                            viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                        }
                    });
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
        public async Task ParseByInnerText(string link, string command)
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
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        if (viewModel.SelectedHtmlTab.Content is null)
                        {
                            MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        else
                        {
                            TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                            textBox.Text = resultCode;
                            viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                        }
                    });
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
        public async Task ParseId(string link, string command)
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
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        if (viewModel.SelectedHtmlTab.Content != null)
                            if (viewModel.SelectedHtmlTab.Content is null)
                            {
                                MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                            }
                            else
                            {
                                TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                                textBox.Text = resultCode;
                                viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                            }
                    });
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
        public async Task ParseAttributes(string link, string command)
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    if (viewModel.SelectedHtmlTab.Content is null)
                    {
                        MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                        textBox.Text = resultAttribute;
                        viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                    }
                });
                resultAttribute = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }

        }
        public async Task ParseParentalNode(string link, string command)
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    if (viewModel.SelectedHtmlTab.Content is null)
                    {
                        MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                        textBox.Text = resultCode;
                        viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                    }
                });
                resultCode = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        public async Task ParseXPath(string link, string command)
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
                Application.Current.Dispatcher.Invoke(() =>
                {
                    
                    if (viewModel.SelectedHtmlTab.Content is null)
                    {
                        MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        TextBox textBox = viewModel.SelectedHtmlTab.Content as TextBox;
                        textBox.Text = resultCode;
                        viewModel.OnPropertyChanged(nameof(viewModel.SelectedHtmlTab));
                    }
                });
                resultCode = null;
            }
            else
            {
                MessageBox.Show("Запит не видав результатів!", "Іформую!", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }

}
