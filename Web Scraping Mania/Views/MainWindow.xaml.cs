using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.Wpf;
using System.Windows;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for ЬфштЦштвщц.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SearchParseFuncs searchParseFuncs = new SearchParseFuncs();
        public MainWindow()
        {

            this.DataContext = App.hosting.Services.GetRequiredService<MainWindowViewModel>();
            InitializeComponent();

        }
    }
}
