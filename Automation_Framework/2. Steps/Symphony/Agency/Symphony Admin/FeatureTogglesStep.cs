using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Feature_Toggles;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Symphony_Admin
{
    [Binding]
    public class FeatureTogglesStep : BaseStep
    {
        private readonly FeatureTogglesPage _featureTogglesPage;

        public FeatureTogglesStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _featureTogglesPage = new FeatureTogglesPage(driver, featureContext);
        }

        [Given(@"I configure my feature toggles")]
        public void ConfigureFeatureToggles()
        {
            _featureTogglesPage.DisableAllFeatureToggles();
            _featureTogglesPage.ConfigureFeatureToggles();
        }
    }
}