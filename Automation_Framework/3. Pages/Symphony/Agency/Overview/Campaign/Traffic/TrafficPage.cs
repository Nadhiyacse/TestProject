using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using Automation_Framework.DataModels.WorkflowTestData.Trafficking;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Traffic
{
    public class TrafficPage : BasePage
    {
        private IWebElement _btnTrafficAll => FindElementById("ctl00_Content_ctl01_btnTrafficAll");
        private IWebElement _trafficProgressBar => FindElementByXPath("//div[@role='progressbar']");
        private IWebElement _lblTrafficMessage => FindElementById("trafficking-message-wrapper");
        private IWebElement _tblHeaderRow => FindElementByXPath("//table[contains(@class, 'schedules fdtable fdtable-min-padding')]//thead");
        private IWebElement _lnkAdServer(string name) => FindElementByXPath($"//div[@id='ctl00_Content_ctl01_AdServerTabControl_pnlServerTabs']//a[contains(text(),'{name}')]");
        private IList<IWebElement> _lstTraffickingItems => FindElementsByCssSelector("#ctl00_Content_ctl01_gdvTrafficking tbody tr").ToList();

        public TrafficPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void VerifyExcludedItemsNotVisible(List<TraffickingData> traffickingTestData)
        {
            var errors = new StringBuilder();
            string displayedCount;
            foreach (var traffickingDataItem in traffickingTestData)
            {
                displayedCount = Regex.Match(_lnkAdServer(traffickingDataItem.AdServer).Text, @"\d+").Value;

                if (int.Parse(displayedCount) != traffickingDataItem.VisibleItemCount)
                {
                    errors.Append($"Item count does not match for {traffickingDataItem.AdServer}.\nExpected: {traffickingDataItem.VisibleItemCount}\nActual: {displayedCount}");
                }
            }
           
            if (!string.IsNullOrWhiteSpace(errors.ToString()))
                throw new ArgumentException($"Failed to verify the count of trafficking items with the following errors:\n{errors.ToString()}");
        }

        public void TrafficAllAdserverItems(string adServer)
        {
            ClickElement(_lnkAdServer(adServer));
            TrafficAllItems();
        }

        public void NavigateToAdserverTab(string adServer)
        {
            ClickElement(_lnkAdServer(adServer));
        }

        public void TrafficAllItems()
        {
            ClickElement(_btnTrafficAll);
            AcceptAlert();

            var errorMessagePanelId = "ctl00_Content_pnlErrorMessage";
            if (IsElementPresent(By.Id(errorMessagePanelId)))
            {
                Assert.Fail(FindElementById(errorMessagePanelId).Text);
            }
        }

        public bool IsTrafficSuccessful()
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1000));
            wait.Until(d => (_trafficProgressBar.GetAttribute("class").Contains("success")) || (_trafficProgressBar.GetAttribute("class").Contains("progress-bar-danger")));
            return _trafficProgressBar.GetAttribute("class").Contains("success");
        }

        public string GetProgressBarPercent()
        {
            return _trafficProgressBar.GetAttribute("style").Substring(7);
        }

        public string GetTrafficMessage()
        {
            return _lblTrafficMessage.Text;
        }

        public string GetTrafficStatusMessage()
        {
            return $"\nProgress at {GetProgressBarPercent()}.\nMessage: {GetTrafficMessage()}";
        }

        public void VerifySinglePlacementsItemsStatus(List<SinglePlacement> singlePlacements)
        {
            foreach (var singlePlacement in singlePlacements)
            {
                if (_lstTraffickingItems.Any(x => x.Text.Contains(singlePlacement.DetailTabData.Location)))
                {
                    var item = _lstTraffickingItems.First(x => x.Text.Contains(singlePlacement.DetailTabData.Location));
                    var isItemEdited = !string.IsNullOrEmpty(singlePlacement.EditData.Goal);
                    CheckItemStatus(item, isItemEdited, singlePlacement.IsCancelledItem);
                }
            }
        }

        public void VerifyPerformancePackageItemsStatus(List<PerformancePackage> performancePackages)
        {
            foreach (var performancePackage in performancePackages)
            {
                if (_lstTraffickingItems.Any(x => x.Text.Contains(performancePackage.ExpectedPackageName)))
                {
                    var item = _lstTraffickingItems.First(x => x.Text.Contains(performancePackage.ExpectedPackageName));
                    var isItemEdited = !string.IsNullOrEmpty(performancePackage.EditData.Goal);
                    CheckItemStatus(item, isItemEdited, performancePackage.IsCancelledItem);
                }
            }
        }

        public void VerifySponsorshipPackageItemsStatus(List<SponsorshipPackage> sponsorshipPackages)
        {
            foreach (var sponsorshipPackage in sponsorshipPackages)
            {
                if (_lstTraffickingItems.Any(x => x.Text.Contains(sponsorshipPackage.SponsorshipPackageName)))
                {
                    var item = _lstTraffickingItems.First(x => x.Text.Contains(sponsorshipPackage.SponsorshipPackageName));
                    var isItemEdited = sponsorshipPackage.SinglePlacements.Any(x => !string.IsNullOrEmpty(x.EditData.Goal));
                    var isCancelledItem = sponsorshipPackage.SinglePlacements.All(x => x.IsCancelledItem);
                    CheckItemStatus(item, isItemEdited, isCancelledItem);
                }
            }
        }

        private void CheckItemStatus(IWebElement actualItem, bool isItemEdited, bool isItemCancelled)
        {
            var status = actualItem.FindElement(By.XPath("(./td)[3]")).Text;
            var itemName = actualItem.FindElement(By.ClassName("wrap")).Text;

            if (isItemCancelled)
            {
                Assert.AreEqual("DEL", status, $"Incorrect status: expected - DEL, actual - {status}. Item name: {itemName}");
            }
            else if (isItemEdited)
            {
                Assert.AreEqual("UPD", status, $"Incorrect status: expected - UPD, actual - {status}. Item name: {itemName}");
            }
            else
            {
                Assert.AreEqual("NEW", status, $"Incorrect status: expected - NEW, actual - {status}. Item name: {itemName}");
            }
        }
    }
}