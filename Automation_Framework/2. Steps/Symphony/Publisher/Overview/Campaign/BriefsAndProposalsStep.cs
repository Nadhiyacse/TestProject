using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Publisher.Overview
{
    [Binding]
    public class BriefsAndProposalsStep : BaseStep
    {
        public BriefsAndProposalsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        [When(@"I open the received brief")]
        public void PublisherOpenTheReceivedBrief()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"As role publisher, I create all single placements from test data")]
        public void PublisherCreateAllSinglePlacementsFromTestData()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"As role publisher, I create all performance packages from test data")]
        public void PublisherCreateAllPerformancePackagesFromTestData()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"As role publisher, I create all sponsorship packages from test data")]
        public void PublisherCreateAllSponsorshipPackagesFromTestData()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I send all created items to the agency")]
        public void PublisherSendAllCreatedItemsToTheAgency()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
