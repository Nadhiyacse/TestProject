using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder
{
    public class ViewInsertionOrderPage : BasePage
    {
        private const string FRAME_ACCEPT_IO = "/InsertionOrder/SignoffIO.aspx?";
        private const string FRAME_EXPORT = "/Export/Export.aspx?";
        private const string FRAME_SEND_IO = "/InsertionOrder/SendIO.aspx?";
        private const string FRAME_SEND_IO_APPROVAL = "/InsertionOrder/SendIOApproval.aspx?";
        private const string FRAME_REJECT_IO_APPROVAL = "/InsertionOrder/RejectIOApproval.aspx?";
        private const string FRAME_REJECT_IO = "/InsertionOrder/RejectIO.aspx?";
        
        private IWebElement _btnSignOff => FindElementById("ctl00_Content_btnSignoff");
        private IWebElement _btnReject => FindElementById("ctl00_Content_btnReject");
        private IWebElement _btnEdit => FindElementById("ctl00_Content_btnEdit");
        private IWebElement _btnExport => FindElementById("ctl00_Content_btnExport");
        private IWebElement _btnImport => FindElementById("ctl00_Content_btnImport");
        private IWebElement _btnIssue => FindElementById("ctl00_Content_btnIssue");
        private IWebElement _pnlMessage => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _btnSubmit => FindElementById("ctl00_Content_btnMarkReadyForAgencyApproval");
        private IWebElement _btnApprove => FindElementById("ctl00_Content_btnAgencyApprove");
        private IWebElement _btnRejectApproval => FindElementById("ctl00_Content_btnAgencyApproverReject");
        private IWebElement _btnCancelApproval => FindElementById("ctl00_Content_btnCancel");
        private IWebElement _btnResend => FindElementById("ctl00_Content_btnResendApprovedInsertionOrder");
        private IWebElement _btnRecall => FindElementById("ctl00_Content_btnRecall");


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

        public void ClickEdit()
        {
            ClickElement(_btnEdit);
        }

        public void ClickCancel()
        {
            ClickElement(_btnCancelApproval);
            AcceptAlert();
        }

        public void ClickIssueButton()
        {
            driver.Navigate().Refresh();
            ClickElement(_btnIssue);
            SwitchToFrame(FRAME_SEND_IO);
        }

        public bool IsBtnSubmitDisplay()
        {
            return _btnSubmit.Displayed;
        }

        public void ClickSubmitButton()
        {
            ClickElement(_btnSubmit);
            SwitchToFrame(FRAME_SEND_IO_APPROVAL);
        }

        public void ClickApproveButton()
        {
            ClickElement(_btnApprove);
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            wait.Until(driver => _btnRecall.Displayed);
        }

        public void ClickRejectApprovalButton()
        {
            ClickElement(_btnRejectApproval);
            SwitchToFrame(FRAME_REJECT_IO_APPROVAL);
        }

        public void ClickRecallButton()
        {
            ClickElement(_btnRecall);
            AcceptAlert();
        }

        public void ClickRejectButton()
        {
            ClickElement(_btnReject);
            SwitchToFrame(FRAME_REJECT_IO);
        }
    }
}