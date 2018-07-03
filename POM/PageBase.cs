using OpenQA.Selenium;
using POM.Tools;
using SeleniumExtras.PageObjects;
using System;

namespace POM
{
    public class PageBase
    {
        protected string baseURL;
        protected IWebDriver driver;

        protected virtual string GetRelativeURL() => "";
        protected virtual string GetURL() => new Uri(new Uri(baseURL), GetRelativeURL()).ToString(); //baseURL + GetRelativeURL();

        public PageBase(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(this, new RetryingElementLocator(driver, TimeSpan.FromSeconds(30), TimeSpan.FromMilliseconds(100)));
            //PageFactory.InitElements(driver, this);
        }

        public void Open(string url = null)
        {
            if (url == null)
                url = GetURL();
            driver.Navigate().GoToUrl(url);
        }
        public void Quit()
        {
            driver?.Quit();
            driver = null;
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
                //throw new Exception($"Problem while saving screenshot:[{screenshotsFolderPath}]\nEXCEPTION:{e.Message}", e);
            }
        }
        public static TPage CreateInstance<TPage>(string browserType, string baseUrl) where TPage : PageBase
        {
            var driver = DriverFactory.GetDriver(browserType);
            var page = (TPage)Activator.CreateInstance(typeof(TPage), driver);
            page.baseURL = baseUrl;
            driver.Manage().Window.Maximize();
            page.Open();
            return page;
        }
    }
}
