using System.Configuration;
using Automation_Framework.DataModels.CommonData;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Common
{
    public class AdslotLoginPage : BasePage
    {
        private IWebElement _txtUserName => FindElementByName("username");
        private IWebElement _txtPassword => FindElementByName("password");
        private IWebElement _btnSubmit => FindElementByXPath("*//button[text()='Log in']");

        private string _publisherUrl;
        public AdslotLoginPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _publisherUrl = ConfigurationManager.AppSettings["AdslotPublisherUrl"];
        }

        public void LoginToAdslotPublisher(LoginUserData loginUserData)
        {
            OpenAndSwitchToNewWindow();
            driver.Navigate().GoToUrl(_publisherUrl);
            SetElementText(_txtUserName, loginUserData.Username);
            ClearInputAndTypeValue(_txtPassword, loginUserData.Password);
            ClickElement(_btnSubmit);
        }
    }
}
