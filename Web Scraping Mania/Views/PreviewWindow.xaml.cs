using Microsoft.Extensions.DependencyInjection;
using Microsoft.Web.WebView2.Core;
using System.Windows;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for PreviewWindow.xaml
    /// </summary>
    public partial class PreviewWindow : Window
    {
        SearchParseFuncs searchParseFuncs;
        public PreviewWindow()
        {
            InitializeComponent();
            this.DataContext = App.hosting.Services.GetRequiredService<OpenPreviewWindowViewModel>();
            MainWindowViewModel viewModel = App.hosting.Services.GetRequiredService<MainWindowViewModel>();
            //searchParseFuncs = new SearchParseFuncs();
        }
        private void WebViewBrowser_CoreWebView2InitializationCompleted(object sender, CoreWebView2InitializationCompletedEventArgs e)
        {
            WebViewBrowser.CoreWebView2.ContextMenuRequested += delegate (object sender, CoreWebView2ContextMenuRequestedEventArgs args)
            {
                CoreWebView2ContextMenuItem getActive = WebViewBrowser.CoreWebView2.Environment.CreateContextMenuItem("Знайти код активного елемента", null, CoreWebView2ContextMenuItemKind.Command);
                getActive.CustomItemSelected += delegate (object sender, object ex)
                {
                    searchParseFuncs.ReturnObjAttribute(WebViewBrowser, App.hosting.Services.GetRequiredService<OpenPreviewWindowViewModel>());
                };
                CoreWebView2ContextMenuItem getSelection = WebViewBrowser.CoreWebView2.Environment.CreateContextMenuItem("Знайти код виділеного елемента", null, CoreWebView2ContextMenuItemKind.Command);
                getSelection.CustomItemSelected += delegate (object sender, object ex)
                {
                    searchParseFuncs.ReturnSelectedTextAtributes(WebViewBrowser, App.hosting.Services.GetRequiredService<OpenPreviewWindowViewModel>());
                };
                args.MenuItems.Add(getActive);
                args.MenuItems.Add(getSelection);
            };
        }
    }
}
