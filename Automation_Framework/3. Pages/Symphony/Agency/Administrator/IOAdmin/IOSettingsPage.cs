using Automation_Framework.DataModels.InfrastructureData.Administrator;
using Automation_Framework.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.IOAdmin
{
    public class IOSettingsPage : BasePage
    {
        private IWebElement _txtBillingAddress => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtBillingAddress");
        private IWebElement _txtInvoiceEmail => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtInvoiceEmail");
        private IWebElement _txtInvoiceFax => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtInvoiceFax");
        private IWebElement _lnkEditTermsAndConditions => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_lnkEditUpload");
        private IWebElement _flUploadTermsAndConditions => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_flToCUpload");
        private IWebElement _txtPaymentTerms => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtPaymentTerms");
        private IWebElement _txtComments => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtComments");
        private IWebElement _txtFinanceContactName => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtFinanceContactName");
        private IWebElement _txtFinanceContactEmail => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtFinanceContactEmail");
        private IWebElement _txtFinanceContactPhone => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_txtFinanceContactPhone");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_ucIOSettings_ctl00_btnSaveDefaultSetting");

        public IOSettingsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void PopulateDefaultIOSettings(IOAdminData ioadminData)
        {
            ClearInputAndTypeValueIfRequired(_txtBillingAddress, ioadminData.BillingAddress);
            ClearInputAndTypeValueIfRequired(_txtInvoiceEmail, ioadminData.InvoiceEmail);
            ClearInputAndTypeValueIfRequired(_txtInvoiceFax, ioadminData.InvoiceFax);
            ClearInputAndTypeValueIfRequired(_txtPaymentTerms, ioadminData.PaymentTerms);
            ClearInputAndTypeValueIfRequired(_txtComments, ioadminData.Comments);
            ClearInputAndTypeValueIfRequired(_txtFinanceContactName, ioadminData.FinanceContactName);
            ClearInputAndTypeValueIfRequired(_txtFinanceContactEmail, ioadminData.FinanceContactEmail);
            ClearInputAndTypeValueIfRequired(_txtFinanceContactPhone, ioadminData.FinanceContactPhone);

            if (!string.IsNullOrEmpty(ioadminData.TermsAndConditionsFileName))
            {
                var fileNamePath = FileHelper.GetDocumentFilePath(ioadminData.TermsAndConditionsFileName);
                ClickElement(_lnkEditTermsAndConditions);
                TypeValueIfRequired(_flUploadTermsAndConditions, fileNamePath);
            }

            ClickElement(_btnSave);
        }
    }
}
