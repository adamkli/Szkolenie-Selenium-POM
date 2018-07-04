using System;
using AutoIt;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace POM
{
    public class FileUploadPageObject : PageBase
    {
        [FindsBy(How = How.Id, Using = "file-upload")]
        private IWebElement selectFileElement;
        [FindsBy(How = How.Id, Using = "uploaded-files")]
        private IWebElement uploadedFilesElement;
        [FindsBy(How = How.Id, Using = "file-submit")]
        private IWebElement fileSubmitElement;

        public string FileUploaded => uploadedFilesElement.Text;
            
        protected override string GetRelativeURL() => "upload";
        public FileUploadPageObject(IWebDriver driver) : base(driver)
        {
        }

        public void UploadFile(string fileName)
        {
            selectFileElement.Click();
            AutoItX.Send(fileName);
            AutoItX.Send("{ENTER}");

            //selectFileElement.SendKeys(fileName);
            
            fileSubmitElement.Click();
        }
    }
}