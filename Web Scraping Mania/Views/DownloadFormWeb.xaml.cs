using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Web_Scraping_Mania.ViewModels;


namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for DownloadFormWeb.xaml
    /// </summary>
    public partial class DownloadFormWeb : Window
    {
        public DownloadFormWeb()
        {
            this.DataContext = App.hosting.Services.GetRequiredService<DownloadFromWebViewModel>();
            InitializeComponent();

        }
    }
}
