using System.Windows;
using System.Windows.Media;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Views
{
    /// <summary>
    /// Interaction logic for ЬфштЦштвщц.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //SearchParseFuncs searchParseFuncs = new SearchParseFuncs();
        private bool IsCtrl = false;
        double[] defaultValue = { 1.0, 1.0, 1.0 };
        public MainWindow(MainWindowViewModel mainWindowViewModel)
        {
            this.DataContext = mainWindowViewModel;
            InitializeComponent();
        }

        /*private void HtmlCodeHandler_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftCtrl)
            {
                IsCtrl = true;
            }
        }

        private void HtmlCodeHandler_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == System.Windows.Input.Key.LeftCtrl)
            {
                IsCtrl = false;
            }
        }

        private void HtmlCodeHandler_MouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && IsCtrl == true)
            {
                defaultValue[0] += 0.2;
                HtmlCodeHandler.LayoutTransform = new ScaleTransform(defaultValue[0], defaultValue[0]);
            }
            else if (e.Delta < 0 && IsCtrl == true)
            {
                defaultValue[0] -= 0.2;
                if (defaultValue[0] < 0.3)
                {
                    defaultValue[0] = 0.3;
                }
                HtmlCodeHandler.LayoutTransform = new ScaleTransform(defaultValue[0], defaultValue[0]);
            }
        }

        private void CssCodeHandler_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && IsCtrl == true)
            {
                defaultValue[1] += 0.2;
                CssCodeHandler.LayoutTransform = new ScaleTransform(defaultValue[1], defaultValue[1]);
            }
            else if (e.Delta < 0 && IsCtrl == true)
            {
                defaultValue[1] -= 0.2;
                if (defaultValue[1] < 0.3)
                {
                    defaultValue[1] = 0.3;
                }
                CssCodeHandler.LayoutTransform = new ScaleTransform(defaultValue[1], defaultValue[1]);
            }
        }

        private void ScriptsHandler_PreviewMouseWheel(object sender, System.Windows.Input.MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && IsCtrl == true)
            {
                defaultValue[2] += 0.2;
                ScriptsHandler.LayoutTransform = new ScaleTransform(defaultValue[2], defaultValue[2]);
            }
            else if (e.Delta < 0 && IsCtrl == true)
            {
                defaultValue[2] -= 0.2;
                if (defaultValue[2] < 0.3)
                {
                    defaultValue[2] = 0.3;
                }
                ScriptsHandler.LayoutTransform = new ScaleTransform(defaultValue[2], defaultValue[2]);
            }
        }*/
    }
}
