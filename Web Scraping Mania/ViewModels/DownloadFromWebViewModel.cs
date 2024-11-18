using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Web_Scraping_Mania.Commands.Functions;

namespace Web_Scraping_Mania.ViewModels
{
    public class DownloadFromWebViewModel
    {
        MainWindowViewModel mainWindowViewModel { get; set; }
        public DownloadFromWebViewModel(MainWindowViewModel viewModel)
        {
            mainWindowViewModel = viewModel;
        }
        private string _link;

        public string Link
        {
            get { return _link; }
            set { _link = value; }
        }
        private AsyncRelayCommand _downloadCommand;
        public IAsyncRelayCommand DownloadCommand
        {
            get
            {
                SaveFunctions saveFuncs = new SaveFunctions(mainWindowViewModel);
                _downloadCommand = new AsyncRelayCommand(async param => await Task.Run(async () => await saveFuncs.SaveResourceAsync(Link)));
                return _downloadCommand;
            }
        }
        //https://www.w3schools.com/lib/topnav/main.css?v=1.0.26
    }
}
