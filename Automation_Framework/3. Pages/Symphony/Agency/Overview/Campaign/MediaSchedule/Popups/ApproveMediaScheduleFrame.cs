using System;
using System.IO;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.Helpers;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class ApproveMediaScheduleFrame : BasePage
    {
        private IWebElement _ddlApprovalType => FindElementByXPath("//label[text() = 'Approval type']/../..//div[contains(@class,'select-component__control')]");
        private IWebElement _txtEstimateNumber => FindElementByXPath("//*[text()= 'Estimate Number']/ancestor::div[@class= 'campaign-approval-field row']//input");
        private IWebElement _btnCancel => FindElementByXPath("//*[text()= 'Cancel']/ancestor::button[not(@id)]");
        private IWebElement _btnConfirm => FindElementByXPath("//*[text()= 'Confirm']/ancestor::button[not(@id)]");
        private IWebElement _lblError => FindElementByCssSelector(".has-error.control-label");
        private IWebElement _fileSelect => FindElementByXPath("//div[@class = 'fileupload-component-uploadInputBox']/input");

        public ApproveMediaScheduleFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        private void SelectApproverType(string type)
        {
            if (!string.IsNullOrEmpty(type))
            {
                SelectSingleValueFromReactDropdownByText(_ddlApprovalType, type);
            }
        }

        public bool ApproveMediaScheduleSuccessfully(CampaignApprovalData campaignApprovalData, string approverType = "none", int version = 1)
        {
            if (approverType != "none")
            {
                SelectApproverType(approverType);
            }

            string estimateNumber = GetEstimateNumber(version);
            ClearInputAndTypeValue(_txtEstimateNumber, estimateNumber);

            if (campaignApprovalData != null && !string.IsNullOrEmpty(campaignApprovalData.UploadDocumentName))
            {
                UploadDocument(campaignApprovalData.UploadDocumentName);
            }

            ClickElement(_btnConfirm);
            return IsSubmitForApprovalSuccessful();
        }

        private string GetEstimateNumber(int version)
        {
            string campaignId = FeatureContext[ContextStrings.CampaignId] as string;
            string year = DateTime.Now.ToString("yyyy");
            string estimateNumber = $"{campaignId}/{version}/{year}";
            return estimateNumber;
        }

        private bool IsSubmitForApprovalSuccessful()
        {
            return !IsElementPresent(By.CssSelector(".has-error.control-label"));
        }

        public string GetErrorMessageText()
        {
            Wait.Until(driver => _lblError.Displayed);
            return _lblError.Text;
        }

        public MediaSchedulePage ClickCancelButton()
        {
            ClickElement(_btnCancel);
            return new MediaSchedulePage(driver, FeatureContext);
        }

        private void UploadDocument(string fileName)
        {
            var filePath = FileHelper.GetDocumentFilePath(fileName);

            if (!File.Exists(filePath))
                throw new IOException($"Could not find file {filePath} to upload");

            _fileSelect.SendKeys(filePath);
        }
    }
}