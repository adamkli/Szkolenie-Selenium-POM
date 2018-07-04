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
        public static void SetUpDriver(string browserType)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(path);
            switch (browserType.ToLower())
            {
                case "chrome":
                    new DriverManager().SetUpDriver(new ChromeConfig());
                    break;
                case "firefox":
                case "ff":
                    new DriverManager().SetUpDriver(new FirefoxConfig());
                    break;
                case "edge":
                    new DriverManager().SetUpDriver(new EdgeConfig());
                    break;
                case "internetexplorer":
                case "ie":
                    new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    break;
                default:
                    throw new Exception("Requested browser tyype [" + browserType + "] not defined in DriverFactory");
            }
        }
        public static IWebDriver GetDriver(string browserType)
        {
            IWebDriver _webDriver;

            switch (browserType.ToLower())
            {
                case "chrome":
                    _webDriver = new ChromeDriver();
                    break;
                case "firefox":
                case "ff":
                    _webDriver = new FirefoxDriver();
                    break;
                case "edge":
                    _webDriver = new EdgeDriver();
                    break;
                case "internetexplorer":
                case "ie":
                    _webDriver = new InternetExplorerDriver();
                    break;
                default:
                    throw new Exception("Requested browser tyype [" + browserType + "] not defined in DriverFactory");
            }
            return _webDriver;
        }
    }
}
