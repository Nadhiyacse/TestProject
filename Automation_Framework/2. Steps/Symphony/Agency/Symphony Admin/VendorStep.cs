using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Vendors;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Vendors.Popups;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Symphony_Admin
{
    [Binding]
    public class VendorStep : BaseStep
    {
        private readonly VendorListPage _vendorListPage;
        private readonly VendorAddEditFrame _vendorAddEditFrame;

        public VendorStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _vendorListPage = new VendorListPage(driver, featureContext);
            _vendorAddEditFrame = new VendorAddEditFrame(driver, featureContext);
        }

        [Given(@"I configure my vendors")]
        public void ConfigureVendors()
        {
            foreach (var vendor in GenericSetupData.SymphonyAdminData.Vendors)
            {
                var vendorName = vendor.Vendor;

                if (_vendorListPage.IsVendorExistInCountry(vendor.Country, vendorName))
                {
                    _vendorListPage.ClickEdit(vendorName);
                    _vendorAddEditFrame.EditVendor(vendor);
                    Assert.AreEqual("successfully saving vendor", _vendorAddEditFrame.GetPanelMessage(), "The vendor was not saved.");
                    _vendorAddEditFrame.ClickClose();
                }
                else
                {
                    _vendorListPage.ClickCreate();
                    _vendorAddEditFrame.CreateVendor(vendor);
                }
            }
        }
    }
}