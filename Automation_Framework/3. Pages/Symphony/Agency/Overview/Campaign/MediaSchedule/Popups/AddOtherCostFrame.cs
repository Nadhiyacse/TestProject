using System;
using System.Collections.Generic;
using System.Text;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class AddOtherCostFrame : BasePage
    {
        private IWebElement _ddlCountry => FindElementByXPath("//label[text() = 'Country:']/..//select[@name='ctl00$Content$ddlCountry']");
        private IWebElement _ddlCategory => FindElementByXPath("//label[text() = 'Category:']/..//select[@name='ctl00$Content$ddlCategory']");
        private IWebElement _ddlVendor => FindElementByXPath("//label[text() = 'Vendor:']/..//select[@name='ctl00$Content$ddlVendor']");
        private IWebElement _txtAgencyCost => FindElementByName("ctl00$Content$txtAgencyCost");
        private IWebElement _txtDescription => FindElementByName("ctl00$Content$txtDescription");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _lblAddOtherCostPanelMessage => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _btnClose => FindElementByCssSelector("#ctl00_pnlButtonBar #ctl00_ButtonBar_btnClose");
        private IWebElement _btnBack => FindElementById("btnBack");

        public AddOtherCostFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddOtherCosts(List<NonMediaCostData> otherCostsData)
        {
            foreach (var otherCostData in otherCostsData)
            {
                EnsureMandatoryValuesAreProvided(otherCostData);
                SetAddOtherCostData(otherCostData);
            }
        }

        private void SetAddOtherCostData(NonMediaCostData othercostData)
        {
            SelectWebformDropdownValueByText(_ddlCountry, othercostData.Country);
            SelectWebformDropdownValueByText(_ddlCategory, othercostData.Category);
            SelectWebformDropdownValueByText(_ddlVendor, othercostData.Vendor);
            ClearInputAndTypeValue(_txtAgencyCost, othercostData.AgencyCost);
            ClearInputAndTypeValue(_txtDescription, othercostData.Description);
            ClickElement(_btnSave);
        }

        public bool IsOtherCostItemAddedSuccessfully()
        {
            return _lblAddOtherCostPanelMessage.Text.Contains("Other Cost item has been successfully created");
        }

        public void CloseAddOtherCostFrame()
        {
            ClickElement(_btnClose);
            SwitchToDefaultContent();
        }

        private void EnsureMandatoryValuesAreProvided(NonMediaCostData othercostData)
        {
            var dataErrorFound = false;
            var otherCostDataErrors = new StringBuilder();
            otherCostDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(othercostData.AgencyCost))
            {
                dataErrorFound = true;
                otherCostDataErrors.Append("\n- Mandatory field Agency Cost is not available in test data file");
            }

            if (string.IsNullOrWhiteSpace(othercostData.Country))
            {
                dataErrorFound = true;
                otherCostDataErrors.Append("\n- Mandatory field Country is not available in test data file");
            }

            if (string.IsNullOrWhiteSpace(othercostData.Description))
            {
                dataErrorFound = true;
                otherCostDataErrors.Append("\n- Mandatory field Description is not available in test data file");
            }

            if (string.IsNullOrWhiteSpace(othercostData.Category))
            {
                dataErrorFound = true;
                otherCostDataErrors.Append("\n- Mandatory field Category is not available in test data file");
            }

            if (string.IsNullOrWhiteSpace(othercostData.Vendor))
            {
                dataErrorFound = true;
                otherCostDataErrors.Append("\n- Mandatory field Vendor is not available in test data file");
            }

            if (dataErrorFound)
                throw new ArgumentException(otherCostDataErrors.ToString());
        }
    }
}
