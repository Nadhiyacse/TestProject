using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.WorkflowTestData.Billing;
using Newtonsoft.Json;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Billing
{
    public class BillingPage : BasePage
    {
        private const string SUBSCRIBER_PUBLISHER_SUFFIX = " (S)";
        private const string TAG_NAME_TH = "th";
        private const string TAG_NAME_TD = "td";
        private const string FRAME_EXPORT = "/Export/Export.aspx";

        private IWebElement _btnSave => FindElementById("ctl00_Content_btnSave");
        private IWebElement _ddlCurrencyType => FindElementById("ctl00_Content_ddlCurrencySourceList");
        private IWebElement TableHeaderRow => FindElementByXPath("//*[@id=\"ctl00_Content_CurrentlyLoadedViewControlId_gvPublishers\"]/table/thead");
        private IEnumerable<IWebElement> TableRows => FindElementsByXPath("//*[@id=\"ctl00_Content_CurrentlyLoadedViewControlId_gvPublishers\"]/table/tbody//tr");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountryList");
        private IWebElement _btnBillingExport => FindElementByCssSelector("#ctl00_Content_btnExport span");
        private IWebElement GetChkOverride(string publisher)        
        {
            return FindElementByXPath(
                $"//a[contains(text(),'{publisher}')]/ancestor::tr//span[@class=\"checkbox-wrapper\"]/input");
        }

        private IWebElement GetDdlAllocationMethod(string publisher)
        {
            return FindElementByXPath($"//a[contains(text(),'{publisher}')]/ancestor::tr//select");
        }

        private IWebElement GetLnkPublisher(string publisher)
        {
            return FindElementByXPath($"//a[contains(text(),'{publisher}')]");
        }

        public BillingPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CorrectBillingItemsPresent(List<BillingData> billingData)
        {
            EnsureMandatoryBillingDataProvided(billingData);

            List<BillingItem> billingItems;

            if (billingData.Count > 1)
            {
                string country = GetSelectedValueFromDropDown(_ddlCountry);
                string currency  = GetSelectedValueFromDropDown(_ddlCurrencyType);
                billingItems = billingData.Where(d => d.Currency == currency && d.Country == country).First().BillingItems;
            }
            else
            {
                billingItems = billingData.First().BillingItems;
            }

            var displayedBillingItems = GetDisplayedBillingItems();
            Assert.AreEqual(billingItems.Count, displayedBillingItems.Count, "Mismatch in number of billing items");

            foreach (var billingItem in billingItems)
            {
                var displayed = displayedBillingItems.Where(dis => (dis.Currency == billingItem.Currency) && (dis.Publisher == billingItem.Publisher)).First();
                Assert.IsTrue(billingItem.Equals(displayed),
                    $"Mismatch in billing item.\nExpected: {JsonConvert.SerializeObject(billingItem, Formatting.Indented)}\nActual: {JsonConvert.SerializeObject(displayed, Formatting.Indented)}");
            }
        }

        public void OverrideBillingAllocationForEachPublisher(List<BillingData> billingData)
        {
            EnsureMandatoryBillingDataProvided(billingData);

            string country = GetSelectedValueFromDropDown(_ddlCountry);
            var data = billingData.Where(item => item.Country == country).First();

            foreach (var item in data.BillingItems)
            {
                var allocationMethod = GetDdlAllocationMethod(item.Publisher);
                var overrideCheck = GetChkOverride(item.Publisher);
                SelectWebformDropdownValueByText(allocationMethod, item.BillingAllocationMethod);
                SetWebformCheckBoxState(overrideCheck, item.isOverride);
            }
            ClickElement(_btnSave);
        }

        private void EnsureMandatoryBillingDataProvided(List<BillingData> billingData)
        {
            var billingDataErrors = new StringBuilder();

            if (billingData == null || !billingData.Any())
            {
                billingDataErrors.Append("\n- No billing data present in the test data");
            }
            else
            {
                foreach (var item in billingData)
                {
                    if (string.IsNullOrWhiteSpace(item.Country))
                    {
                        billingDataErrors.Append($"\n- No country specified in the test data for billing items");
                    }
                }
            }

            if (!string.IsNullOrEmpty(billingDataErrors.ToString()))
                throw new ArgumentException("The feature file " + FeatureContext.FeatureInfo.Title + $" has the following data issues: \n {billingDataErrors.ToString()}");
        }

        public void SetDisplayCurrency(string currency)
        {
            SelectWebformDropdownValueByText(_ddlCurrencyType, currency);
        }

        public bool IsMultiplePublishersAndCurrenciesDisplayed()
        {
            var publisherSet = new HashSet<string>();
            var currencySet = new HashSet<string>();

            var publisherColumnIndex = TableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList()
                .FindIndex(x => x.Text == "Publisher");

            var currencyColumnIndex = TableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList()
                .FindIndex(x => x.Text == "Currency");

            foreach (var row in TableRows)
            {
                var cells = row.FindElements(By.TagName(TAG_NAME_TD));
                publisherSet.Add(cells[publisherColumnIndex].Text);
                currencySet.Add(cells[currencyColumnIndex].Text);
            }

            return publisherSet.Count > 1 && currencySet.Count > 1;
        }

        public List<BillingItem> GetDisplayedBillingItems()
        {
            var billingItems = new List<BillingItem>();

            var publisherColumnIndex = TableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList()
                .FindIndex(x => x.Text == "Publisher");

            var currencyColumnIndex = TableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList()
                .FindIndex(x => x.Text == "Currency");

            var plannedTotalsIndex = TableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList()
                .FindIndex(x => x.Text == "Planned Totals");

            foreach (var row in TableRows)
            {
                var cells = row.FindElements(By.TagName(TAG_NAME_TD));
                string publisherName = cells[publisherColumnIndex].Text;
                string currency = cells[currencyColumnIndex].Text;
                string plannedTotals = cells[plannedTotalsIndex].Text;
                var allocationDdl = GetDdlAllocationMethod(publisherName);
                string allocationMethod = GetSelectedValueFromDropDown(allocationDdl);
                var isOverride = GetChkOverride(publisherName).Selected;

                billingItems.Add(new BillingItem
                {
                    Publisher = publisherName,
                    Currency = currency,
                    PlannedTotals = plannedTotals,
                    BillingAllocationMethod = allocationMethod,
                    isOverride = isOverride
                });
            }
            return billingItems;
        }

        public void NavigateToCustomBillingPage(string publisher)
        {
            var _lnkPublisher = GetLnkPublisher(publisher);
            ScrollAndClickElement(_lnkPublisher);
        }

        public void ClickBillingExport()
        {
            ClickElement(_btnBillingExport);
            SwitchToFrame(FRAME_EXPORT);
        }
    }
}