using System;
using Automation_Framework.DataModels.WorkflowTestData.InsertionOrder;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder
{
    public class ManageMultipleInsertionOrderPage : BasePage
    {
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnCreate");
        private IWebElement _btnRevise => FindElementById("ctl00_Content_lstPublishers_ctrl1_lnkRevise");
        private IWebElement _lnkIoStatus(string ioname) => FindElementByXPath($"//td[contains(., '{ioname}')]/ancestor::tr//a[contains(@id, 'lnkPendingIOVersion')]");
        private IWebElement _lnkLastSignOffVersion(string ioname) => FindElementByXPath($"//td[contains(., '{ioname}')]/ancestor::tr//a[contains(@id, 'lnkLastSignedOffVersion')]");        
        private IWebElement _rdoPublisher(string publisher) => FindElementByXPath($"//td[contains(text(), '{publisher}')]/ancestor::tr//input[@value= 'rdoPublisher']");

        public ManageMultipleInsertionOrderPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectCountry(string country)
        {
            if (!string.IsNullOrEmpty(country))
            {
                SelectWebformDropdownValueByText(_ddlCountry, country);
            }
        }

        public void ChoosePublisherAndClickCreateButton(InsertionOrderData insertionOrderData)
        {
            ClickElement(_rdoPublisher(insertionOrderData.IOPublisher));
            ClickElement(_btnCreate);
        }

        public void ClickIoStatusLink()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            WaitForElementToBeClickable(_lnkIoStatus(ioname));
            ClickElement(_lnkIoStatus(ioname));
        }

        public void ClickReviseButton()
        {
            ClickElement(_btnRevise);
        }

        public bool IsLastSignedOffVersionCorrect(string date, string version)
        {
            string versionNumber = version.Split(new char[0])[1];
            return GetLastSignedOffVersion().Equals($"{date} {versionNumber}", StringComparison.OrdinalIgnoreCase);
        }

        public string GetIOStatus()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return _lnkIoStatus(ioname).Text;
        }

        public string GetLastSignedOffVersion()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return _lnkLastSignOffVersion(ioname).Text;
        }

        public void ClickLastSignedOffVersionLink()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            ClickElement(_lnkLastSignOffVersion(ioname));
        }

        public bool DoesIOExist()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return IsElementPresent(By.XPath($"//*[contains(text(), '{ioname}')]"));
        }
    }
}