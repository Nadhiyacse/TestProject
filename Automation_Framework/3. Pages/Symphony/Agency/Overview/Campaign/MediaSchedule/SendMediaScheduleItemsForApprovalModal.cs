using System;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class SendMediaScheduleItemsForApprovalModal : BasePage
    {
        private IWebElement _ddlApprovers => FindElementByXPath("//label[text() = 'Approvers']/../..//div[contains(@class,'select-component__control')]");
        private IWebElement _btnClose => FindElementByXPath("//*[text()= 'Close']/ancestor::button[not(@id)]");
        private IWebElement _btnConfirm => FindElementByXPath("//*[text()= 'Confirm']/ancestor::button[not(@id)]");

        private const string FRAME_APPROVAL_NOTIFICATION = "submitforapproval";

        public SendMediaScheduleItemsForApprovalModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectApproversAndConfirm(CampaignApprovalData campaignApprovalData)
        {
            if (campaignApprovalData == null)
                throw new ArgumentNullException("Campaign Approval Data is empty");

            if (campaignApprovalData.Approvers == null)
                throw new ArgumentNullException("Approvers are not available in test data");

            SelectMultipleValuesFromReactDropdownByText(_ddlApprovers, campaignApprovalData.Approvers);
            ClickElement(_btnConfirm);
            WaitForLoaderSpinnerToDisappear();
        }
    }
}
