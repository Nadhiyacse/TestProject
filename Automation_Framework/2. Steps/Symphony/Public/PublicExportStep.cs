using Automation_Framework._3._Pages.Symphony.Public;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Public
{
    [Binding]
    public class PublicExportStep : BaseStep
    {
        private AGPublicExportPage _automaticGuaranteedPublicExportPage;

        public PublicExportStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _automaticGuaranteedPublicExportPage = new AGPublicExportPage(driver, featureContext);
        }

        [Then(@"I should be on the AG IO Public Export Page")]
        public void CheckIfOnAgPublicExportPage()
        {
            _automaticGuaranteedPublicExportPage.IsOnAgPublicExportPage();
        }

        [Then(@"The AG IO Export Campaign Name Should Be My Current Campaign And The Version Number Should Be (\d+)")]
        public void VerifyAgExportData(int versionNumber)
        {
            _automaticGuaranteedPublicExportPage.IsCampaignNameVisible(WorkflowTestData.CampaignData.DetailsData.CampaignName);
            _automaticGuaranteedPublicExportPage.IsVersionNumberVisible(versionNumber);
        }

        [Then(@"I Should Be Able To Download The AG IO Export")]
        public void DownloadAgIoExport()
        {
            _automaticGuaranteedPublicExportPage.DownloadAgIoExport(WorkflowTestData.CampaignData);
        }

        [Then(@"I Close The Public Export Tab And Go Back To The Previous Tab")]
        public void ClosePublicExportAndGoBackToPreviousTab()
        {
            _automaticGuaranteedPublicExportPage.ClosePublicExportTab();
        }
    }
}
