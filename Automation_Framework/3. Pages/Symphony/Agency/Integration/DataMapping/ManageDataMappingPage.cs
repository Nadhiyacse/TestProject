using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.DataMapping
{
    public class ManageDataMappingPage : BasePage
    {
        private IWebElement _menuApplication(string application) => FindElementByXPath($"//a[contains(@id, 'lnkApplication')][text() = '{application.ToUpper()}']");
        private IWebElement _ddlType => FindElementById("ctl00_ctl00_Content_Content_ddlType");
        private IWebElement _btnDisplay => FindElementById("ctl00_ctl00_Content_Content_btnDisplay");
        private IWebElement _ddlStatus => FindElementById("ctl00_ctl00_Content_Content_ddlStatus");
        private IWebElement _ddlPublisher => FindElementById("ctl00_ctl00_Content_Content_ddlPublisher");
        private IWebElement _ddlCountry => FindElementById("ctl00_ctl00_Content_Content_ddlCountry");
        private IWebElement _txtForeignId => FindElementById("ctl00_ctl00_Content_Content_ctl00_txtForeignId");
        private IWebElement _chkDynamicCreative => FindElementById("ctl00_ctl00_Content_Content_ctl00_chkDynamicCreative");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");
        private IWebElement _btnCancel => FindElementById("ctl00_ctl00_Content_Content_btnCancel");
        private IWebElement _pnlMessageSuccess => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");
        private IWebElement _tabSearch => FindElementById("ctl00_ctl00_Content_Content_lbtSearch");
        private IWebElement _tabBrowse => FindElementById("ctl00_ctl00_Content_Content_lbtBrowse");
        private IWebElement _txtItemName => FindElementById("ctl00_ctl00_Content_Content_txtItemName");
        private IWebElement _btnSearch => FindElementById("ctl00_ctl00_Content_Content_btnSearch");

        protected const string FRAME_MAPPING_POPUP = "MappingPopup.aspx?";

        public ManageDataMappingPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectApplication(string application)
        {
            ScrollAndClickElement(_menuApplication(application.ToUpper()));
            WaitForPageLoadComplete();
        }

        public void SelectType(string type)
        {
            SelectWebformDropdownValueByText(_ddlType, type);
            WaitForPageLoadComplete();
        }

        public void SelectStatus(string status)
        {
            SelectWebformDropdownValueByText(_ddlStatus, status);
        }

        public void SelectPublisher(string publisher)
        {
            SelectWebformDropdownValueByText(_ddlPublisher, publisher);
        }

        public void SelectCountry(string country)
        {
            SelectWebformDropdownValueByText(_ddlCountry, country);
        }

        public void ClickDisplay()
        {
            ScrollAndClickElement(_btnDisplay);
            WaitForPageLoadComplete();
        }

        public void ClickSave()
        {
            ScrollAndClickElement(_btnSave);
        }

        public bool SelectClientToMapData(ClientData data)
        {
            ClearClientMappingsIfExist(data);

            var isSelectedSuccessful = false;
            if (IsElementPresent(By.XPath($"//span[text()='{data.Name}']/ancestor::tr//a[text() ='Add']")))
            {
                WaitForElementToBeVisible(By.XPath($"//span[text()='{data.Name}']/ancestor::tr//a[text() ='Add']"));
                ScrollAndClickElement(FindElementByXPath($"//span[text()='{data.Name}']/ancestor::tr//a[text() ='Add']"));
                SwitchToFrame(FRAME_MAPPING_POPUP);
                isSelectedSuccessful = true;
            }

            return isSelectedSuccessful;
        }

        private void ClearClientMappingsIfExist(ClientData data)
        {
            foreach (var field in data.Fields)
            {
                if (field.Name.ToLower().Equals("foreignid"))
                {
                    if (IsClientForeignIdExist(data) && !IsExistingClientForeignIdCorrect(data))
                    {
                        var _btnRemove = FindElementByXPath($"//span[text()='{data.Name}']/ancestor::tr//a[contains(@id, 'lkbRemove')]");
                        ScrollAndClickElement(_btnRemove);
                    }
                }
            }
        }

        private bool IsExistingClientForeignIdCorrect(ClientData data)
        {
            WaitForElementToBeVisible(By.XPath($"//span[text()='{data.Name}']"));
            var foreignIdValue = string.Empty;

            foreach (var field in data.Fields)
            {
                if (field.Name.ToLower().Equals("foreignid"))
                {
                    foreignIdValue = field.Value;
                }
            }

            var actualForeignIdValue = FindElementByXPath($"//span[text()='{data.Name}']/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;

            return actualForeignIdValue.Equals(foreignIdValue);
        }

        private bool IsClientForeignIdExist(ClientData data)
        {
            WaitForElementToBeVisible(By.XPath($"//span[text()='{data.Name}']"));
            var actualForeignIdValue = FindElementByXPath($"//span[text()='{data.Name}']/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;
            var isForeignIdExist = false;

            if (!string.IsNullOrEmpty(actualForeignIdValue))
            {
                isForeignIdExist = true;
            }

            return isForeignIdExist;
        }

        public bool SelectSiteToMapData(SiteData data)
        {
            ClearSiteMappingIfExists(data);

            var isSelectedSuccessful = false;
            if (IsElementPresent(By.XPath($"//strong[text() = \"{data.Name}\"]/ancestor::tr//a[text() ='Add']")))
            {
                WaitForElementToBeVisible(By.XPath($"//strong[text() = \"{data.Name}\"]/ancestor::tr//a[text() ='Add']"));
                ScrollAndClickElement(FindElementByXPath($"//strong[text() = \"{data.Name}\"]/ancestor::tr//a[text() ='Add']"));
                SwitchToFrame(FRAME_MAPPING_POPUP);
                isSelectedSuccessful = true;
            }

            return isSelectedSuccessful;
        }

        private void ClearSiteMappingIfExists(SiteData data)
        {
            if (IsSiteForeignIdExist(data) && !IsExistingSiteForeignIdCorrect(data))
            {
                var _btnRemove = FindElementByXPath($"//strong[text() = \"{data.Name}\"]/ancestor::tr//a[contains(@id, 'lkbRemove')]");
                ScrollAndClickElement(_btnRemove);
            }
        }

        private bool IsExistingSiteForeignIdCorrect(SiteData data)
        {
            WaitForElementToBeVisible(By.XPath($"//strong[text()=\"{data.Name}\"]"));
            var foreignIdValue = string.Empty;

            foreach (var field in data.Fields)
            {
                if (field.Name.ToLower().Equals("foreignid"))
                {
                    foreignIdValue = field.Value;
                }
            }

            var actualForeignIdValue = FindElementByXPath($"//strong[text()=\"{data.Name}\"]/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;

            return actualForeignIdValue.Equals(foreignIdValue);
        }

        private bool IsSiteForeignIdExist(SiteData data)
        {
            WaitForElementToBeVisible(By.XPath($"//strong[text()=\"{data.Name}\"]"));
            var actualForeignIdValue = FindElementByXPath($"//strong[text()=\"{data.Name}\"]/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;
            var isForeignIdExist = false;

            if (!string.IsNullOrEmpty(actualForeignIdValue))
            {
                isForeignIdExist = true;
            }

            return isForeignIdExist;
        }

        public bool SelectVendorToMapData(VendorData data)
        {
            ClearVendorDataMappingsIfExist(data);

            var vendorNameWithCountryCode = $"{data.Name} ({GetCountryCode(data.Country)})";
            var isSelectedSuccessful = false;
            if (IsElementPresent(By.XPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[text() ='Add']")))
            {
                WaitForElementToBeVisible(By.XPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[text() ='Add']"));
                ScrollAndClickElement(FindElementByXPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[text() ='Add']"));
                SwitchToFrame(FRAME_MAPPING_POPUP);
                isSelectedSuccessful = true;
            }

            return isSelectedSuccessful;
        }

        private void ClearVendorDataMappingsIfExist(VendorData data)
        {
            foreach (var field in data.Fields)
            {
                if (field.Name.ToLower().Equals("foreignid"))
                {
                    if (IsVendorForeignIdExist(data) && !IsExistingVendorForeignIdCorrect(data))
                    {
                        var vendorNameWithCountryCode = $"{data.Name} ({GetCountryCode(data.Country)})";
                        var _btnRemove = FindElementByXPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[contains(@id, 'lkbRemove')]");
                        ScrollAndClickElement(_btnRemove);
                    }
                }
            }
        }

        private bool IsExistingVendorForeignIdCorrect(VendorData data)
        {
            var vendorNameWithCountryCode = $"{data.Name} ({GetCountryCode(data.Country)})";
            WaitForElementToBeVisible(By.XPath($"//span[text()='{vendorNameWithCountryCode}']"));
            var foreignIdValue = string.Empty;

            foreach (var field in data.Fields)
            {
                if (field.Name.ToLower().Equals("foreignid"))
                {
                    foreignIdValue = field.Value;
                }
            }

            var actualForeignIdValue = FindElementByXPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;

            return actualForeignIdValue.Equals(foreignIdValue);
        }

        private bool IsVendorForeignIdExist(VendorData data)
        {
            var vendorNameWithCountryCode = $"{data.Name} ({GetCountryCode(data.Country)})";
            WaitForElementToBeVisible(By.XPath($"//span[text()='{vendorNameWithCountryCode}']"));
            var actualForeignIdValue = FindElementByXPath($"//span[text()='{vendorNameWithCountryCode}']/ancestor::tr//a[contains(@id, 'lnkEditForeignId')]").Text;
            var isForeignIdExist = false;

            if (!string.IsNullOrEmpty(actualForeignIdValue))
            {
                isForeignIdExist = true;
            }

            return isForeignIdExist;
        }

        public void MapAgencyData(Field data)
        {
            switch (data.Name.ToLower())
            {
                case "foreignid":
                    ClearInputAndTypeValueIfRequired(_txtForeignId, data.Value);
                    break;
                case "dynamic creative":
                    SetWebformCheckBoxState(_chkDynamicCreative, bool.Parse(data.Value));
                    break;
            }
        }

        public string GetMessage()
        {
            Wait.Until(driver => _pnlMessageSuccess.Displayed);
            return _pnlMessageSuccess.Text;
        }

        public void SearchItemName(string itemName, string country = null)
        {
            ClickElement(_tabSearch);
            WaitForElementToBeVisible(_btnSearch);
            ClearInputAndTypeValue(_txtItemName, itemName);
            ClickElement(_btnSearch);
        }

        private string GetCountryCode(string country)
        {
            var regions = CultureInfo.GetCultures(CultureTypes.SpecificCultures).Select(x => new RegionInfo(x.LCID));
            var englishRegion = regions.FirstOrDefault(region => region.EnglishName.Contains(country));
            return englishRegion.ThreeLetterISORegionName;
        }

        public void ClickBrowseTab()
        {
            ClickElement(_tabBrowse);
            WaitForElementToBeVisible(_btnDisplay);
        }
    }
}
