using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.Generic;
using System.Linq;

namespace POM
{
    public class MainPageObject : PageBase
    {
        [FindsBy(How = How.TagName, Using = "h1")]
        private IWebElement headerTitleElement;

        [FindsBy( How = How.CssSelector, Using = "li a")]
        private IList<IWebElement> subPageLinkElements;

        public string HeaderTitle => headerTitleElement.Text;
        public IEnumerable<string> LinkTitles => subPageLinkElements.Select(el=>el.Text);

        public MainPageObject(IWebDriver driver) : base(driver)
        {
        }
        public string GetHeaderTitle()
        {
            return headerTitleElement.Text;
        }
        public List<string> GetLinkTitles()
        {
            List<string> result = new List<string>();
            foreach (IWebElement link in subPageLinkElements)
                result.Add(link.Text);
            return result;
        }
    }
}