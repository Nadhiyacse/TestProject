using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TechTalk.SpecFlow;
using Automation_Framework.DataModels.WorkflowTestData.MediaScheduleItem.NonMediaCosts;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using Automation_Framework._3._Pages.Symphony.Common.Enums;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder
{
    public class AddEditInsertionOrderPage : BasePage
    {
        private const string FRAME_ADD_IO_COST_ITEMS = "/InsertionOrder/AddIOCostItems.aspx?";
        private const string FRAME_ADD_IO_ITEMS = "/AddIOItems.aspx?";

        private IWebElement _btnSave => FindElementById("ctl00_Content_btnSave");
        private IWebElement _btnCancel => FindElementById("ctl00_Content_btnCancel");
        private IWebElement _txtIOName => FindElementById("ctl00_Content_txtIoName");
        private IWebElement _txtBillingAddress => FindElementById("ctl00_Content_ucIOSetting_txtBillingAddress");
        private IWebElement _txtPaymentTerm => FindElementById("ctl00_Content_ucIOSetting_txtPaymentTerms");
        private IWebElement _lnkAgencyContact => FindElementById("ctl00_Content_ucIOSetting_lnkAgencyContact");
        private IWebElement _txtInvoiceEmail => FindElementById("ctl00_Content_ucIOSetting_txtInvoiceEmail");
        private IWebElement _txtComments => FindElementById("ctl00_Content_ucIOSetting_txtComments");
        private IWebElement _txtName => FindElementById("ctl00_Content_ucIOSetting_txtFinanceContactName");
        private IWebElement _txtInvoiceFax => FindElementById("ctl00_Content_ucIOSetting_txtInvoiceFax");
        private IWebElement _txtEmail => FindElementById("ctl00_Content_ucIOSetting_txtFinanceContactEmail");
        private IWebElement _lnkEdit => FindElementById("ctl00_Content_ucIOSetting_lnkEditUpload");
        private IWebElement _fileSelect => FindElementById("ctl00_Content_ucIOSetting_flToCUpload");
        private IWebElement _lnkCancel => FindElementById("ctl00_Content_ucIOSetting_lnkCancelUpload");
        private IWebElement _txtPhone => FindElementById("ctl00_Content_ucIOSetting_txtFinanceContactPhone");
        private IWebElement _txtBookingAgency => FindElementById("ctl00_Content_ucIOSetting_txtRepresentedByAgency");
        private IWebElement _btnExclude => FindElementById("ctl00_Content_tabWidgetControl_tabMediaSchedule_ucIOItemList_btnRevise");
        private IWebElement _btnInclude => FindElementById("ctl00_Content_tabWidgetControl_tabMediaSchedule_ucIOItemList_btnAdd");
        private IWebElement _lnkTabNonMediaCostsIncluded => FindElementById("lnktabOtherCosts");
        private IWebElement _btnIncludeNonMediaCosts => FindElementById("ctl00_Content_tabWidgetControl_tabOtherCosts_ucIOOtherCostItemList_btnAdd");
        private IWebElement _tblNonMediaCostItems => FindElementById("ctl00_Content_tabWidgetControl_tabOtherCosts_ucIOOtherCostItemList_gvOtherCost");

        public AddEditInsertionOrderPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void EnterBillingAddress(string address)
        {
            if (!string.IsNullOrEmpty(address))
            {
                ClearInputAndTypeValue(_txtBillingAddress, address);
            }
        }

        public void EnterPaymentTerms(string paymentTerms)
        {
            if (!string.IsNullOrEmpty(paymentTerms))
            {
                ClearInputAndTypeValue(_txtPaymentTerm, paymentTerms);
            }
        }

        public void EnterInvoiceEmail(string invoiceEmail)
        {
            if (!string.IsNullOrEmpty(invoiceEmail))
            {
                ClearInputAndTypeValue(_txtInvoiceEmail, invoiceEmail);
            }
        }

        public void EnterComments(string comments)
        {
            if (!string.IsNullOrEmpty(comments))
            {
                ClearInputAndTypeValue(_txtComments, comments);
            }
        }

        public void EnterName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ClearInputAndTypeValue(_txtName, name);
            }
        }

        public void EnterIOName(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ClearInputAndTypeValue(_txtIOName, name);
            }
        }

        public void EnterInvoiceFax(string invoiceFax)
        {
            if (!string.IsNullOrEmpty(invoiceFax))
            {
                ClearInputAndTypeValue(_txtInvoiceFax, invoiceFax);
            }
        }

        public void EnterEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                ClearInputAndTypeValue(_txtEmail, email);
            }
        }

        public void UploadTermConditions(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var binpath = path.Substring(0, path.LastIndexOf("bin"));
                var filelocation = Path.Combine(binpath, $@"Documents\\{fileName}");
                _fileSelect.SendKeys(filelocation);
            }
        }

        public void EnterPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                ClearInputAndTypeValue(_txtPhone, phone);
            }
        }

        public void EnterBookingAgency(string bookingAgency)
        {
            if (!string.IsNullOrEmpty(bookingAgency))
            {
                ClearInputAndTypeValue(_txtBookingAgency, bookingAgency);
            }
        }

        public void SaveIO()
        {
            ClickElement(_btnSave);

            var lnkIOName = FindElementById("ctl00_Content_lnkIOHeaderPanelIOName");

            Wait.Until(driver => lnkIOName.Displayed);
            FeatureContext[ContextStrings.IOName] = lnkIOName.Text;
        }

        public void ClickIncludeNonMediaCost()
        {
            ClickElement(_lnkTabNonMediaCostsIncluded);
            ScrollAndClickElement(_btnIncludeNonMediaCosts);
            SwitchToFrame(FRAME_ADD_IO_COST_ITEMS);
        }

        public void ClickNonMediaCostItemsIncludedTab()
        {
            ClickElement(_lnkTabNonMediaCostsIncluded);
        }

        public bool VerifyAllNonMediaCostItemsExist(List<NonMediaCostData> nonMediaCostData)
        {
            if ((nonMediaCostData == null || !nonMediaCostData.Any()))
                throw new Exception($"The non media cost data is not available in Test data");

            var rows = _tblNonMediaCostItems.FindElements(By.XPath("./table/tbody/tr"));
            if (!(rows != null && rows.Any()))
                throw new Exception($"The non media cost is not available in Insertion Order page");

            foreach (var nonMediaCost in nonMediaCostData)
            {
                var isFound = GetFirstElementWithText(rows, nonMediaCost.Name) != null;

                if (!isFound)
                    return false;
            }
            return true;
        }

        public bool VerifyEditOnNonMediaCostItems(List<NonMediaCostData> nonMediaCostData)
        {
            if ((nonMediaCostData == null || !nonMediaCostData.Any()))
                throw new Exception($"The non media cost data is not available in Test data");

            var rows = _tblNonMediaCostItems.FindElements(By.XPath("./table/tbody/tr"));
            if (!(rows != null && rows.Any()))
                throw new Exception($"The non media cost is not available in Insertion Order page");

            foreach (var nonMediaCost in nonMediaCostData)
            {
                var nonMediaCostItem = GetFirstElementWithText(rows, nonMediaCost.Name);
                var isEditUpdated = ElementContainsText(nonMediaCostItem, nonMediaCost.EditNonMediaCostData.AgencyCost);

                if (!isEditUpdated)
                    return false;
            }
            return true;
        }

        public void ClickInclude()
        {
            ScrollAndClickElement(_btnInclude);
            SwitchToFrame(FRAME_ADD_IO_ITEMS);
        }
    }
}