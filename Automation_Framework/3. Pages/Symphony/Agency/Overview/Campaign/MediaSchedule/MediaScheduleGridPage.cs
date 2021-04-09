using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Automation_Framework.DataModels.WorkflowTestData.Billing;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.PerformancePackage;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SinglePlacement;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using Automation_Framework.DataModels.WorkflowTestData.Campaign;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class MediaScheduleGridPage : BasePage
    {
        private const string FRAME_EXPORT = "/Export/Export.aspx?";

        private IWebElement _btnAdd => FindElementById("msgrid_add_button");
        private IWebElement _lnkSinglePlacement => FindElementById("msgrid_add_placement");
        private IWebElement _lnkPerformancePackage => FindElementById("msgrid_add_performance");
        private IWebElement _lnkSponsorshipPackage => FindElementById("msgrid_add_sponsorship");
        private IWebElement _grdMediaScheduleBody => FindElementByXPath("//div[@data-test-selector='media-schedule-grid']//div[@class='ag__group-body']");
        private IWebElement _duplicateSponsorshipPackageModal => FindElementByClassName("duplicate-package");
        private IWebElement _gridHeader => FindElementByXPath("//div[@class='ag__header ag__with-fix-and-free ag__sticky']");
        private IWebElement _btnEdit => FindElementById("msgrid_edit_button");
        private IWebElement _btnDuplicateReplaceDropdown => FindElementById("msgrid_duplicate_replace_dropdown");
        private IWebElement _lnkDuplicate => FindElementById("msgrid_duplicate_menu_option");
        private IWebElement _lnkReplace => FindElementById("msgrid_replace_menu_option");
        private IWebElement _btnLockUnlockDropdown => FindElementById("msgrid_lock_unlock_dropdown");
        private IWebElement _lnkLock => FindElementById("msgrid_lock_menu_option");
        private IWebElement _lnkUnlock => FindElementById("msgrid_unlock_menu_option");
        private IWebElement _btnSponsorshipPackagesDropdown => FindElementById("msgrid_sponsorship_dropdown");
        private IWebElement _lnkCreateSponsorshipPackages => FindElementById("msgrid_sponsorship-create-option");
        private IWebElement _btnConfirm => FindElementById("msgrid_confirm_button");
        private IWebElement _btnConfirmModal => FindElementByCssSelector("#confirm-items-modal .btn-primary");
        private IWebElement _btnSignOff => FindElementById("msgrid_signoff_button");
        private IWebElement _chkTermConditionAccepted => FindElementByCssSelector(".gtm_ms_grid_signoff_ag_modal .checkbox-component-icon");
        private IWebElement _btnSignOffModal => FindElementByCssSelector(".modal-sm .btn-primary");
        private IWebElement _chkSelectAllItems => FindElementByCssSelector(".ag__row--is-head .checkbox-component-icon");
        private IWebElement _btnImport => FindElementById("msgrid_import_button");
        private IWebElement _btnExport => FindElementById("msgrid_export_button");
        private IWebElement _btnAddNonMediaCost => FindElementById("msgrid_add_campaign_fixed_non_media_cost_item_button");
        private IWebElement _scrollBar => FindElementByClassName("ag__scrollbar-handle");
        private IWebElement _scrollableArea => FindElementByClassName("ag__scrollbar-scrollable-area");
        private IWebElement _btnAutomatedGuaranteed => FindElementById("media-schedule-tabs-tab-AG");
        private IWebElement _btnCancel => FindElementById("msgrid_cancel_button");
        private IWebElement _btnConfirmCancellationModal => FindElementByCssSelector(".modal-sm .btn-primary");
        private List<IWebElement> _lstCancelledItems => FindElementsByCssSelector(".gtm_ms_grid_cancelmodal ul li").ToList();
        private IWebElement _btnConfirmToRestoreItems => FindElementByCssSelector(".restore-sponsorship-items .btn-primary");
        private IWebElement _liAgTab => FindElementByXPath(".//ul[@role = 'tablist']/li[2]");
        private IWebElement _btnGroup => FindElementByXPath("//span[text()='Group']/ancestor::button");
        private IWebElement _btnGroupCategory(string groupCategory) => FindElementByXPath($"//div[@data-test-selector='media-schedule-grid-group-by-menu']/div[text()='{groupCategory}']");
        private IWebElement _txtGroupHeader(string groupHeader) => FindElementByXPath($"//div[@class='ag__cell ag__cell--is-group-label']/span[text()='{groupHeader}:']");
        private IWebElement _campaignStatusDropdown => FindElementById("campaign-status-dropdown");
        private IWebElement _btnManageColumns => FindElementById("msgrid_manage_columns_button");
        private IWebElement _btnManageColumnsModalSave => FindElementByCssSelector(".modal-dialog .btn-primary div");
        private IWebElement GetDropdownOption(string option)
        {
            return FindElementByXPath($"//span[contains(text(), '{option}')]/../..");
        }

        private const string ACCEPTED_CONFIRMED_VERSION_XPATH = "(.//div[@class='version-column']/span/span)[5]";
        private const string SIGNOFF_VERSION_XPATH = "(.//div[@class='version-column']/span/span)[1]";
        private const string VERSION_BUTTON_XPATH = ".//span[@role='button']/span";

        public MediaScheduleGridPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickConfirmToRestoreItems()
        {
            ClickElement(_btnConfirmToRestoreItems);
        }

        public void ClickAddSinglePlacement()
        {
            ClickElement(_btnAdd);
            ClickElement(_lnkSinglePlacement);
        }

        public void ClickAddPerformancePackage()
        {
            WaitForLoaderSpinnerToDisappear();
            ClickElement(_btnAdd);
            ClickElement(_lnkPerformancePackage);
        }

        public void ClickAddSponsorshipPackage()
        {
            WaitForLoaderSpinnerToDisappear();
            ClickElement(_btnAdd);
            ClickElement(_lnkSponsorshipPackage);
        }

        public void ClickDuplicate()
        {
            if (!_lnkDuplicate.Displayed)
            {
                ClickElement(_btnDuplicateReplaceDropdown);
            }

            ClickElement(_lnkDuplicate);
        }

        public void ClickReplace()
        {
            if (!_lnkDuplicate.Displayed)
            {
                ClickElement(_btnDuplicateReplaceDropdown);
            }

            ClickElement(_lnkReplace);
        }

        public void Lock()
        {
            if (!_lnkLock.Displayed)
            {
                ClickElement(_btnLockUnlockDropdown);
            }

            ClickElement(_lnkLock);
        }

        public void ClickCreateSimpleSponsorshipPackage()
        {
            if (!_lnkCreateSponsorshipPackages.Displayed)
            {
                ClickElement(_btnSponsorshipPackagesDropdown);
            }

            ClickElement(_lnkCreateSponsorshipPackages);
        }

        public void Unlock()
        {
            if (!_lnkUnlock.Displayed)
            {
                ClickElement(_btnLockUnlockDropdown);
            }

            ClickElement(_lnkUnlock);
        }

        public void ClickCancel()
        {
            ClickElement(_btnCancel);
            WaitForElementToBeVisible(_btnConfirmCancellationModal);
        }

        public void ClickConfirmCancellation()
        {
            ClickElement(_btnConfirmCancellationModal);
        }

        public void CheckApprovalStatus(string expectedStatus)
        {
            try
            {
                Wait.Until(driver => _campaignStatusDropdown.Text == expectedStatus);
            }
            catch
            {
                throw new Exception($"The expected approval status '{expectedStatus}' was not found, status is '{_campaignStatusDropdown.Text}'");
            }
        }

        public void SelectCampaignStatusDropdownAndOption(string option)
        {
            ClickElement(_campaignStatusDropdown);
            GetDropdownOption(option).Click();
        }

        public bool CheckItemsOnCancellationModal(int itemsCount, string message)
        {
            return _lstCancelledItems.Any(t => t.Text.Contains($"{itemsCount} {message}"));
        }

        public void SelectCostItemRowWithName(string costItemRowName, bool state = true)
        {
            var costItemRow = _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{costItemRowName}')]/../.."));
            var rowCheckBox = costItemRow.FindElement(By.ClassName("checkbox-component-icon"));
            SetReactCheckBoxState(rowCheckBox, state);
        }

        public void SelectSinglePlacement(string singlePlacementName, bool state)
        {
            var singlePlacementCheckbox = GetCheckboxBySinglePlacementName(singlePlacementName);
            SetReactCheckBoxState(singlePlacementCheckbox, state);
        }

        private IWebElement GetCheckboxBySinglePlacementName(string singlePlacementName)
        {
            return _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{singlePlacementName}')]//..//div[1]//div[@class='checkbox-component-icon']"));
        }

        public void CheckAllCostItems()
        {
            var uncheckedCostItemHeader = _grdMediaScheduleBody.FindElements(By.XPath("//div[contains(@class, 'ag__row--is-head')]//div[@class='checkbox-component']"));

            if (uncheckedCostItemHeader.Any())
            {
                ScrollAndClickElement(uncheckedCostItemHeader[0]);
            }
        }

        public void UncheckAllCostItems()
        {
            var checkedCostItems = _grdMediaScheduleBody.FindElements(By.CssSelector(".checkbox-component.checked"));

            foreach (var checkedCostItem in checkedCostItems)
            {
                var checkBox = checkedCostItem.FindElement(By.ClassName("checkbox-component-icon"));
                ScrollAndClickElement(checkBox);
                if (_grdMediaScheduleBody.FindElements(By.CssSelector(".checkbox-component.checked")).Count == 0)
                    break;
            }
        }

        public void CollapseAllPackages()
        {
            var costItemNameCells = _grdMediaScheduleBody.FindElements(By.XPath($"//a[contains(@class, 'ag__icon') and contains(@class, 'ag__icon-bundle') and contains(@class, 'ag__icon-bundle-collapsed')]/../.."));

            foreach (var costItemNameCell in costItemNameCells)
            {
                Actions builder = new Actions(driver);

                if (!IsElementClickable(costItemNameCell))
                {
                    ScrollAndClickElement(costItemNameCell);
                }

                var collapseButton = costItemNameCell.FindElement(By.CssSelector(".ag__icon.ag__icon-bundle.ag__icon-bundle-collapsed"));

                builder.MoveToElement(costItemNameCell).Build().Perform();
                builder.Click(collapseButton).Build().Perform();
            }
        }

        public void ExpandAllPackages()
        {
            var costItemNameCells = _grdMediaScheduleBody.FindElements(By.XPath($"//a[contains(@class, 'ag__icon') and contains(@class, 'ag__icon-bundle') and not(contains(@class, 'ag__icon-bundle-collapsed'))]/../.."));

            foreach (var costItemNameCell in costItemNameCells)
            {
                Actions builder = new Actions(driver);

                if (!IsElementClickable(costItemNameCell))
                {
                    ScrollAndClickElement(costItemNameCell);
                }

                var expandButton = costItemNameCell.FindElement(By.XPath("//a[@class='ag__icon ag__icon-bundle ']"));

                builder.MoveToElement(costItemNameCell).Build().Perform();
                builder.Click(expandButton).Build().Perform();
            }
        }

        public void ExpandPackageWithName(string name)
        {
            var costItemNameCell = _grdMediaScheduleBody.FindElement(By.XPath($"//div[(text()='{name}')]/..//a[contains(@class,'ag__icon ag__icon-bundle')]/../.."));

            Actions builder = new Actions(driver);
            var expandButton = costItemNameCell.FindElement(By.CssSelector(".ag__icon.ag__icon-bundle"));

            if (!IsElementClickable(costItemNameCell))
            {
                ScrollAndClickElement(costItemNameCell);
            }

            builder.MoveToElement(costItemNameCell).Build().Perform();
            builder.Click(expandButton).Build().Perform();
        }

        public void EnterDuplicateSponsorshipPackageNameAndSave(string name)
        {
            WaitForElementToBeVisible(_duplicateSponsorshipPackageModal);
            var duplicateSponsorshipNameInput = _duplicateSponsorshipPackageModal.FindElement(By.TagName("input"));
            var saveButton = _duplicateSponsorshipPackageModal.FindElement(By.ClassName("btn-primary"));

            ClearInputAndTypeValue(duplicateSponsorshipNameInput, name);
            saveButton.Click();
        }

        public bool CheckAllPlacementsLocked()
        {
            WaitForElementToBeVisible(By.XPath($"//div[contains(@class, 'type-svg-column')]//*[name()='use' and contains(@href, 'locked')]"));
            var placementIcons = _grdMediaScheduleBody.FindElements(By.XPath($"//div[contains(@class, 'type-svg-column')]//*[name()='use']"));
            return placementIcons.All(element => element.GetAttribute("href").EndsWith("locked"));
        }

        public bool CheckAllPlacementsUnlocked()
        {
            WaitForElementToBeVisible(By.XPath($"//div[contains(@class, 'type-svg-column')]//*[name()='use' and not(contains(@href, 'locked'))]"));
            var placementIcons = _grdMediaScheduleBody.FindElements(By.XPath($"//div[contains(@class, 'type-svg-column')]//*[name()='use']"));
            return placementIcons.All(element => !element.GetAttribute("href").EndsWith("locked"));
        }

        private class RowGrouping
        {
            public IWebElement FixedColumns { get; set; }
            public IWebElement ScrollableColumns { get; set; }
        }

        private IList<RowGrouping> GetRowGroupings(string placementType)
        {            
            var rows = new List<IWebElement>();
            var placementXpath = $"//*[contains(@href,'{placementType}')]/../../../..";
            if (placementType.Equals("non-media-cost-item"))
            {
                placementXpath = $"(//*[contains(@href,'{placementType}')])[2]/../../../..";
            }               

            Wait.Until(d => _grdMediaScheduleBody.FindElements(By.XPath(placementXpath)).Count > 0);
            rows = _grdMediaScheduleBody.FindElements(By.XPath(placementXpath))
            .Where(element => !string.IsNullOrEmpty(element.Text)).ToList();          

            var rowGroups = new List<RowGrouping>();

            rows.ForEach(row =>
            {
                var fixedColumns = row.FindElement(By.CssSelector(".ag__cell-pane--is-fixed"));
                var scrollableColumns = row.FindElement(By.CssSelector(".ag__cell-pane--is-scrollable"));
                rowGroups.Add(new RowGrouping
                {
                    FixedColumns = fixedColumns,
                    ScrollableColumns = scrollableColumns
                });
            });

            return rowGroups;
        }

        private IList<RowGrouping> GetAllRows()
        {
            var rows = _grdMediaScheduleBody.FindElements(By.XPath("./div[contains(@class,'ag__row ag__row--is-body')]"))
                .Where(element => !string.IsNullOrEmpty(element.Text)).ToList();

            var rowGroups = new List<RowGrouping>();

            rows.ForEach(row =>
            {
                var fixedColumns = row.FindElement(By.CssSelector(".ag__cell-pane--is-fixed"));
                var scrollableColumns = row.FindElement(By.CssSelector(".ag__cell-pane--is-scrollable"));
                rowGroups.Add(new RowGrouping
                {
                    FixedColumns = fixedColumns,
                    ScrollableColumns = scrollableColumns
                });
            });

            return rowGroups;
        }

        public bool VerifyAllSinglePlacementsExist(List<SinglePlacement> singlePlacements)
        {
            var rowGroups = GetRowGroupings("placement");

            if (singlePlacements == null || !singlePlacements.Any() || !rowGroups.Any())
                throw new NotFoundException("Single placements missing in test data");

            foreach (var singlePlacement in singlePlacements)
            {
                var rowName = singlePlacement.IsImportedItem || !singlePlacement.DetailTabData.AutoGenerateName ? singlePlacement.DetailTabData.Name
                    : ReplaceItemNamePlaceholderText(singlePlacement.DetailTabData.Name);

                var rowGroup = new RowGrouping();
                try
                { 
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, rowName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected single placement '{rowName}' does not exist on the Media Schedule grid page");
                }
                var rowFixedColumns = rowGroup.FixedColumns;
                var rowScrollableColumns = rowGroup.ScrollableColumns;

                var siteData = singlePlacement.DetailTabData.Site;
                var isSiteFound = ElementContainsText(rowFixedColumns, siteData);

                var publisherData = singlePlacement.DetailTabData.Publisher.Contains("(") ? singlePlacement.DetailTabData.Publisher.Substring(0, singlePlacement.DetailTabData.Publisher.IndexOf('(')).Trim() : singlePlacement.DetailTabData.Publisher;
                var isPublisherFound = ElementContainsText(rowFixedColumns, publisherData);

                var locationData = singlePlacement.DetailTabData.Location;
                var isLocationFound = ElementContainsText(rowScrollableColumns, locationData);

                var creativeTypeData = singlePlacement.DetailTabData.CreativeType;
                var isCreativeTypeFound = ElementContainsText(rowScrollableColumns, creativeTypeData);

                if (!isSiteFound || !isPublisherFound || !isCreativeTypeFound)
                {
                    Console.WriteLine($"ERROR: Line item containing text '{siteData}', '{publisherData}', or '{creativeTypeData}' not found.");
                    Console.WriteLine($"Site found: {isSiteFound}\nPublisher found:{isPublisherFound}\nIsCreativeTypeFound: {isCreativeTypeFound}");
                    return false;
                }
            }

            return true;
        }

        public bool VerifyAllSinglePlacementsExistAsAg(List<SinglePlacement> singlePlacements)
        {
            var rowGroups = GetRowGroupings("placement");

            if (singlePlacements == null || !singlePlacements.Any() || !rowGroups.Any())
                throw new NotFoundException("Single placements missing in test data");

            foreach (var singlePlacement in singlePlacements)
            {
                var rowName = singlePlacement.IsImportedItem ? singlePlacement.DetailTabData.Name
                    : ReplaceItemNamePlaceholderText(singlePlacement.DetailTabData.Name);

                var rowGroup = new RowGrouping();
                try
                {
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, rowName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected single placement '{rowName}' does not exist on the Media Schedule grid page");
                }

                var rowFixedColumns = rowGroup.FixedColumns;

                try
                {
                    rowFixedColumns.FindElement(By.ClassName("adslot-row"));
                }
                catch (NoSuchElementException)
                {
                    throw new Exception($"The expected single placement '{rowName} was is not an AG item'");
                }
            }

            return true;
        }

        public bool VerifyAllPerformancePackageExist(List<PerformancePackage> performancePackages)
        {
            var rowGroups = GetRowGroupings("performance");

            if (performancePackages == null || !performancePackages.Any() || !rowGroups.Any())
                throw new NotFoundException("Performance packages missing in test data");

            foreach (var performancePackage in performancePackages)
            {
                var rowGroup = new RowGrouping();
                
                ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, "//div[contains(@data-test-selector, 'goal')]");
                var rowName = performancePackage.IsAutomatedGuaranteedItem ? performancePackage.DetailTabData.Name
                    : performancePackage.DetailTabData.AutoGenerateName ? performancePackage.ExpectedPackageName
                    : performancePackage.DetailTabData.Name;
                try
                { 
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, rowName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected performance package '{rowName}' doesnot exist on the Media Schedule grid page");
                }
                var rowFixedColumns = rowGroup.FixedColumns;
                var rowScrollableColumns = rowGroup.ScrollableColumns;

                var siteData = performancePackage.DetailTabData.Site;
                var isSiteFound = ElementContainsText(rowFixedColumns, siteData);

                var publisherData = performancePackage.DetailTabData.Publisher.Contains("(") ? performancePackage.DetailTabData.Publisher.Substring(0, performancePackage.DetailTabData.Publisher.IndexOf('(')).Trim() : performancePackage.DetailTabData.Publisher;
                var isPublisherFound = ElementContainsText(rowFixedColumns, publisherData);

                var purchaseTypeData = performancePackage.DetailTabData.PurchaseType;
                var isPurchaseTypeFound = ElementContainsText(rowScrollableColumns, purchaseTypeData);

                var goalData = int.Parse(performancePackage.DetailTabData.Goal).ToString("N0");
                var isGoalFound = ElementContainsText(rowScrollableColumns, goalData);

                if (!isSiteFound || !isPublisherFound || !isPurchaseTypeFound || !isGoalFound)
                {
                    Console.WriteLine($"ERROR: Line item containing text '{siteData}', '{publisherData}', '{purchaseTypeData}', or '{goalData}' not found.");
                    Console.WriteLine($"Site found: {isSiteFound}\nPublisher found:{isPublisherFound}\nIsPurchaseTypeFound: {isPurchaseTypeFound}\nIsGoalFound: {isGoalFound}");
                    return false;
                }
            }

            return true;
        }

        public bool VerifyAllPerformancePackageExistAsAg(List<PerformancePackage> performancePackages)
        {
            var rowGroups = GetRowGroupings("performance");

            if (performancePackages == null || !performancePackages.Any() || !rowGroups.Any())
                throw new NotFoundException("Performance packages missing in test data");

            foreach (var performancePackage in performancePackages)
            {
                var rowGroup = new RowGrouping();

                ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, "//div[contains(@data-test-selector, 'goal')]");
                var rowName = performancePackage.IsAutomatedGuaranteedItem ? performancePackage.DetailTabData.Name
                    : performancePackage.DetailTabData.AutoGenerateName ? performancePackage.ExpectedPackageName
                    : performancePackage.DetailTabData.Name;
                try
                {
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, rowName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected performance package '{rowName}' doesnot exist on the Media Schedule grid page");
                }

                var rowFixedColumns = rowGroup.FixedColumns;

                try
                {
                    rowFixedColumns.FindElement(By.ClassName("adslot-row"));
                }
                catch (NoSuchElementException)
                {
                    throw new Exception($"The expected performance package '{rowName} was is not an AG item'");
                }
            }

            return true;
        }

        public bool VerifyAllSponsorshipPackagesExist(List<SponsorshipPackage> sponsorshipPackages)
        {
            var rowGroups = GetRowGroupings("sponsorship");

            if (sponsorshipPackages == null || !sponsorshipPackages.Any() || !rowGroups.Any())
                return false;

            foreach (var sponsorshipPackage in sponsorshipPackages)
            {
                var rowGroup = new RowGrouping();
                try
                { 
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, sponsorshipPackage.SponsorshipPackageName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected sponsorship package '{sponsorshipPackage.SponsorshipPackageName}' doesnot exist on the Media Schedule grid page");
                }
                var rowFixedColumns = rowGroup.FixedColumns;
                var rowScrollableColumns = rowGroup.ScrollableColumns;

                var publisherData = sponsorshipPackage.Publisher.Contains("(") ? sponsorshipPackage.Publisher.Substring(0, sponsorshipPackage.Publisher.IndexOf('(')).Trim() : sponsorshipPackage.Publisher;
                var isPublisherFound = ElementContainsText(rowFixedColumns, publisherData);

                if (!isPublisherFound)
                    return false;
            }

            return true;
        }

        public Dictionary<NonMediaCostData, bool> VerifyAllNonMediaCostsDisplayed(List<NonMediaCostData> nonMediaCosts)
        {
            var rowGroups = GetRowGroupings("non-media-cost-item");
            var isNonMediaCostsDisplayed = new Dictionary<NonMediaCostData, bool>();

            if (nonMediaCosts == null || !nonMediaCosts.Any() || !rowGroups.Any())
                throw new ArgumentNullException($"Non media costs were missed in test data.");

            foreach (var nonMediaCost in nonMediaCosts)
            {
                var rowGroup = new RowGrouping();
                ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, "//div[contains(@data-test-selector,'rate-card-cost')]");
                try
                { 
                    rowGroup = rowGroups.First(rg => ElementContainsText(rg.FixedColumns, nonMediaCost.Name));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected non media cost '{nonMediaCost.Name}' doesnot exist on the Media Schedule grid page");
                }
                var isVendorFound = ElementContainsText(rowGroup.FixedColumns, nonMediaCost.Vendor);
                var agencyCostData = decimal.Parse(nonMediaCost.AgencyCost).ToString("N2");
                var isAgencyCostFound = ElementContainsText(rowGroup.ScrollableColumns, agencyCostData);
                var isNonMediaCostsFound = (isVendorFound && isAgencyCostFound);
                isNonMediaCostsDisplayed.Add(nonMediaCost, isNonMediaCostsFound);
            }

            return isNonMediaCostsDisplayed;
        }

        public Dictionary<CustomBillingItem, bool> VerifyAllBilledGoalsDisplayed(List<CustomBillingItem> customBillingItems)
        {
            ExpandAllPackages();

            var rows = GetAllRows();
            var areBilledGoalsDisplayed = new Dictionary<CustomBillingItem, bool>();

            if (customBillingItems == null || !customBillingItems.Any() || !rows.Any())
                throw new ArgumentNullException($"Custom billing items not found in test data.");

            foreach (var item in customBillingItems)
            {
                var rowGroup = new RowGrouping();
                ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, "//div[contains(@data-test-selector,'version')]");
                try
                {
                    var itemName = ReplaceItemNamePlaceholderText(item.Name);
                    rowGroup = rows.First(rg => ElementContainsText(rg.FixedColumns, itemName));
                }
                catch (InvalidOperationException)
                {
                    throw new Exception($"The expected billed goal for '{item.Name}' does not exist on the Medis Schedule Grid page");
                }
                var isBilledGoalFound = ElementContainsText(rowGroup.ScrollableColumns, item.BilledGoal);
                areBilledGoalsDisplayed.Add(item, isBilledGoalFound);
            }

            return areBilledGoalsDisplayed;
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

        public bool VerifyPlacementUpdated(EditData costItemEditData)
        {
            WaitForLoaderSpinnerToDisappear();
            var costItemRow = _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{costItemEditData.Name}')]/../../.."));
            var goalValue = costItemRow.FindElement(By.CssSelector("[data-test-selector=\"goal\"]"));

            return string.IsNullOrWhiteSpace(costItemEditData.Goal) || goalValue.Text == costItemEditData.Goal;
        }

        public void MeasureMediaScheduleItemsLoadTime(int maxLoop = 5)
        {
            var stopwatch = Stopwatch.StartNew();

            for (var loop = 1; loop <= maxLoop; loop++)
            {
                try
                {
                    Wait.Until(d => _grdMediaScheduleBody.Displayed);
                }
                catch (WebDriverException e)
                {
                    if (e.Message.Contains("timed out after 60 seconds") && loop == maxLoop)
                        throw new Exception($"Web page unresponsive after {loop} retries", e);

                    Thread.Sleep(500);
                }
            }
            
            stopwatch.Stop();
            FeatureContext[ContextStrings.ElapsedTime] = stopwatch.Elapsed;
        }

        public int GetNumberOfLineItemsLoaded(int maxLoop = 5)
        {
            var rowCount = 0;

            for (var loop = 1; loop <= maxLoop; loop++)
            {
                try
                {
                    var rows = Wait.Until(d => _grdMediaScheduleBody.FindElements(By.XPath("//div[@class='ag__row ag__row--is-body']")));
                    rowCount = rows.Count;
                }
                catch (WebDriverException e)
                {
                    if (e.Message.Contains("timed out after 60 seconds") && loop == maxLoop)
                        throw new Exception($"Web page unresponsive after {loop} retries", e);

                    Thread.Sleep(500);
                }
            }

            return rowCount;
        }

        public bool CheckNumberOfElementsExistsInGridBodyContainingText(string text, int numberOfElements = 1)
        {
            return CheckNumberOfElementsThatExistContainingText(_grdMediaScheduleBody, text, numberOfElements);
        }

        public bool CheckNumberOfElementsExistsInGridBodyByExactText(string text, int numberOfElements = 1)
        {
            return CheckNumberOfElementsThatExistByExactText(_grdMediaScheduleBody, text, numberOfElements);
        }

        public void WaitUntilItemWithTextExistsInGridContainingTextWithCount(string text, int numberOfElements = 1)
        {
            Wait.Until(driver => CheckNumberOfElementsExistsInGridBodyContainingText(text, numberOfElements));
        }

        public void WaitUntilItemWithExactTextExistsInGridWithCount(string text, int numberOfElements = 1)
        {
            Wait.Until(driver => CheckNumberOfElementsExistsInGridBodyByExactText(text, numberOfElements));
        }

        public void ClickConfirm()
        {
            ClickElement(_btnConfirm);
            WaitForElementToBeVisible(_btnConfirmModal);
            WaitForElementToBeClickable(_btnConfirmModal);
            ClickElement(_btnConfirmModal);
        }

        public void ClickSignOff()
        {
            ClickElement(_btnSignOff);
            WaitForElementToBeVisible(_btnSignOffModal);
            ClickElement(_chkTermConditionAccepted);
            ClickElement(_btnSignOffModal);
        }

        public void SelectAllMediaScheduleItems()
        {
            SetReactCheckBoxState(_chkSelectAllItems, true);
        }

        public bool WaitUntilAllMediaScheduleItemsHaveStatus(string status)
        {
            return WaitForAllMediaScheduleItemsStatus($"status-{status}");
        }

        public bool WaitUntilAllMediaScheduleItemsAreCancelled()
        {
            return WaitForAllMediaScheduleItemsStatus($"cancelled");
        }

        private bool WaitForAllMediaScheduleItemsStatus(string status)
        {
            var rows = _grdMediaScheduleBody.FindElements(By.XPath("./div[@class='ag__row ag__row--is-body']"));

            try
            {
                Wait.Until(d => rows.All(x => x.FindElement(By.ClassName(status)).Displayed));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public bool WaitForMediaScheduleItemStatus(string itemUniqueData, string status)
        {
            var costItemRow = _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{itemUniqueData}')]/../../.."));

            try
            {
                Wait.Until(d => costItemRow.FindElement(By.ClassName(status)));
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void ClickExportButton()
        {
            ClickElement(_btnExport);
            SwitchToFrame(FRAME_EXPORT);
        }

        public void ClickImportButton()
        {
            ClickElement(_btnImport);
        }

        public void ClickAddNonMediaCostButton()
        {
            WaitForLoaderSpinnerToDisappear();
            ClickElement(_btnAddNonMediaCost);
        }

        public void ClickEditButton()
        {
            ClickElement(_btnEdit);
        }

        public void OpenMediaScheduleItemForEditing(string mediaScheduleItemName)
        {
            SelectCostItemRowWithName(mediaScheduleItemName);
            ClickEditButton();
            WaitForLoaderSpinnerToDisappear();
        }

        public bool IsVersionIncremented(int versionBeforeEdit, string costItemName)
        {
            var versionAfterEdit = int.Parse(GetItemVersionNumber(costItemName));

            return (versionAfterEdit != versionBeforeEdit + 1) ? false : true;
        }

        public string GetItemVersionNumber(string costItemRowName)
        {
            ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, ACCEPTED_CONFIRMED_VERSION_XPATH);
            var costItemRow = _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{costItemRowName}')]/ancestor::div[contains(@class,'ag__row ag__row--is-body')]"));
            var version = costItemRow.FindElement(By.XPath(ACCEPTED_CONFIRMED_VERSION_XPATH));
            if (version.Text == "-")
            {
                version = costItemRow.FindElement(By.XPath(SIGNOFF_VERSION_XPATH));
            }
            return version.Text;
        }

        public void OpenMediaScheduleItemVersionHistory(string costItemRowName)
        {
            ScrollRightUntilElementIsDisplayed(_scrollBar, _scrollableArea, ACCEPTED_CONFIRMED_VERSION_XPATH);
            var costItemRow = _grdMediaScheduleBody.FindElement(By.XPath($"//div[contains(text(), '{costItemRowName}')]/ancestor::div[contains(@class,'ag__row ag__row--is-body')]"));
            var buttonVersion = costItemRow.FindElement(By.XPath(VERSION_BUTTON_XPATH));
            ClickElement(buttonVersion);
        }

        public void GroupMediaScheduleGridBy(string groupCategory)
        {
            ClickElement(_btnGroup);

            try
            {
                WaitForElementToBeVisible(_btnGroupCategory(groupCategory));
            }
            catch (NoSuchElementException e)
            {
                throw new NoSuchElementException($"Group category {groupCategory} element was not displayed on the page.", e);
            }

            _btnGroupCategory(groupCategory).Click();
        }

        public bool IsMediaScheduleGroupHeaderAppeared(string groupHeader)
        {
            return _txtGroupHeader(groupHeader).Displayed;
        }

        public bool IsAddButtonEnabled()
        {
            return _btnAdd.Enabled;
        }

        public bool IsImportButtonEnabled()
        {
            return _btnImport.Enabled;
        }

        public void CheckColumnsAreShown(IEnumerable<string> columnNames)
        {
            foreach (var columnName in columnNames)
            {
                var column = _gridHeader.FindElements(By.XPath($"//div[contains(@class, 'ag__header')]//div[contains(text(), '{columnName}') and @class='ag__cell--wrap-text']"));

                if (!column.Any())
                {
                    Assert.Fail($"Column {columnName} was not found when it should be shown");
                }
            }
        }

        public void CheckColumnsAreHidden(IEnumerable<string> columnNames)
        {
            foreach (var columnName in columnNames)
            {
                var column = _gridHeader.FindElements(By.XPath($"//div[contains(@class, 'ag__header')]//div[contains(text(), '{columnName}') and @class='ag__cell--wrap-text']"));

                if (column.Any())
                {
                    Assert.Fail($"Column {columnName} was found when it should be hidden");
                }
            }
        }

        public void OpenManageColumnModal()
        {
            WaitForElementToBeInvisible(By.CssSelector(".empty-state"));
            _btnManageColumns.Click();
        }

        public void WaitForExportDownloaded(string exportType, CampaignData campaignData)
        {
            WaitUntilFileIsDownloaded(exportType, campaignData);
        }
    }
}