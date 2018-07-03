using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using System;
using System.IO;
using System.Reflection;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace POM.Tools
{
    public class DriverFactory
    {
        public static IWebDriver GetDriver(string browserType)
        {
            IWebDriver _webDriver;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(path);
            bool chromeReady = false;
            bool ffReady = false;
            bool edgeReady = false;
            bool ieReady = false;

            switch (browserType.ToLower())
            {
                case "chrome":
                    if (!chromeReady)
                        new DriverManager().SetUpDriver(new ChromeConfig());
                    chromeReady = true;
                    _webDriver = new ChromeDriver();
                    break;
                case "firefox":
                case "ff":
                    if (!ffReady)
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                    ffReady = true;
                    _webDriver = new FirefoxDriver();
                    break;
                case "edge":
                    if (!edgeReady)
                        new DriverManager().SetUpDriver(new EdgeConfig());
                    edgeReady = true;
                    _webDriver = new EdgeDriver();
                    break;
                case "internetexplorer":
                case "ie":
                    if (!ieReady)
                        new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    ieReady = true;
                    _webDriver = new InternetExplorerDriver();
                    break;
                default:
                    throw new Exception("Requested browser tyype [" + browserType + "] not defined in DriverFactory");
            }
            return _webDriver;
        }
    }
}
