using System;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Traffic;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class TrafficStep : BaseStep
    {
        private readonly TrafficPage _trafficPage;

        public TrafficStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _trafficPage = new TrafficPage(driver, featureContext);
        }

        [Then(@"Excluded cost items are not visible")]
        public void ThenExcludedCostItemsAreNotVisible()
        {
            _trafficPage.VerifyExcludedItemsNotVisible(WorkflowTestData.TraffickingData);
        }

        [When(@"I traffic all cost items")]
        public void TrafficAllCostItems()
        {
            _trafficPage.TrafficAllItems();
        }

        [When(@"I traffic all cost items for all AdServers")]
        [Then(@"I traffic all cost items for all AdServers")]
        public void TrafficAllCostItemsForAllAdServers()
        {
            for (int i = 0; i < WorkflowTestData.TraffickingData.Count; i++)
            {
                if (i != 0)
                {
                    _trafficPage.RefreshPage();
                }
                _trafficPage.TrafficAllAdserverItems(WorkflowTestData.TraffickingData[i].AdServer);
                Assert.IsTrue(_trafficPage.IsTrafficSuccessful(), $"Traffic not successful" + _trafficPage.GetTrafficStatusMessage());
            }
        }

        [Then(@"All cost items should be trafficked successfully")]
        public void VerifyAllCostItemsTraffickedSuccessfully()
        {
            Assert.IsTrue(_trafficPage.IsTrafficSuccessful(), $"Traffic not successful" + _trafficPage.GetTrafficStatusMessage());
        }

        [Then(@"All cost items should have correct status")]
        public void VerifyAllCostItemsStatus()
        {
            for (int i = 0; i < WorkflowTestData.TraffickingData.Count; i++)
            {
                _trafficPage.NavigateToAdserverTab(WorkflowTestData.TraffickingData[i].AdServer);
                _trafficPage.VerifySinglePlacementsItemsStatus(WorkflowTestData.SinglePlacements);
                _trafficPage.VerifyPerformancePackageItemsStatus(WorkflowTestData.PerformancePackages);
                _trafficPage.VerifySponsorshipPackageItemsStatus(WorkflowTestData.SponsorshipPackages);
            }
        }
    }
}
