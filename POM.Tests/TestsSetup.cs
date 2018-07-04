using NUnit.Framework;
using POM.Tests.Tools;
using POM.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    [SetUpFixture]
    public class TestsSetupClass
    {

        [OneTimeSetUp]
        public static void GlobalSetup()
        {
            KillOrphantBrowsers();
            DriverFactory.SetUpDriver(GlobalSettings.BrowserType);
        }

        [OneTimeTearDown]
        public static void GlobalTeardown()
        {
            KillOrphantBrowsers();
        }
        private static void KillOrphantBrowsers()
        {
            if (GlobalSettings.BrowserType.ToLower().Contains("chrome"))
                ProcKiller.KillOrphantDriversWithChildren("chromedriver");
        }
    }
}
