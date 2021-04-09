using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyFeaturesPage : BasePage
    {
        private const string FRAME_MANAGE_ACTIONS_REQUIRING_REAPPROVAL = "/symphony-app/symphony-admin/agency-branch/manage-campaign-approval-exemptions";

        private const string CHECKBOX_COMPONENT_XPATH = "//label[text() = '{0}']/../..//input[@type = 'checkbox']";
        private const string RADIO_COMPONENT_XPATH = "//label[text() = '{0}']/../..//input[@type = 'radio']";
        private const string DYNAMIC_CHECKBOX_COMPONENT_XPATH = "//h2[contains(text(), '{0}')]/..//table//tbody//tr//td//label[text() = '{1}']/../..//input[@type = 'checkbox']";

        private IWebElement _ddlIoPoNumberGenerateOnSave => FindElementById("ctl00_ctl00_Content_Content_rptInsertionOrderSettings_ctl08_ddlInsertionOrderPurchaseOrderNumberFormats");
        private IWebElement _ddlIoApprovalWorkflowExportDocument => FindElementById("ctl00_ctl00_Content_Content_rptInsertionOrderSettings_ctl09_ddlIOApprovalWorkflowExportDocument");
        private IWebElement _ddlMultipleCurrencyBillingAllocationsEditableIn => FindElementById("ctl00_ctl00_Content_Content_rptBillingSettings_ctl06_ddlMultiCurrencyEditableTypes");
        private IWebElement _ddlAgencyCostBasis => FindElementById("ctl00_ctl00_Content_Content_rptBillingSettings_ctl09_ddlCostBasisTypes");
        private IWebElement _ddlApprovalActionsExportDocument => FindElementById("ctl00_ctl00_Content_Content_rpMediaScheduleApproval_ctl03_ddlApprovalExportDocument");
        private IWebElement _txtMaximumForecastingPercentage => FindElementById("ctl00_ctl00_Content_Content_rpDefaultForecastingMetrics_ctl01_txtDefaultForecastingMetric");
        private IWebElement _ddlCostDefaultAgencyFees => FindElementById("ctl00_ctl00_Content_Content_CostDefaultSettings_rpCostDefaultSettings_ctl01_ddlAgencyFeeOptions");
        private IWebElement _lnkManageActionsRequiringReapproval => FindElementById("lnkApprovalExemptFields");

        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");

        public AgencyFeaturesPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ConfigureAgencyFeatures(AgencyFeaturesData agencyFeatures)
        {
            ConfigureMediaScheduleItemSettings(agencyFeatures.MediaScheduleItemSettings);
            ConfigureCheckboxes(agencyFeatures.MediaScheduleExports, "Media Schedule Exports");
            ConfigureCheckboxes(agencyFeatures.ServiceFeeOptions, "Service Fee Options");
            ConfigureCheckboxes(agencyFeatures.Trafficking, "Trafficking");
            ConfigureCheckboxes(agencyFeatures.CampaignSettings, "Campaign Settings");
            ConfigureInsertionOrders(agencyFeatures.InsertionOrders);
            ConfigureEmailNotifications(agencyFeatures.EmailNotifications);
            ConfigureBilling(agencyFeatures.Billing);
            ConfigureCheckboxes(agencyFeatures.MediaInsights, "Media Insights");
            ConfigureMediaScheduleApproval(agencyFeatures.MediaScheduleApproval);
            ConfigureCheckboxes(agencyFeatures.RatecardManagement, "Ratecard Management");
            ConfigureCustomForecastingMetricsSettings(agencyFeatures.ForecastingMetrics);
            ConfigureCostDefaultSetting(agencyFeatures.CostDefaultSetting);
            ConfigureCheckboxes(agencyFeatures.AdministratorRoles, "Administrator Roles");
        }

        public void ClickSaveButton()
        {
            ClickElement(_btnSave);
        }

        public bool IsAgencyFeatureEnabledInList(List<Checkbox> featureList, string featureName)
        {
            if (featureList == null)
                return false;

            if (!featureList.Any(feature => feature.Name == featureName))
                return false;

            return featureList.Find(feature => feature.Name == featureName).Enabled;
        }

        private void ConfigureCheckboxes(IEnumerable<Checkbox> checkboxes, string sectionHeaderName)
        {
            if (checkboxes == null || !checkboxes.Any())
                return;

            foreach (var checkbox in checkboxes)
            {
                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, checkbox.Name, DYNAMIC_CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, checkbox.Enabled);
            }
        }

        private void ConfigureInsertionOrders(IEnumerable<Checkbox> insertionOrderSettings)
        {
            if (insertionOrderSettings == null || !insertionOrderSettings.Any())
                return;

            foreach (var insertionOrderSetting in insertionOrderSettings)
            {
                if (insertionOrderSetting.Name.Equals("Use Grid for Insertion Order Landing Page") && !IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage))
                    continue;

                SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, insertionOrderSetting.Name), insertionOrderSetting.Enabled);

                if (insertionOrderSetting.Enabled && !string.IsNullOrWhiteSpace(insertionOrderSetting.Metadata))
                {
                    if (insertionOrderSetting.Name.Equals("IO PO Number Generated on Save", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlIoPoNumberGenerateOnSave, insertionOrderSetting.Metadata);
                    }
                    else if (insertionOrderSetting.Name.Equals("IO Approval Workflow", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlIoApprovalWorkflowExportDocument, insertionOrderSetting.Metadata);
                    }
                    else
                        throw new Exception("The following insertion order setting: " + insertionOrderSetting.Name + " had metadata that was not supported. Metadata was: " + insertionOrderSetting.Metadata);
                }
            }
        }

        private void ConfigureMediaScheduleItemSettings(IEnumerable<Checkbox> MediaScheduleItemSettings)
        {
            if (MediaScheduleItemSettings == null || !MediaScheduleItemSettings.Any())
                return;

            foreach (var mediaScheduleItemSetting in MediaScheduleItemSettings)
            {
                if (mediaScheduleItemSetting.Name.Equals("Enable Linked Non-Media Cost (Snapshotted)") && !IsFeatureToggleEnabled(FeatureToggle.LinkedNonMediaPhase1CostPerUnit))
                    continue;

                SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, mediaScheduleItemSetting.Name), mediaScheduleItemSetting.Enabled);
            }
        }

        private void ConfigureEmailNotifications(EmailNotificationsData emailNotifications)
        {
            if (emailNotifications == null)
                return;

            if (!string.IsNullOrWhiteSpace(emailNotifications.Role))
            {
                if (emailNotifications.RoleNotifications == null || !emailNotifications.RoleNotifications.Any())
                    return;

                const string insertionOrderApproverRole = "IO Approver";
                const string insertionOrdersignoffApproverNotification = "IO_WORKFLOW_IO_SIGNOFF_APPROVER_NOTIFICATION";
                const string insertionOrderRejectApproverNotification = "IO_WORKFLOW_IO_REJECT_APPROVER_NOTIFICATION";
                const string dynamicRoleCheckboxXPath = "*//table[@class = 'email-notification-table']//tbody//tr//td//span[contains(text(), '{0}')]/../..//td//input[@value = '{1}']";

                if (emailNotifications.Role.Equals("IO Approver", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var emailNotificationsRoleNotification in emailNotifications.RoleNotifications)
                    {
                        IWebElement checkboxWebElement;
                        if (emailNotificationsRoleNotification.Name.Equals("IO signed off by Publisher", StringComparison.InvariantCultureIgnoreCase))
                        {
                            checkboxWebElement = FindElementByXPath(string.Format(dynamicRoleCheckboxXPath, insertionOrderApproverRole, insertionOrdersignoffApproverNotification));
                        }
                        else if (emailNotificationsRoleNotification.Name.Equals("IO rejected by Publisher", StringComparison.InvariantCultureIgnoreCase))
                        {
                            checkboxWebElement = FindElementByXPath(string.Format(dynamicRoleCheckboxXPath, insertionOrderApproverRole, insertionOrderRejectApproverNotification));
                        }
                        else
                            throw new Exception("For the following role: " + emailNotifications.Role + " the following notification option is not supported: " + emailNotificationsRoleNotification.Name);

                        SetWebformCheckBoxState(checkboxWebElement, emailNotificationsRoleNotification.Enabled);
                    }
                }
                else
                    throw new Exception("The email notification setting tried to set properties for the role: " + emailNotifications.Role + " which is not supported");
            }
        }

        private void ConfigureBilling(IEnumerable<Checkbox> billingSettings)
        {
            if (billingSettings == null || !billingSettings.Any())
                return;

            foreach (var billingSetting in billingSettings)
            {
                SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, billingSetting.Name), billingSetting.Enabled);

                if (billingSetting.Enabled && !string.IsNullOrWhiteSpace(billingSetting.Metadata))
                {
                    if (billingSetting.Name.Equals("Multiple Currency Billing Allocations", StringComparison.InvariantCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlMultipleCurrencyBillingAllocationsEditableIn, billingSetting.Metadata);
                    }
                    else if (billingSetting.Name.Equals("Agency Cost Basis", StringComparison.CurrentCultureIgnoreCase))
                    {
                        SelectWebformDropdownValueByText(_ddlAgencyCostBasis, billingSetting.Metadata);
                    }
                    else
                        throw new Exception("The following billing setting: " + billingSetting.Name + " had metadata that was not supported. Metadata was: " + billingSetting.Metadata);
                }
            }
        }

        private void ConfigureMediaScheduleApproval(MediaScheduleApprovalData mediaScheduleApproval)
        {
            if (mediaScheduleApproval == null)
                return;

            SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, "Enable Approval Workflow"), mediaScheduleApproval.EnableApprovalWorkflow);

            if (mediaScheduleApproval.EnableApprovalWorkflow)
            {
                if (mediaScheduleApproval.IsApprovalActionsEnabled)
                {
                    ScrollAndClickElement(GetWebElementUsingSettingLabel(RADIO_COMPONENT_XPATH, "Approval Actions"));
                    SelectWebformDropdownValueByText(_ddlApprovalActionsExportDocument, mediaScheduleApproval.ExportDocument);
                    SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, "Require document upload"), mediaScheduleApproval.RequireDocumentUpload);
                    SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, "Enable contact email notification modal"), mediaScheduleApproval.EnableContactEmailNotification);
                }
                else
                {
                    ScrollAndClickElement(GetWebElementUsingSettingLabel(RADIO_COMPONENT_XPATH, "HSN Codes"));
                }
            }
        }

        public void NavigateToActionsNotRequiringReapprovalFrame()
        {
            ScrollAndClickElement(_lnkManageActionsRequiringReapproval);
            var frame = FRAME_MANAGE_ACTIONS_REQUIRING_REAPPROVAL;
            SwitchToFrame(frame);
        }

        private void ConfigureCustomForecastingMetricsSettings(ForecastingMetricsSettingsData forecastingMetrics)
        {
            if (forecastingMetrics == null)
                return;

            if (forecastingMetrics.DefaultMetrics != null)
            {
                SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, forecastingMetrics.DefaultMetrics.Name), forecastingMetrics.DefaultMetrics.Enabled);

                if (forecastingMetrics.DefaultMetrics.Enabled && !string.IsNullOrWhiteSpace(forecastingMetrics.DefaultMetrics.Metadata))
                {
                    ClearInputAndTypeValue(_txtMaximumForecastingPercentage, forecastingMetrics.DefaultMetrics.Metadata);
                }
            }

            if (forecastingMetrics.CustomForecastingMetrics == null || !forecastingMetrics.CustomForecastingMetrics.Any())
                return;

            foreach (var customForecastingMetric in forecastingMetrics.CustomForecastingMetrics)
            {
                SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, customForecastingMetric.Name), customForecastingMetric.Enabled);
            }
        }

        private void ConfigureCostDefaultSetting(string costDefaultSetting)
        {
            SelectWebformDropdownValueByText(_ddlCostDefaultAgencyFees, costDefaultSetting);
        }

        private IWebElement GetWebElementUsingDynamicXpathByHeaderAndLabel(string sectionHeaderName, string labelName, string dynamicXPath)
        {
            var dynamicXpath = string.Format(dynamicXPath, sectionHeaderName, labelName);
            var checkboxElement = FindElementByXPath(dynamicXpath);

            return checkboxElement;
        }
    }
}