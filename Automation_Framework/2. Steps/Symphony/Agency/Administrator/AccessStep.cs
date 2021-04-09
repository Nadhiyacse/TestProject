using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Access;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class AccessStep : BaseStep
    {
        private readonly AccessPage _accessPage;

        public AccessStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _accessPage = new AccessPage(driver, featureContext);
        }

        [Given(@"I configure my agencies access control as Administrator")]
        public void ConfigureAgencyAdministratorAccess()
        {
            _accessPage.ConfigureAccess(AgencySetupData.AgencyAdministratorData.Access);
        }
    }
}
