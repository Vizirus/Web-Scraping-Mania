using System;
using System.Diagnostics;
using System.IO;

namespace Web_Scraping_Mania.Commands.Functions
{
    public class FormatingFuncs
    {
        private void formateCode(string fileName)
        {
            using (Process process = new Process())
            {
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.RedirectStandardInput = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.UseShellExecute = false;
                process.Start();
                process.StandardInput.WriteLine($"npx prettier --write {fileName}");
                process.StandardInput.Close();
                process.Close();
            }
        }
        public string FormatHTML(string formatingString)
        {

            string result = formatingString.Replace("\\u003C", "<").Replace("u003C", "<").Replace("\\n", "\n").Replace("\\t", "\t").Replace("\\\"", "\"");
            return result;
        }
        public string FormatCSS(string formatingString)
        {
            string result = String.Empty;
            using (StreamWriter sw = new StreamWriter(@"style.css"))
            {
                sw.Write(formatingString);
                sw.Close();
            }
            formateCode("style.css");
            using (StreamReader sr = new StreamReader(@"style.css"))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
        public string FormatJS(string formatingString)
        {
            string result = String.Empty;
            using (StreamWriter sw = new StreamWriter(@"scripts.js"))
            {
                sw.Write(formatingString);
                sw.Close();
            }
            formateCode("scripts.js");
            using (StreamReader sr = new StreamReader(@"scripts.js"))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}
