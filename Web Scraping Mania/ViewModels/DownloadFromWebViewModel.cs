using CommunityToolkit.Mvvm.Input;
using System.Threading.Tasks;
using Web_Scraping_Mania.Commands.Functions;

namespace Web_Scraping_Mania.ViewModels
{
    public class DownloadFromWebViewModel
    {
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
                SearchParseFuncs searchParseFuncs = new SearchParseFuncs();
                _downloadCommand = new AsyncRelayCommand(async param => await Task.Run(async () => await searchParseFuncs.DownloadCodeAsync(Link)));
                return _downloadCommand;
            }
        }
        //https://www.w3schools.com/lib/topnav/main.css?v=1.0.26
    }
}
