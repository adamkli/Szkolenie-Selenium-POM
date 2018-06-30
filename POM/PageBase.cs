using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM
{
    public class PageBase
    {
        protected string baseURL = "http://the-internet.herokuapp.com/";
        protected IWebDriver driver;

        protected virtual string GetRelativeURL() => "";
        protected virtual string GetURL() => new Uri(new Uri(baseURL), GetRelativeURL()).ToString(); //baseURL + GetRelativeURL();

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30), TimeSpan.FromMilliseconds(100)));
            //PageFactory.InitElements(driver, this);
        }

        public void Open()
        {
            driver.Navigate().GoToUrl(GetURL());
        }
        public void SaveScreenshot(string screenshotsFolderPath)
        {
            try
            {
                (driver as ITakesScreenshot)?.GetScreenshot()
                    .SaveAsFile(screenshotsFolderPath, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception($"Problem while saving screenshot:[{screenshotsFolderPath}]\nEXCEPTION:{e.Message}", e);
            }
        }
    }
}
