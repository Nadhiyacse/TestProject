using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Custom_Fields;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyCustomFieldPage : BasePage
    {
        private IWebElement _btnAdd => FindElementByXPath("//button//div[contains(text(), 'Add')]");
        private IWebElement _btnEdit => FindElementByXPath("//button//div[contains(text(), 'Edit')]");
        private IWebElement _btnDelete => FindElementByXPath("//button//div[contains(text(), 'Delete')]");
        private IWebElement _ddlApplyTo => FindElementByXPath("//div[@class='applyTo-field row']//div[contains(@class,'select-component__control')]");
        private IWebElement _ddlName => FindElementByXPath("//div[@class='name-field row']//div[contains(@class,'select-component__control')]");
        private IWebElement _txtDisplayName => FindElementByXPath("//div[@class='display-name-field row']//input");
        private IWebElement _txtCharacterLimit => FindElementByXPath("//div[@class='character-limit-field row']//input");
        private IWebElement _txtDefaultValue => FindElementByXPath("//div[@class='default-text-field row']//input");
        private IWebElement _chkMandatory => FindElementByXPath("//div[@class='mandatory-field row']//div[@class='checkbox-component-icon']");
        private IWebElement _btnSave => FindElementByXPath("//button//div[contains(text(), 'Save')]");
        private IWebElement _btnAddMultiSelectOption => FindElementByXPath("//button[@class='aui--button btn-add btn-inverse btn btn-xs btn-default']");
        private IWebElement _lblCustomFieldType => FindElementByXPath("//label[@class='add-custom-field-type']");
        private IWebElement _txtWidth => FindElementByXPath("//label[@id ='labelResolutionWidth']/../input");
        private IWebElement _txtHeight => FindElementByXPath("//label[@id ='labelResolutionHeight']/../input");

        private const string DYNAMIC_CUSTOM_FIELD_XPATH = "//div[@class='grid-component-cell grid-component-cell-4-5 grid-component-cell-body grid-component-cell-site-cell'][contains(text(), '{0}')]";
        private const string CUSTOM_FIELD_CHECKBOX = "//div[@class='grid-component-cell grid-component-cell-4-5 grid-component-cell-body grid-component-cell-site-cell'][contains(text(), '{0}')]//..//div[@class='checkbox-component-icon']";
        private const string MULTI_SELECT_OPTION_ROW_XPATH = "//div[@class='grid-component-row grid-component-row-body grid-component-row-horizontal-border grid-component-row-vertical-cell-border']";

        public AgencyCustomFieldPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddCustomField(CustomFieldData customField)
        {
            EnsureMandatoryValuesAreProvided(customField);

            ClickElement(_btnAdd);
            SelectSingleValueFromReactDropdownByText(_ddlApplyTo, customField.ApplyTo.ToString());
            SelectSingleValueFromReactDropdownByText(_ddlName, customField.Name);
            ClearInputAndTypeValue(_txtDisplayName, customField.DisplayName);
            PopulateDynamicFieldsBasedOnCustomFieldType(customField);
            ClickElement(_btnSave);
            WaitForElementToBeInvisible(By.XPath("//div[@class='modal-sm modal-dialog']"));
        }

        public void EditCustomField(CustomFieldData customField)
        {
            EnsureMandatoryValuesAreProvided(customField);

            var customFieldCheckbox = FindElementByXPath(string.Format(CUSTOM_FIELD_CHECKBOX, customField.Name));
            ClickElement(customFieldCheckbox);
            ClickElement(_btnEdit);
            ClearInputAndTypeValue(_txtDisplayName, customField.DisplayName);
            PopulateDynamicFieldsBasedOnCustomFieldType(customField);
            ClickElement(_btnSave);
            WaitForElementToBeInvisible(By.XPath("//div[@class='modal-sm modal-dialog']"));
        }

        public void DeleteCustomField(CustomFieldData customField)
        {
            var customFieldCheckbox = FindElementByXPath(string.Format(CUSTOM_FIELD_CHECKBOX, customField.Name));
            ClickElement(customFieldCheckbox);
            ClickElement(_btnDelete);
        }

        public bool DoesCustomFieldExist(CustomFieldData customField)
        {
            return IsElementPresent(By.XPath(string.Format(DYNAMIC_CUSTOM_FIELD_XPATH, customField.Name)));
        }

        private void EnsureMandatoryValuesAreProvided(CustomFieldData customFieldData)
        {
            var dataErrorFound = false;
            var customFieldDataErrors = new StringBuilder();
            customFieldDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");
            if (string.IsNullOrWhiteSpace(customFieldData.ApplyTo.ToString()))
            {
                dataErrorFound = true;
                customFieldDataErrors.Append("\n- ApplyTo was not set");
            }

            if (string.IsNullOrWhiteSpace(customFieldData.Name))
            {
                dataErrorFound = true;
                customFieldDataErrors.Append("\n- Name was not set");
            }

            if (string.IsNullOrWhiteSpace(customFieldData.DisplayName))
            {
                dataErrorFound = true;
                customFieldDataErrors.Append("\n- Display Name was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(customFieldDataErrors.ToString());
        }

        private void AddMultiSelectOptions(List<MultiSelectOptions> multiSelectOptions)
        {
            if (multiSelectOptions == null || !multiSelectOptions.Any())
                throw new ArgumentNullException("MultiSelect options were not set");

            var optionRowIndex = 1;
            foreach (var option in multiSelectOptions)
            {
                ClickElement(_btnAddMultiSelectOption);
                var optionRowDisplayName = FindElementByXPath($"(({MULTI_SELECT_OPTION_ROW_XPATH})[{optionRowIndex}]//input)[1]");
                var optionRowValue = FindElementByXPath($"(({MULTI_SELECT_OPTION_ROW_XPATH})[{optionRowIndex}]//input)[2]");
                ClearInputAndTypeValue(optionRowDisplayName, option.DisplayName);
                ClearInputAndTypeValue(optionRowValue, option.Value);
                optionRowIndex++;
            }
        }

        private void DeleteAllMultiSelectOptions()
        {
            var optionRows = FindElementsByXPath(MULTI_SELECT_OPTION_ROW_XPATH).Reverse();

            if (optionRows.Count() == 0)
                return;

            foreach (var optionRow in optionRows)
            {
                var deleteButton = optionRow.FindElement(By.XPath("//div[@class='removeItem']//button"));
                ClickElement(deleteButton);
            }
        }

        private string GetCustomFieldType()
        {
            return _lblCustomFieldType.Text;
        }

        private void PopulateDynamicFieldsBasedOnCustomFieldType(CustomFieldData customField)
        {
            var type = GetCustomFieldType();
            switch (type)
            {
                case "Text":
                case "Multiline":
                    if (customField.CharacterLimit == 0)
                        throw new ArgumentException("Character limit cannot be 0");
                    if (_txtCharacterLimit.Enabled)
                    {
                        ClearInputAndTypeValue(_txtCharacterLimit, customField.CharacterLimit.ToString());
                    }
                    if (_txtDefaultValue.Enabled)
                    {
                        ClearInputAndTypeValueIfRequired(_txtDefaultValue, customField.DefaultText);
                    }
                    if (!customField.ApplyTo.ToString().Equals("Client"))
                    {
                        SetReactCheckBoxState(_chkMandatory, customField.Mandatory);
                    }
                    break;

                case "MultiSelect":
                    DeleteAllMultiSelectOptions();
                    AddMultiSelectOptions(customField.MultiSelectOptions);
                    if (!customField.ApplyTo.ToString().Equals("Client"))
                    {
                        SetReactCheckBoxState(_chkMandatory, customField.Mandatory);
                    }
                    break;

                case "Image":
                    if (customField.Resolution != null)
                    {
                        ClearInputAndTypeValueIfRequired(_txtWidth, customField.Resolution.Width);
                        ClearInputAndTypeValueIfRequired(_txtHeight, customField.Resolution.Height);
                    }
                    break;

                case "DatePicker":
                    if (!customField.ApplyTo.ToString().Equals("Client"))
                    {
                        SetReactCheckBoxState(_chkMandatory, customField.Mandatory);
                    }
                    break;

                default:
                    throw new NotSupportedException($"Custom field type: '{type}' not supported.");
            }
        }
    }
}