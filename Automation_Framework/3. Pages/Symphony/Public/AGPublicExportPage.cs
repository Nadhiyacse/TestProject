using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Public
{
    public class AGPublicExportPage : BasePage
    {
        private const string AG_IO_EXPORT_BODY_XPATH = @"//div[contains(@class, 'export-body-section')]//div[{0}][text()='{1}']";
        private const string DOWNLOAD_BUTTON_XPATH = @"//div[contains(@class,'export-header-content')]//button";
        private const string PUBLIC_EXPORT_IFRAME_ID = "ncm_modal_frame";
        private const string PUBLIC_EXPORT_IFRAME_SRC = "/public/symphony-app/export/";
        private const string AG_IO_EXPORT_TYPE = "AG IO PDF Export (Prorata Monthly Allocations with Site Breakdown)";

        public AGPublicExportPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void IsOnAgPublicExportPage()
        {
            WaitForElementToBeVisible(By.Id(PUBLIC_EXPORT_IFRAME_ID));

            SwitchToFrame(PUBLIC_EXPORT_IFRAME_SRC);

            WaitForElementToBeVisible(By.ClassName("export-container"));
        }

        public void IsCampaignNameVisible(string campaignName)
        {
            var campaignNameXpath = string.Format(AG_IO_EXPORT_BODY_XPATH, "1", campaignName);

            WaitForElementToBeVisible(By.XPath(campaignNameXpath));
        }

        public void IsVersionNumberVisible(int version)
        {
            var versionNumberXpath = string.Format(AG_IO_EXPORT_BODY_XPATH, "2", version.ToString());

            Wait.Until(d => IsElementPresent(By.XPath(versionNumberXpath)));
        }

        public void DownloadAgIoExport(CampaignData campaignData)
        {
            var downloadButton = FindElementByXPath(DOWNLOAD_BUTTON_XPATH);

            ClickElement(downloadButton);

            WaitUntilFileIsDownloaded(AG_IO_EXPORT_TYPE, campaignData);
        }

        public void ClosePublicExportTab()
        {
            CloseTabAndSwitchToNewWindow(string.Empty);
        }
    }
}
