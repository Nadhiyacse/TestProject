using Automation_Framework._3._Pages.Symphony.Agency.Administrator.IOAdmin;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class IOAdminStep : BaseStep
    {
        private readonly IOSettingsPage _iosettingsPage;

        public IOAdminStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _iosettingsPage = new IOSettingsPage(driver, featureContext);
        }

        [Given(@"I configure my IO Settings")]
        public void GivenIConfigureMyIOSettings()
        {
            _iosettingsPage.PopulateDefaultIOSettings(AgencySetupData.AgencyAdministratorData.IOAdminData);
        }
    }
}
