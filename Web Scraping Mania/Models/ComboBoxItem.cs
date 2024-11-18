using System.Collections.ObjectModel;

namespace Web_Scraping_Mania.Models
{
    public class ComboBoxItem
    {
        public string? Link { get; set; }
        public string? Title { get; set; }
        public ObservableCollection<TabItemModel>? HtmlFiles { get; set; }
        public ObservableCollection<TabItemModel>? CssFiles { get; set; }
        public ObservableCollection<TabItemModel>? Scripts { get; set; }
    }
}
