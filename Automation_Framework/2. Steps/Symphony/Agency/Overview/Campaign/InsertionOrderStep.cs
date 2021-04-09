using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class InsertionOrderStep : BaseStep
    {
        private readonly string gridIoLandingPage = "Use Grid for Insertion Order Landing Page";
        private readonly string multipleIoPerPublisher = "Multiple Insertion Orders Per Publisher";
        private readonly string insertionOrderApprovalWorkflow = "IO Approval Workflow";

        private readonly ManageMultipleInsertionOrderPage _manageMultipleInsertionOrderPage;
        private readonly AddEditInsertionOrderPage _addEditInsertionOrderPage;
        private readonly ViewInsertionOrderPage _viewInsertionOrderPage;
        private readonly IssueInsertionOrderFrame _issueInsertionOrderFrame;
        private readonly AcceptInsertionOrderFrame _acceptInsertionOrderFrame;
        private readonly ManageInsertionOrderPage _manageInsertionOrderPage;
        private readonly ExportInsertionOrderFrame _exportInsertionOrderFrame;
        private readonly SendForApprovalFrame _sendForApprovalFrame;
        private readonly RejectInsertionOrderApprovalFrame _rejectInsertionOrderApprovalFrame;
        private readonly RejectInsertionOrderFrame _rejectInsertionOrderFrame;
        private readonly AddNonMediaCostItemsFrame _addNonMediaCostItemsFrame;
        private readonly AddCostItemsFrame _addCostItemsFrame;
        private readonly GridInsertionOrderLandingPage _gridInsertionOrderLandingPage;

        public InsertionOrderStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _manageMultipleInsertionOrderPage = new ManageMultipleInsertionOrderPage(driver, featureContext);
            _addEditInsertionOrderPage = new AddEditInsertionOrderPage(driver, featureContext);
            _viewInsertionOrderPage = new ViewInsertionOrderPage(driver, featureContext);
            _issueInsertionOrderFrame = new IssueInsertionOrderFrame(driver, featureContext);
            _acceptInsertionOrderFrame = new AcceptInsertionOrderFrame(driver, featureContext);
            _manageInsertionOrderPage = new ManageInsertionOrderPage(driver, featureContext);
            _exportInsertionOrderFrame = new ExportInsertionOrderFrame(driver, featureContext);
            _sendForApprovalFrame = new SendForApprovalFrame(driver, featureContext);
            _rejectInsertionOrderApprovalFrame = new RejectInsertionOrderApprovalFrame(driver, featureContext);
            _rejectInsertionOrderFrame = new RejectInsertionOrderFrame(driver, featureContext);
            _addNonMediaCostItemsFrame = new AddNonMediaCostItemsFrame(driver, featureContext);
            _addCostItemsFrame = new AddCostItemsFrame(driver, featureContext);
            _gridInsertionOrderLandingPage = new GridInsertionOrderLandingPage(driver, featureContext);

            FeatureContext.Set(false, ContextStrings.HasIoApproverRejected);
        }

        [When(@"I create insertion order")]
        public void CreateInsertionOrder()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.CreateInsertionOrder(WorkflowTestData.InsertionOrderData);
            }
            else
            {
                _manageMultipleInsertionOrderPage.ChoosePublisherAndClickCreateButton(WorkflowTestData.InsertionOrderData);
            }

            _addEditInsertionOrderPage.SaveIO();
            Assert.AreEqual("The IO has been successfully saved", _viewInsertionOrderPage.GetMsgText(), $"Something went wrong when saving the IO for {WorkflowTestData.InsertionOrderData.IOName}.");
        }

        [When(@"I submit Insertion Order for approval")]
        public void SubmitInsertionOrderForApproval()
        {
            var agencyContacts = WorkflowTestData.InsertionOrderData.InsertionOrderIssueData.AgencyContact;
            var publisherContacts = WorkflowTestData.InsertionOrderData.InsertionOrderIssueData.PublisherContact;

            _viewInsertionOrderPage.ClickSubmitButton();
            _sendForApprovalFrame.SelectAgencyContact(agencyContacts);
            _sendForApprovalFrame.SelectPublisherContact(publisherContacts);
            _sendForApprovalFrame.ClickSubmitButton();
        }

        [When(@"I approve submitted insertion order")]
        public void ApproveInsertionOrder()
        {
            _viewInsertionOrderPage.ClickApproveButton();
            Assert.AreEqual("Insertion Order Approved and Issued.", _viewInsertionOrderPage.GetMsgText());
        }

        [When(@"I reject submitted insertion order")]
        public void RejectInsertionOrder()
        {
            ClickPendingIOStatusLink();
            _viewInsertionOrderPage.ClickRejectApprovalButton();
            _rejectInsertionOrderApprovalFrame.RejectInsertionOrderApproval();
            Assert.AreEqual("The Insertion Order has been rejected by the Agency Approver", _viewInsertionOrderPage.GetMsgText());
            FeatureContext.Set(true, ContextStrings.HasIoApproverRejected);
        }

        [Then(@"The insertion order should no longer appear")]
        public void InsertionOrderDoesNotExist()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                Assert.IsFalse(_gridInsertionOrderLandingPage.DoesInsertionOrderExist(), "The insertion order was not removed");
            }
            else
            {
                Assert.IsFalse(_manageMultipleInsertionOrderPage.DoesIOExist(), "The insertion order was not removed");
            }
        }

        [When(@"I click the Pending IO status link")]
        public void ClickPendingIOStatusLink()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.ViewInsertionOrder(FeatureContext[ContextStrings.IOName].ToString());
            }
            else
            {
                _manageMultipleInsertionOrderPage.ClickIoStatusLink();
            }
        }

        [Then(@"The insertion order status should be '(.*)'")]
        public void VerifyInsertionOrderStatus(string status)
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                var isIoApprovalEnabled = IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, insertionOrderApprovalWorkflow);
                var hasIoApproverRejected = FeatureContext.Get<bool>(ContextStrings.HasIoApproverRejected);
                status = _gridInsertionOrderLandingPage.TranslateLegacyStatusToGridStatus(status, isIoApprovalEnabled, hasIoApproverRejected);
                Assert.AreEqual(status, _gridInsertionOrderLandingPage.GetInsertionOrderStatus(FeatureContext[ContextStrings.IOName].ToString()));
            }
            else if (IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, multipleIoPerPublisher))
            {
                VerifyInsertionOrderStatusOnManageMultiIOPage(status);
            }
            else
            {
                VerifyInsertionOrderStatusOnManageIOPage(status);
            }
        }

        [When(@"I sign off the insertion order as Agency")]
        public void SignOffTheIOsSuccessfullyAsAgency()
        {
            ClickPendingIOStatusLink();
            _viewInsertionOrderPage.ClickSignOffButton();
            _acceptInsertionOrderFrame.SignOffIO();
            Assert.AreEqual("The IO has been successfully signed off", _viewInsertionOrderPage.GetMsgText(), "Agency was not able to sign off.");
        }

        [When(@"I issue the insertion order")]
        [Given(@"I issue the insertion order")]
        public void IssueTheInsertionOrder()
        {
            _viewInsertionOrderPage.ClickIssueButton();
            _issueInsertionOrderFrame.SendIOToPublisher(WorkflowTestData.InsertionOrderData.InsertionOrderIssueData);
            Assert.AreEqual("The IO has been successfully issued", _viewInsertionOrderPage.GetMsgText(), "Something went wrong when IO is being issued.");
        }

        [Then(@"I cancel insertion order")]
        public void CanceInsertionOrder()
        {
            ClickPendingIOStatusLink();
            _viewInsertionOrderPage.ClickCancel();

            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.WaitForPendingInsertionOrderStatusLinkToBeDisplayed(WorkflowTestData.InsertionOrderData.IOPublisher);
            }
            else
            {
                var isPendingIOStatusLinkDisplayed = _manageInsertionOrderPage.IsPendingIOStatusLinkDisplayed(1);
                Assert.IsFalse(isPendingIOStatusLinkDisplayed, "Pending IO status link displayed. IO was not cancelled.");
            }
        }

        [When(@"the AG IO is issued")]
        public void CheckInsertionOrderStatus()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.WaitForPendingInsertionOrderStatusLinkToBeDisplayed(WorkflowTestData.InsertionOrderData.IOPublisher);
            }
            else
            {
                var isPendingIOStatusLinkDisplayed = _manageInsertionOrderPage.IsPendingIOStatusLinkDisplayed();
                Assert.IsTrue(isPendingIOStatusLinkDisplayed, "Pending IO status link not displayed after 5 attempts.");
            }
        }

        [When(@"I export the insertion order export '(.*)'")]
        public void ExportTheInsertionOrderExport(string export)
        {
            _viewInsertionOrderPage.ClickExport();
            _exportInsertionOrderFrame.DownloadIOExport(export);
        }

        [When(@"the insertion order export is exported")]
        [Then(@"the insertion order export should be exported")]
        public void InsertionOrderExportIsExported()
        {
            var msg = _exportInsertionOrderFrame.GetMsgSuccess();
            Assert.AreEqual("Please save your export file.", msg, "Export failed");
        }

        [When(@"I recall the insertion order")]
        public void RecallInsertionOrder()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                if (WorkflowTestData.InsertionOrderData.IsAutomatedGuaranteedInsertionOrder)
                {
                    _gridInsertionOrderLandingPage.ViewInsertionOrder(WorkflowTestData.InsertionOrderData.IOPublisher);
                }
                else
                {
                    _gridInsertionOrderLandingPage.ViewInsertionOrder(FeatureContext[ContextStrings.IOName].ToString());
                }
            }
            else if (IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, multipleIoPerPublisher))
            {
                _manageMultipleInsertionOrderPage.ClickIoStatusLink();
            }
            else
            {
                _manageInsertionOrderPage.ClickIOStatusLink();
            }

            _viewInsertionOrderPage.ClickRecallButton();
            Assert.AreEqual("The IO has been successfully recalled", _viewInsertionOrderPage.GetMsgText());
        }

        [When(@"I reject the insertion order as Agency")]
        public void RejectInsertionOrderAsAgency()
        {
            ClickPendingIOStatusLink();
            _viewInsertionOrderPage.ClickRejectButton();
            _rejectInsertionOrderFrame.PopulateFieldsThenClickReject();
            Assert.AreEqual("The IO has been successfully rejected", _viewInsertionOrderPage.GetMsgText());
        }

        [When(@"I include cost items in insertion order")]
        public void IncludeCostItemsInInsertionOrder()
        {
            ClickPendingIOStatusLink();
            _viewInsertionOrderPage.ClickEdit();
            _addEditInsertionOrderPage.ClickInclude();
            _addCostItemsFrame.SelectAllCostItems();
            _addCostItemsFrame.ClickSave();
            _addEditInsertionOrderPage.SaveIO();
            Assert.AreEqual("The IO has been successfully saved", _viewInsertionOrderPage.GetMsgText(), "Something went wrong when saving the IO.");
        }

        [When(@"I click on last signed off link")]
        public void ClickOnLastSignedOffLink()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                if (WorkflowTestData.InsertionOrderData.IsAutomatedGuaranteedInsertionOrder)
                {
                    _gridInsertionOrderLandingPage.ViewInsertionOrder(WorkflowTestData.InsertionOrderData.IOPublisher);
                }
                else
                {
                    _gridInsertionOrderLandingPage.ViewInsertionOrder(FeatureContext[ContextStrings.IOName].ToString());
                }
            }
            else if (IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, multipleIoPerPublisher))
            {
                _manageMultipleInsertionOrderPage.ClickLastSignedOffVersionLink();
            }
            else
            {
                _manageInsertionOrderPage.ClickLastSignedOffVersionLink();
            }
        }

        [When(@"I revise and save insertion order")]
        public void ReviseAndSaveInsertionOrder()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.ViewInsertionOrder(FeatureContext[ContextStrings.IOName].ToString());
            }
            else
            {
                _manageMultipleInsertionOrderPage.ClickReviseButton();
            }

            _addEditInsertionOrderPage.SaveIO();
            Assert.AreEqual("The IO has been successfully saved", _viewInsertionOrderPage.GetMsgText(), "Something went wrong when saving the IO.");
        }

        [When(@"I revise and check Non Media Cost in insertion order as Agency")]
        public void ReviseVerifyAndSaveInsertionOrder()
        {
            if (IsFeatureToggleEnabled(FeatureToggle.InsertionOrderGridLandingPage) && IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.InsertionOrders, gridIoLandingPage))
            {
                _gridInsertionOrderLandingPage.ViewInsertionOrder(FeatureContext[ContextStrings.IOName].ToString());
            }
            else
            {
                _manageMultipleInsertionOrderPage.ClickReviseButton();
            }

            _addEditInsertionOrderPage.ClickNonMediaCostItemsIncludedTab();
            Assert.IsTrue(_addEditInsertionOrderPage.VerifyAllNonMediaCostItemsExist(WorkflowTestData.NonMediaCostData) && _addEditInsertionOrderPage.VerifyEditOnNonMediaCostItems(WorkflowTestData.NonMediaCostData), "Failed to verify edit on non media costs");
            _addEditInsertionOrderPage.SaveIO();
            Assert.AreEqual("The IO has been successfully saved", _viewInsertionOrderPage.GetMsgText(), "Something went wrong when saving the IO.");
        }

        private void VerifyInsertionOrderStatusOnManageMultiIOPage(string status)
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
                Assert.AreEqual(status, _manageMultipleInsertionOrderPage.GetIOStatus(), "Pending IO Status mismatch.");
            }
        }

        private void VerifyInsertionOrderStatusOnManageIOPage(string status)
        {
            if (status.ToLower().Contains("datesignedoff"))
            {
                var expectedDateTime = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, AgencyTimeZoneInfo);
                var expectedDate = expectedDateTime.ToString("dd/MM/yyyy");
                Assert.IsTrue(_manageInsertionOrderPage.IsLastSignedOffVersionCorrect(expectedDate, status),
                    $"The Last Signed Off date or version is incorrect.\nDateTime.Now: {expectedDate}\nLast Signed Off version: {_manageInsertionOrderPage.GetLastSignedOffVersion()}");
            }
            else
            {
                Assert.AreEqual(status, _manageInsertionOrderPage.GetIOStatus(), "Pending IO Status mismatch.");
            }
        }
    }
}
