using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.Common;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class AddEditNonMediaCostModal : AddEditModal
    {
        //tab links
        private IWebElement _tabDetails => FindElementByXPath("//li[contains(@class, 'detail-tab')]//a");
        private IWebElement _tabCosts => FindElementByXPath("//li[contains(@class, 'costs-tab')]//a");

        // Common Elements
        private IWebElement _btnAdd => FindElementByCssSelector(".modal-heading .btn-primary.btn-default");
        private IWebElement _btnClose => FindElementByXPath("//div[text()='Close']/ancestor::button");
        private IWebElement _btnSave => FindElementByXPath("//div[text()='Save']/ancestor::button");
        private IWebElement _lblAlert => FindElementByXPath("//div[@role='alert']");

        // Details tab
        private IWebElement _txtName => FindElementByXPath("//input[@placeholder='Enter a cost name']");
        private IWebElement _txtStartDate => FindElementByXPath("//label[text() = 'Start']/..//input");
        private IWebElement _txtEndDate => FindElementByXPath("//label[text() = 'End']/..//input");
        private IWebElement _ddlPurchaseOrderNumber => FindElementByXPath("//label[text() = 'PO Number']/../div");
        private IWebElement _txtAgencyCost => FindElementById("cost");
        private IWebElement _txtComments => FindElementById("comments");
        private IWebElement _ddlCategory => FindElementByXPath("//label[text() = 'Category']/..//div[@id='vendorCategories']");
        private IWebElement _ddlVendor => FindElementByXPath("//label[text() = 'Vendor']/..//div[@id='vendors']");
        
        //Costs tab
        private IWebElement _rowAdjustment(string adjustmentName) => FindElementByXPath($"//div[text()='{adjustmentName}']/ancestor::div[2]");
        private IWebElement _chkBaseCost(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"(.//input[@type='checkbox'])[{columnNumber}]"));
        private IWebElement _txtRate(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"(.//div[contains(@class, 'grid-component-cell-value')]/input)[{columnNumber}]"));
        private IWebElement _txtCost(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"(.//div[@class='grid-component-cell']/input)[{columnNumber}]"));
        private IWebElement _ddlType(string adjustmentName, int columnNumber) => _rowAdjustment(adjustmentName).FindElement(By.XPath($"(.//div[contains(@class, 'select-component__control')])[{columnNumber}]"));
        private IWebElement _txtAgencyFee => FindElementByXPath($"//div[text()='Agency Fee']/ancestor::div[2]//div[contains(@class, 'grid-component-cell-value')]/input");

        public AddEditNonMediaCostModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddNonMediaCost(NonMediaCostData nonMediaCostData, bool isHideClassification)
        {
            EnsureMandatoryValuesAreProvided(nonMediaCostData, isHideClassification);
            SetNonMediaCostData(nonMediaCostData, isHideClassification);
        }

        public void EditNonMediaCost(EditNonMediaCostData editNonMediaCostData)
        {
            if (string.IsNullOrWhiteSpace(editNonMediaCostData.Name))
            {
                ClearInputAndTypeValue(_txtName, editNonMediaCostData.Name);
            }

            if (string.IsNullOrWhiteSpace(editNonMediaCostData.StartDate))
            {
                editNonMediaCostData.StartDate = GetTodaysDate();
                EnterDate(_txtStartDate, editNonMediaCostData.StartDate);
            }

            if (string.IsNullOrWhiteSpace(editNonMediaCostData.EndDate))
            {
                editNonMediaCostData.EndDate = GetTodaysDate();
                EnterDate(_txtEndDate, editNonMediaCostData.EndDate);
            }

            if (!string.IsNullOrWhiteSpace(editNonMediaCostData.AgencyCost))
            {
                ClearInputAndTypeValue(_txtAgencyCost, editNonMediaCostData.AgencyCost);
            }
        }

        private void SetNonMediaCostData(NonMediaCostData nonMediaCostData, bool isHideClassification)
        {
            WaitForElementToBeVisible(_txtName);
            ClearInputAndTypeValue(_txtName, nonMediaCostData.Name);

            if (isHideClassification)
            {
                SelectSingleValueFromReactDropdownByText(_ddlCategory, nonMediaCostData.Category);
            }                
            SelectSingleValueFromReactDropdownByText(_ddlVendor, nonMediaCostData.Vendor);

            if (string.IsNullOrWhiteSpace(nonMediaCostData.StartDate))
            {
                nonMediaCostData.StartDate = GetTodaysDate();
            }

            if (string.IsNullOrWhiteSpace(nonMediaCostData.EndDate))
            {
                nonMediaCostData.EndDate = GetTodaysDate();
            }

            EnterDate(_txtStartDate, nonMediaCostData.StartDate);
            EnterDate(_txtEndDate, nonMediaCostData.EndDate);

            if (!string.IsNullOrWhiteSpace(nonMediaCostData.PurchaseOrderNumber))
            {
                SelectSingleValueFromReactDropdownByText(_ddlPurchaseOrderNumber, nonMediaCostData.PurchaseOrderNumber);
            }

            if (!string.IsNullOrWhiteSpace(nonMediaCostData.AgencyCost))
            {
                ClearInputAndTypeValue(_txtAgencyCost, nonMediaCostData.AgencyCost);
            }

            if (!string.IsNullOrWhiteSpace(nonMediaCostData.Comment))
            {
                ClearInputAndTypeValue(_txtComments, nonMediaCostData.Comment);
            }
        }

        private void EnsureMandatoryValuesAreProvided(NonMediaCostData nonMediaCostData, bool isHideClassification)
        {
            var dataErrorFound = false;
            var nonMediaCostDataErrors = new StringBuilder();
            nonMediaCostDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(nonMediaCostData.Name))
            {
                dataErrorFound = true;
                nonMediaCostDataErrors.Append("\n- Mandatory field Name is not available in test data file");
            }

            if (isHideClassification && string.IsNullOrWhiteSpace(nonMediaCostData.Category))
            { 
                dataErrorFound = true;
                nonMediaCostDataErrors.Append("\n- Mandatory field Category is not available in test data file");
            }

            if (string.IsNullOrWhiteSpace(nonMediaCostData.Vendor))
            {
                dataErrorFound = true;
                nonMediaCostDataErrors.Append("\n- Mandatory field Vendor is not available in test data file");
            }

            if (dataErrorFound)
                throw new ArgumentException(nonMediaCostDataErrors.ToString());
        }
    }
}
