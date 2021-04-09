using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Access;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyAccessPage : BasePage
    {
        private IWebElement _lblMessagePanel => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");
        private IWebElement _ddlCostModel => FindElementById("ctl00_ctl00_Content_Content_ddlCostModels");
        private IWebElement _ddlPlacementNamingConvention => FindElementById("ctl00_ctl00_Content_Content_ddlPlacementNamingConventions");
        private IWebElement _ddlPackageNamingConvention => FindElementById("ctl00_ctl00_Content_Content_ddlPackageNamingConvention");
        private IWebElement _ddlCampaignNamingConvention => FindElementById("ctl00_ctl00_Content_Content_ddlCampaignNamingConvention");
        private IWebElement _ddlInventoryProvider => FindElementById("ctl00_ctl00_Content_Content_rpInventoryProviders_ctl01_ddlInventoryProviders");
        private IWebElement _ddlBillingMethod => FindElementById("ctl00_ctl00_Content_Content_rpInventoryProviders_ctl01_ddlBillingMethod");
        private IWebElement _lnkNew => FindElementById("ctl00_ctl00_Content_Content_rpInventoryProviders_ctl01_lnkNewUpload");
        private IWebElement _flTermsAndConditions => FindElementById("ctl00_ctl00_Content_Content_rpInventoryProviders_ctl01_flTermsAndConditionsUpload");
        private IWebElement _ddlExportType => FindElementById("ctl00_ctl00_Content_Content_ddlExportOptionsExportType");
        private IWebElement _ddlFacebookCampaignNameConvention => FindElementById("ctl00_ctl00_Content_Content_rpAdServers_ctl07_ddlCampaignNameConvention");
        private IWebElement _ddlGoogleAdWordsCampaignNameConvention => FindElementById("ctl00_ctl00_Content_Content_rpAdServers_ctl08_ddlCampaignNameConvention");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");
        private IWebElement _btnCancel => FindElementById("ctl00_ctl00_Content_Content_btnCancel");

        private const string CHECKBOX_COMPONENT_XPATH = "//h2[contains(text(), '{0}')]/..//table//tbody//tr//td//label[text() = '{1}']/../..//input[@type = 'checkbox']";
        private const string RADIO_COMPONENT_XPATH = "//h2[contains(text(), '{0}')]/..//table//tbody//tr//td//label[text() = '{1}']/../..//input[@type = 'radio']";

        public AgencyAccessPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }

        public void ConfigureAgencyAccessControl(AgencyAccessData agencyAccessData)
        {
            if (agencyAccessData == null)
                return;

            SelectWebformDropdownValueIfRequired(_ddlCostModel, agencyAccessData.CostModel);
            SelectWebformDropdownValueIfRequired(_ddlPlacementNamingConvention, agencyAccessData.PlacementNamingConvention);
            SelectWebformDropdownValueIfRequired(_ddlPackageNamingConvention, agencyAccessData.PackageNamingConvention);
            SelectWebformDropdownValueIfRequired(_ddlCampaignNamingConvention, agencyAccessData.CampaignNamingConvention);
            ConfigureAccessControlledItems(agencyAccessData.MediaScheduleExports, "Media Schedule Exports");
            ConfigureAccessControlledItems(agencyAccessData.InsertionOrderExports, "Insertion Order Exports");
            ConfigureAccessControlledItems(agencyAccessData.CostModelImports, "Cost Model Imports");
            ConfigureAccessControlledItems(agencyAccessData.BillingExports, "Billing Exports");
            ConfigureAccessControlledItems(agencyAccessData.PublisherDetailExports, "Publisher Detail Exports");
            ConfigureAccessControlledItems(agencyAccessData.DataMapping, "Data Mapping");
            ConfigureThirdPartyAdServers(agencyAccessData.ThirdPartyAdServers);
            ConfigureAccessControlledItems(agencyAccessData.FourthPartyAdServers, "4th Party Tracking");
            ConfigureAccessControlledItems(agencyAccessData.Languages, "Languages");
            ConfigureAccessControlledItems(agencyAccessData.ExternalCredentials, "External Credentials");
            ConfigureAccessControlledItems(agencyAccessData.MediaScheduleViews, "Media Schedule Views");
            ConfigureAccessControlledItems(agencyAccessData.CampaignIdStrategies, "Campaign Id Strategies");
            ConfigureInventoryProviders(agencyAccessData.InventoryProviders);
            ConfigureCheckboxes(agencyAccessData.CostItemAdjustments, "Cost Item Adjustments");
            ConfigureCheckboxes(agencyAccessData.PurchaseTypes, "Purchase Types");
            ConfigureScheduleBudgetIncludes(agencyAccessData.ScheduleBudgetIncludes);
            ConfigureCheckboxes(agencyAccessData.CreativeTypes, "Creative Types");
            ConfigureAccessControlledItems(agencyAccessData.OtherCostCategories, "Other Costs Categories");
            ConfigureCheckboxes(agencyAccessData.MandatoryClassifications, "Mandatory Classifications");
            ConfigureCheckboxes(agencyAccessData.HideStandardImportTemplateColumns, "Hide Standard Import Template Columns");
            ConfigureMediaScheduleExportOptions(agencyAccessData.MediaScheduleExportOptions);
            ConfigureAccessControlledItems(agencyAccessData.BillingAllocations, "Billing Allocations");

            ClickElement(_btnSave);
        }

        private void ConfigureAccessControlledItems(IEnumerable<AccessControlledItem> accessControlledItems, string sectionHeaderName)
        {
            if (accessControlledItems == null || !accessControlledItems.Any())
                return;

            foreach (var accessControlledItem in accessControlledItems)
            {
                var accessControlItemName = accessControlledItem.AccessItem.Name;
                if (accessControlItemName.Equals("GroupM (BE) Technical Specifications Export") && !IsFeatureToggleEnabled(FeatureToggle.BEProdScheduleChanges))
                    continue;

                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, accessControlItemName, CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, accessControlledItem.AccessItem.Enabled);

                if (accessControlledItem.IsDefault)
                {
                    var radioButtonElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, accessControlItemName, RADIO_COMPONENT_XPATH);
                    ScrollAndClickElement(radioButtonElement);
                }
            }
        }

        private void ConfigureCheckboxes(IEnumerable<Checkbox> checkboxes, string sectionHeaderName)
        {
            if (checkboxes == null || !checkboxes.Any())
                return;

            foreach (var checkbox in checkboxes)
            {
                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, checkbox.Name, CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, checkbox.Enabled);
            }
        }

        private void ConfigureThirdPartyAdServers(IEnumerable<AccessControlledItem> accessControlledItems)
        {
            ConfigureAccessControlledItems(accessControlledItems, "3rd Party Ad Servers");

            var accessControlItemsWithMetadata = accessControlledItems.Where(aci => !string.IsNullOrWhiteSpace(aci.AccessItem.Metadata)).ToList();
            if (accessControlItemsWithMetadata.Any())
            {
                foreach (var accessControlledItem in accessControlItemsWithMetadata)
                {
                    var accessControlItemName = accessControlledItem.AccessItem.Name;
                    var accessControlItemMetaDataValue = accessControlledItem.AccessItem.Metadata;

                    if (accessControlItemName.Equals("Facebook", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlFacebookCampaignNameConvention, accessControlItemMetaDataValue);
                    }
                    else if (accessControlItemName.Equals("Google AdWords", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlGoogleAdWordsCampaignNameConvention, accessControlItemMetaDataValue);
                    }
                    else
                        throw new Exception("The following third party adserver access controlled item: " + accessControlItemName + " had metadata that was not supported. Metadata was: " + accessControlItemMetaDataValue);
                }
            }
        }

        private void ConfigureInventoryProviders(InventoryProviderData inventoryProviderData)
        {
            if (inventoryProviderData == null)
                return;

            if (inventoryProviderData.Enabled)
            {
                var sectionHeaderName = "Inventory Providers";
                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, inventoryProviderData.Name, CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, inventoryProviderData.Enabled);
                SelectWebformDropdownValueIfRequired(_ddlInventoryProvider, inventoryProviderData.Provider);
                SelectWebformDropdownValueIfRequired(_ddlBillingMethod, inventoryProviderData.BillingMethod);

                if (!string.IsNullOrEmpty(inventoryProviderData.TermsAndCondition))
                {
                    ClickElement(_lnkNew);
                    TypeValueIfRequired(_flTermsAndConditions, inventoryProviderData.TermsAndCondition);
                }
            }
        }

        private void ConfigureScheduleBudgetIncludes(ScheduleBudgetIncludesData scheduleBudgetIncludesData)
        {
            if (scheduleBudgetIncludesData == null)
                return;

            var sectionHeaderName = "Schedule Budget Includes";
            var radioButtonElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, scheduleBudgetIncludesData.Base, RADIO_COMPONENT_XPATH);
            ScrollAndClickElement(radioButtonElement);
            ConfigureCheckboxes(scheduleBudgetIncludesData.Additions, "Schedule Budget Includes");
        }

        private void ConfigureMediaScheduleExportOptions(MediaScheduleExportOptionsData mediaScheduleExportOptionsData)
        {
            if (mediaScheduleExportOptionsData == null)
                return;

            if (!string.IsNullOrEmpty(mediaScheduleExportOptionsData.ExportType))
            {
                SelectWebformDropdownValueByText(_ddlExportType, mediaScheduleExportOptionsData.ExportType);
                ConfigureCheckboxes(mediaScheduleExportOptionsData.IncludeInExport, "Media Schedule Export Options");
            }
        }

        private IWebElement GetWebElementUsingDynamicXpathByHeaderAndLabel(string sectionHeaderName, string labelName, string dynamicXPath)
        {
            var dynamicXpath = string.Format(dynamicXPath, sectionHeaderName, labelName);
            var checkboxElement = FindElementByXPath(dynamicXpath);

            return checkboxElement;
        }
    }
}