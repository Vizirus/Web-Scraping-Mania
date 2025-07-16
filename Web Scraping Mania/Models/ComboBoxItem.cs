using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace Web_Scraping_Mania.Models
{
    public class ComboBoxItem
    {
        public string Link { get; set; }
        public string Title { get; set; }
        public ObservableCollection<TabItem> HtmlFiles { get; set; }
        public ObservableCollection<TabItem> CssFiles { get; set; }
        public ObservableCollection<TabItem> Scripts { get; set; }
    }
}
