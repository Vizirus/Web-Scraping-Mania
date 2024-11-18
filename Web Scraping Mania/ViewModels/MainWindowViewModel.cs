using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Web_Scraping_Mania.Commands;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.Models;
using Web_Scraping_Mania.Views;

namespace Web_Scraping_Mania.ViewModels
{
    public class MainWindowViewModel : ShellViewModel
    {
        //Collections
        public ObservableCollection<Models.ComboBoxItem> WebSites { get; set; }
        private List<TabItemModel> MissedHtml { get; set; }
        private List<TabItemModel> MissedCss { get; set; }
        private List<TabItemModel> MissedScript { get; set; }
        public ObservableCollection<bool> IsEnabled { get; set; }
        //Property`s region
        private Models.ComboBoxItem _selectedItem;
        public Models.ComboBoxItem SelectedItem
        {
            get { return _selectedItem; }
            set { _selectedItem = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private string _searchTextBox = "";
        public string SearchText
        {
            get { return _searchTextBox; }
            set { _searchTextBox = value; OnPropertyChanged(nameof(SearchText)); }
        }
        private int _selectionStart;

        public int SelectionStart
        {
            get { return _selectionStart; }
            set { _selectionStart = value; OnPropertyChanged(nameof(SelectionStart)); }
        }
        private int _selectionLength;

        public int SelectionLength
        {
            get { return _selectionLength; }
            set { _selectionLength = value; OnPropertyChanged(nameof(SelectionLength)); }
        }


        private TabItemModel _selectedHtmlTab;

        public TabItemModel SelectedHtmlTab
        {
            get { return _selectedHtmlTab; }
            set { _selectedHtmlTab = value; OnPropertyChanged(nameof(SelectedHtmlTab)); }
        }
        private TabItemModel _selectedCssTab;
        public TabItemModel SelectedCssTab
        {
            get { return _selectedCssTab; }
            set { _selectedCssTab = value; OnPropertyChanged(nameof(SelectedCssTab)); }
        }
        private TabItemModel _selectedScriptTab;
        public TabItemModel SelectedScriptTab
        {
            get { return _selectedScriptTab; }
            set { _selectedScriptTab = value; OnPropertyChanged(nameof(SelectedScriptTab)); }
        }
        private int _fontSize = 15;

        public int FontSize
        {
            get { return _fontSize; }
            set { _fontSize = value; OnPropertyChanged(nameof(FontSize)); }
        }

        public int Test = 0;
        private SaveFunctions saveFuncs { get; set; }
        private SearchParseFuncs searchParse { get; set; }
        private int NumberOfSelection { get; set; }
        //Constructor
        public MainWindowViewModel()
        {
            WebSites = new ObservableCollection<Models.ComboBoxItem>();
            MissedHtml = new List<TabItemModel>();
            MissedCss = new List<TabItemModel>();
            MissedScript = new List<TabItemModel>();
            IsEnabled = new ObservableCollection<bool>() { false, false, false };
            //saveFuncs = new SaveFunctions();
            searchParse = new SearchParseFuncs(this);
        }
        //Funcs region
        private void OpenAddTabWindow()
        {

            AddTabWindow window = new AddTabWindow();
            window.Show();
        }
        private void RemoveTab()
        {
            if (SelectedItem != null)
            {
                WebSites.Remove(SelectedItem);
            }
            else
            {
                MessageBox.Show("Цю вкладку вже було видалено!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OpenParseByTagWindow()
        {
            ParseByTag window = new ParseByTag();
            window.Show();
        }

        private void openDownloadCodeWindow()
        {
            DownloadFormWeb inter = new DownloadFormWeb();
            inter.Show();
        }
        private void plusSelection(TabItemModel tabItem)
        {
            tabItem.IsTextBoxFocused = false;
            tabItem.SearchCount[0] += 1;
            int index = tabItem.SearchCount[0];
            tabItem.SearchCount[2] = index + 1;
            if (index >= tabItem.SearchCount[1] - 1)
            {
                index = tabItem.SearchCount[0] = tabItem.SearchCount[1] - 1;
                tabItem.SearchCount[2] = tabItem.SearchCount[1];
            }
            SelectionStart = tabItem.SelectionCollection[index][0];
            SelectionLength = tabItem.SelectionCollection[index][1];
            tabItem.IsTextBoxFocused = true;
        }
        private void minusSelection(TabItemModel tabItem)
        {
            tabItem.IsTextBoxFocused = false;
            int index = tabItem.SearchCount[0] -= 1;
            tabItem.SearchCount[2] = index + 1;
            if (index < 0)
            {
                tabItem.SearchCount[0] = 0;
                index = tabItem.SearchCount[0];
                tabItem.SearchCount[2] = 1;
            }

            SelectionStart = tabItem.SelectionCollection[index][0];
            SelectionLength = tabItem.SelectionCollection[index][1];
            tabItem.IsTextBoxFocused = true;
        }
        private void getAllCod()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {

                SelectedHtmlTab.TabItemText = searchParse.GetAllCode(SelectedItem.Link);
                OnPropertyChanged(nameof(SelectedItem));
            });
        }
        private void ClearSelected(TabItemModel tabItem)
        {
            tabItem.SearchCount[0] = 0;
            tabItem.SearchCount[1] = 0;
            tabItem.SearchCount[2] = 0;
        }
        private void updateUI()
        {
            SearchText = "";
            IsEnabled = new ObservableCollection<bool> { false, false, false };
            /*searchParse.ItemsExchange(SelectedItem.HtmlFiles, MissedHtml);
            searchParse.ItemsExchange(SelectedItem.CssFiles, MissedCss);
            searchParse.ItemsExchange(SelectedItem.Scripts, MissedScript);
            SelectedHtmlTab = SelectedItem.HtmlFiles[0];
            ClearSelected(SelectedHtmlTab);
            OnPropertyChanged(nameof(SelectedHtmlTab));
            ClearSelected(SelectedCssTab);
            OnPropertyChanged(nameof(SelectedCssTab));
            ClearSelected(SelectedScriptTab);
            OnPropertyChanged(nameof(SelectedScriptTab));
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(IsEnabled));
            getAllCod();
            GC.Collect();*/
        }
        public void PlusFontSize(ObservableCollection<TabItemModel> fileCollection)
        {
            foreach (TabItemModel tabItem in fileCollection)
            {
                FontSize += 2;

            }
        }
        public void MinusFontSize(ObservableCollection<TabItemModel> fileCollection)
        {
            foreach (TabItemModel tabItem in fileCollection)
            {
                FontSize -= 2;
                if (FontSize <= 1)
                {
                    FontSize = 1;
                }
            }
        }
        //Command region
        private CommandBase _addCommand;
        public ICommand AddTabCommand
        {
            get
            {
                _addCommand = new CommandBase(param => OpenAddTabWindow(), param => true);
                return _addCommand;
            }
        }

        private CommandBase _removeTabCommand;
        public ICommand RemoveTabCommand
        {
            get
            {
                _removeTabCommand = new CommandBase(param => RemoveTab(), param => true);
                return _removeTabCommand;
            }

        }

        private AsyncRelayCommand _getAllCommand;

        public IAsyncRelayCommand GetAllCommand
        {
            get
            {
                _getAllCommand = new AsyncRelayCommand(async param => await Task.Run(() => { getAllCod(); }));
                return _getAllCommand;
            }
        }

        private CommandBase _getByTagCommand;
        public ICommand GetByTagCommand
        {
            get
            {
                _getByTagCommand = new CommandBase(param => OpenParseByTagWindow(), param => true);
                return _getByTagCommand;
            }
        }
        private CommandBase _saveCommand;

        public ICommand SaveProjectCommand
        {
            get
            {
                _saveCommand = new CommandBase(async param => await saveFuncs.SaveProjectOrSelected(0), param => true);
                return _saveCommand;
            }
        }
        private CommandBase _saveSelectedCommand;

        public ICommand SaveSelectedFile
        {
            get
            {
                _saveSelectedCommand = new CommandBase(async param => await saveFuncs.SaveProjectOrSelected(1), param => true);
                return _saveSelectedCommand;
            }
        }

        private CommandBase _downloadCode;
        public ICommand DownloadCode
        {
            get
            {

                _downloadCode = new CommandBase(param => openDownloadCodeWindow(), param => true);
                return _downloadCode;
            }
        }

        private CommandBase _asyncRelayCommand;
        public ICommand OpenFile
        {
            get
            {
                _asyncRelayCommand = new CommandBase(param => WebSites.Add(saveFuncs.OpenCode()), param => true);
                return _asyncRelayCommand;
            }
        }

        private CommandBase _textSelection;
        private void findText()
        {
            searchParse.FindText(SearchText, SelectedItem.HtmlFiles, 0, MissedHtml);
            searchParse.FindText(SearchText, SelectedItem.CssFiles, 1, MissedCss);
            searchParse.FindText(SearchText, SelectedItem.Scripts, 2, MissedScript);
            OnPropertyChanged(nameof(SelectedHtmlTab));
            OnPropertyChanged(nameof(SelectedScriptTab));
            OnPropertyChanged(nameof(SelectedCssTab));
        }
        public ICommand TextSelection
        {
            get
            {
                _textSelection = new CommandBase(param => findText(), param => true);
                return _textSelection;
            }
        }

        private CommandBase _plusNumberHtml;
        public ICommand PlusSelectionHtml
        {
            get
            {
                _plusNumberHtml = new CommandBase(obj => { plusSelection(SelectedHtmlTab); OnPropertyChanged(nameof(SelectedHtmlTab)); }, param => true);
                return _plusNumberHtml;
            }
        }

        private CommandBase _minusSelectionHtml;
        public ICommand MinusSelectionHtml
        {
            get
            {
                _minusSelectionHtml = new CommandBase(obj => { minusSelection(SelectedHtmlTab); OnPropertyChanged(nameof(SelectedHtmlTab)); }, param => true);
                return _minusSelectionHtml;
            }
        }
        private CommandBase _plusNumberCss;
        public ICommand PlusSelectionCss
        {
            get
            {
                _plusNumberCss = new CommandBase(obj => { plusSelection(SelectedCssTab); OnPropertyChanged(nameof(SelectedCssTab)); }, param => true);
                return _plusNumberCss;
            }
        }

        private CommandBase _minusSelectionCss;
        public ICommand MinusSelectionCss
        {
            get
            {
                _minusSelectionCss = new CommandBase(obj => { minusSelection(SelectedCssTab); OnPropertyChanged(nameof(SelectedCssTab)); }, param => true);
                return _minusSelectionCss;
            }
        }
        private CommandBase _plusNumberScript;
        public ICommand PlusSelectionScript
        {
            get
            {
                _plusNumberScript = new CommandBase(obj => { plusSelection(SelectedScriptTab); OnPropertyChanged(nameof(SelectedScriptTab)); }, param => true);
                return _plusNumberScript;
            }
        }

        private CommandBase _minusSelectionScript;
        public ICommand MinusSelectionScript
        {
            get
            {
                _minusSelectionScript = new CommandBase(obj => { minusSelection(SelectedScriptTab); OnPropertyChanged(nameof(SelectedScriptTab)); }, param => true);
                return _minusSelectionScript;
            }
        }
        private AsyncRelayCommand _updateButton;

        public IAsyncRelayCommand UpdateButton
        {
            get
            {
                _updateButton = new AsyncRelayCommand(() => Task.Run(updateUI));
                return _updateButton;
            }
        }
        private CommandBase _aboutAuthor;

        public ICommand AboutAuthor
        {
            get
            {
                _aboutAuthor = new CommandBase(param => MessageBox.Show("Розробник: Снетков Артем\nУчень 11 класу Наукового ліцею 'Політ'\nпри Кременчуцькій,\nгуманітарно-технічній академії ім. А.С. Макаренка", "Інформація про мене", MessageBoxButton.OK, MessageBoxImage.Information), param => true);
                return _aboutAuthor;
            }
        }
        private CommandBase _openPreviewWindow;
        private void openPrevWindow()
        {
            PreviewWindow previewWindow = new PreviewWindow();
            previewWindow.Show();
        }
        public ICommand OpenPreviewWindow
        {
            get
            {
                _openPreviewWindow = new CommandBase(param => openPrevWindow(), param => true);
                return _openPreviewWindow;
            }
        }
        private CommandBase _saveSelectedAsType;
        private void saveSelectedFilesAsType()
        {
            saveFuncs.SaveFunc(SelectedHtmlTab);
            saveFuncs.SaveFunc(SelectedCssTab);
            saveFuncs.SaveFunc(SelectedScriptTab);
        }
        public ICommand SaveSelectedAsType
        {
            get
            {
                _saveSelectedAsType = new CommandBase(param => saveSelectedFilesAsType(), param => true);
                return _saveSelectedAsType;
            }
        }

    }
}
