using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Web_Scraping_Mania.Models
{
    public class TabItemModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public string Header { get; set; }
        private string _tabItemText;

        public string TabItemText
        {
            get { return _tabItemText; }
            set { _tabItemText = value; OnPropertyChanged(); }
        }

        private bool _isTextBoxFocused = false;

        public bool IsTextBoxFocused
        {
            get { return _isTextBoxFocused; }
            set { _isTextBoxFocused = value; OnPropertyChanged(); }
        }

        public int[] SearchCount { get; set; }
        public List<int[]> SelectionCollection;
        public TabItemModel(string header, string textBoxText)
        {
            this.Header = header;
            this.TabItemText = textBoxText;
            SelectionCollection = new List<int[]>();
            SearchCount = new int[] { 0, 0, 0 };
        }
    }
}
