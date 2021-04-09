using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Overview.Campaign
{
    [Binding]
    public class MediaScheduleStep : BaseStep
    {
        private readonly string enableNMCActionBarButton = "NMC: Enable Action Bar and Campaign Non-Media Cost";
        private readonly string hideClassificationTabForNMC = "Hide classifications tab for non-media costs";

        private readonly MediaSchedulePage _mediaSchedulePage;
        private readonly MediaScheduleGridPage _mediaScheduleGridPage;
        private readonly OtherCostsPage _otherCostsPage;
        private readonly ManageSinglePlacementModal _manageSinglePlacementModal;
        private readonly ManagePerformancePackageModal _managePerformancePackageModal;
        private readonly ManageSponsorshipPackageModal _manageSponsorshipPackageModal;
        private readonly ConfirmMediaSchedulePopUp _confirmMediaSchedulePopUp;
        private readonly BeginSignoffFrame _beginSignoffFrame;
        private readonly ApproveMediaScheduleFrame _approveMediaScheduleFrame;
        private readonly ExportMediaScheduleFrame _exportMediaScheduleFrame;
        private readonly ImportMediaScheduleModal _importMediaScheduleModal;
        private readonly ConfirmCancellationFrame _confirmCancellationFrame;
        private readonly RestoreSponsorshipFrame _restoreSponsorshipFrame;
        private readonly AddOtherCostFrame _addOtherCostFrame;
        private readonly SendMediaScheduleItemsForApprovalModal _sendMediaScheduleItemsForApprovalModal;
        private readonly HistoryLineItemStatusFrame _historyLineItemStatusFrame;
        private readonly AddEditNonMediaCostModal _addEditNonMediaCostModal;
        private readonly AddEditModal _addEditModal;
        private readonly CreateSponsorshipPackageModal _createSponsorshipPackageModal;
        private readonly ManageColumnsModal _manageColumnsModal;
        private readonly VersionHistoryModal _versionHistoryModal;

        private string duplicatedSinglePlacementName => WorkflowTestData.SinglePlacements.First(sp => sp.IsDuplicateReplaceItem).DetailTabData.Name;
        private string duplicatedNonMediaCostItemName => WorkflowTestData.NonMediaCostData.First(nmc => nmc.IsDuplicateItem).Name;
        private string sponsorshipPackageToDuplicateHeaderName => WorkflowTestData.SponsorshipPackages.First(sp => sp.IsDuplicateReplaceItem).SponsorshipPackageName;
        private string duplicatedSponsorshipPackageHeaderName;
        private string duplicatedPerformancePackageHeaderName => WorkflowTestData.PerformancePackages.First(pp => pp.IsDuplicateReplaceItem).ExpectedPackageName;
        private string duplicatedSponsorshipPackageChildItemName => WorkflowTestData.SponsorshipPackages.First(sp => sp.IsDuplicateReplaceItem).SinglePlacements.First(sp => sp.IsDuplicateReplaceItem).DetailTabData.Name;
        private string duplicatedPerformancePackageChildItemName => WorkflowTestData.PerformancePackages.First(pp => pp.IsDuplicateReplaceItem).PlacementsTabData.Placements.First(p => p.isDuplicateReplaceItem).PlacementName;
        public IEnumerable<string> multipleSinglePlacementNames => WorkflowTestData.SinglePlacements.Skip(3).Take(2).Select(sp => sp.DetailTabData.Name);
        public IEnumerable<string> multiplePerformancePackageNames => WorkflowTestData.PerformancePackages.Skip(3).Take(3).Select(sp => sp.DetailTabData.Name);
        public IEnumerable<string> multiplePerformancePackageChildNames => WorkflowTestData.PerformancePackages.Skip(6).First().PlacementsTabData.Placements.Take(2).Select(p => p.PlacementName);
        public IEnumerable<string> multipleSponsorshipPackageChildNames => WorkflowTestData.SponsorshipPackages.Skip(3).First().SinglePlacements.Take(1).Select(sp => sp.DetailTabData.Name);
        private EditData singlePlacementReplaceData => WorkflowTestData.SinglePlacements.First(p => p.DetailTabData.Name == duplicatedSinglePlacementName).EditData;
        private EditData performancePackageHeaderReplaceData => WorkflowTestData.PerformancePackages.First(pp => pp.ExpectedPackageName == duplicatedPerformancePackageHeaderName).EditData;
        private EditData sponsorshipPackageHeaderReplaceData => WorkflowTestData.SponsorshipPackages.First(sp => sp.SponsorshipPackageName == sponsorshipPackageToDuplicateHeaderName).EditData;
        private EditData performancePackageChildItemReplaceData => WorkflowTestData.PerformancePackages.First(pp => pp.IsDuplicateReplaceItem).PlacementsTabData.Placements.First(p => p.isDuplicateReplaceItem).EditData;
        private EditData sponsorshipPackageChildItemReplaceData => WorkflowTestData.SponsorshipPackages.First(sp => sp.IsDuplicateReplaceItem).SinglePlacements.First(sp => sp.IsDuplicateReplaceItem).EditData;
        private List<PerformancePackage> nonConvertedPerformancePackages => WorkflowTestData.PerformancePackages.Where(p => !p.IsItemGoingToConvertFromRfpToAg && !p.IsAutomatedGuaranteedItem && !p.IsImportedItem).ToList();

        private List<SinglePlacement> nonConvertedSinglePlacements { get { return WorkflowTestData.SinglePlacements.Where(p => !p.IsItemGoingToConvertFromRfpToAg && !p.IsAutomatedGuaranteedItem && !p.IsImportedItem).ToList(); } }

        public MediaScheduleStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _mediaSchedulePage = new MediaSchedulePage(driver, featureContext);
            _mediaScheduleGridPage = new MediaScheduleGridPage(driver, featureContext);
            _otherCostsPage = new OtherCostsPage(driver, featureContext);
            _manageSinglePlacementModal = new ManageSinglePlacementModal(driver, featureContext);
            _managePerformancePackageModal = new ManagePerformancePackageModal(driver, featureContext);
            _manageSponsorshipPackageModal = new ManageSponsorshipPackageModal(driver, featureContext);
            _confirmMediaSchedulePopUp = new ConfirmMediaSchedulePopUp(driver, featureContext);
            _beginSignoffFrame = new BeginSignoffFrame(driver, featureContext);
            _approveMediaScheduleFrame = new ApproveMediaScheduleFrame(driver, featureContext);
            _exportMediaScheduleFrame = new ExportMediaScheduleFrame(driver, featureContext);
            _importMediaScheduleModal = new ImportMediaScheduleModal(driver, featureContext);
            _confirmCancellationFrame = new ConfirmCancellationFrame(driver, featureContext);
            _restoreSponsorshipFrame = new RestoreSponsorshipFrame(driver, featureContext);
            _addOtherCostFrame = new AddOtherCostFrame(driver, featureContext);
            _sendMediaScheduleItemsForApprovalModal = new SendMediaScheduleItemsForApprovalModal(driver, featureContext);
            _historyLineItemStatusFrame = new HistoryLineItemStatusFrame(driver, featureContext);
            _addEditNonMediaCostModal = new AddEditNonMediaCostModal(driver, featureContext);
            _addEditModal = new AddEditModal(driver, featureContext);
            _createSponsorshipPackageModal = new CreateSponsorshipPackageModal(driver, featureContext);
            _manageColumnsModal = new ManageColumnsModal(driver, featureContext);
            _versionHistoryModal = new VersionHistoryModal(driver, featureContext);
        }

        [When(@"I create all single placements from test data")]
        public void CreateSinglePlacements()
        {
            var singlePlacementsToAdd = WorkflowTestData.SinglePlacements.Where(p => !p.IsImportedItem).ToList();

            var sponsorshipPackagesToUpdate = WorkflowTestData.SponsorshipPackages.Where(p => !p.IsImportedItem);

            foreach (var sponsorshipPackage in sponsorshipPackagesToUpdate)
            {
                foreach (var singlePlacement in sponsorshipPackage.SinglePlacements)
                {
                    singlePlacement.DetailTabData.Publisher = sponsorshipPackage.Publisher;
                    singlePlacementsToAdd.Add(singlePlacement);
                }
            }

            _mediaScheduleGridPage.ClickAddSinglePlacement();

            FeatureContext[ContextStrings.ExpectedDateTimeStart] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, AgencyTimeZoneInfo);

            foreach (var singlePlacement in singlePlacementsToAdd)
            {
                _manageSinglePlacementModal.CreateSinglePlacement(singlePlacement);
                _manageSinglePlacementModal.ClickAddAnother();
            }

            FeatureContext[ContextStrings.ExpectedDateTimeEnd] = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, AgencyTimeZoneInfo);

            _manageSinglePlacementModal.ClickClose();
        }

        [When(@"I create all single placements that will convert from RFP to AG from test data")]
        public void ConvertAllSinglePlacementsFromRfpToAg()
        {
            _mediaScheduleGridPage.ClickAddSinglePlacement();

            foreach (var singlePlacement in WorkflowTestData.SinglePlacements.Where(p => p.IsItemGoingToConvertFromRfpToAg))
            {
                _manageSinglePlacementModal.CreateSinglePlacement(singlePlacement);
                _manageSinglePlacementModal.ClickAddAnother();
            }

            _manageSinglePlacementModal.ClickClose();
        }

        [When(@"I create all single placements that will not convert from RFP to AG from test data")]
        public void CreateRfpSinglePlacements()
        {
            _mediaScheduleGridPage.ClickAddSinglePlacement();

            foreach (var singlePlacement in nonConvertedSinglePlacements)
            {
                _manageSinglePlacementModal.CreateSinglePlacement(singlePlacement);
                _manageSinglePlacementModal.ClickAddAnother();
            }

            _manageSinglePlacementModal.ClickClose();
        }

        [When(@"I create all performance packages from test data")]
        public void CreatePerformancePackages()
        {
            foreach (var performancePackage in WorkflowTestData.PerformancePackages.Where(p => !p.IsImportedItem))
            {
                _mediaScheduleGridPage.ClickAddPerformancePackage();
                AddPerformancePackage(performancePackage);
            }
        }

        [When(@"I create all performance packages that will convert from RFP to AG from test data")]
        public void ConvertAllPerformancePackagesFromRfpToAg()
        {
            foreach (var performancePackage in WorkflowTestData.PerformancePackages.Where(p => p.IsItemGoingToConvertFromRfpToAg))
            {
                _mediaScheduleGridPage.ClickAddPerformancePackage();
                AddPerformancePackage(performancePackage);
            }
        }

        [When(@"I create all performance packages that will not convert from RFP to AG from test data")]
        public void CreateRfpPerformancePackages()
        {
            foreach (var performancePackage in nonConvertedPerformancePackages)
            {
                _mediaScheduleGridPage.ClickAddPerformancePackage();
                AddPerformancePackage(performancePackage);
            }
        }

        [When(@"I create all sponsorship packages from test data")]
        public void CreateSponsorshipPackages()
        {
            foreach (var sponsorshipPackage in WorkflowTestData.SponsorshipPackages.Where(p => !p.IsImportedItem))
            {
                foreach (var singlePlacement in sponsorshipPackage.SinglePlacements)
                {
                    _mediaScheduleGridPage.SelectSinglePlacement(singlePlacement.DetailTabData.Name, true);
                }

                _mediaScheduleGridPage.ClickCreateSimpleSponsorshipPackage();
                CreateSponsorshipPackage(sponsorshipPackage.SponsorshipPackageName);

                Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());

                _mediaScheduleGridPage.DismissAllToastNotifications();

                // Deselecting placements
                _mediaScheduleGridPage.ExpandPackageWithName(sponsorshipPackage.SponsorshipPackageName);
                foreach (var singlePlacement in sponsorshipPackage.SinglePlacements)
                {
                    _mediaScheduleGridPage.SelectSinglePlacement(singlePlacement.DetailTabData.Name, false);
                }
            }
        }

        [When(@"I confirm all items")]
        public void ConfirmAllItems()
        {
            _mediaScheduleGridPage.SelectAllMediaScheduleItems();
            _mediaScheduleGridPage.ClickConfirm();
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I approve all items")]
        public void ApproveAllItems()
        {
            _mediaScheduleGridPage.SelectCampaignStatusDropdownAndOption("Approve");
            bool isSuccess = _approveMediaScheduleFrame.ApproveMediaScheduleSuccessfully(WorkflowTestData.MediaScheduleData.CampaignApprovalData);
            Assert.IsTrue(isSuccess, "Failed to approve media schedule.");
        }

        [When(@"I submit for approval")]
        public void SubmitForApproval()
        {
            _mediaScheduleGridPage.SelectCampaignStatusDropdownAndOption("Submit for Approval");

            if (AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleApproval.EnableContactEmailNotification)
            {
                _sendMediaScheduleItemsForApprovalModal.SelectApproversAndConfirm(WorkflowTestData.MediaScheduleData.CampaignApprovalData);
            }
        }

        [Then(@"The approval status is (.*)")]
        public void ApprovalStatusIs(string status)
        {
            _mediaScheduleGridPage.CheckApprovalStatus(status);
        }

        [When(@"I approve the items as (.*)")]
        public void ApproveTheItemsAs(string approver)
        {
            _mediaScheduleGridPage.SelectCampaignStatusDropdownAndOption("Approve");
            bool isSuccessful = _approveMediaScheduleFrame.ApproveMediaScheduleSuccessfully(WorkflowTestData.MediaScheduleData.CampaignApprovalData, approver);
            Assert.IsTrue(isSuccessful, "Failed to approve media schedule");
        }

        [When(@"I approve the version (.*) of media schedule items")]
        public void ApproveTheItemsWithVersionNumber(string versionNumber)
        {
            _mediaSchedulePage.ClickApproveButton();
            bool isSuccessful = _approveMediaScheduleFrame.ApproveMediaScheduleSuccessfully(WorkflowTestData.MediaScheduleData.CampaignApprovalData, "none", int.Parse(versionNumber));
            Assert.IsTrue(isSuccessful, "Failed to approve media schedule");
        }

        [When(@"I confirm all AG items")]
        public void ConfirmAllAGItems()
        {
            _mediaScheduleGridPage.SelectAllMediaScheduleItems();
            _mediaScheduleGridPage.ClickConfirm();
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I signoff all items")]
        public void SignoffAllItems()
        {
            _mediaScheduleGridPage.SelectAllMediaScheduleItems();
            _mediaScheduleGridPage.ClickSignOff();
        }

        [When(@"I cancel all my cost items")]
        public void CancelAllMyCostItems()
        {
            _mediaScheduleGridPage.SelectAllMediaScheduleItems();
            _mediaScheduleGridPage.ClickCancel();
            _mediaScheduleGridPage.ClickConfirmCancellation();
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown(), "Success toast was not shown.");
            UncheckAllCostItems();
        }

        [When(@"I cancel Non Media costs based on test data")]
        public void CancelNonMediaCostsBasedOnTestData()
        {
            var cancelledNonMediaCosts = WorkflowTestData.NonMediaCostData.Where(p => p.IsCancelled);

            foreach (var item in cancelledNonMediaCosts)
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(item.Name);
            }

            _mediaScheduleGridPage.ClickCancel();
            Assert.IsTrue(_mediaScheduleGridPage.CheckItemsOnCancellationModal(cancelledNonMediaCosts.Count(), "Campaign Non-Media Costs"), "Incorrect message on cancelation modal");
            _mediaScheduleGridPage.ClickConfirmCancellation();
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown(), "Success toast was not shown.");
        }

        [Then(@"Non Media costs were cancelled")]
        public void NonMediaCostsWereCancelled()
        {
            foreach (var item in WorkflowTestData.NonMediaCostData.Where(p => p.IsCancelled))
            {
                Assert.IsTrue(_mediaScheduleGridPage.WaitForMediaScheduleItemStatus(item.Name, "cancelled"), $"Non Media cost item {item.Name} was not cancelled.");
            }
        }

        [When(@"I cancel single placements based on test data")]
        public void CancelSinglePlacementsBasedOnTestData()
        {
            UncheckAllCostItems();

            foreach (var singlePlacement in WorkflowTestData.SinglePlacements.Where(p => p.IsCancelledItem))
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(singlePlacement.DetailTabData.Name);
            }

            CancelCostItems("Single Placement");
        }

        [When(@"I cancel performance packages based on test data")]
        public void CancelPerformancePackagesBasedOnTestData()
        {
            UncheckAllCostItems();

            foreach (var singlePlacement in WorkflowTestData.PerformancePackages.Where(p => p.IsCancelledItem))
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(singlePlacement.ExpectedPackageName);
            }

            CancelCostItems("Performance Package");
        }

        [When(@"I cancel sponsorship packages based on test data")]
        public void CancelSponsorshipPackagesBasedOnTestData()
        {
            UncheckAllCostItems();

            foreach (var sponsorshipPackage in WorkflowTestData.SponsorshipPackages)
            {
                _mediaScheduleGridPage.ExpandPackageWithName(sponsorshipPackage.SponsorshipPackageName);

                foreach (var singlePlacement in sponsorshipPackage.SinglePlacements.Where(p => p.IsCancelledItem))
                {
                    _mediaScheduleGridPage.SelectCostItemRowWithName(singlePlacement.DetailTabData.Name);
                }
            }

            CancelCostItems("Sponsorship Package");
            CollapseAllPlacements();
        }

        private void CancelCostItems(string costItem)
        {
            _mediaScheduleGridPage.ClickCancel();
            _mediaScheduleGridPage.ClickConfirmCancellation();
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown(), $"Success toast was not shown while cancelling placements from {costItem}");
        }

        [When(@"I cancel all cost items based on test data")]
        public void CancelAllCostItemsBasedOnTestData()
        {
            CancelSinglePlacementsBasedOnTestData();
            CancelPerformancePackagesBasedOnTestData();
            CancelSponsorshipPackagesBasedOnTestData();
        }

        [When(@"I edit the first single placement from test data without making any changes")]
        public void EditTheFirstSinglePlacementFromTestDataWithoutMakingAnyChanges()
        {
            var placement = WorkflowTestData.SinglePlacements.First();
            FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(placement.DetailTabData.Name);
            _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(placement.DetailTabData.Name);
            _manageSinglePlacementModal.SaveItem();
        }

        [When(@"I edit all AG single placements from test data without making any changes")]
        public void EditAllAgPlacementsFromTestDataWithoutMakingAnyChanges()
        {
            foreach (var placement in WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem))
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(placement.DetailTabData.Name);
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(placement.DetailTabData.Name);
                _manageSinglePlacementModal.SaveItem();
                UncheckAllCostItems();
            }
        }

        [When(@"I edit the first performance package from test data without making any changes")]
        public void EditTheFirstPerformancePackageFromTestDataWithoutMakingAnyChanges()
        {
            var package = WorkflowTestData.PerformancePackages.First();
            FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(package.ExpectedPackageName);
            _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.ExpectedPackageName);
            _managePerformancePackageModal.SaveItem();
        }

        [When(@"I edit all AG performance packages from test data without making any changes")]
        public void EditAllAgPerformancePackagesFromTestDataWithoutMakingAnyChanges()
        {
            foreach (var package in WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem))
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(package.ExpectedPackageName);
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.ExpectedPackageName);
                _managePerformancePackageModal.SaveItem();
                UncheckAllCostItems();
            }
        }

        [When(@"I edit the first placement inside the first sponsorship package from test data without making any changes")]
        public void EditTheFirstPlacementInsideTheFirstSponsorshipPackageFromTestDataWithoutMakingAnyChanges()
        {
            var package = WorkflowTestData.SponsorshipPackages.First();
            _mediaScheduleGridPage.ExpandPackageWithName(package.SponsorshipPackageName);
            FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(package.SinglePlacements.First().DetailTabData.Name);
            _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.SinglePlacements.First().DetailTabData.Name);
            _manageSinglePlacementModal.SaveItem();
        }

        [Then(@"The version for the first single placement from test data is incremented")]
        public void VersionForTheFirstSinglePlacementFromTestDataIsIncremented()
        {
            var placement = WorkflowTestData.SinglePlacements.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaSchedulePage.IsVersionIncremented(versionBeforeEdit, placement.DetailTabData.Publisher);
            Assert.IsTrue(isVersionIncremented, "The version was not incremented when it should");
        }

        [Then(@"The version for the first single placement from test data is not incremented")]
        public void VersionForTheFirstSinglePlacementFromTestDataIsNotIncremented()
        {
            var placement = WorkflowTestData.SinglePlacements.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, placement.DetailTabData.Name);
            Assert.IsFalse(isVersionIncremented, "The version was incremented when it should not");
        }

        [Then(@"The version for all single placements from test data is incremented")]
        public void VersionForAllSinglePlacementsFromTestDataIsIncremented()
        {
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);

            foreach (var placement in WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList())
            {
                var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, placement.DetailTabData.Name);
                Assert.IsTrue(isVersionIncremented, $"The version of placement {WorkflowTestData.SinglePlacements.IndexOf(placement) + 1} was not incremented when it should");
            }
        }

        [Then(@"The version for the first performance package from test data is incremented")]
        public void VersionForTheFirstPerformancePackageFromTestDataIsIncremented()
        {
            var package = WorkflowTestData.PerformancePackages.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaSchedulePage.IsVersionIncremented(versionBeforeEdit, package.ExpectedPackageName);
            Assert.IsTrue(isVersionIncremented, "The version was not incremented when it should");
        }

        [Then(@"The version for the first performance package from test data is not incremented")]
        public void VersionForTheFirstPerformancePackageFromTestDataIsNotIncremented()
        {
            var package = WorkflowTestData.PerformancePackages.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, package.ExpectedPackageName);
            Assert.IsFalse(isVersionIncremented, "The version was incremented when it should not");
        }

        [Then(@"The version for all single placements from test data is not incremented")]
        public void VersionForAllSinglePlacementsFromTestDataIsNotIncremented()
        {
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);

            foreach (var placement in WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList())
            {
                var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, placement.DetailTabData.Name);
                Assert.IsFalse(isVersionIncremented, "The version was incremented when it should not");
            }
        }

        [Then(@"The version for all performance packages from test data is not incremented")]
        public void VersionForAllPerformancePackagesFromTestDataIsNotIncremented()
        {
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();

            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);

            foreach (var performancePackage in performancePackages)
            {
                var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, performancePackage.ExpectedPackageName);
                Assert.IsFalse(isVersionIncremented, "The version was incremented when it should not");
            }
        }

        [Then(@"The version for all performance packages from test data is incremented")]
        public void VersionForAllPerformancePackagesFromTestDataIsIncremented()
        {
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();

            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);

            for (var i = 0; i < performancePackages.Count; i++)
            {
                var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, performancePackages[i].ExpectedPackageName);
                Assert.IsTrue(isVersionIncremented, $"The version of package {i + 1} was not incremented when it should");
            }
        }

        [Then(@"The version for the first sponsorship package from test data is incremented")]
        public void VersionForTheFirstSponsorshipPackageFromTestDataIsIncremented()
        {
            var package = WorkflowTestData.SponsorshipPackages.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaSchedulePage.IsVersionIncremented(versionBeforeEdit, package.SponsorshipPackageName);
            Assert.IsTrue(isVersionIncremented, "The version was not incremented when it should");
        }

        [Then(@"The version for the first sponsorship package from test data is not incremented")]
        public void VersionForTheFirstSponsorshipPackageFromTestDataIsNotIncremented()
        {
            var package = WorkflowTestData.SponsorshipPackages.First();
            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, package.SinglePlacements.First().DetailTabData.Name);
            Assert.IsFalse(isVersionIncremented, "The version was incremented when it should not");
        }

        [When(@"I edit all AG single placements")]
        public void EditAllAGSinglePlacementsBasedOnTestData()
        {
            var singlePlacements = WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();

            foreach (var placement in singlePlacements)
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(placement.DetailTabData.Location);
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(placement.DetailTabData.Name);
                _manageSinglePlacementModal.EditSinglePlacement(placement.EditData);
                _manageSinglePlacementModal.SaveItem();
                UncheckAllCostItems();
            }
        }

        [When(@"I edit all AG performance packages")]
        public void EditAllAGPerformancePackagesBasedOnTestData()
        {
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();

            foreach (var package in performancePackages)
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(package.ExpectedPackageName);
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.ExpectedPackageName);
                _managePerformancePackageModal.EditPerformancePackage(package.EditData);
                _managePerformancePackageModal.SaveItem();
                UncheckAllCostItems();
            }
        }

        [When(@"I edit all single placements")]
        public void EditAllSinglePlacementsBasedOnTestData()
        {
            foreach (var placement in WorkflowTestData.SinglePlacements)
            {
                UncheckAllCostItems();
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(placement.DetailTabData.Name);
                _manageSinglePlacementModal.EditSinglePlacement(placement.EditData);
                _manageSinglePlacementModal.SaveItem();
            }
        }

        [When(@"I edit all performance packages")]
        public void EditAllPerformancePackagesBasedOnTestData()
        {
            foreach (var package in WorkflowTestData.PerformancePackages)
            {
                UncheckAllCostItems();
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.ExpectedPackageName);
                _managePerformancePackageModal.EditPerformancePackage(package.EditData);
                _managePerformancePackageModal.SaveItem();
            }
        }

        [When(@"I edit all sponsorship packages")]
        public void EditAllSponsorshipPackagesBasedOnTestData()
        {
            foreach (var package in WorkflowTestData.SponsorshipPackages)
            {
                UncheckAllCostItems();
                _mediaScheduleGridPage.ExpandPackageWithName(package.SponsorshipPackageName);
                _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(package.SinglePlacements.First().DetailTabData.Name);
                _manageSinglePlacementModal.EditSinglePlacement(package.SinglePlacements.First().EditData);
                _manageSinglePlacementModal.SaveItem();
                    
                if (_mediaScheduleGridPage.IsElementPresent(By.ClassName("restore-sponsorship-items")))
                {
                    _mediaScheduleGridPage.ClickConfirmToRestoreItems();
                }

                CollapseAllPlacements();
            }
        }

        [When(@"I edit all cost items based on test data")]
        public void EditAllCostItemsBasedOnTestData()
        {
            EditAllSinglePlacementsBasedOnTestData();
            EditAllPerformancePackagesBasedOnTestData();
            EditAllSponsorshipPackagesBasedOnTestData();
        }

        [Then(@"All the items should be cancelled")]
        public void AllTheItemsShouldBeCancelled()
        {
            Assert.IsTrue(_mediaScheduleGridPage.WaitUntilAllMediaScheduleItemsAreCancelled(), $"One or more media schedule items are not cancelled.");
        }

        [Then(@"All the items should be not cancelled")]
        public void AllTheItemsShouldNotBeCancelled()
        {
            Assert.IsFalse(_mediaScheduleGridPage.WaitUntilAllMediaScheduleItemsAreCancelled(), $"One or more media schedule items are still cancelled.");
        }

        [Then(@"All the items should have status (.*)")]
        public void ValidateAllItemsHaveExpectedStatus(string status)
        {
            Assert.IsTrue(_mediaScheduleGridPage.WaitUntilAllMediaScheduleItemsHaveStatus(status), $"One or more media schedule items are NOT {status}.");
        }

        [When(@"I create all cost items from test data")]
        public void CreateAllCostItems()
        {
            CreateSinglePlacements();
            CreatePerformancePackages();
            CreateSponsorshipPackages();
        }

        [Then(@"All cost items imported from the proposal should be present in the media schedule")]
        [Then(@"The cost items should be present in the media schedule")]
        public void CostItemsPresentInMediaSchedule()
        {
            var singlePlacements = WorkflowTestData.SinglePlacements.Where(item => !item.IsImportedItem).ToList();
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => !item.IsImportedItem).ToList();
            var sponsorshipPackages = WorkflowTestData.SponsorshipPackages.Where(item => !item.IsImportedItem).ToList();

            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(singlePlacements),
                "Failed to verify creation of single placements");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(performancePackages),
                "Failed to verify creation of performance packages");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSponsorshipPackagesExist(sponsorshipPackages),
                "Failed to verify creation of sponsorship packages");
        }

        [Then(@"The AG cost items should be present in the media schedule")]
        public void AGCostItemsShouldBePresentInTheMediaSchedule()
        {
            var singlePlacements = WorkflowTestData.SinglePlacements.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();
            var performancePackages = WorkflowTestData.PerformancePackages.Where(item => item.InventoryProvider.Equals(InventoryProvider) && item.IsAutomatedGuaranteedItem).ToList();

            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(singlePlacements),
                "Failed to verify creation of AG single placements");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(performancePackages),
                    "Failed to verify creation of AG performance packages");
        }

        [When(@"I export the media schedule export '(.*)'")]
        public void ExportTheMediaScheduleExport(string export)
        {
            FeatureContext[ContextStrings.ExportName] = export;
            if (export.Equals("GroupM (BE) Technical Specifications Export") && !IsFeatureToggleEnabled(FeatureToggle.BEProdScheduleChanges))
            {
                Console.WriteLine($"WARN: The {export} file is not supported as it is under Feature tooggle and not enabled currently");                
                return;
            }

            _mediaScheduleGridPage.ClickExportButton();
            _exportMediaScheduleFrame.DownloadMediaScheduleExport(export, WorkflowTestData.MediaScheduleData.MediaScheduleExportData, WorkflowTestData.CampaignData);
        }

        [When(@"I download the media schedule export '(.*)'")]
        public void DownloadTheMediaScheduleExport(string export)
        {
            FeatureContext[ContextStrings.ExportName] = export;
            _exportMediaScheduleFrame.SwitchToExportFrame();
            _exportMediaScheduleFrame.DownloadMediaScheduleExport(export, WorkflowTestData.MediaScheduleData.MediaScheduleExportData, WorkflowTestData.CampaignData);
        }

        [Then(@"The imported cost items should be present in the media schedule")]
        public void TheImportedCostItemsShouldBePresentInTheMediaSchedule()
        {
            var importedSinglePlacements = WorkflowTestData.SinglePlacements.Where(item => item.IsImportedItem).ToList();
            var importedPerformancePackages = WorkflowTestData.PerformancePackages.Where(item => item.IsImportedItem).ToList();
            var importedSponsorshipPackages = WorkflowTestData.SponsorshipPackages.Where(item => item.IsImportedItem).ToList();

            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(importedSinglePlacements),
                "Not all imported single placements exist in the media schedule");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(importedPerformancePackages),
                    "Not all imported performance packages exist in the media schedule");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSponsorshipPackagesExist(importedSponsorshipPackages),
                    "Not all imported sponsorship packages exist in the media schedule");
        }

        [Then(@"The imported AG items should be present in the media schedule")]
        public void TheImportedAGItemsShouldBePresentInTheMediaSchedule()
        {
            var importedSinglePlacements = WorkflowTestData.SinglePlacements.Where(item => item.IsImportedItem).ToList();
            var importedPerformancePackages = WorkflowTestData.PerformancePackages.Where(item => item.IsImportedItem).ToList();

            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(importedSinglePlacements),
                "Not all imported single placements exist in the media schedule");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(importedPerformancePackages),
                    "Not all imported performance packages exist in the media schedule");
        }

        [When(@"The media schedule export is exported")]
        [Then(@"The media schedule export should be exported")]
        public void MediaScheduleExportIsExported()
        {
            if (FeatureContext[ContextStrings.ExportName].Equals("GroupM (BE) Technical Specifications Export") && !IsFeatureToggleEnabled(FeatureToggle.BEProdScheduleChanges))
            {
                FeatureContext[ContextStrings.ExportName] = string.Empty;
                return;
            }

            var msg = _exportMediaScheduleFrame.GetSuccessMessage();
            Assert.AreEqual("Please save your export file.", msg);
        }

        [When(@"I download the import template")]
        public void DownloadImportTemplate()
        {
            _mediaScheduleGridPage.ClickImportButton();
            _importMediaScheduleModal.DownloadImportTemplate();
        }

        [Then(@"the import template should be downloaded")]
        public void IsImportTemplateDownloaded()
        {
            var message = _importMediaScheduleModal.GetTextMsgLoading();
            Assert.IsTrue((message.Equals("Starting...") || message.Equals("Connected...")), $"Download import template failed.\nActual message: '{message}'");
        }

        [When(@"I import the media schedule items")]
        public void ImportTheMediaScheduleItems()
        {
            _mediaScheduleGridPage.ClickImportButton();
            _importMediaScheduleModal.ImportMediaSchedule();
            Assert.AreEqual("Import was Successful", _importMediaScheduleModal.GetImportStatus(), "Failed to import media schedule items");
            _importMediaScheduleModal.ClickClose();
        }

        [When(@"I import the media schedule items from downloaded file")]
        public void ImportTheMediaScheduleItemsFromDownloadedFile()
        {
            _mediaScheduleGridPage.ClickImportButton();
            _importMediaScheduleModal.ImportMediaSchedule(WorkflowTestData.CampaignData.DetailsData.CampaignName);
            Assert.AreEqual("Import was Successful", _importMediaScheduleModal.GetImportStatus(), "Failed to import media schedule items");
            _importMediaScheduleModal.ClickClose();
        }

        [When(@"I import (\d+) media schedule items")]
        public void WhenIImportMediaScheduleItems(int mediaScheduleItemsCount)
        {
            FeatureContext[ContextStrings.ExpectedMediaScheduleItemsCount] = mediaScheduleItemsCount;
            _mediaSchedulePage.ClickImportButton();
            _importMediaScheduleModal.ImportMediaSchedule();
            _importMediaScheduleModal.SetWaitTimeout(1200);
            Assert.AreEqual("Import was Successful", _importMediaScheduleModal.GetImportStatus(), "Failed to import media schedule items");
            _importMediaScheduleModal.ClickClose();
        }

        [When(@"The export is downloaded")]
        [Then(@"The export is downloaded")]
        public void ExportIsDownloaded()
        {
            if (!AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleApproval.EnableContactEmailNotification)
            {
                _mediaScheduleGridPage.IsSingleSuccessToastNotificationShown();
                _mediaScheduleGridPage.WaitForExportDownloaded(AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleApproval.ExportDocument, WorkflowTestData.CampaignData);
            }
            else
            {
                _mediaScheduleGridPage.WaitForExportDownloaded(AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleApproval.ExportDocument, WorkflowTestData.CampaignData);
            }
        }

        [When(@"I add non media cost items")]
        public void AddNonMediaCostItems()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            if (!IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleItemSettings, enableNMCActionBarButton))
                throw new Exception($"NMC: Enable Action Bar and Campaign Non-Media Cost feature is not enabled for the Agency");

            foreach (var nonMediaCost in WorkflowTestData.NonMediaCostData)
            {
                _mediaScheduleGridPage.ClickAddNonMediaCostButton();
                bool isHideClassification = IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleItemSettings, hideClassificationTabForNMC);
                _addEditNonMediaCostModal.AddNonMediaCost(nonMediaCost, isHideClassification);
                if (!isHideClassification)
                {
                    _addEditModal.SetClassificationTabData(nonMediaCost.ClassificationsTabData);
                }
                _addEditNonMediaCostModal.ClickSave();
                _mediaScheduleGridPage.IsSingleSuccessToastNotificationShown();
            }
        }

        [When(@"I edit Non Media cost based on test data")]
        public void WhenIEditNonMediaCostBasedOnTestData()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            foreach (var nonMediaCost in WorkflowTestData.NonMediaCostData)
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(nonMediaCost.Name);
                _mediaScheduleGridPage.SelectCostItemRowWithName(nonMediaCost.Name);
                _mediaScheduleGridPage.ClickEditButton();
                _addEditNonMediaCostModal.EditNonMediaCost(nonMediaCost.EditNonMediaCostData);
                _addEditNonMediaCostModal.ClickSave();
                _mediaScheduleGridPage.IsSingleSuccessToastNotificationShown();
            }
        }

        [When(@"I edit Non Media cost based without making any changes")]
        public void ThenIEditNonMediaCostBasedWithoutMakingAnyChanges()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            foreach (var nonMediaCost in WorkflowTestData.NonMediaCostData)
            {
                FeatureContext[ContextStrings.CostItemVersion] = _mediaScheduleGridPage.GetItemVersionNumber(nonMediaCost.Name);
                _mediaScheduleGridPage.SelectCostItemRowWithName(nonMediaCost.Name);
                _mediaScheduleGridPage.ClickEditButton();
                _addEditNonMediaCostModal.ClickSave();
                _mediaScheduleGridPage.IsSingleSuccessToastNotificationShown();
            }
        }

        [Then(@"The version of Non Media cost is incremented")]
        public void ThenTheVersionOfNonMediaCostIsIncremented()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = false;

            foreach (var nonMediaCost in WorkflowTestData.NonMediaCostData)
            {
                isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, nonMediaCost.Name);
                Assert.IsTrue(isVersionIncremented, $"The version of non media cost {WorkflowTestData.NonMediaCostData.IndexOf(nonMediaCost) + 1} was not incremented when it should");
            }
        }

        [Then(@"The version of Non Media cost is not incremented")]
        public void VerifyNonMediaCostIsNotIncremented()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            var versionBeforeEdit = Convert.ToInt32(FeatureContext[ContextStrings.CostItemVersion]);
            var isVersionIncremented = false;

            foreach (var nonMediaCost in WorkflowTestData.NonMediaCostData)
            {
                isVersionIncremented = _mediaScheduleGridPage.IsVersionIncremented(versionBeforeEdit, nonMediaCost.Name);
                Assert.IsFalse(isVersionIncremented, $"The version of non media cost {WorkflowTestData.NonMediaCostData.IndexOf(nonMediaCost) + 1} was incremented when it should not");
            }
        }

        [Then(@"the non media cost items are added successfully")]
        public void HasNonMediaCostItems()
        {
            var isNonMediaCostsDisplayed = _mediaScheduleGridPage.VerifyAllNonMediaCostsDisplayed(WorkflowTestData.NonMediaCostData);
            Assert.IsTrue(isNonMediaCostsDisplayed.All(x => x.Value), $"Non media costs is not displayed on media schedule grid page: {string.Join("\n", isNonMediaCostsDisplayed.Where(x => !x.Value).Select(y => y.Key.Name).ToArray())}");
        }

        [When(@"I open the version history of the first single placement")]
        public void OpenVersionHistoryOfFirstSinglePlacement()
        {
            var placement = WorkflowTestData.SinglePlacements.First();
            _mediaScheduleGridPage.OpenMediaScheduleItemVersionHistory(placement.DetailTabData.Name);
        }

        [Then(@"the datetime of the (\d+)(?:st|nd|rd|th) row should be based on the agency time zone")]
        public void DateTimeOfRowShouldBeBasedOnTheAgencyTimeZone(int rowNumber)
        {
            var expectedDateTimeStart = (DateTime)FeatureContext[ContextStrings.ExpectedDateTimeStart];
            var expectedDateTimeEnd = (DateTime)FeatureContext[ContextStrings.ExpectedDateTimeEnd];
            var actualDateTime = DateTime.ParseExact(_versionHistoryModal.GetRowDateTime(rowNumber), "dd/MM/yyyy HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            Assert.IsTrue(actualDateTime >= expectedDateTimeStart && actualDateTime < expectedDateTimeEnd,
                $"\nExpected DateTime Start: {expectedDateTimeStart}\nExpected DateTime End: {expectedDateTimeEnd}\nActual DateTime: {actualDateTime}");
        }

        [Then(@"all media schedule items should be loaded within (\d+) seconds")]
        public void AllMediaScheduleItemsAreLoadedInSeconds(int seconds)
        {
            _mediaScheduleGridPage.MeasureMediaScheduleItemsLoadTime();
            var expectedMediaScheduleItemsCount = (int)FeatureContext[ContextStrings.ExpectedMediaScheduleItemsCount];
            Assert.AreEqual(expectedMediaScheduleItemsCount, _mediaScheduleGridPage.GetNumberOfLineItemsLoaded());
            var actualTime = (TimeSpan)FeatureContext[ContextStrings.ElapsedTime];
            var expectedTime = TimeSpan.FromSeconds(seconds);
            Assert.LessOrEqual(actualTime, expectedTime);
            Console.WriteLine($"Actual load time: {actualTime}");
        }

        [When(@"I select a single placement cost item")]
        public void SelectSinglePlacementCostItem()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(duplicatedSinglePlacementName);
        }

        [When(@"I select a non media cost item")]
        public void SelectNonMediaCostItem()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(duplicatedNonMediaCostItemName);
        }

        [When(@"I click the duplicate button")]
        public void ClickDuplicateButton()
        {
            _mediaScheduleGridPage.ClickDuplicate();
        }

        [Then(@"The single placement should be duplicated with success toast")]
        public void SinglePlacementDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedSinglePlacementName, 2);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [Then(@"The non media cost item should be duplicated with success toast")]
        public void NonMediaCostItemDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithTextExistsInGridContainingTextWithCount(duplicatedNonMediaCostItemName);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I select a sponsorship package header row")]
        public void SelectSponsorshipPackageItemHeader()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(sponsorshipPackageToDuplicateHeaderName);
            duplicatedSponsorshipPackageHeaderName = sponsorshipPackageToDuplicateHeaderName + " " + DateTime.Now;
        }

        [When(@"I enter a new duplicate sponsorship package name and click save")]
        public void EnterDuplicateSponsorshipPackageNameAndClickSave()
        {
            _mediaScheduleGridPage.EnterDuplicateSponsorshipPackageNameAndSave(duplicatedSponsorshipPackageHeaderName);
        }

        [Then(@"The sponsorship package should be duplicated with success toast")]
        public void SponsorshipPackgeDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedSponsorshipPackageHeaderName, 1);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I select a performance package header row")]
        public void SelectPerformancePackageHeader()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(duplicatedPerformancePackageHeaderName);
        }

        [Then(@"The performance package should be duplicated with success toast")]
        public void PerformancePackageDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedPerformancePackageHeaderName, 2);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I expand all placements")]
        public void ExpandAllPlacements()
        {
            _mediaScheduleGridPage.ExpandAllPackages();
        }

        [When(@"I collapse all placements")]
        public void CollapseAllPlacements()
        {
            _mediaScheduleGridPage.CollapseAllPackages();
        }

        [When(@"I check all cost items")]
        public void CheckAllCostItems()
        {
            _mediaScheduleGridPage.CheckAllCostItems();
        }

        [When(@"I uncheck all cost items")]
        public void UncheckAllCostItems()
        {
            _mediaScheduleGridPage.UncheckAllCostItems();
        }

        [When(@"I select a sponsorship package child item")]
        public void SelectSponsorshipPackageChildItem()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(duplicatedSponsorshipPackageChildItemName);
        }

        [Then(@"The sponsorship package child item should be duplicated with success toast")]
        public void SponsorshipPackageChildItemDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedSponsorshipPackageChildItemName, 2);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I select a performance package child item")]
        public void SelectPerformancePackageChildItem()
        {
            _mediaScheduleGridPage.SelectCostItemRowWithName(duplicatedPerformancePackageChildItemName);
        }

        [Then(@"The performance package child item should be duplicated with success toast")]
        public void PerformancePackageChildItemDuplicated()
        {
            _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedPerformancePackageChildItemName, 2);
            Assert.IsTrue(_mediaScheduleGridPage.IsSingleSuccessToastNotificationShown());
        }

        [When(@"I dismiss all toast notifications")]
        public void DismissAllToastNotifications()
        {
            _mediaScheduleGridPage.DismissAllToastNotifications();
        }

        [When(@"I click the replace button")]
        public void ClickReplaceButton()
        {
            _mediaScheduleGridPage.ClickReplace();
        }

        [When(@"I replace the single placement")]
        public void ReplaceSinglePlacement()
        {
            _manageSinglePlacementModal.EditSinglePlacement(singlePlacementReplaceData);
            _manageSinglePlacementModal.ClickReplace();
        }

        [Then(@"The single placement should be replaced")]
        public void SinglePlacementReplaced()
        {
            _mediaScheduleGridPage.VerifyPlacementUpdated(singlePlacementReplaceData);
        }

        [When(@"I replace the performance package header row")]
        public void ReplacePerformancePackageHeaderRow()
        {
            _managePerformancePackageModal.EditPerformancePackage(performancePackageHeaderReplaceData);
            _managePerformancePackageModal.ClickReplace();
        }

        [Then(@"The performance package header row should be replaced")]
        public void PerformancePackageHeaderRowReplaced()
        {
            _mediaScheduleGridPage.VerifyPlacementUpdated(performancePackageHeaderReplaceData);
        }

        [When(@"I replace the sponsorship package header row")]
        public void ReplaceSponsorshipPackageHeaderRow()
        {
            _manageSponsorshipPackageModal.EditSponsorshipPackage(sponsorshipPackageHeaderReplaceData);
            _manageSponsorshipPackageModal.ClickReplacePackage();
        }

        [Then(@"The sponsorship package header row should be replaced")]
        public void SponsorshipPackageHeaderRowReplaced()
        {
            _mediaScheduleGridPage.VerifyPlacementUpdated(sponsorshipPackageHeaderReplaceData);
        }

        [When(@"I expand a performance package header row")]
        public void PerformancePackageHeaderRowExpand()
        {
            _mediaScheduleGridPage.ExpandPackageWithName(performancePackageHeaderReplaceData.Name);
        }

        [When(@"I replace the performance package child item")]
        public void ReplacePerformancePackageChildItem()
        {
            _managePerformancePackageModal.EditPerformancePackagePlacement(performancePackageChildItemReplaceData);
            _managePerformancePackageModal.ClickSave();
        }

        [Then(@"The performance package child item should be replaced")]
        public void PerformancePackageChildItemReplaced()
        {
            _mediaScheduleGridPage.VerifyPlacementUpdated(performancePackageChildItemReplaceData);
        }

        [When(@"I expand a sponsorship package header row")]
        public void SponsorshipPackageHeaderRowExpand()
        {
            _mediaScheduleGridPage.ExpandPackageWithName(sponsorshipPackageHeaderReplaceData.Name);
        }

        [When(@"I replace the sponsorship package child item")]
        public void ReplaceSponsorshipPackageChildItem()
        {
            _manageSinglePlacementModal.EditSinglePlacement(sponsorshipPackageChildItemReplaceData);
            _manageSinglePlacementModal.ClickReplace();
        }

        [Then(@"The sponsorship package child item should be replaced")]
        public void SponsorshipPackageChildItemReplaced()
        {
            _mediaScheduleGridPage.VerifyPlacementUpdated(sponsorshipPackageChildItemReplaceData);
        }

        [Then(@"the RFP single placements should have converted to AG items")]
        public void ThenTheRFPSinglePlacementsShouldHaveConvertedToAGItems()
        {
            var convertedSinglePlacements = WorkflowTestData.SinglePlacements.Where(p => p.IsItemGoingToConvertFromRfpToAg).ToList();
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(convertedSinglePlacements), "Failed to verify creation of AG single placements");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExistAsAg(convertedSinglePlacements), "Failed to verify single placements were converted to AG");
        }

        [Then(@"the RFP performance packages should have converted to AG items")]
        public void ThenTheRFPPerformancePackagesShouldHaveConvertedToAGItems()
        {
            var convertedPerformancePackages = WorkflowTestData.PerformancePackages.Where(p => p.IsItemGoingToConvertFromRfpToAg).ToList();
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(convertedPerformancePackages), "Failed to verify creation of AG performance packages");
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExistAsAg(convertedPerformancePackages), "Failed to verify performance packages were converted to AG");
        }

        [Then(@"the RFP single placements should not have converted to AG items")]
        public void ThenTheRFPSinglePlacementsShouldNotHaveConvertedToAGItems()
        {
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllSinglePlacementsExist(nonConvertedSinglePlacements), "Failed to verify single placements were not converted to AG");
        }

        [Then(@"the RFP performance packages should not have converted to AG items")]
        public void ThenTheRFPPerformancePackagesShouldNotHaveConvertedToAGItems()
        {
            Assert.IsTrue(_mediaScheduleGridPage.VerifyAllPerformancePackageExist(nonConvertedPerformancePackages), "Failed to verify performance packages were not converted to AG");
        }

        [When(@"I group media schedule grid by '(.*)'")]
        public void GroupMediaScheduleGridBy(string groupCategory)
        {
            _mediaScheduleGridPage.GroupMediaScheduleGridBy(groupCategory);
        }

        [Then(@"Media schedule group '(.*)' are displayed")]
        public void VerifyMediaScheduleGroupDisplay(string groupValue)
        {
            Assert.IsTrue(_mediaScheduleGridPage.IsMediaScheduleGroupHeaderAppeared(groupValue), "Media schedule group was not displayed.");
        }

        [Then(@"I should see the Billed Goals under Delivered column in the media schedule")]
        public void VerifyBilledGoalsInTheMediaSchedule()
        {
            var customBillingItems = WorkflowTestData.CustomBillingData.First().CustomBillingItems;
            var areBilledGoalsDisplayed = _mediaScheduleGridPage.VerifyAllBilledGoalsDisplayed(customBillingItems);
            Assert.IsTrue(areBilledGoalsDisplayed.All(x => x.Value), $"Billed goals were not displayed on media schedule grid page:\n{string.Join("\n", areBilledGoalsDisplayed.Where(x => !x.Value).Select(y => y.Key.Name).ToArray())}");
        }

        [When(@"I click the lock button")]
        public void ClickLockButton()
        {
            _mediaScheduleGridPage.Lock();
        }

        [Then(@"All items should be locked")]
        public void VerifyAllItemsAreLocked()
        {
            Assert.IsTrue(_mediaScheduleGridPage.CheckAllPlacementsLocked());
        }

        [Then(@"Lock success toast is shown")]
        public void VerifyLockSuccessToastIsShown()
        {
            var expectedMessage = $"{GetTotalNumberOfNonImportedPlacements()} items locked";
            Assert.IsTrue(_mediaScheduleGridPage.IsSuccessNotificationShownWithMessage(expectedMessage));
        }

        [Then(@"The add and import buttons should be disabled")]
        public void VerifyAddImportButtonsDisabled()
        {
            Assert.IsFalse(_mediaScheduleGridPage.IsAddButtonEnabled(), "The add button is enabled");
            Assert.IsFalse(_mediaScheduleGridPage.IsImportButtonEnabled(), "The import button is enabled");
        }

        [When(@"I click the unlock button")]
        public void ClickUnlockButton()
        {
            _mediaScheduleGridPage.Unlock();
        }

        [Then(@"All items should be unlocked")]
        public void VerifyAllItemsAreUnlocked()
        {
            Assert.IsTrue(_mediaScheduleGridPage.CheckAllPlacementsUnlocked());
        }

        [Then(@"The add and import buttons should be enabled")]
        public void VerifyAddImportButtonsEnabled()
        {
            Assert.IsTrue(_mediaScheduleGridPage.IsAddButtonEnabled(), "The add button is disabled");
            Assert.IsTrue(_mediaScheduleGridPage.IsImportButtonEnabled(), "The import button is disabled");
        }

        [Then(@"Unlock success toast is shown")]
        public void VerifyUnlockSuccessToastIsShown()
        {
            var expectedMessage = $"{GetTotalNumberOfNonImportedPlacements()} items unlocked";
            Assert.IsTrue(_mediaScheduleGridPage.IsSuccessNotificationShownWithMessage(expectedMessage));
        }

        [Then(@"Part signed success toast is shown")]
        public void VerifyPartSignedSuccessToastIsShown()
        {
            var expectedMessage = $"items have been part-signed";
            Assert.IsTrue(_mediaScheduleGridPage.IsSuccessNotificationShownWithMessage(expectedMessage));
        }

        private void AddPerformancePackage(PerformancePackage performancePackage)
        {
            _managePerformancePackageModal.CreatePerformancePackage(performancePackage);
            _managePerformancePackageModal.ClickClose();
        }

        private void AddSponsorshipPackage(SponsorshipPackage sponsorshipPackage)
        {
            _manageSponsorshipPackageModal.CreateSponsorshipPackage(sponsorshipPackage);

            foreach (var singlePlacement in sponsorshipPackage.SinglePlacements)
            {
                _manageSponsorshipPackageModal.ClickAddPlacement();
                _manageSinglePlacementModal.CreateSinglePlacement(singlePlacement, isSponsorshipChildItem: true);
            }

            _manageSponsorshipPackageModal.ClickSavePackage();
        }

        private void CreateSponsorshipPackage(string sponsorshipPackageName)
        {
            _createSponsorshipPackageModal.CreateSponsorshipPackage(sponsorshipPackageName);
        }

        private void AddOtherCostsInLegacyUI()
        {
            _mediaSchedulePage.ClickAddTotalOtherCosts();
            _otherCostsPage.ClickAddButton();
            _addOtherCostFrame.AddOtherCosts(WorkflowTestData.NonMediaCostData);
            Assert.IsTrue(_addOtherCostFrame.IsOtherCostItemAddedSuccessfully(), "Other cost could not be added");
            _addOtherCostFrame.CloseAddOtherCostFrame();
            _otherCostsPage.ClickBackButton();
        }

        private int GetTotalNumberOfNonImportedPlacements()
        {
            var singlePlacementCount = WorkflowTestData.SinglePlacements.Where(item => !item.IsImportedItem).Count();
            var performancePackageCount = WorkflowTestData.PerformancePackages.Where(item => !item.IsImportedItem).Count();
            var sponsorshipPackageCount = WorkflowTestData.SponsorshipPackages.Where(item => !item.IsImportedItem).SelectMany(package => package.SinglePlacements).Count();
            var nonMediaPlacementCount = WorkflowTestData.NonMediaCostData.Count;

            return singlePlacementCount + performancePackageCount + sponsorshipPackageCount + nonMediaPlacementCount;
        }

        [When(@"I open the first non media cost item")]
        public void OpenFirstNonMediaCostItem()
        {
            if (WorkflowTestData.NonMediaCostData == null || !WorkflowTestData.NonMediaCostData.Any())
                throw new NotFoundException($"Non media costs were missed in test data.");

            var nonMediaCostItem = WorkflowTestData.NonMediaCostData.First().Name;
            _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(nonMediaCostItem);
        }

        [When(@"I open the first Single Placement")]
        public void OpenTheFirstSinglePlacement()
        {
            if (WorkflowTestData.SinglePlacements == null || !WorkflowTestData.SinglePlacements.Any())
                throw new NotFoundException($"Single Placements were missed in test data.");

            var singlePlacementItem = WorkflowTestData.SinglePlacements.Where(p => !p.IsImportedItem).First().DetailTabData;
            _mediaScheduleGridPage.OpenMediaScheduleItemForEditing(singlePlacementItem.Name);
        }

        [Then(@"the values of estimate fields in forecast tab is as expected")]
        public void VerifyEstimateFieldValues()
        {
            _addEditModal.VerifyForecastValues(WorkflowTestData.SinglePlacements.Where(p => !p.IsImportedItem).First().ForecastTabData);
            _addEditModal.ClickClose();
        }
        
        [When(@"I switch to (.*) tab")]
        public void SwitchToTab(string tabName)
        {
            _addEditModal.ClickTab(tabName);
        }

        [When(@"I set cost adjustment values")]
        public void SetCostAdjustmentValues()
        {
            var nonMediaCost = WorkflowTestData.NonMediaCostData.First();

            if (nonMediaCost.CostsTabData.Cost.Client != null)
            {
                _addEditNonMediaCostModal.SetCost("Client", nonMediaCost.CostsTabData);
            }

            if (nonMediaCost.CostsTabData.VendorAdjustments != null)
            {
                _addEditNonMediaCostModal.SetAdjustments(nonMediaCost.CostsTabData.VendorAdjustments, true);
            }

            if (nonMediaCost.CostsTabData.ClientAdjustments != null)
            {
                _addEditNonMediaCostModal.SetAdjustments(nonMediaCost.CostsTabData.ClientAdjustments);
            }
        }

        [Then(@"the cost summaries in cost tab are as expected")]
        public void VerifyCostSummaryOfCostsTab()
        {
            var nonMediaCost = WorkflowTestData.NonMediaCostData.First();
            if (nonMediaCost.CostsTabData.CostSummaries == null || !nonMediaCost.CostsTabData.CostSummaries.Any())
                throw new NotFoundException($"Cost data Adjustments in costs tab data missing in test data");

            foreach (var costSummary in nonMediaCost.CostsTabData.CostSummaries)
            {
                Assert.IsTrue(_addEditNonMediaCostModal.VerifyCostSummary(costSummary), $"The {costSummary.CostSummaryName} is not displayed as expected in the cost tab");
            }
        }

        [When(@"I set classification values")]
        public void SetClassificationValues()
        { 
            if (IsAgencyFeatureEnabled(AgencySetupData.SymphonyAdminData.Agencies.First().Features.MediaScheduleItemSettings, hideClassificationTabForNMC))
                return;

            var nonMediaCost = WorkflowTestData.NonMediaCostData.First();
            _addEditNonMediaCostModal.SetClassificationTabData(nonMediaCost.ClassificationsTabData);
        }

        [Then(@"the classifications in Classifications tab are as expected")]
        public void VerifyClassificationValues()
        {
            var nonMediaCost = WorkflowTestData.NonMediaCostData.First();
            _addEditNonMediaCostModal.VerifyClassificationTabData(nonMediaCost.ClassificationsTabData);
        }

        [Then(@"I save the cost item")]
        [When(@"I save the cost item")]
        public void SaveCostItem()
        {
            _addEditNonMediaCostModal.SaveItem();
            _mediaScheduleGridPage.DismissAllToastNotifications();
        }

        [When(@"I select columns to display")]
        public void SelectColumnsToDisplay()
        {
            var columnsToDisplay = WorkflowTestData.ColumnSettings;

            _mediaScheduleGridPage.OpenManageColumnModal();
            _manageColumnsModal.ShowColumns(columnsToDisplay);
        }

        [Then(@"I should see the columns on the page")]
        public void CheckColumnsDisplayed()
        {
            var columnsToDisplay = WorkflowTestData.ColumnSettings;

            _mediaScheduleGridPage.CheckColumnsAreShown(columnsToDisplay);
        }

        [When(@"I select columns to hide")]
        public void SelectColumnsToHide()
        {
            var columnsToHide = WorkflowTestData.ColumnSettings;

            _mediaScheduleGridPage.OpenManageColumnModal();
            _manageColumnsModal.HideColumns(columnsToHide);
        }

        [Then(@"I should not see the columns on the page")]
        public void CheckColumnsHidden()
        {
            var columnsToDisplay = WorkflowTestData.ColumnSettings;

            _mediaScheduleGridPage.CheckColumnsAreHidden(columnsToDisplay);
        }

        [When(@"I select multiple single placement cost items")]
        public void SelectMultipleSinglePlacementCostItems()
        {
            foreach (var singlePlacementName in multipleSinglePlacementNames)
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(singlePlacementName);
            }
        }

        [When(@"I select multiple performance package header rows")]
        public void SelectMultiplePerformancePackageHeaderRows()
        {
            foreach (var performancePackageName in multiplePerformancePackageNames)
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(performancePackageName);
            }
        }

        [When(@"I select multiple performance package placements")]
        public void SelectMultiplePerformancePackagePlacement()
        {
            foreach (var performancePackagePlacementName in multiplePerformancePackageChildNames)
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(performancePackagePlacementName);
            }
        }

        [When(@"I select multiple sponsorship package child items")]
        public void SelectMultipleSponsorshipPackageChildItems()
        {
            foreach (var sponsorshipPackageChildItemName in multipleSponsorshipPackageChildNames)
            {
                _mediaScheduleGridPage.SelectCostItemRowWithName(sponsorshipPackageChildItemName);
            }
        }

        [Then(@"The selected items should bulk duplicate with a success toast message with (.*) item count")]
        public void CostItemsBulkDuplicated(int itemCount)
        {
            Assert.IsTrue(_mediaScheduleGridPage.IsSuccessNotificationShownWithMessage($"{itemCount} items have been duplicated"), "The success toast was shown with the correct message and item count");

            _mediaScheduleGridPage.ExpandAllPackages();

            var duplicateCostItemNames = multipleSinglePlacementNames.Concat(multiplePerformancePackageNames).Concat(multiplePerformancePackageChildNames).Concat(multipleSponsorshipPackageChildNames);

            foreach (var duplicatedCostItemName in duplicateCostItemNames)
            {
                _mediaScheduleGridPage.WaitUntilItemWithExactTextExistsInGridWithCount(duplicatedCostItemName, 2);
            }
        }
    }
}