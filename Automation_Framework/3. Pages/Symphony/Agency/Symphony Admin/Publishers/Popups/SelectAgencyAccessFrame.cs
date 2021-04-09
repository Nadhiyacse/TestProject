using System;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers.Popups
{
    public class SelectAgencyAccessFrame : BasePage
    {
        private IWebElement _txtSearch => FindElementByName("search");
        private IWebElement _rowSearchResult => FindElementByXPath("//div[contains(@class ,'treepickernode-component')]");
        private IWebElement _btnPlus => FindElementByXPath("//div[contains(@class ,'treepickernode-component')]//button");
        private IWebElement _btnSave => FindElementByXPath("//div[text() = 'Save']/ancestor::button");
        private IWebElement _pnlAgenciesWithAccess => FindElementByXPath("//div[@data-test-selector='treepicker-splitpane-selected-node']");

        public SelectAgencyAccessFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectAgencyAccess(RestrictAgencyAccessData data, string publisherName)
        {
            ClearInputAndTypeValue(_txtSearch, data.Country);
            Assert.AreEqual(data.Country, _rowSearchResult.Text);
            ScrollAndClickElement(_rowSearchResult);

            foreach (var agency in data.Agencies)
            {
                if (DoesAgencyHaveAccess(agency, publisherName))
                {
                    continue;
                }

                ClearInputAndTypeValue(_txtSearch, agency);
                ScrollAndClickElement(_btnPlus);
            }

            ScrollAndClickElement(_btnSave);
        }

        private bool DoesAgencyHaveAccess(string agencyName, string publisherName)
        {
            try
            {
                _pnlAgenciesWithAccess.FindElement(By.XPath($"//span[text()='{agencyName}']"));
                return true;
            }
            catch
            {
                Console.WriteLine($"INFO: '{agencyName}' does not have access with publisher '{publisherName}'");
                return false;
            }
        }
    }
}
