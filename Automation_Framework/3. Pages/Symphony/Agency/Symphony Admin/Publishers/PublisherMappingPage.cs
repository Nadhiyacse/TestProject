using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherMappingPage : BasePage
    {
        private IWebElement _ddlSelectSite => FindElementByXPath("(//div[contains(@class,'mapping-admin-filter')]/div)[1]");
        private IWebElement _ddlSelectScope => FindElementByXPath("(//div[contains(@class,'mapping-admin-filter')]/div)[2]");
        private IWebElement _btnAdd => FindElementByXPath("//div[text()='Add']/ancestor::button");
        private IWebElement _btnDelete => FindElementByXPath("//div[text()='Delete']/ancestor::button");
        private IWebElement _btnMakeGlobal => FindElementByXPath("//div[text()='Make Global']/ancestor::button");
        private IWebElement _ddlSite => FindElementById("SiteSelect");
        private IWebElement _ddlScope => FindElementById("ScopeSelect");
        private IWebElement _ddlLocation => FindElementById("LocationSelect");
        private IWebElement _ddlFormat => FindElementById("FormatSelect");
        private IWebElement _btnSave => FindElementByXPath("//div[text()='Save']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath("//div[text()='Cancel']/ancestor::button");
        private IWebElement _chkSelectAll => FindElementByXPath("//div[contains(@class, 'row--is-head')]//input");
        private IWebElement _lblAlertMessage => FindElementByXPath("//div[@role='alert']");

        private const string DYNAMIC_MAPPING_XPATH = "//div[contains(@class, 'pane--is-free')]/div[2][text()='{0}' and ..//div[3][text()='{1}']]";
        private const string MAPPING_CHECKBOX = "//div[contains(@class, 'pane--is-free')]/div[2][text()='{0}' and ..//div[3][text()='{1}']]/..//input";

        public PublisherMappingPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddMapping(LocationFormatMappingData mappingData)
        {
            EnsureMandatoryValuesAreProvided(mappingData);
            ClickElement(_btnAdd);
            SelectSingleValueFromReactDropdownByText(_ddlSite, mappingData.Site);
            SelectSingleValueFromReactDropdownByText(_ddlScope, mappingData.Scope);

            var location = $"{mappingData.Location} ({mappingData.Scope})";
            var format = $"{mappingData.Format} ({mappingData.Scope})";
            SelectSingleValueFromReactDropdownByText(_ddlLocation, location);
            SelectSingleValueFromReactDropdownByText(_ddlFormat, format);

            ClickElement(_btnSave);
        }

        public void DeleteMapping(LocationFormatMappingData mappingData)
        {
            EnsureMandatoryValuesAreProvided(mappingData);

            var mappingCheckbox = FindElementByXPath(string.Format(MAPPING_CHECKBOX, mappingData.Location, mappingData.Format));
            ClickElement(mappingCheckbox);
            ClickElement(_btnDelete);
        }

        public void MakeMappingScopeGlobal(LocationFormatMappingData mappingData)
        {
            EnsureMandatoryValuesAreProvided(mappingData);

            var mappingCheckbox = FindElementByXPath(string.Format(MAPPING_CHECKBOX, mappingData.Location, mappingData.Format));
            ClickElement(mappingCheckbox);
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

        public bool DoesMappingExist(LocationFormatMappingData mappingData)
        {
            EnsureMandatoryValuesAreProvided(mappingData);

            SelectSingleValueFromReactDropdownByText(_ddlSelectSite, mappingData.Site);
            SelectSingleValueFromReactDropdownByText(_ddlSelectScope, mappingData.Scope);

            return IsElementPresent(By.XPath(string.Format(DYNAMIC_MAPPING_XPATH, mappingData.Location, mappingData.Format)));
        }

        public string GetAlertMessage()
        {
            Wait.Until(driver => _lblAlertMessage.Displayed);
            return _lblAlertMessage.Text;
        }

        private void EnsureMandatoryValuesAreProvided(LocationFormatMappingData mappingData)
        {
            var dataErrorFound = false;
            var locationFormatMappingDataErrors = new StringBuilder();
            locationFormatMappingDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(mappingData.Site))
            {
                dataErrorFound = true;
                locationFormatMappingDataErrors.Append("\n- Site was not set");
            }

            if (string.IsNullOrWhiteSpace(mappingData.Scope))
            {
                dataErrorFound = true;
                locationFormatMappingDataErrors.Append("\n- Scope was not set");
            }

            if (string.IsNullOrWhiteSpace(mappingData.Location))
            {
                dataErrorFound = true;
                locationFormatMappingDataErrors.Append("\n- Location was not set");
            }

            if (string.IsNullOrWhiteSpace(mappingData.Format))
            {
                dataErrorFound = true;
                locationFormatMappingDataErrors.Append("\n- Format was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(locationFormatMappingDataErrors.ToString());
        }
    }
}
