using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Vendors;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Vendors.Popups
{
    public class VendorAddEditFrame : BasePage
    {
        private IWebElement _txtVendorName => FindElementById("ctl00_Content_txtVendorName");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _chkTaxOtherCosts => FindElementById("ctl00_Content_chkListTax_0");
        private IWebElement _lblMessagePanel => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
            
        private const string CHECKBOX_COMPONENT_XPATH = "//label[text() = '{0}']/..//input[@type = 'checkbox']";

        public VendorAddEditFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateVendor(VendorData vendor)
        {
            EnsureMandatoryValuesAreProvided(vendor);

            ClearInputAndTypeValue(_txtVendorName, vendor.Vendor);
            SelectWebformDropdownValueByText(_ddlCountry, vendor.Country);
            SetWebformCheckBoxState(_chkTaxOtherCosts, vendor.TaxOtherCosts);
            ConfigureCheckboxes(vendor.Categories);

            ClickElement(_btnSave);
        }

        public void EditVendor(VendorData vendor)
        {
            EnsureMandatoryValuesAreProvided(vendor);

            ClearInputAndTypeValue(_txtVendorName, vendor.Vendor);
            SetWebformCheckBoxState(_chkTaxOtherCosts, vendor.TaxOtherCosts);
            ConfigureCheckboxes(vendor.Categories);

            ClickElement(_btnSave);
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
        }

        private void ConfigureCheckboxes(IEnumerable<Checkbox> checkboxes)
        {
            if (checkboxes == null || !checkboxes.Any())
                return;

            foreach (var checkbox in checkboxes)
            {
                var checkboxElement = GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, checkbox.Name);
                SetWebformCheckBoxState(checkboxElement, checkbox.Enabled);
            }
        }

        private void EnsureMandatoryValuesAreProvided(VendorData vendor)
        {
            var vendorDataErrors = new StringBuilder();

            if (vendor == null)
            {
                vendorDataErrors.Append("\n- No vendor data present in the test data");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(vendor.Vendor))
                {
                    vendorDataErrors.Append("\n- Vendor Name not set");
                }

                if (string.IsNullOrWhiteSpace(vendor.Country))
                {
                    vendorDataErrors.Append("\n- Vendor Country not set");
                }

                if (vendor.Categories == null || !vendor.Categories.Any())
                {
                    vendorDataErrors.Append("\n- Vendor Categories not set. Must have at least one category");
                }
            }

            if (!string.IsNullOrEmpty(vendorDataErrors.ToString()))
                throw new ArgumentException($"The feature file {FeatureContext.FeatureInfo.Title} has the following data issues:\n {vendorDataErrors}");
        }
    }
}