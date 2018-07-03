using NUnit.Framework;
using NUnit.Framework.Interfaces;
using POM.Tests.Tools;
using System;
using System.IO;

namespace POM.Tests
{
    class TestWith<TPage> where TPage : PageBase
    {
        public TPage Page
        {
            get;
            private set;
        }
        [SetUp]
        public void DriverOpen()
        {
            Page = PageBase.CreateInstance<TPage>(GlobalSettings.BrowserType, GlobalSettings.BaseURL);
            Console.WriteLine(typeof(TPage) +" opened with " + GlobalSettings.BrowserType);
        }

        [TearDown]
        public void DriverClose()
        {
            if (Page == null)
                return;
            try
            {
                if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure) || TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error))
                {
                    var msg = TestContext.CurrentContext.Result.Message + Environment.NewLine + TestContext.CurrentContext.Result.StackTrace;
                    var fileName = $"{GetSafeFilename(TestContext.CurrentContext.Test.FullName)}.{DateTime.Now:yyyyMMddHHmmss}.png";

                    var fullPath = Path.GetFullPath(fileName);
                    Page.SaveScreenshot(fullPath);
                    if (File.Exists(fullPath))
                    {
                        TestContext.AddTestAttachment(fullPath, msg);
                        Console.WriteLine("Screenshot saved with name:" + fileName);
                    }
                }
            }
            catch { }
            try
            {
                Page.Quit();
            }
            catch { }
            Page = null;
        }
        private string GetSafeFilename(string filename)
        {
            var safePath = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            safePath = safePath.Replace("(", "").Replace(")", "").Replace(",", "");//for TeamCity artifact (should take into account TC documentation regarding allowed chars)
            return safePath;
        }
    }
}
