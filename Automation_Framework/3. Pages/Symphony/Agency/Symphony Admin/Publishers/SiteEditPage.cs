using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class SiteEditPage : BasePage
    {
        private IWebElement _txtSiteName => FindElementById("ctl00_ctl00_Content_Content_txtSiteName");
        private IWebElement _txtSecondaryLanguageName => FindElementById("ctl00_ctl00_Content_Content_txtSecondarySiteName");
        private IWebElement _txtUrl => FindElementById("ctl00_ctl00_Content_Content_txtSiteUrl");
        private IWebElement _txtLanguage => FindElementById("ctl00_ctl00_Content_Content_txtSiteLanguage");
        private IWebElement _ddlPrimaryCategory => FindElementById("ctl00_ctl00_Content_Content_ddlCategory");
        private IWebElement _ddlSecondaryCategory => FindElementById("ctl00_ctl00_Content_Content_ddlSecondCategory");
        private IWebElement _chkProprietaryMediaSite => FindElementById("ctl00_ctl00_Content_Content_chkIsProprietaryMedia");
        private IWebElement _ddlParentSite => FindElementById("ctl00_ctl00_Content_Content_ddlParentSite");
        private IWebElement _txtDescription => FindElementById("ctl00_ctl00_Content_Content_txtDescription");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");
        private IWebElement _btnCancel => FindElementById("ctl00_ctl00_Content_Content_btnCancel");
        private IWebElement _btnBack => FindElementById("ctl00_ctl00_Content_Content_btnBack");
        private IWebElement _pnlMessage => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");

        public SiteEditPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void EditSite(SiteData siteData)
        {
            EnsureMandatoryValuesAreProvided(siteData);

            ClearInputAndTypeValue(_txtSiteName, siteData.SiteName);
            ClearInputAndTypeValueIfRequired(_txtSecondaryLanguageName, siteData.SecondaryLanguageName);
            ClearInputAndTypeValue(_txtUrl, siteData.Url);
            ClearInputAndTypeValueIfRequired(_txtLanguage, siteData.Language);
            SelectWebformDropdownValueIfRequired(_ddlPrimaryCategory, siteData.PrimaryCategory);
            SelectWebformDropdownValueIfRequired(_ddlSecondaryCategory, siteData.SecondaryCategory);
            SetWebformCheckBoxState(_chkProprietaryMediaSite, siteData.IsProprietaryMediaSite);
            ClearInputAndTypeValueIfRequired(_txtDescription, siteData.Description);
            ClickElement(_btnSave);
        }

        public void SetParentSiteAndSave(string parentSiteName)
        {
            SelectWebformDropdownValueByText(_ddlParentSite, parentSiteName);
            ClickElement(_btnSave);
        }

        public void ClickBack()
        {
            ClickElement(_btnBack);
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _pnlMessage.Displayed);
            return _pnlMessage.Text;
        }

        private void EnsureMandatoryValuesAreProvided(SiteData siteData)
        {
            var siteDataErrors = new StringBuilder();

            if (siteData == null)
            {
                siteDataErrors.Append("\n- No site data present in the test data");
            }
            else
            {
                if (string.IsNullOrWhiteSpace(siteData.SiteName))
                {
                    siteDataErrors.Append("\n- Site Name not set");
                }

                if (string.IsNullOrWhiteSpace(siteData.Url))
                {
                    siteDataErrors.Append("\n- Site URL not set");
                }
            }

            if (!string.IsNullOrEmpty(siteDataErrors.ToString()))
                throw new ArgumentException($"The feature file {FeatureContext.FeatureInfo.Title} has the following data issues:\n {siteDataErrors}");
        }
    }
}
