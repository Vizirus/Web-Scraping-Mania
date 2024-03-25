using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for AddTabWindow.xaml
    /// </summary>
    public partial class AddTabWindow : Window
    {
        public AddTabWindow()
        {
            this.DataContext = App.hosting.Services.GetRequiredService<AddNewTabViewModel>();
            InitializeComponent();
        }
    }
}
