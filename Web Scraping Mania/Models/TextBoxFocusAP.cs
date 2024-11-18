using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Models
{
    public class TextBoxFocusAP
    {
        public static readonly DependencyProperty IsFocusedProperty = DependencyProperty.RegisterAttached("IsFocused", typeof(bool), typeof(TextBoxFocusAP), new FrameworkPropertyMetadata(defaultValue: false, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, propertyChangedCallback: OnIsFocusedPropertyChanged));

        public static bool GetIsFocused(DependencyObject uIElement)
        {
            return (bool)uIElement.GetValue(IsFocusedProperty);
        }

        public static void SetIsFocused(DependencyObject uIElement, bool value)
        {
            uIElement.SetValue(IsFocusedProperty, value);
        }
        private static void OnIsFocusedPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var uie = (TextBox)d;
            if ((bool)e.NewValue)
            {
                MainWindowViewModel mainWindowViewModel = App.hosting.Services.GetRequiredService<MainWindowViewModel>();
                uie.Select(mainWindowViewModel.SelectionStart, mainWindowViewModel.SelectionLength);
                uie.ScrollToLine(uie.GetLineIndexFromCharacterIndex(mainWindowViewModel.SelectionStart));
                uie.Focus();
            }
        }
    }
}
