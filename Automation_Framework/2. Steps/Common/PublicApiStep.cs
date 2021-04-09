using Automation_Framework.PublicApi.Scenarios;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Common
{
    [Binding]
    public class PublicApiStep : BaseStep
    {
        private readonly AuthenticationScenario authenticationScenario;
        private readonly CampaignScenario campaignScenario;

        public PublicApiStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            authenticationScenario = new AuthenticationScenario();
            campaignScenario = new CampaignScenario();
        }

        [Given(@"I create (\d+) campaigns via Public API if they don't exist")]
        [When(@"I create (\d+) campaigns via Public API if they don't exist")]
        public void CreateCampaigns(int numberOfCampaigns)
        {
            var token = authenticationScenario.GenerateAccessToken(WorkflowTestData.AgencyLoginUserData);
            var existingCampaignNames = campaignScenario.GetCampaignNames(token);

            if (existingCampaignNames.Count < numberOfCampaigns)
            {
                for (int i = 0; i < (numberOfCampaigns - existingCampaignNames.Count); i++)
                {
                    campaignScenario.CreateCampaign(WorkflowTestData.CampaignData, WorkflowTestData.AgencyLoginUserData, token);
                }
            }
        }
    }
}
