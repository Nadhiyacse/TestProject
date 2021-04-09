using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class BriefsAndProposalsStep : BaseStep
    {
        public BriefsAndProposalsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        [When(@"I create brief from test data")]
        public void CreateBriefFromTestData()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I send brief to publisher")]
        public void SendBriefToPublisher()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I click on publisher link")]
        public void ClickOnPublisherLink()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I import all proposed cost items")]
        public void ImportAllProposedCostItems()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I mark rating for these cost items")]
        public void MarkRatingForTheseCostItems()
        {
            ScenarioContext.Current.Pending();
        }
    }
}
