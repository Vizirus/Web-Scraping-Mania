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

namespace Web_Scraping_Mania.Commands.Functions
{
    [TestClass()]
    public class Result2
    {
        [TestMethod()]
        
        public void AppendResourceToTabDAsyncGetCode()
        {
            ObservableCollection<bool> bools = new ObservableCollection<bool>() { false, false, false };
            Debug.WriteLine(bools[0]);
            changeBool(bools[0]);
            Debug.WriteLine(bools[0]);
        }
        List<int[]> findText(string data, string test, int[] dictCount)
        {
            List<int[]> selectionCollection = new List<int[]>();
            string text = test;
                if (text.Contains(data) == true)
                {
                    foreach (Match match in Regex.Matches(text, data))
                    {
                        selectionCollection.Add([match.Index, match.Value.Length]);
                    }
                }
                else
                {
                    MessageBox.Show("Не вдалося знайти слово або символ в тексті!", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                dictCount[1] = selectionCollection.Count;
            return selectionCollection;
        }
        private void plusSelection(List<int[]> selectionArr, int[] dictCount)
        {
            dictCount[0] += 1;
            if (dictCount[0] > dictCount[1])
            {
                dictCount[0] = dictCount[1];
            }
            Debug.WriteLine(dictCount[0]);
        }
        void changeBool(bool en)
        {
            en = true;
        }
    }
}
