using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.SponsorshipPackage;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class ManageSponsorshipPackageModal : AddEditModal
    {
        // Common Links
        private IWebElement _txtSponsorshipName => FindElementByXPath("//div[contains(@class, 'sponsorship-package')]//*[text() = 'Name']/../..//input");
        private IWebElement _ddlPublisher => FindElementByXPath("//div[contains(@class, 'sponsorship-package')]//div[contains(@class,'select-component__control')]");
        private IWebElement _btnAddPlacement => FindElementById("addPlacementButton");
        private IWebElement _btnSavePackage => FindElementByXPath("//div[text() = 'Save Package']/ancestor::button");
        private IWebElement _btnReplacePackage => FindElementByXPath("//div[text() = 'Replace']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath("//div[text() = 'Cancel']/ancestor::button");
        private IList<IWebElement> _btnLinkPlacement(string placementName) => FindElementsByXPath($"//div[@class='placement-row']//div[@class='grid-component-cell grid-component-cell-publisher-name']//div[contains(text(),'{placementName}')]/../..").ToList();

        public ManageSponsorshipPackageModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateSponsorshipPackage(SponsorshipPackage sponsorshipPackage)
        {
            WaitForLoaderSpinnerToDisappear();
            SelectSingleValueFromReactDropdownByText(_ddlPublisher, sponsorshipPackage.Publisher);
            ClearInputAndTypeValue(_txtSponsorshipName, sponsorshipPackage.SponsorshipPackageName);
        }

        public void EditSponsorshipPackage(EditData sponsorshipPackageEditData)
        {
            WaitForLoaderSpinnerToDisappear();
            Wait.Until(driver => !string.IsNullOrEmpty(_txtSponsorshipName.GetAttribute("value")));
            ClearInputAndTypeValue(_txtSponsorshipName, sponsorshipPackageEditData.Name);
        }

        public void ClickSavePackage()
        {
            ScrollAndClickElement(_btnSavePackage);
            WaitUntilAlertContains("All Done!");
            WaitForLoaderSpinnerToDisappear();
        }

        public void ClickReplacePackage()
        {
            ScrollAndClickElement(_btnReplacePackage);
            WaitUntilAlertContains("All Done!");
            WaitForLoaderSpinnerToDisappear();
        }

        public void ClickAddPlacement()
        {
            ScrollAndClickElement(_btnAddPlacement);
        }

        public void ClickCancel()
        {
            ClickElement(_btnCancel);
        }

        public void OpenPlacementWithPublisherName(string publisherName)
        {
            ClickElement(GetFirstPlacemenLinktWithPublisherName(publisherName));
        }

        private IWebElement GetFirstPlacemenLinktWithPublisherName(string publisherName)
        {
            return _btnLinkPlacement(publisherName).First();
        }
    }
}