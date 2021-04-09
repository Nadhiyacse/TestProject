using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using TechTalk.SpecFlow;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.Helpers;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class AddEditModal : BasePage
    {
        // Common Elements
        private IWebElement _btnAdd => FindElementByCssSelector(".modal-heading .btn-primary.btn-default");
        private IWebElement _btnClose => FindElementByXPath("//div[text()='Close']/ancestor::button");
        private IWebElement _btnSave => FindElementByXPath("//div[@class='button-box']/button[contains(@class,'btn-primary')]");
        private IWebElement _btnReplace => FindElementByXPath("//div[text()='Replace']/ancestor::button");
        private IWebElement _lnkAddAnother => FindElementByXPath("(//div[@class='top-alert-buttons']//div)[1]");
        private IWebElement _lnkImDone => FindElementByXPath("(//div[@class='top-alert-buttons']//div)[2]");
        private IWebElement _lblAlert => FindElementByXPath("//div[@role='alert']");

        // Common Tab Links
        private IWebElement _tabDetails => FindElementByXPath("//li[contains(@class, 'detail-tab')]//a");
        private IWebElement _tabFlighting => FindElementByXPath("//li[contains(@class, ' flighting-tab')]//a");
        private IWebElement _tabTraffic => FindElementByXPath("//li[contains(@class, 'traffic-tab')]//a");
        private IWebElement _tabCosts => FindElementByXPath("//li[contains(@class, 'costs-tab')]//a");
        private IWebElement _tabForecast => FindElementByXPath("//li[contains(@class, 'forecast-tab')]//a");
        private IWebElement _tabMore => FindElementByXPath("//li[contains(@class, 'more-tab')]//a");
        private IWebElement _tabClassification => FindElementByXPath("//li[contains(@class, 'classifications-tab')]//a");

        // Detail Tab Links
        private IWebElement _rdoGoal => FindElementByXPath("//div[text() = 'Goal']/..//span[contains(@class, 'iradio')]");
        private IWebElement _rdoRate => FindElementByXPath("(//div[text() = 'Rate']/..//span[contains(@class, 'iradio')])[1]");
        private IWebElement _rdoCost => FindElementByXPath("//div[text() = 'Cost']/..//span[contains(@class, 'iradio')]");
        private IWebElement _txtGoal => FindElementByXPath("//label[contains(text(), 'Goal')]/..//input");
        private IWebElement _txtRatecardRate => FindElementById("RateCardRate");
        private IWebElement _txtRatecardCost => FindElementById("RateCardCost");
        private IWebElement _txtBaseRate => FindElementById("BaseRate");
        private IWebElement _txtBaseCost => FindElementById("BaseCost");
        private IWebElement _txtNetRate => FindElementById("msaddedit_costs_netRate");
        private IWebElement _txtNetCost => FindElementById("msaddedit_costs_netCost");

        // Classification Tab Links
        private IWebElement _ddlClassification(string label) => FindElementByXPath($"//div[@class='classification']//label[text()='{label}']/..//div[contains(@class,'select-component__control')]");

        // Traffic Tab Links
        private IWebElement _ddlPlatform => FindElementByXPath(PLATFORM_XPATH);
        private IWebElement _ddlBuyingType => FindElementByXPath("//div[text() = 'Buying Type']/..//div[contains(@class,'select-component__control')]");
        private IWebElement _ddlCampaignObjective => FindElementByXPath("//div[text() = 'Campaign Objective']/..//div[contains(@class,'select-component__control')]");
        private IWebElement _ddlCampaignType => FindElementByXPath("//div[text() = 'Campaign Type']/..//div[contains(@class,'select-component__control')]");
        private IWebElement _rdoTrafficRate => FindElementByXPath("//*[text() = 'Rate']/ancestor::label//input[@name='trafficType']");
        private IWebElement _rdoTrafficCost => FindElementByXPath("//*[text() = 'Client Cost %']/ancestor::label//input[@name='trafficType']");

        // Costs Tab Links
        private IWebElement _rowAdjustment(string adjustmentName) => FindElementByXPath($"//div[@id='add-and-edit-tabs']//div[text()='{adjustmentName}']/ancestor::div[2]");
        private IWebElement _chkBaseCost(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"./div[{columnNumber}]//label/div[@class='checkbox-component-icon']"));
        private IWebElement _txtRate(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"./div[{columnNumber}]//div[contains(@class, 'grid-component-cell-value')]/input"));
        private IWebElement _txtCost(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"./div[{columnNumber}]//div[@class='grid-component-cell']/input"));
        private IWebElement _txtGenericCost(string costName) => FindElementByXPath($"//div[text()='{costName}']/input");
        private IWebElement _ddlType(string adjustmentName) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"./div//div[contains(@class, 'select-component__control')]"));
        private IWebElement _lblCostSummary(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"(.//div[contains(@class, 'grid-component-cell-bold')])[{columnNumber}]"));

        // Forecast Tab WebElement
        private IWebElement _txtForecastMetric(string metricName) => FindElementByXPath($"//label[text() = '{metricName}']/..//input");
        private IWebElement _rdoValue => FindElementByXPath("//div[text() = 'Value']/..//span[contains(@class, 'iradio')]");
        
        // More Tab Links
        private IWebElement _ddlPurchaseOrder => FindElementByXPath("//label[text() = 'Purchase Order']/..//div[contains(@class,'select-component__control')]");
        private IWebElement _ddlBillingSource => FindElementByXPath("//label[text() = 'Billing Source']/..//div[contains(@class,'select-component__control')]");
        private IWebElement _chkExclusion(string exclusionType) => FindElementByXPath($"//div[text() = '{exclusionType}']");

        private const string PLATFORM_XPATH = "//div[text() = 'Platform']/..//div[contains(@class,'select-component__control')]";

        private const string IMPRESSIONS_AVAILABLE_ALERT = "Great News! It looks like those impressions are available!";
        private const string IMPRESSIONS_NOT_AVAILABLE_ALERT = "impressions may not be available";
        private const string FIXED_PURCHASE_TYPE = "FIXED";

        public AddEditModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        protected void AddItem(bool isAG, PurchaseType purchaseType)
        {
            if (isAG && (purchaseType != PurchaseType.CPD))
            {
                WaitUntilAlertContainsAny(IMPRESSIONS_AVAILABLE_ALERT, IMPRESSIONS_NOT_AVAILABLE_ALERT);
            }

            WaitForElementToBeInvisible(By.XPath("//div[@class='spinner-container']"));
            ClickElement(_btnAdd);
            WaitForLoaderSpinnerToDisappear();
        }

        protected void WaitUntilAlertContains(string message)
        {
            Wait.Until(driver => _lblAlert.Displayed);
            Assert.IsTrue(_lblAlert.Text.Contains(message), $"Actual message \"{_lblAlert.Text}\" does NOT contain the string \"{message}\"");
        }

        protected void WaitUntilAlertContainsAny(params string[] messages)
        {
            Wait.Until(driver => _lblAlert.Displayed);

            foreach (var message in messages)
            {
                if (_lblAlert.Text.Contains(message))
                    return;
            }

            Assert.Fail($"Actual message \"{_lblAlert.Text}\"");
        }

        public void ClickImDone()
        {
            Wait.Until(driver => _lnkImDone.Displayed);
            ClickElement(_lnkImDone);
            WaitForLoaderSpinnerToDisappear();
        }

        public void ClickAddAnother()
        {
            Wait.Until(driver => IsElementPresent(By.XPath("//div[@class='top-alert-buttons']//div[1]")));
            ClickElement(_lnkAddAnother);
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
            WaitForLoaderSpinnerToDisappear();
        }

        public void ClickSave()
        {
            ClickElement(_btnSave);
        }

        public void ClickReplace()
        {
            ClickElement(_btnReplace);
        }

        public void SaveItem()
        {
            WaitForElementToBeInvisible(By.XPath("//div[@class='spinner-container']"));
            ClickSave();

            try
            {
                WaitForElementToBeInvisible(By.XPath("//div[@class='modal-body']"));
            }
            catch
            {
                Assert.Fail($"Error saving item: '{_lblAlert.Text}'");
            }
        }

        public void ClickTab(string tabName)
        {
            switch (tabName)
            {
                case "Details":
                    ScrollAndClickElement(_tabDetails);
                    break;
                case "Flighting":
                    ScrollAndClickElement(_tabFlighting);
                    break;
                case "Traffic":
                    ScrollAndClickElement(_tabTraffic);
                    break;
                case "Costs":
                    ScrollAndClickElement(_tabCosts);
                    break;
                case "Forecast":
                    ScrollAndClickElement(_tabForecast);
                    break;
                case "More":
                    ScrollAndClickElement(_tabMore);
                    break;
                case "Classifications":
                    ScrollAndClickElement(_tabClassification);
                    break;
            }
        }

        protected void ValidateAutoCalculatedField(DetailTabData detailTabData)
        {
            var isAutoCalculatedFieldMatch = true;
            var errorMsg = new StringBuilder();
            var purchaseType = ConvertToPurchaseTypeEnum(detailTabData.PurchaseType);

            if (detailTabData.PurchaseType != null && detailTabData.PurchaseType.Equals(FIXED_PURCHASE_TYPE, StringComparison.OrdinalIgnoreCase))
            {
                if (!string.IsNullOrWhiteSpace(detailTabData.Goal) && purchaseType.IsGoalEditableForPurchaseType())
                {
                    ClearInputAndTypeValue(_txtGoal, detailTabData.Goal);
                }

                ClearInputAndTypeValue(_txtBaseCost, detailTabData.BaseCost);
                ClearInputAndTypeValueIfRequired(_txtRatecardCost, detailTabData.RatecardCost);

                return;
            }

            switch (detailTabData.AutoCalculateField.ToLower())
            {
                case "goal":
                    ScrollAndClickElement(_rdoGoal);
                    ClearInputAndTypeValue(_txtBaseRate, detailTabData.BaseRate);
                    ClearInputAndTypeValue(_txtBaseCost, detailTabData.BaseCost);
                    ClearInputAndTypeValueIfRequired(_txtRatecardRate, detailTabData.RatecardRate);
                    ClearInputAndTypeValueIfRequired(_txtRatecardCost, detailTabData.RatecardCost);
                    try
                    {
                        Wait.Until(driver => _txtGoal.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.Goal));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        isAutoCalculatedFieldMatch = false;
                        errorMsg.Append($"Actual Goal: {_txtGoal.GetAttribute("value").Replace(",", string.Empty)} does NOT contain: {detailTabData.Goal}");
                    }
                    break;

                case "rate":
                    ScrollAndClickElement(_rdoRate);
                    if (!string.IsNullOrWhiteSpace(detailTabData.Goal) && purchaseType.IsGoalEditableForPurchaseType())
                    {
                        ClearInputAndTypeValue(_txtGoal, detailTabData.Goal);
                    }
                    ClearInputAndTypeValue(_txtBaseCost, detailTabData.BaseCost);
                    ClearInputAndTypeValueIfRequired(_txtRatecardCost, detailTabData.RatecardCost);
                    try
                    {
                        Wait.Until(driver => _txtBaseRate.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.BaseRate));
                        Wait.Until(driver => _txtRatecardRate.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.RatecardRate));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        isAutoCalculatedFieldMatch = false;
                        errorMsg.Append($"Actual Base Rate or Ratecard Rate does NOT contain expected values:");
                        errorMsg.Append($"\nActual Base Rate: {_txtBaseRate.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.BaseRate}");
                        errorMsg.Append($"\nActual Ratecard Rate: {_txtRatecardRate.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.RatecardRate}");
                    }
                    break;

                case "cost":
                    ScrollAndClickElement(_rdoCost);
                    if (!string.IsNullOrWhiteSpace(detailTabData.Goal) && purchaseType.IsGoalEditableForPurchaseType())
                    {
                        ClearInputAndTypeValue(_txtGoal, detailTabData.Goal);
                    }
                    ClearInputAndTypeValue(_txtBaseRate, detailTabData.BaseRate);
                    ClearInputAndTypeValueIfRequired(_txtRatecardRate, detailTabData.RatecardRate);
                    try
                    {
                        Wait.Until(driver => _txtBaseCost.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.BaseCost));
                        Wait.Until(driver => _txtRatecardCost.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.RatecardCost));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        isAutoCalculatedFieldMatch = false;
                        errorMsg.Append($"Actual Base Cost or Ratecard Cost does NOT contain expected values:");
                        errorMsg.Append($"\nActual Base Cost: {_txtBaseCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.BaseCost}");
                        errorMsg.Append($"\nActual Ratecard Cost: {_txtRatecardCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.RatecardCost}");
                    }
                    break;
            }

            if (!isAutoCalculatedFieldMatch)
            {
                Assert.Fail(errorMsg.ToString());
            }
        }

        protected void ValidateReverseCalculateFromNetFields(DetailTabData detailTabData)
        {
            if (detailTabData.NetCost == null || detailTabData.NetRate == null)
                return;

            if (!IsElementPresent(By.Id("msaddedit_costs_netCost")) || !IsElementPresent(By.Id("msaddedit_costs_netRate")))
                return;

            var errorMsg = new StringBuilder();
            switch (detailTabData.AutoCalculateField.ToLower())
            {
                case "rate":
                    ClearInputAndTypeValue(_txtNetCost, detailTabData.NetCost);
                    try
                    {
                        Wait.Until(driver => _txtNetRate.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.NetRate));
                        Wait.Until(driver => _txtBaseCost.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.BaseCost));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        errorMsg.Append($"Actual Net Rate or Base Cost does NOT contain expected values:");
                        errorMsg.Append($"\nActual Net Rate: {_txtRatecardCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.NetRate}");
                        errorMsg.Append($"\nActual Base Cost: {_txtBaseCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.BaseCost}");
                    }
                    break;

                case "cost":
                    ClearInputAndTypeValue(_txtNetRate, detailTabData.NetRate);
                    try
                    {
                        Wait.Until(driver => _txtNetRate.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.NetCost));
                        Wait.Until(driver => _txtBaseCost.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.BaseCost));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        errorMsg.Append($"Actual Net Cost or Base Cost does NOT contain expected values:");
                        errorMsg.Append($"\nActual Net Rate: {_txtRatecardCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.NetCost}");
                        errorMsg.Append($"\nActual Base Cost: {_txtBaseCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.BaseCost}");
                    }
                    break;

                case "goal":
                    ClearInputAndTypeValue(_txtNetRate, detailTabData.NetRate);
                    ClearInputAndTypeValue(_txtNetCost, detailTabData.NetCost);
                    try
                    {
                        Wait.Until(driver => _txtBaseCost.GetAttribute("value").Replace(",", string.Empty).Contains(detailTabData.BaseCost));
                    }
                    catch (WebDriverTimeoutException)
                    {
                        errorMsg.Append($"Actual Base Cost does NOT contain expected values:");
                        errorMsg.Append($"\nActual Base Cost: {_txtBaseCost.GetAttribute("value").Replace(",", string.Empty)}\nExpected: {detailTabData.BaseCost}");
                    }
                    break;
            }
        }

        public void SetClassificationTabData(ClassificationTabData classificationTabData)
        {
            if (classificationTabData == null)
                return;

            if (classificationTabData.Classifications == null || !classificationTabData.Classifications.Any())
                throw new NotFoundException($"Classifications from Classification tab data is missing in test data");

            ClickTab("Classifications");

            foreach (var classification in classificationTabData.Classifications)
            {
                SelectSingleValueFromReactDropdownByText(_ddlClassification(classification.Label), classification.Option);
            }
        }

        public void VerifyClassificationTabData(ClassificationTabData classificationTabData)
        {
            if (classificationTabData == null)
                return;

            if (classificationTabData.Classifications == null || !classificationTabData.Classifications.Any())
                throw new NotFoundException($"Classifications from Classification tab data is missing in test data");

            foreach (var classification in classificationTabData.Classifications)
            {
                Assert.IsTrue(GetSelectedValueFromDropDown(_ddlClassification(classification.Label), true).ToString().Equals(classification.Option.ToString()), $"The {classification} is not set in the classification tab");
            }
        }

        protected void SetFlightingTabData(FlightingTabData flightingTabData)
        {
        }

        protected void SetTrafficTabData(TrafficTabData trafficTabData)
        {
            if (trafficTabData == null)
                return;

            ClickTab("Traffic");

            if (!string.IsNullOrEmpty(trafficTabData.Platform) && _ddlPlatform.Text != trafficTabData.Platform)
            {
                SelectSingleValueFromReactDropdownByText(PLATFORM_XPATH, trafficTabData.Platform);
            }
        }

        protected void SetCostTabData(CostsTabData costTabData)
        {
            if (costTabData == null)
                return;

            ClickTab("Costs");

            if (costTabData.ClientAdjustments != null)
            {
                SetAdjustments(costTabData.ClientAdjustments);
            }

            if (costTabData.VendorAdjustments != null)
            {
                SetAdjustments(costTabData.VendorAdjustments, true);
            }
        }

        public void SetAdjustments(List<Adjustment> adjustments, bool isVendorAdjustment = false)
        {
            var columnNumber = isVendorAdjustment ? 3 : 5;

            foreach (var adjustment in adjustments)
            {
                if (adjustment.BaseCost)
                {
                    SetReactCheckBoxState(_chkBaseCost(adjustment.AdjustmentName, columnNumber - 1), adjustment.BaseCost);
                }

                ClearInputAndTypeValueIfRequired(_txtRate(adjustment.AdjustmentName, columnNumber), adjustment.Rate.ToString());
                ClearInputAndTypeValueIfRequired(_txtCost(adjustment.AdjustmentName, columnNumber), adjustment.Cost.ToString());

                if (!string.IsNullOrEmpty(adjustment.Type))
                {
                    SelectSingleValueFromReactDropdownByText(_ddlType(adjustment.AdjustmentName), adjustment.Type);
                }
            }
        }

        public void SetCost(string costType, CostsTabData costsTabData)
        {
            ClearInputAndTypeValue(_txtGenericCost(costType), costsTabData.Cost.Client);
        }

        protected void SetForecastTabData(ForecastTabData forecastTabData)
        {
            if (forecastTabData == null)
                return;

            ClickTab("Forecast");

            ScrollAndClickElement(_rdoValue);

            ClearInputAndTypeValueIfRequired(_txtForecastMetric("Est.Clicks."), forecastTabData.EstClicks);
            ClearInputAndTypeValueIfRequired(_txtForecastMetric("Est.Acqs."), forecastTabData.EstAcqs);
            ClearInputAndTypeValueIfRequired(_txtForecastMetric("Est.Views."), forecastTabData.EstViews);

            Wait.Until(driver => _txtForecastMetric("Est.VTR%").GetAttribute("value").Equals(forecastTabData.EstVtrPercentage));
        }

        protected void SetMoreTabData(MoreTabData moreTabData)
        {
            if (moreTabData == null)
                return;

            ClickTab("More");

            if (!string.IsNullOrEmpty(moreTabData.PurchaseOrder))
            {
                SelectSingleValueFromReactDropdownByText(_ddlPurchaseOrder, moreTabData.PurchaseOrder);
            }

            if (!string.IsNullOrEmpty(moreTabData.BillingSource))
            {
                SelectSingleValueFromReactDropdownByText(_ddlBillingSource, moreTabData.BillingSource);
            }

            if (moreTabData.Exclusions != null && moreTabData.Exclusions.Count > 0)
            {
                foreach (var exclusion in moreTabData.Exclusions)
                {
                    SetReactCheckBoxState(_chkExclusion(exclusion.Name), exclusion.Enabled);
                }
            }
        }

        public void VerifyForecastValues(ForecastTabData forecastTabData)
        {
            if (forecastTabData == null)
                return;
            Assert.AreEqual(_txtForecastMetric("Est.Imps.").GetAttribute("value"), forecastTabData.EstImps);
            Assert.AreEqual(_txtForecastMetric("Est.Clicks.").GetAttribute("value"), forecastTabData.EstClicks);
            Assert.AreEqual(_txtForecastMetric("Est.Acqs.").GetAttribute("value"), forecastTabData.EstAcqs);
            Assert.AreEqual(_txtForecastMetric("Est.Views.").GetAttribute("value"), forecastTabData.EstViews);

            Assert.AreEqual(_txtForecastMetric("Est.CTR%").GetAttribute("value"), forecastTabData.EstCtrPercentage);
            Assert.AreEqual(_txtForecastMetric("Est.CVR%").GetAttribute("value"), forecastTabData.EstCvrPercentage);
            Assert.AreEqual(_txtForecastMetric("Est.VTR%").GetAttribute("value"), forecastTabData.EstVtrPercentage);
  
            Assert.AreEqual(_txtForecastMetric("Est. CPM").GetAttribute("value"), forecastTabData.EstCpm);
            Assert.AreEqual(_txtForecastMetric("Est. CPC").GetAttribute("value"), forecastTabData.EstCpc);
            Assert.AreEqual(_txtForecastMetric("Est. CPA").GetAttribute("value"), forecastTabData.EstCpa);
            Assert.AreEqual(_txtForecastMetric("Est. CPV").GetAttribute("value"), forecastTabData.EstCpv);
        }

        public bool VerifyCostSummary(CostSummary costSummary)
        {
            return (_lblCostSummary(costSummary.CostSummaryName, 2).GetAttribute("value") != (costSummary.Agency.ToString()) || _lblCostSummary(costSummary.CostSummaryName, 3).GetAttribute("value") != (costSummary.Client.ToString()));
        }
    }
}
