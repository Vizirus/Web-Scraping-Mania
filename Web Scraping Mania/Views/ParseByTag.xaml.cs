using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for ParseByTag.xaml
    /// </summary>
    public partial class ParseByTag : Window
    {
        public ParseByTag()
        {
            this.DataContext = App.hosting.Services.GetRequiredService<ParseByTagAndArgViewModel>();
            InitializeComponent();
        }
    }
}
