using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Web_Scraping_Mania.Commands.Functions;

namespace Web_Scraping_Mania.ViewModels
{
    public class AddNewTabViewModel : ShellViewModel
    {

        private MainWindowViewModel _mainWindowViewModel { get; set; }
        private SaveFunctions saveFuncs { get; set; }
        private SearchParseFuncs searchParse { get; set; }
        public AddNewTabViewModel(MainWindowViewModel viewModel)
        {
            _mainWindowViewModel = viewModel;
            saveFuncs = new SaveFunctions();
            searchParse = new SearchParseFuncs();
        }
        private string _tabName;

        public string TabName
        {
            get
            {
                return _tabName;
            }
            set
            {
                _tabName = value;
                OnPropertyChanged(nameof(TabName));
            }
        }
        private string _tabLink;
        public string TabLink
        {
            get
            {
                return _tabLink;
            }
            set
            {
                _tabLink = value;
                OnPropertyChanged(nameof(TabLink));
            }
        }
        private bool _EnableTitle = false;

        public bool EnableTitle
        {
            get { return _EnableTitle; }
            set
            {
                _EnableTitle = value;
                OnPropertyChanged(nameof(EnableTitle));
            }
        }


        private AsyncRelayCommand _addTabCommand;
        private void _addTab()
        {
            string code = searchParse.GetAllCode(TabLink);
            string title = searchParse.GetTitle(TabLink);
            if (EnableTitle)
            {
                saveFuncs.AddNewTab(TabName, code, _mainWindowViewModel, TabLink);
            }
            else
            {
                saveFuncs.AddNewTab(title, code, _mainWindowViewModel, TabLink);
            }
        }
        public IAsyncRelayCommand AddTabCommand
        {
            get
            {

                _addTabCommand = new AsyncRelayCommand(param => Task.Run(_addTab));
                return _addTabCommand;
            }
        }
        //https://www.w3schools.com/xml/xpath_intro.asp
    }
}
