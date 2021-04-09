using System;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Publisher.Overview.Campaign.InsertionOrder
{
    public class ManageMultipleInsertionOrderPage : BasePage
    {
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnCreate");
        private IWebElement _lnkIoStatus(string ioname) => FindElementByXPath($"//td[contains(., '{ioname}')]/ancestor::tr//a[contains(@id, 'lnkPendingIOVersion')]");
        private IWebElement _lnkLastSignOffVersion(string ioname) => FindElementByXPath($"//td[contains(., '{ioname}')]/ancestor::tr//a[contains(@id, 'lnkLastSignedOffVersion')]");

        public ManageMultipleInsertionOrderPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreateButton()
        {
            ClickElement(_btnCreate);
        }

        public void ClickIoStatusLink()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            WaitForElementToBeClickable(_lnkIoStatus(ioname));
            ClickElement(_lnkIoStatus(ioname));
        }

        public bool IsLastSignedOffVersionCorrect(string date, string version)
        {
            string versionNumber = version.Split(new char[0])[1];
            return GetLastSignedOffVersion().Equals($"{date} {versionNumber}", StringComparison.OrdinalIgnoreCase);
        }

        public string GetIoStatus()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return _lnkIoStatus(ioname).Text;
        }

        public string GetLastSignedOffVersion()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return _lnkLastSignOffVersion(ioname).Text;
        }

        public bool DoesIOExist()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return IsElementPresent(By.XPath($"//*[contains(text(), '{ioname}')]"));
        }
    }
}
