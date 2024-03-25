using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Web_Scraping_Mania.Models
{
    public class ComboBoxItem
    {
        //public string Link { get; set; }
        public string Title { get; set; }
        public ObservableCollection<FilesCollectionModel> HtmlFiles { get; set; }
        public ObservableCollection<FilesCollectionModel> CssFiles { get; set; }
        public ObservableCollection<FilesCollectionModel> Scripts { get; set; }
        public ComboBoxItem() 
        {
            HtmlFiles = new ObservableCollection<FilesCollectionModel>();
            CssFiles = new ObservableCollection<FilesCollectionModel>();
            Scripts = new ObservableCollection<FilesCollectionModel>();
        }
    }
}
