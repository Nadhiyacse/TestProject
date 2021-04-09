using Automation_Framework._3._Pages.Symphony.Publisher.Overview;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Publisher.Overview
{
    [Binding]
    public class PublisherOverviewStep : BaseStep
    {
        private readonly PublisherOverviewPage _pubilsherOverviewPage;

        public PublisherOverviewStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _pubilsherOverviewPage = new PublisherOverviewPage(driver, featureContext);
        }

        [Given(@"I select my campaign as a publisher user")]
        public void SelectCampaignAsPublisher()
        {
            _pubilsherOverviewPage.SearchAndSelectCampaign(WorkflowTestData.CampaignData.DetailsData.CampaignName);
        }
    }
}