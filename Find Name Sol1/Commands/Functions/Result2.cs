namespace Web_Scraping_Mania.Commands.Functions;

[TestClass()]
public class Result2
{
    [TestMethod()]
    public void GetCodeGetCode()
    {
        FormatingFuncs formatingFuncs = new FormatingFuncs();
        string code = formatingFuncs.FormatHTML("u003Cbody lang=\\\"uk-ua\\\" dir=\\\"ltr\\\">\\n\\t\\u003Cdiv class=\\\"header-holder has-default-focus\\\">\\n\\t\\t\\u003Ca href=\\\"#main\\\" class=\\\"skip-to-main-link has-outline-color-text visually-hidden-until-focused position-fixed has-inner-focus focus-visible top-0 left-0 right-0 padding-xs has-text-centered has-body-background\\\" tabindex=\\\"1\\\">");
        formatingFuncs.FormatJS(code);
        //Debug.WriteLine(code);
    }
}