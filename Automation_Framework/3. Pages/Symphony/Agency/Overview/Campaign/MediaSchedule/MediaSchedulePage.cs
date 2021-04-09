using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using Automation_Framework.Translations;
using OpenQA.Selenium;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SummaryTableData;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class MediaSchedulePage : BasePage
    {
        private const string FRAME_SINGLE_PLACEMENT = "/symphony-app/single-placement-create/";
        private const string FRAME_PERFORMANCE_PACKAGE = "/symphony-app/performance-package-create/";
        private const string FRAME_SPONSORSHIP_PACKAGE = "/symphony-app/sponsorship-package-create/";
        private const string FRAME_EDIT_SINGLE_PLACEMENT = "single-placement-edit/";
        private const string FRAME_EDIT_PERFORMANCE = "performance-package-edit/";
        private const string FRAME_EDIT_SPONSORSHIP = "sponsorship-package-edit/";
        private const string FRAME_EXPORT = "/Export/Export.aspx?";
        private const string FRAME_IMPORT = "/symphony-app/import";
        private const string FRAME_APPROVE_MEDIA_SCHEDULE = "/approve";
        private const string FRAME_BEGIN_SIGNOFF = "/AgencyCampaign/Signoff";
        private const string FRAME_CONFIRM_CANCEL = "/AgencyCampaign/ConfirmCancellation";
        private const string FRAME_RESTORE_SPONSORSHIP = "/symphony-app/restore-sponsorship-items/";
        private const string FRAME_ITEM_HISTORY = "MediaScheduleItemHistory";

        private IWebElement _btnAdd => FindElementByCssSelector(".fd-add.dropdown-toggle");
        private IWebElement _lnkSinglePlacement => FindElementById("lnkAddSinglePlacement");
        private IWebElement _lnkPerformancePackage => FindElementById("lnkAddPerformancePackage");
        private IWebElement _lnkSponsorshipPackage => FindElementById("lnkAddSponsorshipPackage");
        private IWebElement _btnConfirm => FindElementById("ctl00_Content_btnConfirm");
        private IWebElement _btnConfirmInEditPackageModal => FindElementByCssSelector(".aui--button.btn-primary.btn.btn-default");
        private IWebElement _btnConfirmAll => FindElementById("ctl00_Content_btnConfirmAll");
        private IWebElement _btnImport => FindElementById("ctl00_Content_btnImportCm");
        private IWebElement _btnExport => FindElementById("ctl00_Content_btnExport");
        private IWebElement _btnSubmitForApproval => FindElementById("ctl00_Content_btnSubmitForApproval");
        private IWebElement _btnApprove => FindElementById("ctl00_Content_btnApprove");
        private IWebElement _btnSignoff => FindElementById("ctl00_Content_btnSignoff");
        private IWebElement _btnCancel => FindElementById("ctl00_Content_btnCancel");
        private IWebElement _btnEdit => FindElementById("ctl00_Content_btnEdit");
        private IWebElement _chkSelectAllItems => FindElementById("ctl00_Content_CurrentlyLoadedViewControlId_gvSchedule_ctl01_checkAll");
        private IWebElement _tblMediaScheduleItems => FindElementById("ctl00_Content_CurrentlyLoadedViewControlId_gvSchedule");
        private IWebElement _lblApprovalStatus => FindElementById("ctl00_Content_approvalStatusBadge");
        private IWebElement _lblMessagePanel => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _lnkTotalOtherCosts => FindElementById("ctl00_Content_CurrentlyLoadedViewControlId_ucMediascheduleSummary_lnkAllOtherCost");
        private IWebElement _chkSelectItem(IWebElement row) => row.FindElement(By.XPath(".//td[contains(@title,'Agency')]//input[@type='checkbox']"));
        private IWebElement _ddlView => FindElementByName("ctl00$Content$ddlMSView");

        public MediaSchedulePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectViewByName(string viewName)
        {
            SelectWebformDropdownValueByText(_ddlView, viewName);
        }

        public void ClickAddSinglePlacement()
        {
            ClickElement(_btnAdd);
            ClickElement(_lnkSinglePlacement);
            SwitchToFrame(FRAME_SINGLE_PLACEMENT);
        }

        public void ClickAddPerformancePackage()
        {
            WaitForLoaderSpinnerToDisappear();
            ClickElement(_btnAdd);
            ClickElement(_lnkPerformancePackage);
            SwitchToFrame(FRAME_PERFORMANCE_PACKAGE);
        }

        public void ClickAddTotalOtherCosts()
        {
            ScrollAndClickElement(_lnkTotalOtherCosts);
        }

        public bool IsOtherCostDisplayed(MediaScheduleSummaryTableData data)
        {
            return IsElementPresent(By.XPath($"//a[@class='otherCosts']/ancestor::tr/td[contains(text(), '{data.TotalOtherVendorCost}')]"));
        }

        public void ClickAddSponsorshipPackage()
        {
            WaitForLoaderSpinnerToDisappear();
            ClickElement(_btnAdd);
            ClickElement(_lnkSponsorshipPackage);
            SwitchToFrame(FRAME_SPONSORSHIP_PACKAGE);
        }

        public void ClickConfirmAll()
        {
            ClickElement(_btnConfirmAll);
            SwitchToNewWindow(Strings.ConfirmMediaSchedule);
        }

        public void ClickConfirm()
        {
            ClickElement(_btnConfirm);
            SwitchToNewWindow(Strings.ConfirmMediaSchedule);
        }

        public void ClickCancel()
        {
            ClickElement(_btnCancel);
            SwitchToFrame(FRAME_CONFIRM_CANCEL);
        }

        public void ClickExportButton()
        {
            ClickElement(_btnExport);
            SwitchToFrame(FRAME_EXPORT);
        }

        public void ClickImportButton()
        {
            ClickElement(_btnImport);
            SwitchToFrame(FRAME_IMPORT);
        }

        public string GetApprovalStatus()
        {
            Wait.Until(driver => _lblApprovalStatus.Displayed);
            return _lblApprovalStatus.Text;
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }

        public void ClickApproveButton()
        {
            ClickElement(_btnApprove);
            SwitchToFrame(FRAME_APPROVE_MEDIA_SCHEDULE);
        }

        public void SelectAllMediaScheduleItems()
        {
            ClickElement(_chkSelectAllItems);
        }

        public void ClickSignOffButon()
        {
            ClickElement(_btnSignoff);
            SwitchToFrame(FRAME_BEGIN_SIGNOFF);
        }

        public bool WaitForCancellationStatusToBe(string status, int waitTime = 60)
        {
            return WaitForAllMediaScheduleItemsAttributeToHaveValue("cancelled", status, waitTime);
        }

        public bool WaitForAllMediaScheduleItemsHaveStatus(string status, int waitTime = 60)
        {
            return WaitForAllMediaScheduleItemsAttributeToHaveValue("status", status, waitTime);
        }

        public string GetItemVersionNumber(string name)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
            var placementRow = GetFirstElementWithText(rows, name);
            var version = placementRow.GetAttribute("version");
            return version;
        }

        public void OpenMediaScheduleItemVersionHistory(string name)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
            var placementRow = GetFirstElementWithText(rows, name);
            var placementRowVersionLink = placementRow.FindElement(By.XPath("//a[contains(@href, 'MediaScheduleItemHistory')]"));
            ClickElement(placementRowVersionLink);
            SwitchToFrame(FRAME_ITEM_HISTORY);
        }

        public void OpenSinglePlacementForEditing(string placementData)
        {
            OpenMediaScheduleItemForEditing(placementData);
            SwitchToSinglePlacementFrame();
        }

        public void OpenPerformancePackageForEditing(string packagetName)
        {
            OpenMediaScheduleItemForEditing(packagetName);
            SwitchToPerformancePackageFrame();
        }

        public void OpenSponsorshipPackageForEditing(string packagetName)
        {
            OpenMediaScheduleItemForEditing(packagetName);

            if (IsRestoreSponsorshipFrameVisible())
            {
                SwitchToConfirmRestoreFrame();
                ClickElement(_btnConfirmInEditPackageModal);
            }
            else
            {
                SwitchToSponsorshipPackageFrame();
            }
        }

        public bool IsVersionIncremented(int versionBeforeEdit, string costItemName)
        {
            int versionAfterEdit = int.Parse(GetItemVersionNumber(costItemName));
            if (versionAfterEdit != versionBeforeEdit + 1)
            {
                return false;
            }
            return true;
        }

        public void ClickEdit()
        {
            ClickElement(_btnEdit);
        }

        public bool VerifyAllSinglePlacementsExist(List<SinglePlacement> singlePlacements)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));

            if ((singlePlacements == null || !singlePlacements.Any()) && (rows.Any() || rows == null))
                throw new NotFoundException("Single placements missing in test data");

            foreach (var singlePlacement in singlePlacements)
            {
                var data = singlePlacement.IsAutomatedGuaranteedItem ? singlePlacement.DetailTabData.Name
                    //// TODO: This code to verify all RFP single placements, now only check the publisher, need to find new way more specification
                    : singlePlacement.DetailTabData.AutoGenerateName ? singlePlacement.DetailTabData.Publisher 
                    : singlePlacement.DetailTabData.Name;
                var found = GetFirstElementWithText(rows, data) == null ? false : true;

                if (!found)
                {
                    Console.WriteLine($"ERROR: Line item containing text '{data}' not found.");
                    return false;
                }
            }
            return true;
        }

        public bool VerifyAllPerformancePackageExist(List<PerformancePackage> performancePackages)
        {
            var packages = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr[@packageid]//td[contains(@class, 'package_icon')]"));

            if ((performancePackages == null || !performancePackages.Any()) && (packages == null || packages.Any()))
                throw new NotFoundException("Performance packages missing in test data");

            foreach (var package in performancePackages)
            {
                var expectedPackageName = package.ExpectedPackageName;

                var data = package.IsAutomatedGuaranteedItem ? package.DetailTabData.Name
                    : package.DetailTabData.AutoGenerateName ? expectedPackageName
                    : package.DetailTabData.Name;

                var found = GetFirstElementWithText(packages, data) == null ? false : true;

                if (!found)
                {
                    Console.WriteLine($"ERROR: Line item containing text '{data}' not found.");
                    return false;
                }
            }
            return true;
        }

        public bool VerifyAllSponsorshipPackagesExist(List<SponsorshipPackage> sponsorshipPackages)
        {
            var packages = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr[@categoryid]//td[contains(@class, 'package_icon')]"));

            if ((sponsorshipPackages == null || !sponsorshipPackages.Any()) && (packages == null || packages.Any()))
                return false;

            foreach (var sponsorshipPackage in sponsorshipPackages)
            {
                var found = GetFirstElementWithText(packages, sponsorshipPackage.SponsorshipPackageName) == null ? false : true;
                if (!found)
                    return false;
            }
            return true;
        }

        public void ClickSubmitForApproval()
        {
            ClickElement(_btnSubmitForApproval);
        }

        private bool WaitForAllMediaScheduleItemsAttributeToHaveValue(string attribute, string value, int waitTime)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
            foreach (var row in rows)
            {
                try
                {
                    SetWaitTimeout(waitTime);
                    Wait.Until(d => row.GetAttribute(attribute).Equals(value));
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        private void OpenMediaScheduleItemForEditing(string mediaScheduleItemName)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
            SelectMediaScheduleItem(rows, mediaScheduleItemName);
            ClickEdit();
            WaitForLoaderSpinnerToDisappear();
        }

        public void ClickCancelMediaScheduleItem(string mediaScheduleItemData)
        {
            var rows = _tblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
            SelectMediaScheduleItem(rows, mediaScheduleItemData);
            ClickCancel();
        }

        private void SwitchToSinglePlacementFrame()
        {
            SwitchToFrame(FRAME_EDIT_SINGLE_PLACEMENT);
        }

        private void SwitchToPerformancePackageFrame()
        {
            SwitchToFrame(FRAME_EDIT_PERFORMANCE);
        }
        private void SwitchToSponsorshipPackageFrame()
        {
            SwitchToFrame(FRAME_EDIT_SPONSORSHIP);
        }

        private void SwitchToConfirmRestoreFrame()
        {
            SwitchToFrame(FRAME_RESTORE_SPONSORSHIP);
        }

        private bool IsRestoreSponsorshipFrameVisible()
        {
            return IsFrameVisible(FRAME_RESTORE_SPONSORSHIP);
        }

        private void SelectMediaScheduleItem(IReadOnlyCollection<IWebElement> rows, string name)
        {
            var itemRow = GetFirstElementWithText(rows, name);
            ClickElement(_chkSelectItem(itemRow));
        }

        private IWebElement GetFirstElementWithText(IReadOnlyCollection<IWebElement> elementList, string text)
        {
            return elementList.FirstOrDefault(element => element.Text.Contains(text));
        }

        public void ExpandAllPackageItems()
        {
            var packageNameLinks = FindElementsByCssSelector("td .package-name-link").ToList();
            foreach (var packageLink in packageNameLinks)
            {
                ClickElement(packageLink);
            }
        }

        //public bool AreAllMediaScheduleItemsHaveStatus(string status)
        //{
        //    var rows = TblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
        //    foreach (var row in rows)
        //    {
        //        try
        //        {
        //            if (!row.GetAttribute("status").Equals(status))
        //            {
        //                return false;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public MediaSchedulePage ClickCancelButton()
        //{
        //    ClickElement(BtnCancel);
        //    SwitchToFrame(FRAME_CONFIRM_CANCEL);
        //    ClickElement(BtnConfirmCancel);
        //    SwitchToDefaultContent();

        //    return this;
        //}

        //public bool AreAllMediaScheduleItemsCancelled()
        //{
        //    var rows = TblMediaScheduleItems.FindElements(By.XPath("./table/tbody/tr"));
        //    foreach (var row in rows)
        //    {
        //        try
        //        {
        //            if (row.GetAttribute("cancelled").Equals("false"))
        //            {
        //                return false;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //    }

        //    return true;
        //}

        //public bool IsNoItemsDisplayed()
        //{
        //    return IsElementPresent(By.XPath("//div[contains(text(), 'There are no line items to display.')]"));
        //}

        //public void ClickSubmitForApprovalButton()
        //{
        //    ClickElement(BtnSubmitForApproval);
        //    SwitchToFrame(FRAME_EXPORT);
        //}

        //public bool IsBtnApproveEnabled()
        //{
        //    return BtnApprove.Enabled;
        //}
    }
}