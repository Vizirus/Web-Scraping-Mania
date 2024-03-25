using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Web_Scraping_Mania.Commands;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.Views;

namespace Web_Scraping_Mania.ViewModels
{
    public class MainWindowViewModel : ShellViewModel
    {
        //Collections
        public ObservableCollection<Models.ComboBoxItem> WebSites { get; set; }
        public ObservableCollection<TabItem> HtmlFilesCollection { get; set; }
        //Property`s region
        private Models.ComboBoxItem _selectedItem;
        public Models.ComboBoxItem SelectedItem
        {

            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }

        }
        private bool _isPMButActive = false;
        public bool IsPMButActive
        {
            get { return _isPMButActive; }
            set { _isPMButActive = value; OnPropertyChanged(nameof(SelectedItem)); }
        }

        private string _searchTextBox = "";
        public string SearchText
        {
            get { return _searchTextBox; }
            set { _searchTextBox = value; OnPropertyChanged(nameof(SearchText)); }
        }

        private int _selectionIndex;
        public int SelectionIndex
        {
            get { return _selectionIndex; }
            set { _selectionIndex = value; OnPropertyChanged(nameof(SelectionIndex)); }
        }
        public Uri SourceLink
        {
            get { return new Uri(SelectedItem.Link); }
        }

        private int _dictCount;

        public int DictCount
        {
            get { return _dictCount; }
            set { _dictCount = value; OnPropertyChanged(nameof(DictCount)); }
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


        public Dictionary<int, int> SelectionDict { get; set; }
        private SaveFunctions saveFuncs { get; set; }
        private SearchParseFuncs searchParse { get; set; }
        private int NumberOfSelection { get; set; }
        //Constructor
        public MainWindowViewModel()
        {
            WebSites = new ObservableCollection<Models.ComboBoxItem>() { new Models.ComboBoxItem() { Link = "https://www.google.com.ua/?hl=uk", Code = "Немає", Title = "Головна сторінка" } };
            saveFuncs = new SaveFunctions();
            searchParse = new SearchParseFuncs();
            SelectionDict = new Dictionary<int, int>();
            SelectedItem = WebSites[0];
            HtmlFilesCollection = new ObservableCollection<TabItem>();

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

        private void clearText()
        {
            if (SelectedItem != null)
            {
                SelectedItem.Code = string.Empty;
                OnPropertyChanged(nameof(SelectedItem));
            }
            else
            {
                MessageBox.Show("Вкладку не було вибрано!", "Помилка!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void plusSelection(object obj)
        {
            NumberOfSelection += 1;
            if (NumberOfSelection > SelectionDict.Count - 1)
            {
                NumberOfSelection = SelectionDict.Count - 1;
            }
            TextBox box = obj as TextBox;
            box.Select(SelectionDict.ElementAt(NumberOfSelection).Key, SelectionDict.ElementAt(NumberOfSelection).Value);
            SelectionIndex = NumberOfSelection + 1;
            OnPropertyChanged(nameof(SelectionIndex));
            box.ScrollToLine(box.GetLineIndexFromCharacterIndex(SelectionDict.ElementAt(NumberOfSelection).Key));
            box.Focus();
        }
        private void minusSelection(object obj)
        {
            NumberOfSelection -= 1;
            if (NumberOfSelection <= 0)
            {
                NumberOfSelection = 0;
            }
            TextBox box = obj as TextBox;
            box.Select(SelectionDict.ElementAt(NumberOfSelection).Key, SelectionDict.ElementAt(NumberOfSelection).Value);
            SelectionIndex = NumberOfSelection + 1;
            OnPropertyChanged(nameof(SelectionIndex));
            box.ScrollToLine(box.GetLineIndexFromCharacterIndex(SelectionDict.ElementAt(NumberOfSelection).Key));
            box.Focus();
        }
        private void updateUI()
        {
            SelectedItem.Code = searchParse.GetAllCode(SelectedItem.Link);
            SearchText = "";
            SelectionIndex = 0;
            DictCount = 0;
            OnPropertyChanged(nameof(SelectedItem));
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
                _getAllCommand = new AsyncRelayCommand(async param => await Task.Run(() => { SelectedItem.Code = searchParse.GetAllCode(SelectedItem.Link); OnPropertyChanged(nameof(SelectedItem)); }));
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
        private CommandBase _clearCommand;
        public ICommand ClearText
        {
            get
            {
                _clearCommand = new CommandBase(param => clearText(), param => true);

                return _clearCommand;
            }
        }

        private CommandBase _saveCommand;
        public ICommand SaveFileCommand
        {
            get
            {
                _saveCommand = new CommandBase(param => saveFuncs.SaveFunc(this), param => true);
                return _saveCommand;
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
                _textSelection = new CommandBase(obj => searchParse.FindText(this, SearchText, SelectedItem.Code), param => true);
                return _textSelection;
            }
        }

        private CommandBase _plusNumber;
        public ICommand PlusSelection
        {
            get
            {
                _plusNumber = new CommandBase(obj => plusSelection(obj), param => IsPMButActive);
                return _plusNumber;
            }
        }

        private CommandBase _minusSelection;
        public ICommand MinusSelection
        {
            get
            {
                _minusSelection = new CommandBase(obj => minusSelection(obj), param => IsPMButActive);
                return _minusSelection;
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
    }
}
