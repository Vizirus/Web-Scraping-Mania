using System;
using System.Threading.Tasks;
using Web_Scraping_Mania.ViewModels;

namespace Web_Scraping_Mania.Models
{
    public class ParsePropModel
    {
        public string ComboText { get; set; }
        public Func<MainWindowViewModel, string, string, Task> SelectionFunc { get; set; }
    }
}
