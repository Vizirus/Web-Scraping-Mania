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
        SearchParseFuncs searchParseFuncs = new SearchParseFuncs();
        public MainWindow()
        {

            this.DataContext = App.hosting.Services.GetRequiredService<MainWindowViewModel>();
            InitializeComponent();

        }

        /*private void WebViewBrowser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            WebViewBrowser.CoreWebView2.ContextMenuRequested += delegate (object sender, CoreWebView2ContextMenuRequestedEventArgs args)
            {
                CoreWebView2ContextMenuItem getActive = WebViewBrowser.CoreWebView2.Environment.CreateContextMenuItem("Знайти код активного елемента", null, CoreWebView2ContextMenuItemKind.Command);
                getActive.CustomItemSelected += delegate (object sender, object ex)
                {
                    searchParseFuncs.ReturnObjAttribute(WebViewBrowser, App.hosting.Services.GetRequiredService<MainWindowViewModel>());
                };
                CoreWebView2ContextMenuItem getSelection = WebViewBrowser.CoreWebView2.Environment.CreateContextMenuItem("Знайти код виділеного елемента", null, CoreWebView2ContextMenuItemKind.Command);
                getSelection.CustomItemSelected += delegate (object sender, object ex)
                {
                    searchParseFuncs.ReturnSelectedTextAtributes(WebViewBrowser, App.hosting.Services.GetRequiredService<MainWindowViewModel>());
                };
                args.MenuItems.Add(getActive);
                args.MenuItems.Add(getSelection);

            }
        }*/
    }
}
