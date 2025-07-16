using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web_Scraping_Mania.ViewModels
{
    public class OpenPreviewWindowViewModel: ShellViewModel
    {
        private MainWindowViewModel windowViewModel;
        public OpenPreviewWindowViewModel(MainWindowViewModel mainWindow)
        {
            windowViewModel = mainWindow;
        }
        private string _idName = "Тут буде Id елемента";

        public string IdName
        {
            get { return _idName; }
            set { _idName = value; OnPropertyChanged(nameof(IdName)); }
        }
        private string _className = "Тут буде Class елемента";

        public string ClassName
        {
            get { return _className; }
            set { _className = value; OnPropertyChanged(nameof(ClassName)); }
        }

        public string CurrentLink
        {
            get { return windowViewModel.SelectedItem.Link; }
        }

    }
}
