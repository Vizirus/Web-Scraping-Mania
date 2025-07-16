using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using Web_Scraping_Mania.Commands;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.Views;

namespace Web_Scraping_Mania.ViewModels
{
    public class MainWindowViewModel : ShellViewModel
    {
        //Collections
        public ObservableCollection<Models.ComboBoxItem> WebSites { get; set; }
        private List<int[]> SelectionHTMLDict { get; set; }
        private List<int[]> SelectionCSSDict { get; set; }
        private List<int[]> SelectionScriptDict { get; set; }
        public ObservableCollection<int[]> DictCount {  get; set; }
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

        
        private TabItem _selectedHtmlTab;

        public TabItem SelectedHtmlTab
        {
            get { return _selectedHtmlTab; }
            set { _selectedHtmlTab = value; OnPropertyChanged(nameof(SelectedHtmlTab)); }
        }
        private TabItem _selectedCssTab;
        public TabItem SelectedCssTab
        {
            get { return _selectedCssTab; }
            set { _selectedCssTab = value; OnPropertyChanged(nameof(SelectedCssTab)); }
        }
        private TabItem _selectedScriptTab;
        public TabItem SelectedScriptTab
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
            WebSites = new ObservableCollection<Models.ComboBoxItem>() { new Models.ComboBoxItem() {Title = "Головна сторінка" } };
            DictCount = new ObservableCollection<int[]>();
            DictCount.Add([0, 0, 0]);
            DictCount.Add([0, 0, 0]);
            DictCount.Add([0, 0, 0]);
            IsEnabled = new ObservableCollection<bool>() { false, false, false };
            saveFuncs = new SaveFunctions(this);
            searchParse = new SearchParseFuncs(this);
            SelectedItem = WebSites[0];

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
        private void selectSearchedText(TabItem tabItem, List<int[]> selectionArr,int[] dictCount)
        {
            if(tabItem.Content is not null)
            {
                TextBox box = tabItem.Content as TextBox;
                box.Select(selectionArr[dictCount[0]][0], selectionArr[dictCount[0]][1]);
                box.ScrollToLine(box.GetLineIndexFromCharacterIndex(selectionArr[dictCount[0]][0]));
                box.Focus();
            }
            else
            {
                MessageBox.Show("Вкладка не була обрана!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            GC.Collect();
        }
        private void plusSelection(TabItem tabItem, List<int[]> selectionArr, int[] dictCount)
        {
            dictCount[0] += 1;
            if (dictCount[0] >= dictCount[1]-1)
            {
                dictCount[0] = dictCount[1]-1;
            }
            dictCount[2] = dictCount[0] + 1;
            OnPropertyChanged(nameof(DictCount));
            selectSearchedText(tabItem, selectionArr, dictCount);
        }
        private void minusSelection(TabItem tabItem, List<int[]> selectionArr, int[] dictCount)
        {
            dictCount[0] -= 1;
            if (dictCount[0] <= 0)
            {
                dictCount[0] = 0;
            }
            dictCount[2] = dictCount[0] + 1;
            OnPropertyChanged(nameof(DictCount));
            selectSearchedText(tabItem, selectionArr, dictCount);
        }
        private void getAllCod()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                TextBox textBox = SelectedHtmlTab.Content as TextBox;
                textBox.Text = searchParse.GetAllCode(SelectedItem.Link); OnPropertyChanged(nameof(SelectedItem));
            });
        }
        private void updateUI()
        {
            getAllCod();
            SearchText = "";
            DictCount = new ObservableCollection<int[]>() ;
            IsEnabled = new ObservableCollection<bool> { false, false, false };
            DictCount.Add([0, 0, 0]);
            DictCount.Add([0, 0, 0]);
            DictCount.Add([0, 0, 0]);
            OnPropertyChanged(nameof(SelectedItem));
            OnPropertyChanged(nameof(DictCount));
            OnPropertyChanged(nameof(IsEnabled));
            GC.Collect();
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
            SelectionHTMLDict = searchParse.FindText(SearchText, SelectedHtmlTab, DictCount[0], 0);
            SelectionCSSDict = searchParse.FindText(SearchText, SelectedCssTab, DictCount[1], 1);
            SelectionScriptDict = searchParse.FindText(SearchText, SelectedScriptTab, DictCount[2], 2);
            OnPropertyChanged(nameof(DictCount));
            OnPropertyChanged(nameof(IsEnabled));
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
                _plusNumberHtml = new CommandBase(obj => plusSelection(SelectedHtmlTab, SelectionHTMLDict, DictCount[0]), param => true);
                return _plusNumberHtml;
            }
        }

        private CommandBase _minusSelectionHtml;
        public ICommand MinusSelectionHtml
        {
            get
            {
                _minusSelectionHtml = new CommandBase(obj => minusSelection(SelectedHtmlTab, SelectionHTMLDict, DictCount[0]), param => true);
                return _minusSelectionHtml;
            }
        }
        private CommandBase _plusNumberCss;
        public ICommand PlusSelectionCss
        {
            get
            {
                _plusNumberCss = new CommandBase(obj => plusSelection(SelectedCssTab, SelectionCSSDict, DictCount[1]), param => true);
                return _plusNumberCss;
            }
        }

        private CommandBase _minusSelectionCss;
        public ICommand MinusSelectionCss
        {
            get
            {
                _minusSelectionCss = new CommandBase(obj => minusSelection(SelectedCssTab, SelectionCSSDict, DictCount[1]), param => true);
                return _minusSelectionCss;
            }
        }
        private CommandBase _plusNumberScript;
        public ICommand PlusSelectionScript
        {
            get
            {
                _plusNumberScript = new CommandBase(obj => plusSelection(SelectedScriptTab, SelectionScriptDict, DictCount[2]), param => true);
                return _plusNumberScript;
            }
        }

        private CommandBase _minusSelectionScript;
        public ICommand MinusSelectionScript
        {
            get
            {
                _minusSelectionScript = new CommandBase(obj => minusSelection(SelectedScriptTab, SelectionScriptDict, DictCount[2]), param => true);
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
                _aboutAuthor = new CommandBase(param => MessageBox.Show("Мене звати Снетков Артем. Я навчаюсь в ліцеї 'Політ'. \nЯ навчаюсь в групі Л11-В", "Інформація про мене", MessageBoxButton.OK, MessageBoxImage.Information), param => true);
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
    }
}
