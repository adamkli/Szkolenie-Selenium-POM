using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using POM.Tools;
using System;
using System.IO;

namespace POM.Tests
{
    public class TestBase
    {
        public IWebDriver Driver
        {
            get; private set;
        }

        [SetUp]
        public void DriverOpen()
        {
            Driver = DriverFactory.GetDriver();
        }

        [TearDown]
        public void DriverClose()
        {
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure) || TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error))
            {
                var msg = TestContext.CurrentContext.Result.Message + Environment.NewLine + TestContext.CurrentContext.Result.StackTrace;
                var fullPath = Path.GetFullPath(SaveScreenshot());                
                TestContext.AddTestAttachment(fullPath, msg);
            }

            Driver?.Quit();
            Driver = null;

        }
        private string GetSafeFilename(string filename)
        {
            var safePath = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            safePath = safePath.Replace("(", "").Replace(")", "").Replace(",", "");//for TeamCity artifact (should take into account TC documentation regarding allowed chars)
            return safePath;
        }

        public void SaveScreenshot(string screenshotsFolderPath)
        {
            try
            {
                (Driver as ITakesScreenshot)?.GetScreenshot()
                    .SaveAsFile(screenshotsFolderPath, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
//                throw new Exception($"Problem while saving screenshot:[{screenshotsFolderPath}]\nEXCEPTION:{e.Message}", e);
            }
        }
        public virtual string SaveScreenshot()
        {
            var fileName = $"{GetSafeFilename(TestContext.CurrentContext.Test.FullName)}.{DateTime.Now:yyyyMMddHHmmss}.png";
            SaveScreenshot(fileName);
            return fileName;
        }
    }
}
