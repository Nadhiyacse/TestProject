using Automation_Framework._3._Pages.Symphony.Agency.Campaigns;
using Automation_Framework._3._Pages.Symphony.Agency.Overview;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Details;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview
{
    [Binding]
    public class AgencyOverviewStep : BaseStep
    {
        private readonly AddEditCampaignModal _addEditCampaignPage;
        private readonly DetailsPage _detailsPage;
        private readonly CampaignsGridPage _campaignsGridPage;

        public AgencyOverviewStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _addEditCampaignPage = new AddEditCampaignModal(driver, featureContext);
            _detailsPage = new DetailsPage(driver, featureContext);
            _campaignsGridPage = new CampaignsGridPage(driver, featureContext);
        }

        [Given(@"I create a campaign")]
        [When(@"I create a campaign")]
        public void CreateCampaign()
        {
            _campaignsGridPage.ClickCreateCampaignButton();
            _addEditCampaignPage.CreateCampaign(WorkflowTestData.CampaignData);
            FeatureContext[ContextStrings.CampaignId] = _detailsPage.GetCampaignId();
        }

        [Given(@"I select my campaign")]
        [When(@"I select my campaign")]
        public void SelectCampaign()
        {
            _campaignsGridPage.SearchAndSelectCampaign(WorkflowTestData.CampaignData.DetailsData.CampaignName);
        }

        [Then(@"I received alert that proposal sent to me from the publisher")]
        public void ReceivedAlertThatProposalSentToMeFromThePublisher()
        {
            ScenarioContext.Current.Pending();
        }

        [When(@"I click on the alert link")]
        public void ClickOnTheAlertLink()
        {
            ScenarioContext.Current.Pending();
        }
    }
}