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
        static bool chromeReady = false;
        static bool ffReady = false;
        static bool edgeReady = false;
        static bool ieReady = false;

        public static void SetUpDriver(string browserType)
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(path);
            switch (browserType.ToLower())
            {
                case "chrome":
                    if (!chromeReady)
                    {
                        chromeReady = true;
                        new DriverManager().SetUpDriver(new ChromeConfig());
                    }
                    break;
                case "firefox":
                case "ff":
                    if (!ffReady)
                    {
                        ffReady = true;
                        new DriverManager().SetUpDriver(new FirefoxConfig());
                    }
                    break;
                case "edge":
                    if (!edgeReady)
                    {
                        edgeReady = true;
                        new DriverManager().SetUpDriver(new EdgeConfig());
                    }
                    break;
                case "internetexplorer":
                case "ie":
                    if (!ieReady)
                    {
                        ieReady = true;
                        new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    }
                    break;
                default:
                    throw new Exception("Requested browser tyype [" + browserType + "] not defined in DriverFactory");
            }
        }
        public static IWebDriver GetDriver(string browserType)
        {
            IWebDriver _webDriver;

            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            Directory.SetCurrentDirectory(path);


            switch (browserType.ToLower())
            {
                case "chrome":
                    if (!chromeReady)
                    {
                        chromeReady = true;
                        //new DriverManager().SetUpDriver(new ChromeConfig());
                    }
                    _webDriver = new ChromeDriver();
                    break;
                case "firefox":
                case "ff":
                    if (!ffReady)
                    {
                        ffReady = true;
                        //new DriverManager().SetUpDriver(new FirefoxConfig());
                    }
                    _webDriver = new FirefoxDriver();
                    break;
                case "edge":
                    if (!edgeReady)
                    {
                        edgeReady = true;
                        //new DriverManager().SetUpDriver(new EdgeConfig());
                    }
                    _webDriver = new EdgeDriver();
                    break;
                case "internetexplorer":
                case "ie":
                    if (!ieReady)
                    {
                        ieReady = true;
                        //new DriverManager().SetUpDriver(new InternetExplorerConfig());
                    }
                    _webDriver = new InternetExplorerDriver();
                    break;
                default:
                    throw new Exception("Requested browser tyype [" + browserType + "] not defined in DriverFactory");
            }
            return _webDriver;
        }
    }
}
