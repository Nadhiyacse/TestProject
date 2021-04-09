using System;
using Automation_Framework.DataModels.WorkflowTestData.InsertionOrder;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder
{
    public class GridInsertionOrderLandingPage : BasePage
    {
        private IWebElement _btnCreateIO => FindElementById("iogrid_add_button");
        private IWebElement _ddlMenuItem(string publisher) => FindElementByXPath($"//ul[@role='menu']//a[text()='{publisher}']");
        private IWebElement _ddlCountry => FindElementByXPath("//div[contains(@class='country-select']");
        private IWebElement _tblInsertionOrderGrid => FindElementByClassName("ag__group-body");
        private IWebElement _lnkViewIo(string ioname) => FindElementByXPath($"//div[.='{ioname}']/..//a");
        private IWebElement _txtIoStatus(string ioname) => FindElementByXPath($"//div[.='{ioname}']/..//div[@data-test-selector='insertion-order-status']//div[@class='aui--pill-children']");

        public GridInsertionOrderLandingPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateInsertionOrder(InsertionOrderData insertionOrderData)
        {
            ClickElement(_btnCreateIO);
            ClickElement(_ddlMenuItem(insertionOrderData.IOPublisher));
        }

        public void ViewInsertionOrder(string publisherNameOrIoName)
        {
            WaitForElementToBeClickable(_lnkViewIo(publisherNameOrIoName));
            ClickElement(_lnkViewIo(publisherNameOrIoName));
        }

        public string GetInsertionOrderStatus(string publisherNameOrIoName)
        {
            return _txtIoStatus(publisherNameOrIoName).Text;
        }

        public string TranslateLegacyStatusToGridStatus(string legacyStatus, bool isIoApprovalEnabled, bool hasIoApproverRejected)
        {
            if (legacyStatus.ToLower().Contains("saved"))
                return (isIoApprovalEnabled && hasIoApproverRejected) ? "Attention" : "Draft";
            else if (legacyStatus.ToLower().Contains("saved (recall)"))
                return "Recalled";
            else if (legacyStatus.ToLower().Contains("issued"))
                return "Pending";
            else if (legacyStatus.ToLower().Contains("part signed"))
                return isIoApprovalEnabled ? "Pending" : "Attention";
            else if (legacyStatus.ToLower().Contains("datesignedoff"))
                return "Approved";
            else if (legacyStatus.ToLower().Contains("rejected"))
                return "Attention";
            else if (legacyStatus.ToLower().Contains("pending approval"))
                return "Attention";
            else
                return $"Legacy status '{legacyStatus}' is not supported";
        }

        public bool DoesInsertionOrderExist()
        {
            var ioname = FeatureContext[ContextStrings.IOName].ToString();
            return IsElementPresent(By.XPath($"//*[contains(text(), '{ioname}')]"));
        }

        public void WaitForPendingInsertionOrderStatusLinkToBeDisplayed(string publisherName)
        {
            try
            {
                Wait.Until(d => GetInsertionOrderStatus(publisherName).Equals("Pending"));
            }
            catch (Exception e)
            {
                Assert.Fail($"Pending IO status link not displayed.\n{e.Message}");
            }
        }
    }
}
