using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Marketplace;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class MarketplaceStep : BaseStep
    {
        private readonly MarketPlacePage _marketplacePage;
        private readonly ManageSinglePlacementModal _manageSinglePlacementModal;
        private readonly ManagePerformancePackageModal _managePerformancePackageModal;

        public MarketplaceStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _marketplacePage = new MarketPlacePage(driver, featureContext);
            _manageSinglePlacementModal = new ManageSinglePlacementModal(driver, featureContext);
            _managePerformancePackageModal = new ManagePerformancePackageModal(driver, featureContext);
        }

        [When(@"I create all AG single placements from test data")]
        public void CreateSingleAGPlacements()
        {
            var singlePlacements = WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();
            foreach (var singlePlacement in singlePlacements)
            {
                try
                {
                    _marketplacePage.SearchAgProduct<ManageSinglePlacementModal>(singlePlacement.DetailTabData.Name);
                    _manageSinglePlacementModal.CreateSinglePlacement(singlePlacement);
                    _manageSinglePlacementModal.ClickImDone();
                }
                catch (Exception e)
                {
                    Assert.Fail($"Failed creating item name '{singlePlacement.DetailTabData.Name}'.\nReason: {e.Message}\nStacktrace: {e.StackTrace}");
                }
            }
        }

        [When(@"I create all AG performance packages from test data")]
        public void CreateAGPerformancePackages()
        {
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();
            foreach (var performancePackage in performancePackages)
            {
                try
                {
                    _marketplacePage.SearchAgProduct<ManagePerformancePackageModal>(performancePackage.DetailTabData.Name);
                    _managePerformancePackageModal.CreatePerformancePackage(performancePackage);
                    _managePerformancePackageModal.ClickImDone();
                }
                catch (Exception e)
                {
                    Assert.Fail($"Failed creating item name '{performancePackage.DetailTabData.Name}'.\nReason: {e.StackTrace}");
                }
            }
        }

        [Then(@"The device filter options should be visible")]
        public void DeviceFilterOptionsVisible()
        {
            foreach (var device in WorkflowTestData.DeviceMarketplaceFilters)
            {
                Assert.IsTrue(_marketplacePage.CheckDeviceFilterShown(device), $"Failed to find device filter option '{device}' in the marketplace");
            }
        }

        [Then(@"The buy type filter options should be visible")]
        public void BuyTypeFilterOptionsVisible()
        {
            foreach (var buyType in WorkflowTestData.BuyTypeMarketplaceFilters)
            {
                Assert.IsTrue(_marketplacePage.CheckBuyTypeFiltersShown(buyType), $"Failed to find the buy type filter option '{buyType}' in the marketplace");
            }
        }
    }
}
