using System;
using Automation_Framework._3._Pages.Symphony.Common;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Details.Popups
{
    public class ExportCampaignFrame : ExportFramePage
    {
        public ExportCampaignFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void DownloadCampaignExport(string exportType)
        {
            if (IsElementPresent(By.Id("ctl00_Content_ddlExportProvider")))
            {
                SelectExportType(exportType);
            }

            ScrollAndClickElement(_btnContinue);
        }
    }
}
