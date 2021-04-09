using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.WorkflowTestData.Billing;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Billing
{
    public class CustomBillingPage : BasePage
    {
        private const string NXT_MTH_BTN_XPATH = "//button[@class='arrow-right arrow-right-three-cost-sources']";
        private const string PRV_MTH_BTN_XPATH = "//button[@class='arrow-left']";
        private const string SINGLE_COST_SOURCE = "1";
        private const string THREE_COST_SOURCES = "3";

        private IWebElement _ddlCurrency => FindElementByXPath("//span[./text()='Currency']/..//div[contains(@class,'select-component__control')]");
        private IList<IWebElement> _billingItemCurrencyList => FindElementsByXPath("//div[@class='billing-item-desc-info']").ToList();
        private IList<IWebElement> _colDescription => FindElementsByXPath("//div[@class='actual-billing-list']//div[text()='Description']/..//div[@class='tooltip-overflow']").ToList();
        private IList<IWebElement> _txtBilledGoals => FindElementsByXPath("//input[contains(@class,'actual-goal-input')]").ToList();
        private IList<IWebElement> _colBillingAllocation => FindElementsByXPath("//div[@class='billing-allocation-item']//input").ToList();
        private IList<IWebElement> _colTotals => FindElementsByXPath("//div[@class='grid-component-cell grid-component-cell-three-cost-sources grid-component-cell-display-block grid-component-cell-col']//div[@class='grid-component-cell grid-component-cell-body grid-component-cell-allocation']//div/span").ToList();
        private IList<IWebElement> _rowSummaryTotals => FindElementsByXPath("//div[@class='grid-component-cell grid-component-cell-three-cost-sources grid-component-cell-col grid-component-cell-light-gray-background']/div/div/span").ToList();
        private IList<IWebElement> _divBillingSplits => FindElementsByXPath("//div[@class='billing-allocation-item']").ToList();
        private IList<IWebElement> _colTotalsSingleSource => FindElementsByXPath("//div[@class='grid-component-cell grid-component-cell-single-allocation grid-component-cell-allocation-cell grid-component-cell-bold grid-component-cell-vertical-center grid-component-cell-align-right']/span").ToList();
        private IList<IWebElement> _billingSplitRows => FindElementsByXPath("//div[contains(@class,'grid-component-cell-body')]//div[@class='inline-flex']").ToList();
        private IWebElement _btnBillingSplitsNextMonth => FindElementByXPath(NXT_MTH_BTN_XPATH);
        private IWebElement _btnBillingSplitsPrevMonth => FindElementByXPath(PRV_MTH_BTN_XPATH);
        private IWebElement _msgToEditAllocationsChangeCurrencyBackToBase => FindElementByXPath("//div[@class='alertautocloseable-component alert alert-warning']");
        private IWebElement _btnSave => FindElementByXPath("//button//div[text()='Save']");
        private IWebElement _btnClose => FindElementByXPath("//button//div[text()='Close']");
        private IWebElement _lblAlert => FindElementByXPath("//div[@role='alert']");

        public CustomBillingPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SetDisplayCurrency(string displayCurrency)
        {
            if (_ddlCurrency.Text != displayCurrency)
            {
                SelectSingleValueFromReactDropdownByText(_ddlCurrency, displayCurrency);
                WaitForCurrencySelectionToLoad();
            }
        }

        public void CustomiseBillingValuesPerMonthBasedOnTestData(List<CustomBillingData> allCustomBillingData, bool isLastMonthLockEnabled)
        {
            var customBillingData = GetCustomBillingDataFromTestDataCollection(allCustomBillingData);
            var isNextButtonPresent = false;
            var monthIndex = 0;

            // Loop through each month on Custom Billing Page
            do
            {
                // Loop through billing items per month on Custom Billing Page
                for (var rowIndex = 0; rowIndex < _colDescription.Count; rowIndex++)
                {
                    // Get the corresponding item from test data
                    var billingItemName = _colDescription[rowIndex].Text;
                    var billingItemData = customBillingData.CustomBillingItems.Where(item => item.Name.Equals(billingItemName)).First();

                    if (billingItemData.CustomBillingMonths != null && monthIndex < billingItemData.CustomBillingMonths.Count)
                    {
                        // Try to get the month's billing split text boxes per row
                        var billingSplitRowTextBoxes = _billingSplitRows[rowIndex].FindElements(By.TagName("input"));
                        if (billingSplitRowTextBoxes != null && billingSplitRowTextBoxes.Any())
                        {
                            CustomBillingMonthData billingMonth;
                            switch (customBillingData.CostSources)
                            {
                                case THREE_COST_SOURCES:
                                    billingMonth = billingItemData.CustomBillingMonths[monthIndex];
                                    SetCustomBillingDataWithThreeCostSources(isLastMonthLockEnabled, billingSplitRowTextBoxes, billingMonth);
                                    break;
                            }
                        }
                    }
                }

                if (isNextButtonPresent = IsElementPresent(By.XPath(NXT_MTH_BTN_XPATH)))
                {
                    ClickElement(_btnBillingSplitsNextMonth);
                    monthIndex++;
                }
            }
            while (isNextButtonPresent);

            ClickElement(_btnSave);
            WaitForElementToBeVisible(_lblAlert);
        }

        public void VerifyTotalsValuesPerItemBasedOnTestData(List<CustomBillingData> allCustomBillingData)
        {
            EnsureMandatoryValuesForCustomBilling(allCustomBillingData);

            var customBillingData = GetCustomBillingDataFromTestDataCollection(allCustomBillingData);

            var numberOfLoops = customBillingData.CustomBillingItems.Count / 10;

            if ((customBillingData.CustomBillingItems.Count % 10) == 0)
            {
                numberOfLoops = numberOfLoops - 1;
            }

            for (int i = 1; i <= numberOfLoops; i++)
            {
                ScrollToTopOfView(_colDescription.LastOrDefault());
                WaitForLoaderSpinnerToDisappear();
            }

            var displayedData = GetDisplayedCustomBillingData(customBillingData.CostSources);

            //TODO - Handle scenario for two cost sources
            switch (customBillingData.CostSources)
            {
                case SINGLE_COST_SOURCE:
                    VerifyTotalsForSingleCostSource(customBillingData, displayedData);
                    break;
                case THREE_COST_SOURCES:
                    VerifyTotalsForThreeCostSources(customBillingData, displayedData);
                    break;
            }
        }

        private CustomBillingData GetCustomBillingDataFromTestDataCollection(List<CustomBillingData> allCustomBillingData)
        {
            var customBillingData = allCustomBillingData.Count > 1 ?
                allCustomBillingData.Where(item => item.CurrencyType == _ddlCurrency.Text).First() :
                allCustomBillingData.First();

            return customBillingData;
        }

        private void EnsureMandatoryValuesForCustomBilling(List<CustomBillingData> customBillingData)
        {
            var customBillingDataErrors = new StringBuilder();

            if (customBillingData == null || !customBillingData.Any())
            {
                customBillingDataErrors.Append("\n- No custom billing data present in the test data");
            }
            else
            {
                foreach (var item in customBillingData)
                {
                    foreach (var customItem in item.CustomBillingItems)
                    {
                        if (string.IsNullOrWhiteSpace(customItem.PlannedTotals))
                        {
                            customBillingDataErrors.Append("\n- Planned cost not specified in the test data.");
                        }

                        if (item.CostSources == THREE_COST_SOURCES && string.IsNullOrWhiteSpace(customItem.ActualTotals))
                        {
                            customBillingDataErrors.Append("\n- Actuals total cost not specified in the test data.");
                        }

                        if (item.CostSources == THREE_COST_SOURCES && string.IsNullOrWhiteSpace(customItem.PlannedTotals))
                        {
                            customBillingDataErrors.Append("\n- Planned total cost not specified in the test data.");
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(customBillingDataErrors.ToString()))
                throw new ArgumentException("The feature file " + FeatureContext.FeatureInfo.Title + $" has the following data issues:\n {customBillingDataErrors.ToString()}");
        }

        private void VerifyTotalsForSingleCostSource(CustomBillingData customBillingData, CustomBillingData displayedData)
        {
            Assert.AreEqual(customBillingData.CustomBillingItems.Count, displayedData.CustomBillingItems.Count, "Number of billing items displayed do not match test data.");

            foreach (var data in customBillingData.CustomBillingItems.Zip(displayedData.CustomBillingItems, Tuple.Create))
            {
                Assert.AreEqual(data.Item1.Name, data.Item2.Name, $"Name does not match for {data.Item1.Name}");
                Assert.AreEqual(data.Item1.PlannedTotals, data.Item2.PlannedTotals, $"Total planned do not match for {data.Item1.PlannedTotals}");
            }
        }

        private void VerifyTotalsForThreeCostSources(CustomBillingData customBillingData, CustomBillingData displayedData)
        {
            Assert.AreEqual(customBillingData.CustomBillingItems.Count, displayedData.CustomBillingItems.Count, "Number of billing items displayed do not match test data.");

            foreach (var data in customBillingData.CustomBillingItems.Zip(displayedData.CustomBillingItems, Tuple.Create))
            {
                Assert.AreEqual(data.Item1.Name, data.Item2.Name, $"Names do not match");
                Assert.AreEqual(data.Item1.ActualTotals, data.Item2.ActualTotals, $"Total actuals do not match for {data.Item1.Name}");
                Assert.AreEqual(data.Item1.FirstPartyTotals, data.Item2.FirstPartyTotals, $"Total first party do not match for {data.Item1.Name}");
                Assert.AreEqual(data.Item1.PlannedTotals, data.Item2.PlannedTotals, $"Total planned do not match for {data.Item1.Name}");
            }

            if (string.IsNullOrEmpty(customBillingData.CurrencyType) || customBillingData.CurrencyType.Contains("Base") || !customBillingData.CurrencyType.Contains("Multiple"))
            {
                Assert.AreEqual(customBillingData.SummaryActualTotals, displayedData.SummaryActualTotals, "Summary actuals do not match");
                Assert.AreEqual(customBillingData.SummaryFirstPartyTotals, displayedData.SummaryFirstPartyTotals, "Summary first party do not match");
                Assert.AreEqual(customBillingData.SummaryPlannedTotals, displayedData.SummaryPlannedTotals, "Summary planned do not match");
            }
        }

        public bool IsLockOptionPresentForTheFirstBillingSplit()
        {
            var firstBillingSplit = _divBillingSplits.First();
            bool isLockOptionPresent = IsOptionPresentInBillingSplitTextBox(firstBillingSplit, "Locked");
            return isLockOptionPresent;
        }

        private bool IsOptionPresentInBillingSplitTextBox(IWebElement element, string option)
        {
            var splitOptions = element.FindElements(By.XPath("./ul/li/a"));
            var isOptionPresent = splitOptions.Where(item => item.GetAttribute("textContent").Equals(option)).Any();
            return isOptionPresent;
        }

        private CustomBillingData GetDisplayedCustomBillingData(string costSources)
        {
            var displayedCustomBillingData = new CustomBillingData();
            var displayedBillingItems = new List<CustomBillingItem>();

            //TODO - Handle scenario for two cost sources
            switch (costSources)
            {
                case SINGLE_COST_SOURCE:

                    displayedBillingItems = GetDisplayedDataWithSingleCostSource();
                    break;

                case THREE_COST_SOURCES:

                    displayedBillingItems = GetDisplayedDataWithThreeCostSources();
                    break;
            }


            if (_rowSummaryTotals.Count() > 0)
            {
                displayedCustomBillingData.SummaryPlannedTotals = _rowSummaryTotals[0].Text;
                displayedCustomBillingData.SummaryFirstPartyTotals = _rowSummaryTotals[1].Text;
                displayedCustomBillingData.SummaryActualTotals = _rowSummaryTotals[2].Text;
            }

            displayedCustomBillingData.CustomBillingItems = displayedBillingItems;

            return displayedCustomBillingData;
        }

        private List<CustomBillingItem> GetDisplayedDataWithThreeCostSources()
        {
            int start = 0;
            var displayedBillingItems = new List<CustomBillingItem>();

            for (int i = 0; i < _colDescription.Count(); i++)
            {
                var item = new CustomBillingItem
                {
                    Name = _colDescription[i].Text,
                    Currency = _billingItemCurrencyList[i].Text
                };

                var totals = _colTotals.ToList().GetRange(start, 3);
                start = (i + 1) * 3;
                item.PlannedTotals = totals[0].Text;
                item.FirstPartyTotals = totals[1].Text;
                item.ActualTotals = totals[2].Text;
                displayedBillingItems.Add(item);
            }

            return displayedBillingItems;
        }

        private List<CustomBillingItem> GetDisplayedDataWithSingleCostSource()
        {
            var displayedBillingItems = new List<CustomBillingItem>();

            for (int i = 0; i < _colDescription.Count(); i++)
            {
                var item = new CustomBillingItem
                {
                    Name = _colDescription[i].Text,
                    Currency = _billingItemCurrencyList[i].Text,
                    PlannedTotals = _colTotalsSingleSource[i].Text
                };
                displayedBillingItems.Add(item);
            }

            return displayedBillingItems;
        }

        public void WaitForCurrencySelectionToLoad()
        {
            WaitForElementToBeVisible(By.XPath("//div[@class='alertautocloseable-component alert alert-warning']"));
        }

        private void SetCustomBillingDataWithThreeCostSources(bool isLastMonthLockEnabled, ReadOnlyCollection<IWebElement> billingSplitRowTextBoxes, CustomBillingMonthData billingMonth)
        {
            if (billingMonth.IsLastMonth)
            {
                ValidateLastMonthBehaviourWithThreeCostSources(isLastMonthLockEnabled, billingSplitRowTextBoxes, billingMonth);
            }
            else
            {
                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[0], billingMonth.Planned);
                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[1], billingMonth.FirstParty);
                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[2], billingMonth.Actual);
            }
        }

        private void ValidateLastMonthBehaviourWithThreeCostSources(bool isLastMonthLockEnabled, ReadOnlyCollection<IWebElement> billingSplitRowTextBoxes, CustomBillingMonthData billingMonth)
        {
            if (isLastMonthLockEnabled)
            {
                var isCustomOptionPresent = IsOptionPresentInBillingSplitTextBox(billingSplitRowTextBoxes[0], "Custom");
                Assert.IsFalse(isCustomOptionPresent, "Last Month Planned should NOT be customizable");

                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[1], billingMonth.FirstParty);
                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[2], billingMonth.Actual);
            }
            else
            {
                Assert.IsFalse(billingSplitRowTextBoxes[0].Enabled, "Last Month Planned should NOT be enabled");

                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[1], billingMonth.FirstParty);
                ClearInputAndTypeValueIfRequired(billingSplitRowTextBoxes[2], billingMonth.Actual);
            }
        }

        public void SetBilledGoals(List<CustomBillingData> customBillingDataList)
        {
            var customBillingData = GetCustomBillingDataFromTestDataCollection(customBillingDataList);

            // Loop through billing items per month on Custom Billing Page
            for (var rowIndex = 0; rowIndex < _colDescription.Count; rowIndex++)
            {
                // Get the corresponding item from test data
                var billingItemName = _colDescription[rowIndex].Text;
                var billingItemData = customBillingData.CustomBillingItems.First(item => billingItemName.Equals(ReplaceItemNamePlaceholderText(item.Name)));

                // Try to get billed goal textbox per row and set it
                var billedGoalTextBox = _txtBilledGoals[rowIndex];
                ClearInputAndTypeValue(billedGoalTextBox, billingItemData.BilledGoal);

                // Putting some delay here to simulate actual delay from a real person when typing in the fields
                WaitForDataToBePopulated();
            }

            ClickElement(_btnSave);
            WaitForElementToBeVisible(_lblAlert);
        }

        private string ReplaceItemNamePlaceholderText(string itemName)
        {
            const string CAMPAIGN_NAME_PLACEHOLDER = "{CampaignName}";
            const string PLACEMENT_START_DATE_PLACEHOLDER = "{PlacementStartDate}";

            var name = itemName;
            if (name.Contains(CAMPAIGN_NAME_PLACEHOLDER))
            {
                var campaignName = FeatureContext[ContextStrings.CampaignName] as string;
                name = name.Replace(CAMPAIGN_NAME_PLACEHOLDER, campaignName);
            }

            if (name.Contains(PLACEMENT_START_DATE_PLACEHOLDER))
            {
                var placementStartDate = FeatureContext[itemName] as string;
                name = name.Replace(PLACEMENT_START_DATE_PLACEHOLDER, placementStartDate);
            }

            return name;
        }
    }
}