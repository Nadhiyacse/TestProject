using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherFormatPage : BasePage
    {
        private IWebElement _ddlSelectScope => FindElementByXPath("//div[contains(@class,'format-admin-filter')]/div");
        private IWebElement _btnAdd => FindElementByXPath("//div[text()='Add']/ancestor::button");
        private IWebElement _btnEdit => FindElementByXPath("//div[text()='Edit']/ancestor::button");
        private IWebElement _btnDelete => FindElementByXPath("//div[text()='Delete']/ancestor::button");
        private IWebElement _btnMakeGlobal => FindElementByXPath("//div[text()='Make Global']/ancestor::button");
        private IWebElement _txtName => FindElementById("addEditFormats");
        private IWebElement _txtWidth => FindElementByXPath("(//input[contains(@class,'form-control')])[2]");
        private IWebElement _txtHeight => FindElementByXPath("(//input[contains(@class,'form-control')])[3]");
        private IWebElement _chkGlobalScope => FindElementById("isScoped");
        private IWebElement _ddlAgencyScope => FindElementByXPath("//label[text()='Agency']/../..//div[contains(@class,'select-component__control')]");
        private IWebElement _btnSave => FindElementByXPath("//div[text()='Save']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath("//div[text()='Cancel']/ancestor::button");
        private IWebElement _chkSelectAll => FindElementByXPath("//div[contains(@class, 'row--is-head')]//input");
        private IWebElement _lblAlertMessage => FindElementByXPath("//div[@role='alert']");
        private IWebElement _btnNext => FindElementByXPath(NEXT_BUTTON_XPATH);

        private const string DYNAMIC_FORMAT_XPATH = "//div[contains(@class, 'pane--is-free')]/div[2][text()='{0}' and ..//div[3][text()='{1}'] and ..//div[4][text()='{2}']]";
        private const string FORMAT_CHECKBOX = "//div[contains(@class, 'pane--is-free')]/div[2][text()='{0}' and ..//div[3][text()='{1}'] and ..//div[4][text()='{2}']]/..//label/div";
        private const string NEXT_BUTTON_XPATH = "//button/span[@aria-label='Next']";

        public PublisherFormatPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddFormat(FormatData formatData)
        {
            EnsureMandatoryValuesAreProvided(formatData);

            ClickElement(_btnAdd);

            ClearInputAndTypeValue(_txtName, formatData.Name);
            ClearInputAndTypeValue(_txtWidth, formatData.Width.ToString());
            ClearInputAndTypeValue(_txtHeight, formatData.Height.ToString());
            SetReactCheckBoxState(_chkGlobalScope, formatData.IsGlobalScope);

            if (!formatData.IsGlobalScope)
            {
                SelectSingleValueFromReactDropdownByText(_ddlAgencyScope, formatData.AgencyToScopeTo);
            }

            ClickElement(_btnSave);
        }

        public void DeleteFormat(FormatData formatData)
        {
            EnsureMandatoryValuesAreProvided(formatData);

            var formatCheckbox = FindElementByXPath(string.Format(FORMAT_CHECKBOX, formatData.Name, formatData.Width, formatData.Height));
            ClickElement(formatCheckbox);
            ClickElement(_btnDelete);
        }

        public void MakeFormatScopeGlobal(FormatData formatData)
        {
            EnsureMandatoryValuesAreProvided(formatData);

            var formatCheckbox = FindElementByXPath(string.Format(FORMAT_CHECKBOX, formatData.Name, formatData.Width, formatData.Height));
            ClickElement(formatCheckbox);
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

        public bool DoesFormatExist(FormatData formatData)
        {
            EnsureMandatoryValuesAreProvided(formatData);

            SelectSingleValueFromReactDropdownByText(_ddlSelectScope, formatData.IsGlobalScope ? "Global" : formatData.AgencyToScopeTo);

            var isFound = false;
            bool isNextButtonPresent;

            do
            {
                if (IsElementPresent(By.XPath(string.Format(DYNAMIC_FORMAT_XPATH, formatData.Name, formatData.Width, formatData.Height))))
                {
                    isFound = true;
                    break;
                }

                isNextButtonPresent = IsElementPresent(By.XPath(NEXT_BUTTON_XPATH));
                if (isNextButtonPresent)
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

        private void EnsureMandatoryValuesAreProvided(FormatData formatData)
        {
            var dataErrorFound = false;
            var formatDataErrors = new StringBuilder();
            formatDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(formatData.Name))
            {
                dataErrorFound = true;
                formatDataErrors.Append("\n- Name was not set");
            }

            if (formatData.Width == 0)
            {
                dataErrorFound = true;
                formatDataErrors.Append("\n- Width was not set");
            }

            if (formatData.Height == 0)
            {
                dataErrorFound = true;
                formatDataErrors.Append("\n- Height was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(formatDataErrors.ToString());
        }
    }
}
