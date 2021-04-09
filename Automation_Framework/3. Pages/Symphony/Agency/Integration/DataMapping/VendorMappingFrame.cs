using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.DataMapping
{
    public class VendorMappingFrame : BasePage
    {
        private IWebElement _txtForeignId => FindElementById("ctl00_Content_ctl00_txtForeignId");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _pnlMessageSuccess => FindElementById("ctl00_Content_pnlMessagePopup");

        public VendorMappingFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddVendorMappingData(Field data)
        {
            EnsureMandatoryValuesAreProvided(data);

            switch (data.Name.ToLower())
            {
                case "foreignid":
                    ClearInputAndTypeValueIfRequired(_txtForeignId, data.Value);
                    break;
            }
        }

        public void ClickSave()
        {
            ScrollAndClickElement(_btnSave);
        }

        public string GetMessage()
        {
            Wait.Until(driver => _pnlMessageSuccess.Displayed);
            return _pnlMessageSuccess.Text;
        }

        public void CloseDataMappingFrame()
        {
            ScrollAndClickElement(_btnClose);
        }

        private void EnsureMandatoryValuesAreProvided(Field vendorData)
        {
            var dataErrorFound = false;
            var vendorDataErrors = new StringBuilder();
            vendorDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(vendorData.Name))
            {
                dataErrorFound = true;
                vendorDataErrors.Append("\n- Field name was not set");
            }

            if (string.IsNullOrWhiteSpace(vendorData.Value))
            {
                dataErrorFound = true;
                vendorDataErrors.Append("\n- Field value was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(vendorDataErrors.ToString());
        }
    }
}
