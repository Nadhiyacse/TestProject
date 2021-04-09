using System;
using System.Diagnostics;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Common
{
    public class LoginPage : BasePage
    {
        private IWebElement _txtUserName => FindElementById("UserName");
        private IWebElement _txtPassword => FindElementById("Password");
        private IWebElement _btnSubmit => FindElementById("btnLogin");

        public LoginPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void LoginToApplication(LoginUserData loginUserData)
        {
            if (IsOctopusVariable(loginUserData.Username) || IsOctopusVariable(loginUserData.Password))
                throw new ArgumentException($"Errors in test data found: \n{loginUserData.Username} and/or {loginUserData.Password}");

            SetElementText(_txtUserName, loginUserData.Username);
            SetElementText(_txtPassword, loginUserData.Password);

            var stopwatch = Stopwatch.StartNew();
            ClickElement(_btnSubmit);
            Assert.IsFalse(IsLoginFailed(), "Login failed. Username and/or password might be incorrect.");
            WaitForElementToBeClickable(By.XPath("//div[@class='avatar-component-initials']"));
            stopwatch.Stop();
            FeatureContext[ContextStrings.ElapsedTime] = stopwatch.Elapsed;
        }

        public bool IsLoginFailed()
        {
            return IsElementPresent(By.Id("pnlErrorMessage"));
        }
    }
}