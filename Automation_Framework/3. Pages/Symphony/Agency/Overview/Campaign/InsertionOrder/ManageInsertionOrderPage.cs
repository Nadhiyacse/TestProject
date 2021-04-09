using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder
{
    public class ManageInsertionOrderPage : BasePage
    {
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _lnkIOStatus => FindElementById("ctl00_Content_lstPublishers_ctrl0_lnkPendingIOVersion");
        private IWebElement _lnkLastSignOffVersion => FindElementById("ctl00_Content_lstPublishers_ctrl0_lnkLastSignedOffVersion");

        public ManageInsertionOrderPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectCountry(string country)
        {
            if (!string.IsNullOrEmpty(country))
            {
                SelectWebformDropdownValueByText(_ddlCountry, country);
            }
        }

        public bool IsLastSignedOffVersionCorrect(string date, string version)
        {
            string versionNumber = version.Split(new char[0])[1];
            return GetLastSignedOffVersion().Equals($"{date} {versionNumber}", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsPendingIOStatusLinkDisplayed(int maxAttempts = 5)
        {
            var _isReceived = false;
            var _attempts = 0;
            while (!_isReceived && _attempts < maxAttempts)
            {
                try
                {
                    Wait.Until(d => _lnkIOStatus.Displayed);
                    _isReceived = true;
                }
                catch
                {
                    driver.Navigate().Refresh();
                }
                _attempts++;
            }
            return _isReceived;
        }

        public string GetIOStatus()
        {
            return _lnkIOStatus.Text;
        }
        
        public string GetLastSignedOffVersion()
        {
            return _lnkLastSignOffVersion.Text;
        }

        public void ClickIOStatusLink()
        {
            ClickElement(_lnkIOStatus);
        }

        public void ClickLastSignedOffVersionLink()
        {
            ClickElement(_lnkLastSignOffVersion);
        }
    }
}
