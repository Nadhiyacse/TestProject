using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyEditPage : BasePage
    {
        private IWebElement _txtName => FindElementById("ctl00_ctl00_Content_Content_txtAgencyName");
        private IWebElement _txtAddress => FindElementById("ctl00_ctl00_Content_Content_txtAddress");
        private IWebElement _ddlCountry => FindElementById("ctl00_ctl00_Content_Content_ddlCountry");
        private IWebElement _ddlStatus => FindElementById("ctl00_ctl00_Content_Content_ddlStatus");
        private IWebElement _chkTestAgency => FindElementById("ctl00_ctl00_Content_Content_chkTestAgency");
        private IWebElement _txtTaxNumber => FindElementById("ctl00_ctl00_Content_Content_txtTaxNumber");
        private IWebElement _flLogo => FindElementById("ctl00_ctl00_Content_Content_flLogoUpload");
        private IWebElement _chkIOEnabled => FindElementById("ctl00_ctl00_Content_Content_chkAllowIO");
        private IWebElement _ddlBillingSource => FindElementById("ctl00_ctl00_Content_Content_ddlBillingSource");
        private IWebElement _rdoBillingExportTypeBCC924 => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_0");
        private IWebElement _rdoBillingExportTypeBCC925 => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_1");
        private IWebElement _rdoBillingExportTypeDDSMediaExplorer => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_2");
        private IWebElement _rdoBillingExportTypeDDSUK => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_3");
        private IWebElement _rdoBillingExportTypeDDSUS => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_4");
        private IWebElement _rdoBillingExportTypePegasus => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_5");
        private IWebElement _rdoBillingExportTypeOther => FindElementById("ctl00_ctl00_Content_Content_rdoBillingApp_6");
        private IWebElement _ddlLanguage => FindElementById("ctl00_ctl00_Content_Content_ddlCulture");
        private IWebElement _ddlTimeZone => FindElementById("ctl00_ctl00_Content_Content_ddlTimeZone");
        private IWebElement _chkEnableAgencyOverride => FindElementById("ctl00_ctl00_Content_Content_chkAllowUserCulture");
        private IWebElement _txtContactName => FindElementById("ctl00_ctl00_Content_Content_txtContactName");
        private IWebElement _txtContactEmail => FindElementById("ctl00_ctl00_Content_Content_txtContactEmail");
        private IWebElement _txtContactPhone => FindElementById("ctl00_ctl00_Content_Content_txtContactPhone");
        private IWebElement _ddlAuthenticationMethod => FindElementById("ctl00_ctl00_Content_Content_ddlAuthTypes");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");

        private const string BCC_9_2_4 = "BCC 9.2.4";
        private const string BCC_9_2_5 = "BCC 9.2.5";
        private const string DDS_MEDIA_EXPLORER = "DDS Media Explorer";
        private const string DDS_UK = "DDS UK";
        private const string DDS_US = "DDS US";
        private const string PEGASUS = "Pegasus";
        private const string OTHER = "Other";

        public AgencyEditPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void EditAgency(AgencyData agencyData)
        {
            EnsureMandatoryValuesAreProvided(agencyData);

            ClearInputAndTypeValue(_txtName, agencyData.Name);
            ClearInputAndTypeValue(_txtAddress, agencyData.Address);
            SelectWebformDropdownValueByText(_ddlCountry, agencyData.Country);
            SelectWebformDropdownValueIfRequired(_ddlStatus, agencyData.Status);
            SetWebformCheckBoxState(_chkTestAgency, agencyData.IsTestAgency);
            ClearInputAndTypeValueIfRequired(_txtTaxNumber, agencyData.TaxNumber);
            TypeValueIfRequired(_flLogo, agencyData.LogoFilePath);
            SetWebformCheckBoxState(_chkIOEnabled, agencyData.InsertionOrderEnabled);
            SelectWebformDropdownValueIfRequired(_ddlBillingSource, agencyData.BillingSource);
            SelectWebformDropdownValueIfRequired(_ddlLanguage, agencyData.Language);
            GetAndStoreSelectedTimeZone(agencyData.TimeZone);
            SetWebformCheckBoxState(_chkEnableAgencyOverride, agencyData.EnableAgencyOverride);
            ClearInputAndTypeValue(_txtContactName, agencyData.ContactName);
            ClearInputAndTypeValue(_txtContactEmail, agencyData.ContactEmail);
            ClearInputAndTypeValueIfRequired(_txtContactPhone, agencyData.ContactTelephone);
            SelectWebformDropdownValueIfRequired(_ddlAuthenticationMethod, agencyData.AuthenticationMethod);
            ClickElement(_btnSave);
        }

        private void GetAndStoreSelectedTimeZone(string timeZone)
        {
            IWebElement selectedTimeZone;
            if (string.IsNullOrEmpty(timeZone))
            {
                selectedTimeZone = _ddlTimeZone.FindElements(By.XPath("option")).Where(e => e.Selected == true).First();
            }
            else
            {
                SelectWebformDropdownValueByText(_ddlTimeZone, timeZone);
                selectedTimeZone = _ddlTimeZone.FindElement(By.XPath($"//option[contains(text(),'{timeZone}')]"));
            }
        }

        private void EnsureMandatoryValuesAreProvided(AgencyData agencyData)
        {
            var agencyDataErrorrs = new StringBuilder();

            if (agencyData == null)
            {
                agencyDataErrorrs.Append("\n- No agency data present in the test data");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(agencyData.Name))
                {
                    agencyDataErrorrs.Append("\n- Agency Name not set");
                }

                if (string.IsNullOrWhiteSpace(agencyData.Address))
                {
                    agencyDataErrorrs.Append("\n- Agency Address not set");
                }

                if (string.IsNullOrWhiteSpace(agencyData.Country))
                {
                    agencyDataErrorrs.Append("\n- Agency Country not set");
                }

                if (string.IsNullOrWhiteSpace(agencyData.ContactName))
                {
                    agencyDataErrorrs.Append("\n- Agency Contact Name not set");
                }

                if (string.IsNullOrWhiteSpace(agencyData.ContactEmail))
                {
                    agencyDataErrorrs.Append("\n- Agency Contact Email not set");
                }
            }            

            if (!string.IsNullOrEmpty(agencyDataErrorrs.ToString()))
                throw new ArgumentException($"The feature file {FeatureContext.FeatureInfo.Title} has the following data issues:\n {agencyDataErrorrs.ToString()}");
        }
    }
}