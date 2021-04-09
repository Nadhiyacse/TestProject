using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Vendors
{
    public class VendorListPage : BasePage
    {
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnCreate");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _lnkNext => FindElementByXPath("//a[@class = 'pager-page-item pager-page-item-next']");

        private const string VENDOR_EDIT_XPATH = "//tr/td[contains(text(), '{0}')]/..//td/a";
        private const string VENDOR_NAME_XPATH = "//tr/td[contains(text(), '{0}')]";
        private const string VENDOR_ADD_EDIT_FRAME = "VendorEdit.aspx";

        public VendorListPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
            SwitchToFrame(VENDOR_ADD_EDIT_FRAME);
        }

        public void ClickEdit(string vendorName)
        {
            var vendorNameBasedEditBtn = FindElementByXPath(string.Format(VENDOR_EDIT_XPATH, vendorName));

            ClickElement(vendorNameBasedEditBtn);
            SwitchToFrame(VENDOR_ADD_EDIT_FRAME);
        }

        public bool IsVendorExistInCountry(string country, string vendorName)
        {
            SelectWebformDropdownValueByText(_ddlCountry, country);

            bool isNextLinkPresent;

            do
            {
                if (IsElementPresent(By.XPath(string.Format(VENDOR_NAME_XPATH, vendorName))))
                    return true;

                isNextLinkPresent = IsElementPresent(By.XPath("//a[@class = 'pager-page-item pager-page-item-next']"));
                if (isNextLinkPresent)
                {
                    WaitForPageLoadCompleteAfterClickElement(_lnkNext);
                }
            }
            while (isNextLinkPresent);

            return false;
        }
    }
}