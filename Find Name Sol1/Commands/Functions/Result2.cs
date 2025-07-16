using Web_Scraping_Mania.Commands.Functions;
namespace Web_Scraping_Mania
{
    [TestClass()]
    public class Result2
    {
        
        
        [TestMethod()]
        public void LinkFormationTest()
        {
            SearchParseFuncs searchParseFuncs = new SearchParseFuncs(new ViewModels.MainWindowViewModel());
            string link = "https://www.w3schools.com/xml/xpath_intro.asp";
            string result = LinkForamtion(link, "/lib/topnav/main.css?v=1.0.35>");
            Assert.Equals(result, "https://www.w3schools.com/lib/topnav/main.css?v=1.0.35");
        }

        
    }
}
