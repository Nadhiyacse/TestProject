using System;
using OpenQA.Selenium;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Export;
using Automation_Framework._3._Pages.Symphony.Common;
using TechTalk.SpecFlow;
using Automation_Framework.Helpers;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class ExportMediaScheduleFrame : ExportFramePage
    {
        private IWebElement _ddlManagerApprovalEmail => FindElementById("ctl00_Content_CurrentlyLoadedExportControlId_ManagerApprovalEmail_selContact");

        public ExportMediaScheduleFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }
        
        public void SelectManagerApprovalEmail(string email)
        {
            SelectWebformDropdownValueByText(_ddlManagerApprovalEmail, email);
        }

        public void DownloadMediaScheduleExport(string exportType, MediaScheduleExportData exportData, CampaignData campaignData)
        {
            if (IsElementPresent(By.Id("ctl00_Content_ddlExportProvider")))
            {
                SelectExportType(exportType);
            }

            if (exportData != null)
            {
                if (!string.IsNullOrEmpty(exportData.ManagerApprovalEmail))
                {
                    SelectManagerApprovalEmail(exportData.ManagerApprovalEmail);
                }
            }
            
            ScrollAndClickElement(_btnContinue);
            WaitUntilFileIsDownloaded(exportType, campaignData);
        }
    }
}
