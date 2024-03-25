using HtmlAgilityPack;
using Microsoft.Win32;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class SaveFunctions
    {

        public void SaveFunc(MainWindowViewModel mainWindowViewModel)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Text File|*.txt|JavaScript|*.js|Html File|*.html|XML File|*.xml|PHP file|*.php";
            saveFileDialog.RestoreDirectory = true;
            saveFileDialog.ShowDialog();
            MainWindowViewModel viewModel = mainWindowViewModel;
            if (saveFileDialog.FileName != null)
            {
                using (StreamWriter writer = new StreamWriter(saveFileDialog.FileName))
                {
                    if (saveFileDialog.FilterIndex == 1 || saveFileDialog.FilterIndex == 2 || saveFileDialog.FilterIndex == 3)
                    {
                        writer.Write(viewModel.SelectedItem.Code);

                    }
                    else if (saveFileDialog.FilterIndex == 4)
                    {
                        var htmlDoc = new HtmlDocument();
                        htmlDoc.LoadHtml(viewModel.SelectedItem.Code);
                        htmlDoc.Save(writer);
                    }
                    writer.Close();
                }
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
            return new Models.ComboBoxItem() { Code = result, Title = openFileDialog.FileName, Link = "No link" };
        }
        public void AddNewTab(string TabName, string code, MainWindowViewModel _viewModel, string TabLink = "No Tab Link")
        {

            Application.Current.Dispatcher.Invoke(() =>
            {
                if (TabName == null || TabLink == null)
                {
                    MessageBox.Show("Не було введено назву вкладки, або посилання!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Models.ComboBoxItem comboBoxItem = new Models.ComboBoxItem() { Link = TabLink, Title = TabName, Code = code };
                    _viewModel.HtmlFilesCollection.Add(new TabItem() { Header = TabName, Content = new TextBox() { Text = code } });
                }
            });
        }

    }
}
