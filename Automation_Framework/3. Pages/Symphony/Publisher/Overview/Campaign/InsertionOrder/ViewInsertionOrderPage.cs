using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Publisher.Overview.Campaign.InsertionOrder
{
    public class ViewInsertionOrderPage : BasePage
    {
        private const string FRAME_ACCEPT_IO = "/InsertionOrder/SignoffIO.aspx?";
        private const string FRAME_EXPORT = "/Export/Export.aspx?";
        private const string FRAME_SEND_IO = "/InsertionOrder/SendIO.aspx?";
        private const string FRAME_REJECT_IO = "/InsertionOrder/RejectIO.aspx?";

        private IWebElement _btnSignOff => FindElementById("ctl00_Content_btnSignoff");
        private IWebElement _btnReject => FindElementById("ctl00_Content_btnReject");
        private IWebElement _btnEdit => FindElementById("ctl00_Content_btnEdit");
        private IWebElement _btnExport => FindElementById("ctl00_Content_btnExport");
        private IWebElement _btnIssue => FindElementById("ctl00_Content_btnIssue");
        private IWebElement _pnlMessage => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _btnRecall => FindElementById("ctl00_Content_btnRecall");
        private IWebElement _lnkTabNonMediaCostsIncluded => FindElementById("lnktabOtherCosts");
        private IWebElement _tblNonMediaCostItems => FindElementById("ctl00_Content_tabWidgetControl_tabOtherCosts_ucIOOtherCostItemList_gvOtherCost");

        public ViewInsertionOrderPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickSignOffButton()
        {
            ClickElement(_btnSignOff);
            SwitchToFrame(FRAME_ACCEPT_IO);
        }

        public string GetMsgText()
        {
            Wait.Until(driver => _pnlMessage.Displayed);
            return _pnlMessage.Text;
        }

        public void ClickExport()
        {
            ClickElement(_btnExport);
            SwitchToFrame(FRAME_EXPORT);
        }

        public void ClickIssueButton()
        {
            driver.Navigate().Refresh();
            ClickElement(_btnIssue);
            SwitchToFrame(FRAME_SEND_IO);
        }

        public void ClickRejectButton()
        {
            ClickElement(_btnReject);
            SwitchToFrame(FRAME_REJECT_IO);
        }

        public void ClickNonMediaCostItemsIncludedTab()
        {
            ClickElement(_lnkTabNonMediaCostsIncluded);
        }

        public bool VerifyAllNonMediaCostItemsExist(List<NonMediaCostData> nonMediaCostData)
        {
            var rows = _tblNonMediaCostItems.FindElements(By.XPath("./table/tbody/tr"));

            if ((nonMediaCostData == null || !nonMediaCostData.Any()) && rows != null && rows.Any())
                throw new ArgumentNullException("Non media costs were missed in test data.");

            foreach (var nonMediaCost in nonMediaCostData)
            {
                var isFound = GetFirstElementWithText(rows, nonMediaCost.Vendor) != null;

                if (!isFound)
                    return false;
            }
            return true;
        }
    }
}