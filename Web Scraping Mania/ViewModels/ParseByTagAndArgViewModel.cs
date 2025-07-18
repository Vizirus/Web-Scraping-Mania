using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Web_Scraping_Mania.Commands;
using Web_Scraping_Mania.Commands.Functions;
using Web_Scraping_Mania.Models;
namespace Web_Scraping_Mania.ViewModels
{
    public class ParseByTagAndArgViewModel : ShellViewModel
    {
        //private classes
        private SearchParseFuncs searchParseFuncs;

        private MainWindowViewModel _mainWindowViewModel;

        private SelectionMethods selectionMethods;
        //Propertys
        private string toolTipText = "Назва_елементу/ – обрати дочірній об’єкт контекстного вузла.\nНазва_елементу // – обрати усі об’єкти, які підпорядковані контекстному вузлу (і дочірні, і віддалені об’єкти).\nНазва_елементу [@назва_атрибуту=«значення_атрибуту»] – обрати всі вузли з назвою Назва_елементу, які мають указаний атрибут з заданим значенням.\nНазва_елементу[індекс] – вибрати вузол за індексом (перший елемент в XPath позначається індексом 1)\n";

        public string ToolTipText
        {
            get { return toolTipText; }
            set { toolTipText = value; }
        }

        private string _data;
        public string Data
        {
            get
            {
                return _data;
            }
            set
            {
                _data = value;
                OnPropertyChanged(nameof(_data));
            }
        }
        private ParsePropModel _selectedItem;

        public ParsePropModel SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                OnPropertyChanged(nameof(_selectedItem));
            }
        }

        //Collections
        public List<ParsePropModel> ParseProperty { get; set; }

        //Constructors
        public ParseByTagAndArgViewModel(MainWindowViewModel viewModel)
        {
            _mainWindowViewModel = viewModel;
            selectionMethods = new SelectionMethods();
            ParseProperty = new List<ParsePropModel>() {new ParsePropModel() {ComboText = "Html всередині тегу/тегів", SelectionFunc = selectionMethods.ParseByTagInnerHtml},
                                                        new ParsePropModel() { ComboText = "Весь Html тегу/тегів", SelectionFunc = selectionMethods.ParseByTagOuterHtml},
                                                        new ParsePropModel() { ComboText = "Текст всередині тегу/тегів", SelectionFunc = selectionMethods.ParseByInnerText},
                                                        new ParsePropModel() { ComboText = "Id тега/тегів", SelectionFunc = selectionMethods.ParseId},
                                                        new ParsePropModel() { ComboText = "XPath тегу/тегів", SelectionFunc = selectionMethods.ParseXPath},
                                                        new ParsePropModel() { ComboText = "Батьківська нода тегу", SelectionFunc = selectionMethods.ParseParentalNode},
                                                        new ParsePropModel() { ComboText = "Всі атрибути тегу", SelectionFunc = selectionMethods.ParseAttributes}  };
            SelectedItem = ParseProperty[0];
            searchParseFuncs = new SearchParseFuncs(viewModel);

        }



        //Commands
        private AsyncRelayCommand _command;

        public IAsyncRelayCommand Command
        {
            get
            {
                _command = new AsyncRelayCommand(async () => await Task.Run(() => searchParseFuncs.ParseByTag(Data, SelectedItem)));
                return _command;
            }
        }
        private CommandBase _linkCom;

        public ICommand LinkCommand
        {
            get
            {
                _linkCom = new CommandBase(param => Process.Start(new ProcessStartInfo(@"https://www.w3schools.com/xml/xpath_intro.asp") { UseShellExecute = true }), param => true);
                return _linkCom;
            }
        }

    }
}
