using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherLocationPage : BasePage
    {
        private IWebElement _ddlSelectSite => FindElementByXPath("(//div[contains(@class,'location-admin-filter')]/div)[1]");
        private IWebElement _ddlSelectScope => FindElementByXPath("(//div[contains(@class,'location-admin-filter')]/div)[2]");
        private IWebElement _btnAdd => FindElementByXPath("//div[text()='Add']/ancestor::button");
        private IWebElement _btnEdit => FindElementByXPath("//div[text()='Edit']/ancestor::button");
        private IWebElement _btnDelete => FindElementByXPath("//div[text()='Delete']/ancestor::button");
        private IWebElement _btnMakeGlobal => FindElementByXPath("//div[text()='Make Global']/ancestor::button");
        private IWebElement _ddlSite => FindElementByXPath("//label[text()='Site']/../..//div[contains(@class,'select-component__control')]");
        private IWebElement _txtName => FindElementById("addEditLocations");
        private IWebElement _chkGlobalScope => FindElementById("isScoped");
        private IWebElement _ddlAgencyScope => FindElementByXPath("//label[text()='Agency']/../..//div[contains(@class,'select-component__control')]");
        private IWebElement _btnSave => FindElementByXPath("//div[text()='Save']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath("//div[text()='Cancel']/ancestor::button");
        private IWebElement _chkSelectAll => FindElementByXPath("//div[contains(@class, 'row--is-head')]//input");
        private IWebElement _lblAlertMessage => FindElementByXPath("//div[@role='alert']");
        private IWebElement _btnNext => FindElementByXPath("//span[@aria-label = 'Next']");

        private const string DYNAMIC_LOCATION_XPATH = "//div[contains(@class, 'row--is-body')]//div[text() = '{0}']";
        private const string LOCATION_CHECKBOX = "//div[contains(@class, 'row--is-body')]//div[text() = '{0}']/..//input";

        public PublisherLocationPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddLocation(LocationData locationData)
        {
            EnsureMandatoryValuesAreProvided(locationData);

            ClickElement(_btnAdd);

            SelectSingleValueFromReactDropdownByText(_ddlSite, locationData.Site);
            ClearInputAndTypeValue(_txtName, locationData.Name);
            SetReactCheckBoxState(_chkGlobalScope, locationData.IsGlobalScope);

            if (!locationData.IsGlobalScope)
            {
                SelectSingleValueFromReactDropdownByText(_ddlAgencyScope, locationData.AgencyToScopeTo);
            }

            ClickElement(_btnSave);
        }

        public void EditLocationName(string locationName)
        {
            var locationCheckbox = FindElementByXPath(string.Format(LOCATION_CHECKBOX, locationName));
            ClickElement(locationCheckbox);
            ClickElement(_btnEdit);
            ClearInputAndTypeValue(_txtName, locationName);
            ClickElement(_btnSave);
        }

        public void DeleteLocation(string locationName)
        {
            var locationCheckbox = FindElementByXPath(string.Format(LOCATION_CHECKBOX, locationName));
            ClickElement(locationCheckbox);
            ClickElement(_btnDelete);
        }

        public void MakeLocationScopeGlobal(string locationName)
        {
            var locationCheckbox = FindElementByXPath(string.Format(LOCATION_CHECKBOX, locationName));
            ClickElement(locationCheckbox);
            ClickElement(_btnMakeGlobal);
        }

        public void DeleteAllLocations()
        {
            SetReactCheckBoxState(_chkSelectAll, true);
            ClickElement(_btnDelete);
        }

        public void MakeAllLocationsScopeGlobal()
        {
            SetReactCheckBoxState(_chkSelectAll, true);
            ClickElement(_btnMakeGlobal);
        }

        public bool DoesLocationExist(LocationData locationData)
        {
            EnsureMandatoryValuesAreProvided(locationData);

            SelectSingleValueFromReactDropdownByText(_ddlSelectSite, locationData.Site);
            SelectSingleValueFromReactDropdownByText(_ddlSelectScope, locationData.IsGlobalScope ? "Global" : locationData.AgencyToScopeTo);


            bool isFound = false;
            bool isNextButtonPresent;

            do
            {
                if (IsElementPresent(By.XPath(string.Format(DYNAMIC_LOCATION_XPATH, locationData.Name))))
                {
                    isFound = true;
                    break;
                }

                isNextButtonPresent = IsElementPresent(By.XPath("//span[@aria-label = 'Next']"));
                if (isNextButtonPresent && _btnNext.Enabled)
                {
                    ClickElement(_btnNext);
                }
            }
            while (!isFound && isNextButtonPresent);

            return isFound;
        }

        public string GetAlertMessage()
        {
            Wait.Until(driver => _lblAlertMessage.Displayed);
            return _lblAlertMessage.Text;
        }

        private void EnsureMandatoryValuesAreProvided(LocationData locationData)
        {
            var dataErrorFound = false;
            var locationDataErrors = new StringBuilder();
            locationDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(locationData.Site))
            {
                dataErrorFound = true;
                locationDataErrors.Append("\n- Site was not set");
            }

            if (string.IsNullOrWhiteSpace(locationData.Name))
            {
                dataErrorFound = true;
                locationDataErrors.Append("\n- Name was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(locationDataErrors.ToString());
        }
    }
}
