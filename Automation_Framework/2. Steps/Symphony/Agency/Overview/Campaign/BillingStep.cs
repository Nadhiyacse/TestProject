using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Billing;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Billing.Popups;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class BillingStep : BaseStep
    {
        private readonly BillingPage _billingPage;
        private readonly CustomBillingPage _customBillingPage;
        private readonly ExportBillingFrame _exportBillingFrame;

        public BillingStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _billingPage = new BillingPage(driver, featureContext);
            _customBillingPage = new CustomBillingPage(driver, featureContext);
            _exportBillingFrame = new ExportBillingFrame(driver, featureContext);
        }

        [When(@"I select (.*) from the currency dropdown on the (.*) for the country (.*)")]
        public void SelectFromTheCurrencyDropdown(string currency, string page, string country)
        {
            if (currency.ToLower() == "base")
            {
                string baseCurrency = WorkflowTestData.CampaignData.ExchangeRatesData.ExchangeRateData.Where(item => item.Country == country).First().BaseCurrency;
                currency = currency + $" ({baseCurrency})";
            }

            switch (page)
            {
                case "Billing Landing page":
                    _billingPage.SetDisplayCurrency(currency);
                    break;
                case "Custom Billing page":
                    _customBillingPage.SetDisplayCurrency(currency);
                    break;
            }
        }

        [When(@"I open the Custom Billing page for publisher (.)")]
        public void OpenTheCustomBillingPageForPublisher(string publisher)
        {
            ScenarioContext.Current.Pending();
        }

        [Then(@"The values per publisher should be based on test data")]
        public void ValuesPerPublisherShouldBeBasedOnTestData()
        {
            _billingPage.CorrectBillingItemsPresent(WorkflowTestData.BillingData);
        }

        [When(@"I override the billing allocation method per publisher based on test data")]
        public void OverrideBillingAllocationMethodPerPublisher()
        {
            _billingPage.OverrideBillingAllocationForEachPublisher(WorkflowTestData.BillingData);
        }

        [Then(@"The totals values per item are as per test data")]
        public void VerifyValuesPerItem()
        {
            _customBillingPage.VerifyTotalsValuesPerItemBasedOnTestData(WorkflowTestData.CustomBillingData);
        }

        [When(@"I open the Custom Billing page for the first publisher displayed on the UI")]
        public void OpenTheCustomBillingPageForFirstPublisherDisplayedOnUI()
        {
            var firstPublisher = _billingPage.GetDisplayedBillingItems().First().Publisher;
            _billingPage.NavigateToCustomBillingPage(firstPublisher);
        }

        [Then(@"I should not be able to lock or unlock billing splits")]
        public void IsUnableToLockOrUnlockBillingSplits()
        {
            bool isLockOptionPresent = _customBillingPage.IsLockOptionPresentForTheFirstBillingSplit();
            Assert.IsFalse(isLockOptionPresent);
        }

        [When(@"I export the Billing export '(.*)' with delivery method as '(.*)'")]
        public void WhenIExportTheBillingExport(string exportType, string deliveryMethod)
        {
            _billingPage.ClickBillingExport();
            _exportBillingFrame.DeliverBillingExport(exportType, deliveryMethod, WorkflowTestData.AgencyLoginUserData.Username);
        }

        [Then(@"the Billing export should be exported")]
        public void ThenTheBillingExportShouldBeExported()
        {
            var msg = _exportBillingFrame.GetSuccessMessage();
            Assert.AreEqual("Please save your export file.", msg);
        }

        [Then(@"the Billing export should be delivered")]
        public void ThenTheBillingExportShouldBeDelivered()
        {
            var msg = _exportBillingFrame.GetSuccessMessage();
            Assert.AreEqual("Email delivery succeeded", msg);
        }

        [When(@"I customise my billing values based on test data")]
        public void WhenICustomiseMyBillingValuesBasedOnTestData()
        {
            var isLastMonthLockEnabled = AgencySetupData.SymphonyAdminData.Agencies.First().Features.Billing.First(feature => feature.Name.Equals("Last Month Lock")).Enabled;
            _customBillingPage.CustomiseBillingValuesPerMonthBasedOnTestData(WorkflowTestData.CustomBillingData, isLastMonthLockEnabled);
        }

        [When(@"I enter my Billed Goals based on test data")]
        public void SetBilledGoalsBasedOnTestData()
        {
            _customBillingPage.SetBilledGoals(WorkflowTestData.CustomBillingData);
        }
    }
}
