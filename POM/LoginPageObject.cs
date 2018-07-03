using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace POM
{
    public class LoginPageObject : PageBase
    {
        protected override string GetRelativeURL() => "login";

        [FindsBy(How = How.Name, Using = "username")]
        private IWebElement userNameElement;
        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordElement;
        [FindsBy(How = How.TagName, Using = "button")]
        IWebElement loginButtonElement;
        [FindsBy(How = How.Id, Using = "flash")]
        IWebElement flashMessageElement;

        public string FlashMessage => flashMessageElement.Text;
        public LoginPageObject(IWebDriver driver) : base(driver)
        {
        }

        public void Login(string userName, string password)
        {
            userNameElement.SendKeys(userName);
            passwordElement.SendKeys(password);
            loginButtonElement.Click();
        }
    }
}