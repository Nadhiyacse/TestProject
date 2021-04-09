using System;
using System.Linq;
using System.Text;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherCreatePage : BasePage
    {
        private IWebElement _txtName => FindElementById("ctl00_Content_txtPublisherName");
        private IWebElement _txtSecondaryLanguageName => FindElementById("ctl00_Content_txtSecondaryName");
        private IWebElement _txtAddress => FindElementById("ctl00_Content_txtAddress");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _ddlStatus => FindElementById("ctl00_Content_ddlStatus");
        private IWebElement _chkSubscriber => FindElementById("ctl00_Content_chkSubscriber");
        private IWebElement _txtTaxNumber => FindElementById("ctl00_Content_txtTaxNumber");
        private IWebElement _txtAgencyCommission => FindElementById("ctl00_Content_txtAgencyCommission");
        private IWebElement _txtSource => FindElementById("ctl00_Content_txtSource");
        private IWebElement _flLogoUpload => FindElementById("ctl00_Content_flLogoUpload");
        private IWebElement _ddlLanguage => FindElementById("ctl00_Content_ddlCulture");
        private IWebElement _chkEnablePublisherOverride => FindElementById("ctl00_Content_chkAllowUserCulture");
        private IWebElement _txtContactName => FindElementById("ctl00_Content_txtContactName");
        private IWebElement _txtContactEmail => FindElementById("ctl00_Content_txtContactEmail");
        private IWebElement _txtContactTelephone => FindElementById("ctl00_Content_txtContactPhone");
        private IWebElement _txtContactFax => FindElementById("ctl00_Content_txtContactFax");
        private IWebElement _chkTaxMedia => FindElementById("ctl00_Content_chkListTax_0");
        private IWebElement _btnSave => FindElementById("ctl00_Content_btnSave");
        private IWebElement _pnlMessage => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");
        private IWebElement _chkRestrictAgencyAccess => FindElementById("ctl00_Content_chkRestrictAgencyAccess");

        public PublisherCreatePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreatePublisher(PublisherData publisherData)
        {
            EnsureMandatoryValuesAreProvided(publisherData);

            ClearInputAndTypeValue(_txtName, publisherData.Name);
            ClearInputAndTypeValueIfRequired(_txtSecondaryLanguageName, publisherData.SecondaryLanguageName);
            ClearInputAndTypeValue(_txtAddress, publisherData.Address);
            SelectWebformDropdownValueByText(_ddlCountry, publisherData.Country);
            SelectWebformDropdownValueIfRequired(_ddlStatus, publisherData.Status);
            SetWebformCheckBoxState(_chkSubscriber, publisherData.IsSubscriber);
            SetWebformCheckBoxState(_chkRestrictAgencyAccess, publisherData.IsRestrictAgencyAccess);
            ClearInputAndTypeValueIfRequired(_txtTaxNumber, publisherData.TaxNumber);
            ClearInputAndTypeValue(_txtAgencyCommission, publisherData.AgencyVendorCommission);
            TypeValueIfRequired(_flLogoUpload, publisherData.LogoFilePath);
            SelectWebformDropdownValueIfRequired(_ddlLanguage, publisherData.Language);
            SetWebformCheckBoxState(_chkEnablePublisherOverride, publisherData.EnablePublisherOverride);
            ClearInputAndTypeValue(_txtContactName, publisherData.ContactName);
            ClearInputAndTypeValue(_txtContactEmail, publisherData.ContactEmail);
            ClearInputAndTypeValueIfRequired(_txtContactTelephone, publisherData.ContactTelephone);
            ClearInputAndTypeValueIfRequired(_txtContactFax, publisherData.ContactFax);
            SetWebformCheckBoxState(_chkTaxMedia, publisherData.TaxMedia);

            ClickElement(_btnSave);
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _pnlMessage.Displayed);
            return _pnlMessage.Text;
        }

        private void EnsureMandatoryValuesAreProvided(PublisherData publisherData)
        {
            var publisherDataErrors = new StringBuilder();

            if (publisherData == null)
            {
                publisherDataErrors.Append("\n- No publisher data present in the test data");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(publisherData.Name))
                {
                    publisherDataErrors.Append("\n- Publisher Name not set");
                }

                if (string.IsNullOrWhiteSpace(publisherData.Address))
                {
                    publisherDataErrors.Append("\n- Publisher Address not set");
                }

                if (string.IsNullOrWhiteSpace(publisherData.Country))
                {
                    publisherDataErrors.Append("\n- Publisher Country not set");
                }

                if (string.IsNullOrWhiteSpace(publisherData.AgencyVendorCommission))
                {
                    publisherDataErrors.Append("\n- Publisher Agency/Vendor Commission not set");
                }

                if (string.IsNullOrWhiteSpace(publisherData.ContactName))
                {
                    publisherDataErrors.Append("\n- Publisher Contact Name not set");
                }

                if (string.IsNullOrWhiteSpace(publisherData.ContactEmail))
                {
                    publisherDataErrors.Append("\n- Publisher Contact Email not set");
                }
            }

            if (!string.IsNullOrEmpty(publisherDataErrors.ToString()))
                throw new ArgumentException($"The feature file {FeatureContext.FeatureInfo.Title} has the following data issues:\n {publisherDataErrors}");
        }
    }
}