using System;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Administrator.Details;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Details
{
    public class FeatureSettingsPage : BasePage
    {
        // Tab Navigation Links
        private IWebElement _lnkAgencyFeesTab => FindElementById("feature-settings-tabs-tab-1");
        private IWebElement _lnkCostAdjustmentsTab => FindElementByLinkText("Cost Adjustments");
        private IWebElement _lnkClassificationFiltersTab => FindElementByLinkText("Classification Filters");

        // Agency Fees Tab
        private IWebElement _ddlClientFilter => FindElementById("clientSelect");
        private IWebElement _ddlProductFilter => FindElementById("productSelect");
        private IWebElement _ddlCountryFilter => FindElementById("countrySelect");
        private IWebElement _ddlPublisherFilter => FindElementById("vendorSelect");
        private IWebElement _ddlSiteFilter => FindElementById("siteSelect");
        private IWebElement _ddlCostBreakdownTypes => FindElementById("costBreakDownTypes");
        private IWebElement _txtFeePercentage => FindElementById("feePercentageInput");

        private IWebElement _lnkResetFilters => FindElementByXPath("//button[text()='Reset Filters']");
        private IWebElement _btnRestoreDefault => FindElementById("restoreDefaultButton");
        private IWebElement _btnSave => FindElementById("saveButton");
        private IWebElement _lblAlert => FindElementByXPath("//div[@role='alert']");

        private IWebElement _chkAllowAgencyFeeRateCostOverride => FindElementByXPath("//input[@id='allowOverride']/../div[@class='checkbox-component-icon']");
        private IWebElement _chkAllowAgencyFeeBaseOverride => FindElementByXPath("//input[@id='chkisApplyOnAllowOverride']/../div[@class='checkbox-component-icon']");

        // Cost Adjustments Tab
        private IWebElement _btnAdd => FindElementByXPath("//button[@class='aui--button ms_grid_svg_button btn btn-default']");
        private IWebElement _btnEdit => FindElementByXPath("(//button[@class='aui--button ms_grid_svg_button btn btn-default'])[2]");
        private IWebElement _btnDelete => FindElementByXPath("(//button[@class='aui--button ms_grid_svg_button btn btn-default'])[3]");

        private IWebElement _grdCostAdjustmentsTable => FindElementByXPath("//div[@class='ag__group-body']");

        // Cost Adjustment Dialog modal elements
        private IWebElement _ddlClient => FindElementByName("clientSelect");
        private IWebElement _ddlCountry => FindElementByName("countrySelect");
        private IWebElement _ddlPublisher => FindElementByName("publisherSelect");
        private IWebElement _ddlAdjustment => FindElementByName("adjustmentSelect");
        private IWebElement _txtVendorRate => FindElementById("vendorRate");
        private IWebElement _chkAllowUserOverrideVendorRate => FindElementByXPath("//label[.='Vendor Rate']/..//div[@class='checkbox-component-icon']");
        private IWebElement _txtClientRate => FindElementById("clientRate");
        private IWebElement _chkAllowUserOverrideClientRate => FindElementByXPath("//label[.='Client Rate']/..//div[@class='checkbox-component-icon']");
        private IWebElement _btnDialogAdd => FindElementByXPath("//div[@role='dialog']//button[.='Add']");
        private IWebElement _btnDialogSave => FindElementByXPath("//div[@role='dialog']//button[.='Save']");
        private IWebElement _btnDialogCancel => FindElementByXPath("//div[@role='dialog']//button[.='Cancel']");
        private IWebElement _toastContainer => FindElementByCssSelector(".Toastify");

        // Classification Filters Tab
        private IWebElement _btnImport => FindElementByXPath("//button[(@data-testid='bootstrap-button-wrapper')]/div[contains(text(),'Import')]");
        private IWebElement _btnExport => FindElementByXPath("//button[(@data-testid='bootstrap-button-wrapper')]/div[contains(text(),'Export')]");

        public readonly string Default = "Default";

        public FeatureSettingsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SwitchToAgencyFeesTab()
        {
            ClickElement(_lnkAgencyFeesTab);
            WaitForElementToBeVisible(By.CssSelector(".creativeColumn"));
        }

        public void SetAgencyFeeDefaults(AgencyFeesData agencyFeesData)
        {
            if (agencyFeesData.DefaultFeesData == null || !agencyFeesData.DefaultFeesData.Any())
                return;

            foreach (var defaultFee in agencyFeesData.DefaultFeesData)
            {
                if (!defaultFee.Client.ToLower().Equals("all clients"))
                {
                    SelectSingleValueFromReactDropdownByText(_ddlClientFilter, defaultFee.Client);

                    if (!defaultFee.Product.ToLower().Equals("all products"))
                    {
                        SelectSingleValueFromReactDropdownByText(_ddlProductFilter, defaultFee.Product);
                    }
                    else if (!defaultFee.Country.ToLower().Equals("all countries"))
                    {
                        SelectSingleValueFromReactDropdownByText(_ddlCountryFilter, defaultFee.Country);

                        if (!defaultFee.Publisher.ToLower().Equals("all publishers"))
                        {
                            SelectSingleValueFromReactDropdownByText(_ddlPublisherFilter, defaultFee.Publisher);

                            if (!defaultFee.Product.ToLower().Equals("all sites"))
                            {
                                SelectSingleValueFromReactDropdownByText(_ddlSiteFilter, defaultFee.Site);
                            }
                        }
                    }
                }

                SelectSingleValueFromReactDropdownByText(_ddlCostBreakdownTypes, defaultFee.CostBreakdownType);
                ClearInputAndTypeValue(_txtFeePercentage, defaultFee.FeePercentage);
                ClickElement(_btnSave);
                Assert.AreEqual("All done! Your changes have been saved.", _lblAlert.Text);
                ClickElement(_lnkResetFilters);
            }

            SetReactCheckBoxState(_chkAllowAgencyFeeRateCostOverride, agencyFeesData.AllowAgencyFeeRateCostOverride);
            SetReactCheckBoxState(_chkAllowAgencyFeeBaseOverride, agencyFeesData.AllowAgencyFeeBaseOverride);
            ClickElement(_btnSave);
            Assert.AreEqual("All done! Your changes have been saved.", _lblAlert.Text);
        }

        public void SwitchToCostAdjustmentsTab()
        {
            ClickElement(_lnkCostAdjustmentsTab);
            WaitForElementToBeVisible(_grdCostAdjustmentsTable);
        }

        public void SwitchToClassificationFiltersTab()
        {
            ClickElement(_lnkClassificationFiltersTab);
            WaitForElementToBeVisible(_btnImport);
        }

        public void ClickImportButton()
        {
            ClickElement(_btnImport);
        }

        public void ExportClassificationFilter()
        {
            ClickElement(_btnExport);
            Assert.IsTrue(IsSuccessNotificationShownWithMessage("Classification filters have been exported"), "Success toast was not shown.");
        }

        public bool DoesCustomCostAdjustmentExist(CostAdjustmentsData costAdjustmentsData)
        {
            EnsureMandatoryValuesAreProvided(costAdjustmentsData);
            var rowXpath = BuildCostAdjustmentRowXpath(costAdjustmentsData);
            return IsElementPresent(By.XPath(rowXpath));
        }

        public void AddCustomCostAdjustmentDefaults(CostAdjustmentsData costAdjustmentsData)
        {
            ClickElement(_btnAdd);
            PopulateCostAdjustmentFields(costAdjustmentsData);
            ClickElement(_btnDialogAdd);
            Assert.IsTrue(IsSingleSuccessToastNotificationShown());
        }

        public void EditCostAdjustmentDefaults(CostAdjustmentsData costAdjustmentsData)
        {
            SelectCostAdjustmentRow(costAdjustmentsData);
            ClickElement(_btnEdit);
            PopulateCostAdjustmentFields(costAdjustmentsData);
            ClickElement(_btnDialogSave);
            Assert.IsTrue(IsSingleSuccessToastNotificationShown());
            DismissAllToastNotifications();
            UnselectCostAdjustmentRow(costAdjustmentsData);
        }

        private void PopulateCostAdjustmentFields(CostAdjustmentsData costAdjustmentsData)
        {
            if (!string.IsNullOrEmpty(costAdjustmentsData.Client) && costAdjustmentsData.Client != Default)
            {
                ClearInputAndTypeValue(_ddlClient, costAdjustmentsData.Client);
                ClearInputAndTypeValueIfRequired(_ddlCountry, costAdjustmentsData.Country);
                ClearInputAndTypeValueIfRequired(_ddlPublisher, costAdjustmentsData.Publisher);
                ClearInputAndTypeValue(_ddlAdjustment, costAdjustmentsData.Adjustment);
            }

            ClearInputAndTypeValueIfRequired(_txtVendorRate, costAdjustmentsData.VendorRate);
            SetReactCheckBoxState(_chkAllowUserOverrideVendorRate, costAdjustmentsData.AllowUserOverrideVendorRate);

            if (_txtClientRate.Enabled)
            {
                ClearInputAndTypeValueIfRequired(_txtClientRate, costAdjustmentsData.ClientRate);
                SetReactCheckBoxState(_chkAllowUserOverrideClientRate, costAdjustmentsData.AllowUserOverrideClientRate);
            }
        }

        private void SelectCostAdjustmentRow(CostAdjustmentsData costAdjustmentsData)
        {
            var rowCheckBox = GetCostAdjustmentRowCheckBox(costAdjustmentsData);
            SetReactCheckBoxState(rowCheckBox, true);
        }

        private void UnselectCostAdjustmentRow(CostAdjustmentsData costAdjustmentsData)
        {
            var rowCheckBox = GetCostAdjustmentRowCheckBox(costAdjustmentsData);
            SetReactCheckBoxState(rowCheckBox, false);
        }

        private string BuildCostAdjustmentRowXpath(CostAdjustmentsData costAdjustmentsData)
        {
            var dash = "—";
            var client = costAdjustmentsData.Client;
            var country = costAdjustmentsData.Country;
            var publisher = costAdjustmentsData.Publisher;

            if (client == Default)
            {
                client = country = publisher = dash;
            }

            return $"//div[contains(@class,'ag__row ag__row--is-body')][contains(.,'{client}') and contains(.,'{country}') and contains(.,'{publisher}') and contains(.,'{costAdjustmentsData.Adjustment}')]";
        }

        private IWebElement GetCostAdjustmentRowCheckBox(CostAdjustmentsData costAdjustmentsData)
        {
            var rowXpath = BuildCostAdjustmentRowXpath(costAdjustmentsData);
            var row = _grdCostAdjustmentsTable.FindElement(By.XPath(rowXpath));
            return row.FindElement(By.ClassName("checkbox-component-icon"));
        }

        private void EnsureMandatoryValuesAreProvided(CostAdjustmentsData costAdjustmentsData)
        {
            var dataErrorFound = false;
            var clientDataErrors = new StringBuilder();
            clientDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(costAdjustmentsData.Client))
            {
                dataErrorFound = true;
                clientDataErrors.Append("\n- Client was not set");
            }

            if (string.IsNullOrWhiteSpace(costAdjustmentsData.Adjustment))
            {
                dataErrorFound = true;
                clientDataErrors.Append("\n- Adjustment was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(clientDataErrors.ToString());
        }
    }
}
