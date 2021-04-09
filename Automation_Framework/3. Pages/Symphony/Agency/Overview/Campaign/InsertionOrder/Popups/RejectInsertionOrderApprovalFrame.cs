using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class RejectInsertionOrderApprovalFrame : BasePage
    {
        private IWebElement _txtReasonForRejection => FindElementById("ctl00_Content_txtComments");
        private IWebElement _btnReject => FindElementById("ctl00_ButtonBar_btnReject");
        private IWebElement _btnCancel => FindElementById("ctl00_ButtonBar_btnCancel");

        private readonly string _reasonForRejection = "Rejected by Approver - automated tests";

        public RejectInsertionOrderApprovalFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void RejectInsertionOrderApproval()
        {
            ClearInputAndTypeValue(_txtReasonForRejection, _reasonForRejection);
            ClickElement(_btnReject);
            SwitchToMainWindow();
        }
    }
}
