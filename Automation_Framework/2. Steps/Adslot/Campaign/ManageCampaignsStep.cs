using Automation_Framework._3._Pages.Adslot.Campaigns;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Adslot.Campaign
{
    [Binding]
    public class ManageCampaignsStep : BaseStep
    {
        private readonly ManageCampaignsPage _manageCampaignsPage;

        public ManageCampaignsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _manageCampaignsPage = new ManageCampaignsPage(driver, featureContext);
        }

        [Given(@"I select my campaign in Adslot publisher")]
        [When(@"I select my campaign in Adslot publisher")]
        public void SelectCampaignInAdslotPublisher()
        {
            _manageCampaignsPage.SearchAndSelectCampaign(WorkflowTestData.CampaignData.DetailsData.CampaignName);
        }
    }
}