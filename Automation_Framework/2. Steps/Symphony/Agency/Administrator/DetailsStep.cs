using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Details;
using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Details.Popups;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class DetailsStep : BaseStep
    {
        private readonly FeatureSettingsPage _featureSettingsPage;
        private readonly ImportClassificationFiltersPopup _importClassificationFilterPopup;

        public DetailsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _featureSettingsPage = new FeatureSettingsPage(driver, featureContext);
            _importClassificationFilterPopup = new ImportClassificationFiltersPopup(driver, featureContext);
        }

        [Given(@"I configure my default agency fees")]
        public void ConfigureDefaultAgencyFees()
        {
            NavigationPage.NavigateTo("Administrator Details Feature Settings");

            var featureSettingsData = AgencySetupData.AgencyAdministratorData.FeatureSettingsData;
            if (featureSettingsData == null || featureSettingsData.AgencyFeesData == null)
                return;

            _featureSettingsPage.SetAgencyFeeDefaults(featureSettingsData.AgencyFeesData);
        }

        [Given(@"I configure my agency default cost adjustments")]
        public void ConfigureDefaultCostAdjustments()
        {
            NavigationPage.NavigateTo("Administrator Details Feature Settings");
            _featureSettingsPage.SwitchToCostAdjustmentsTab();

            var featureSettingsData = AgencySetupData.AgencyAdministratorData.FeatureSettingsData;
            if (featureSettingsData == null || featureSettingsData.CostAdjustmentsData == null || !featureSettingsData.CostAdjustmentsData.Any())
                return;

            foreach (var costAdjustment in featureSettingsData.CostAdjustmentsData)
            {
                if (_featureSettingsPage.DoesCustomCostAdjustmentExist(costAdjustment) || costAdjustment.Client == _featureSettingsPage.Default)
                {
                    _featureSettingsPage.EditCostAdjustmentDefaults(costAdjustment);
                }
                else
                {
                    _featureSettingsPage.AddCustomCostAdjustmentDefaults(costAdjustment);
                }
            }
        }

        [Given(@"I navigate to Classification Filter tab")]
        public void GivenINavigateToClassificationFilterTab()
        {
            NavigationPage.NavigateTo("Administrator Details Feature Settings");
            _featureSettingsPage.SwitchToClassificationFiltersTab();
        }

        [Given(@"I import my classification filters")]
        public void GivenIImportMyClassificationFilters()
        {
            _featureSettingsPage.ClickImportButton();
            _importClassificationFilterPopup.ImportClassificationFilters();      
        }

        [Given(@"I export my classification filters")]
        public void GivenIExportMyClassificationFilters()
        {
            _featureSettingsPage.ExportClassificationFilter();
        }
    }
}
