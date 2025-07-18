using CommunityToolkit.Mvvm.Input;
using LanguageExt;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
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
        public ObservableCollection<ComboBoxItem> WebSites { get; set; }
        public ObservableCollection<bool> IsEnabled { get; set; }
        //Property`s region
        private ComboBoxItem _selectedItem;
        public ComboBoxItem SelectedItem
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

        private int _selectionStart = 0;
        public int SelectionStart
        {
            get { return _selectionStart; }
            set { _selectionStart = value; OnPropertyChanged(nameof(SelectionStart)); }
        }

        private int _selectionLength = 0;
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

        private SaveFunctions saveFuncs { get; set; }
        private SearchParseFuncs searchParse { get; set; }
        private int NumberOfSelection { get; set; }
        //Constructor
        public MainWindowViewModel()
        {
            WebSites = new ObservableCollection<ComboBoxItem>();
            IsEnabled = new ObservableCollection<bool>() { false, false, false };
            saveFuncs = new SaveFunctions(this);
            searchParse = new SearchParseFuncs(this);
        }
        //Funcs region
        private void findText()
        {

        }
        private void OpenAddTabWindow()
        {
            AddTabWindow window = new AddTabWindow();
            window.Show();
        }
        private void openPrevWindow()
        {
            PreviewWindow previewWindow = new PreviewWindow();
            previewWindow.Show();
        }
        private void RemoveTab()
        {
            if (SelectedItem is not null)
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
            
        }
        private void minusSelection(TabItemModel tabItem)
        {
            
        }
        private async Task getAllCodeAsync()
        {
            await Application.Current.Dispatcher.InvokeAsync(() =>
            {
                SelectedHtmlTab.TabDocument = new FlowDocument();
                OnPropertyChanged(nameof(SelectedItem));
            });
        }//Refactor this method
        private void saveSelectedFilesAsType()
        {
            
        }

        /*private void ClearSelected(TabItemModel tabItem)
        {
            
        }//Refactor this method/*
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
            GC.Collect();}*/

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
                _getAllCommand = new AsyncRelayCommand(async () => await getAllCodeAsync());
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

        private AsyncRelayCommand _saveCommand;
        public ICommand SaveProjectCommand
        {
            get
            {
                _saveCommand = new AsyncRelayCommand(async () => await saveFuncs.SaveProjectOrSelected(0));
                return _saveCommand;
            }
        }

        private AsyncRelayCommand _saveSelectedCommand;
        public ICommand SaveSelectedFile
        {
            get
            {
                _saveSelectedCommand = new AsyncRelayCommand(async () => await saveFuncs.SaveProjectOrSelected(1));
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
        public ICommand TextSelection
        {
            get
            {
                _textSelection = new CommandBase(param => findText(), param => true);
                return _textSelection;
            }
        }

        /*private CommandBase _plusNumberHtml;
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
        }*/ // This section can be opti,ized (reduce number of function from 6 to 3)
        private AsyncRelayCommand _updateButton;
        /*public IAsyncRelayCommand UpdateButton
        {
            get
            {
                _updateButton = new AsyncRelayCommand(() => Task.Run());
                return _updateButton;
            }
        }*/

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
        public ICommand OpenPreviewWindow
        {
            get
            {
                _openPreviewWindow = new CommandBase(param => openPrevWindow(), param => true);
                return _openPreviewWindow;
            }
        }

        private CommandBase _saveSelectedAsType;
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
