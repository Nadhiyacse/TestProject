using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework._3._Pages.Symphony.Publisher.Overview.Campaign.InsertionOrder;
using Automation_Framework._3._Pages.Symphony.Publisher.Overview.Campaign.InsertionOrder.Popups;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Publisher.Overview.Campaign
{
    [Binding]
    public class InsertionOrderStep : BaseStep
    {
        private readonly ManageMultipleInsertionOrderPage _manageMultipleInsertionOrderPage;
        private readonly ViewInsertionOrderPage _viewInsertionOrderPage;
        private readonly AcceptInsertionOrderFrame _acceptInsertionOrderFrame;
        private readonly RejectInsertionOrderFrame _rejectInsertionOrderFrame;

        public InsertionOrderStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _manageMultipleInsertionOrderPage = new ManageMultipleInsertionOrderPage(driver, featureContext);
            _viewInsertionOrderPage = new ViewInsertionOrderPage(driver, featureContext);
            _acceptInsertionOrderFrame = new AcceptInsertionOrderFrame(driver, featureContext);
            _rejectInsertionOrderFrame = new RejectInsertionOrderFrame(driver, featureContext);
        }

        [Then(@"The insertion order status should be '(.*)' on manage multi IO page for publisher")]
        public void VerifyInsertionOrderStatusOnManageMultiIOPageForPublisher(string status)
        {
            if (status.ToLower().Contains("datesignedoff"))
            {
                var expectedDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, AgencyTimeZoneInfo);
                var expectedDate = expectedDateTime.ToString("dd/MM/yyyy");
                Assert.IsTrue(_manageMultipleInsertionOrderPage.IsLastSignedOffVersionCorrect(expectedDate, status),
                    $"The Last Signed Off date or version is incorrect.\nDateTime.Now: {expectedDate}\nLast Signed Off version: {_manageMultipleInsertionOrderPage.GetLastSignedOffVersion()}");
            }
            else
            {
                Assert.AreEqual(status, _manageMultipleInsertionOrderPage.GetIoStatus(), "Pending IO Status mismatch.");
            }
        }

        [When(@"I sign off the insertion order as Publisher")]
        public void SignOffTheIOsSuccessfullyAsPublisher()
        {
            _manageMultipleInsertionOrderPage.ClickIoStatusLink();
            _viewInsertionOrderPage.ClickSignOffButton();
            _acceptInsertionOrderFrame.SignOffIo();
            Assert.AreEqual("The IO has been successfully signed off", _viewInsertionOrderPage.GetMsgText());
        }

        [When(@"I reject the insertion order as Publisher")]
        public void RejectInsertionOrderAsPublisher()
        {
            _manageMultipleInsertionOrderPage.ClickIoStatusLink();
            _viewInsertionOrderPage.ClickRejectButton();
            _rejectInsertionOrderFrame.PopulateFieldsThenClickReject();
            Assert.AreEqual("The IO has been successfully rejected", _viewInsertionOrderPage.GetMsgText());
        }

        [Given(@"I check non media costs in insertion order as Publisher")]
        public void HasNonMediaCostsInInsertionOrderAsPublisher()
        {
            _manageMultipleInsertionOrderPage.ClickIoStatusLink();
            _viewInsertionOrderPage.ClickNonMediaCostItemsIncludedTab();
            Assert.IsTrue(_viewInsertionOrderPage.VerifyAllNonMediaCostItemsExist(WorkflowTestData.NonMediaCostData), "Failed to verify non media costs");
        }

        [When(@"I click the Pending IO status link as Publisher")]
        public void ClickPendingIOStatusLinkAsPublisher()
        {
            _manageMultipleInsertionOrderPage.ClickIoStatusLink();
        }
    }
}
