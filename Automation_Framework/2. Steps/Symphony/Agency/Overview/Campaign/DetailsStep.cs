using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Details;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Details.Popups;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class DetailsStep : BaseStep
    {
        private DetailsPage _detailsPage;
        private ExportCampaignFrame _exportCampaignFrame;

        public DetailsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _detailsPage = new DetailsPage(driver, featureContext);
            _exportCampaignFrame = new ExportCampaignFrame(driver, featureContext);
        }

        [Then(@"The campaign should be created successfully")]
        public void VerifyCampaignShouldBeCreatedSuccessfully()
        {
            Assert.AreEqual(WorkflowTestData.CampaignData.DetailsData.CampaignName, _detailsPage.GetCampaignName());
            var expectedCampaignDates = $"Dates: {FeatureContext[ContextStrings.CampaignStartDate]} - {FeatureContext[ContextStrings.CampaignEndDate]}";
            Assert.AreEqual(expectedCampaignDates, _detailsPage.GetCampaignDates());
        }

        [When(@"I export the campaign export '(.*)'")]
        public void ExportTheCampaignExport(string export)
        {
            _detailsPage.ClickExportButton();
            _exportCampaignFrame.DownloadCampaignExport(export);
        }

        [Then(@"The campaign export should be exported")]
        public void CampaignExportIsExported()
        {
            var msg = _exportCampaignFrame.GetSuccessMessage();
            Assert.AreEqual("Please save your export file.", msg, "Export failed");
        }
    }
}
