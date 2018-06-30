using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using POM.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace POM.Tests
{
    //[TestFixture]
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
            //Output.WriteLine("Starting [TearDown] for [" + TestContext.CurrentContext.Test.FullName + "]");
            if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Failure) || TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error))
            {


                var msg = Environment.NewLine + TestContext.CurrentContext.Result.Message + Environment.NewLine + TestContext.CurrentContext.Result.StackTrace;
                var fullPath = Path.GetFullPath(SaveScreenshot());

                TestContext.AddTestAttachment(fullPath, msg);

                //if (TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error))
                //    Log.Warn("Unexpected Exception:" + msg);
                //else
                //    Log.Warn("Assertion Failed:" + msg);


                //string xmlData = System.IO.Path.Combine(TestContext.CurrentContext.WorkDirectory, "xmlData.xml");


                //SavePageSource();

            }

            Driver?.Quit();
            Driver = null;

            //var browserLog = Browser.GetLog().ToList();
            //if (browserLog.Count > 0)
            //{
            //    Log.Warn("There were [" + browserLog.Count + "] message(s) on browser console:\n " + string.Join("\n ", browserLog));
            //}

            //if (KeepSession && !TestContext.CurrentContext.Result.Outcome.Equals(ResultState.Error))
            //    Browser.Close();
            //else
            //    Browser.Quit();
            //Log.InfoFormat("-> Ended:" + TestContext.CurrentContext.Test.FullName);
            //Output.WriteLine("Finished [TearDown] for [" + TestContext.CurrentContext.Test.FullName + "]");
        }
        private string GetSafeFilename(string filename)
        {
            var safePath = string.Join("_", filename.Split(Path.GetInvalidFileNameChars()));
            safePath = safePath.Replace("(", "").Replace(")", "").Replace(",", "");//for TeamCity artifact (should take into account TC documentation regarding allowed chars)
            return safePath;
        }

        string _screenshotsFolderPath = "";
        public void SaveScreenshot(string screenshotsFolderPath)
        {
            try
            {
                (Driver as ITakesScreenshot)?.GetScreenshot()
                    .SaveAsFile(screenshotsFolderPath, ScreenshotImageFormat.Png);
            }
            catch (Exception e)
            {
                throw new Exception($"Problem while saving screenshot:[{screenshotsFolderPath}]\nEXCEPTION:{e.Message}", e);
            }
        }
        public virtual string SaveScreenshot()
        {
            var fileName = $"{GetSafeFilename(TestContext.CurrentContext.Test.FullName)}.{DateTime.Now:yyyyMMddHHmmss}.png";
            SaveScreenshot(fileName);
            return fileName;

            //var filePath = $"{_screenshotsFolderPath}{fileName}";
            //try
            //{
            //    Browser.SaveScreenshot(filePath);
            //}
            //catch { }
            //if (File.Exists(filePath))
            //{
            //    //Console.WriteLine("##teamcity[publishArtifacts '" + filePath + "']");
            //    Log.InfoFormat("Screenshot taken: \n\"{0}\"", new Uri(filePath));
            //}
            //else
            //    Log.InfoFormat("Screenshot was not generated.");
            //return filePath;
        }

        protected virtual string SavePageSource()
        {
            var fileName = $"{GetSafeFilename(TestContext.CurrentContext.Test.FullName)}.{DateTime.Now:yyyyMMddHHmmss}.txt";
            var filePath = $"{_screenshotsFolderPath}{fileName}";
            //try
            //{
            //    var pageSource = Browser.PageSource;
            //    if (!string.IsNullOrEmpty(pageSource))
            //    {
            //        File.WriteAllText(filePath, pageSource);
            //        //Console.WriteLine("##teamcity[publishArtifacts '" + filePath + "']");
            //        Log.InfoFormat("PageSource saved: \n\"{0}\"", new Uri(filePath));
            //    }
            //}
            //catch { }
            return filePath;
        }
    }
}
