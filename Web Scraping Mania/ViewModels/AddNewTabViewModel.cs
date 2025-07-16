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
            saveFuncs = new SaveFunctions(viewModel);
            searchParse = new SearchParseFuncs(viewModel);
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
        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value;  OnPropertyChanged(nameof(FilePath)); }
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
        private bool _enableDowmload = false;

        public bool EnableDowmload
        {
            get { return _enableDowmload; }
            set 
            { 
                _enableDowmload = value;
                OnPropertyChanged(nameof(EnableDowmload));
            }
        }
        private bool _enableBuffering = true;

        public bool EnableBuffering
        {
            get { return _enableBuffering; }
            set { _enableBuffering = value; OnPropertyChanged(nameof(EnableBuffering)); }
        }


        private AsyncRelayCommand _addTabCommand;
        private async Task _addTab()
        {
            string code = searchParse.GetAllCode(TabLink);
            string title = searchParse.GetTitle(TabLink);
            if (EnableTitle)
            {
                title = TabName;
            }
            if (EnableDowmload)
            {
                await saveFuncs.AddNewTab(title, code, FilePath, 0, TabLink);
            }
            else if(EnableBuffering)
            {
                await saveFuncs.AddNewTab(title, code, FilePath, 1, TabLink);
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
