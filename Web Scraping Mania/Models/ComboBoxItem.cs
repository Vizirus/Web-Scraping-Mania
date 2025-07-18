
using System.Collections.ObjectModel;

namespace Web_Scraping_Mania.Models
{
    public class ComboBoxItem
    {
        public required string Link { get; set; }
        public required string Title { get; set; }

        private string _savePath = string.Empty;
        public string SavePath
        {
            get { return _savePath; }
            set { _savePath = value; }
        }

        public required ObservableCollection<TabItemModel> HtmlFiles { get; set; }
        public required ObservableCollection<TabItemModel> CssFiles { get; set; }
        public required ObservableCollection<TabItemModel> Scripts { get; set; }
    }
}
