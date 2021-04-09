using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Publisher.Overview.Campaign.InsertionOrder.Popups
{
    public class RejectInsertionOrderFrame : BasePage
    {
        private IWebElement _txtReasonForRejection => FindElementById("ctl00_Content_txtComments");
        private IWebElement _chkTermsAndConditions => FindElementById("ctl00_Content_chkTermsAndConditions");
        private IWebElement _btnReject => FindElementById("ctl00_ButtonBar_btnReject");
        private IWebElement _btnCancel => FindElementById("ctl00_ButtonBar_btnCancel");

        private readonly string _reasonForRejection = "Rejected by Publisher - automated tests";

        public RejectInsertionOrderFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void PopulateFieldsThenClickReject()
        {
            ClearInputAndTypeValue(_txtReasonForRejection, _reasonForRejection);
            SetWebformCheckBoxState(_chkTermsAndConditions, true);
            ClickElement(_btnReject);
            SwitchToMainWindow();
        }
    }
}
