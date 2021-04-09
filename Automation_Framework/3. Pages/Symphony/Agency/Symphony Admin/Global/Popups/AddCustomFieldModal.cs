using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Global;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Global.Popups
{
    public class AddCustomFieldModal : BasePage
    {
        private IWebElement _ddlApplyTo => FindElementByXPath("//div[@class='applyTo-field row']//div[contains(@class,'select-component__control')]");
        private IWebElement _ddlType => FindElementByXPath("//div[@class='name-field row']//div[contains(@class,'select-component__control')]");
        private IWebElement _txtName => FindElementByXPath("//div[@class='name-field row']//div[contains(@class,'select-component__input')]//input");
        private IWebElement _btnSave => FindElementByXPath("//button//div[contains(text(), 'Save')]");

        public AddCustomFieldModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateCustomField(GlobalCustomFieldData globalCustomField)
        {
            EnsureMandatoryValuesAreProvided(globalCustomField);

            SelectSingleValueFromReactDropdownByText(_ddlApplyTo, globalCustomField.ApplyTo.ToString());
            SelectSingleValueFromReactDropdownByText(_ddlType, GetCustomFieldTypeDisplayName(globalCustomField.Type));
            ClearInputAndTypeValue(_txtName, globalCustomField.Name);
            ClickElement(_btnSave);
        }

        private void EnsureMandatoryValuesAreProvided(GlobalCustomFieldData customField)
        {
            var customFieldDataErrors = new StringBuilder();

            if (customField == null)
            {
                customFieldDataErrors.Append("\n- No custom field data present in the test data");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(customField.ApplyTo.ToString()))
                {
                    customFieldDataErrors.Append("\n- Custom Field ApplyTo not set");
                }

                if (string.IsNullOrWhiteSpace(customField.Type.ToString()))
                {
                    customFieldDataErrors.Append("\n- Custom Field Type not set");
                }

                if (string.IsNullOrWhiteSpace(customField.Name))
                {
                    customFieldDataErrors.Append("\n- Custom Field Name not set");
                }
            }

            if (!string.IsNullOrEmpty(customFieldDataErrors.ToString()))
                throw new ArgumentException($"The feature file {FeatureContext.FeatureInfo.Title} has the following data issues:\n {customFieldDataErrors}");
        }

        private string GetCustomFieldTypeDisplayName(CustomFieldType customFieldType)
        {
            switch (customFieldType)
            {
                case CustomFieldType.MultiSelect:
                    return "Multi Select";
                case CustomFieldType.Text:
                    return "Text";
                case CustomFieldType.MultiLine:
                    return "Multi Line";
                case CustomFieldType.Image:
                    return "Image";
                case CustomFieldType.DatePicker:
                    return "Date Picker";
                default:
                    throw new ArgumentOutOfRangeException(nameof(customFieldType), customFieldType, null);
            }
        }
    }
}