using System;
using System.Threading.Tasks;

namespace Web_Scraping_Mania.Models
{
    public class ParsePropModel
    {
        public string ComboText { get; set; }
        public Func<string, string, Task> SelectionFunc { get; set; }
    }
}
