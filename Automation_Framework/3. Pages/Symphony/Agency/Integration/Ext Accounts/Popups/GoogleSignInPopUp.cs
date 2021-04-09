using System;
using System.Configuration;
using Automation_Framework.DataModels.InfrastructureData.Integration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.Ext_Accounts.Popups
{
    public class GoogleSignInPopUp : BasePage
    {
        private readonly bool _isHeadless = Convert.ToBoolean(ConfigurationManager.AppSettings["Headless"]);

        private IWebElement _txtUserName => _isHeadless ? FindElementById("Email") : FindElementById("identifierId");
        private IWebElement _txtPassword => _isHeadless ? FindElementByName("Passwd") : FindElementByName("password");
        private IWebElement _btnUserNameNext => _isHeadless ? FindElementById("next") : FindElementById("identifierNext");
        private IWebElement _btnPasswordNext => _isHeadless ? FindElementById("submit") : FindElementById("passwordNext");
        private IWebElement _btnAllow => FindElementById("submit_approve_access");

        public GoogleSignInPopUp(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SignInToGoogle(ExternalAccountData externalAccountData)
        {
            InputUserNameAndPassword(externalAccountData.Username, externalAccountData.Password);
            SwitchToMainWindow();
        }

        public void SignInToGoogleAndAllowAccess(ExternalAccountData externalAccountData)
        {
            InputUserNameAndPassword(externalAccountData.Username, externalAccountData.Password);
            ClickElement(_btnAllow);
            SwitchToMainWindow();
        }

        private void InputUserNameAndPassword(string username, string password)
        {
            if (IsOctopusVariable(username) || (IsOctopusVariable(password)))            
                throw new ArgumentException($"Errors in test data found: \n{username} and/or {password}");
            
            ClearInputAndTypeValue(_txtUserName, username);
            ClickElement(_btnUserNameNext);
            ClearInputAndTypeValue(_txtPassword, password);
            ClickElement(_btnPasswordNext);
        }
    }
}
