using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web_Scraping_Mania.Commands.Functions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Linq.Expressions;
using Web_Scraping_Mania.Views;
using Web_Scraping_Mania.ViewModels;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Diagnostics;
using HtmlAgilityPack;

namespace Web_Scraping_Mania.Commands.Functions
{
    [TestClass()]
    public class Result2
    {
        [TestMethod()]
        
        public void AppendResourceToTabDAsyncGetCode()
        {
            MainWindowViewModel viewModelq  = new MainWindowViewModel();
            SearchParseFuncs searchParseFuncs = new SearchParseFuncs(viewModelq);
            string result = searchParseFuncs.LinkForamtion("https://www.netacad.com/portal/learning", "/auth/resources/lvhtw/login/skillsforall-theme/node_modules/patternfly/dist/css/patternfly-additions.min.css");
            //Assert.AreEqual(result, "https://auth.netacad.com/auth/resources/lvhtw/login/skillsforall-theme/node_modules/patternfly/dist/css/patternfly-additions.min.css");
           Debug.WriteLine(result);
        }
      
    }
}
