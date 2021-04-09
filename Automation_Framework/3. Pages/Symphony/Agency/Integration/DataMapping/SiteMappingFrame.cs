using System.Text;
using System;
using Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.DataMapping
{
    public class SiteMappingFrame : BasePage
    {
        private IWebElement _txtForeignId => FindElementById("ctl00_Content_ctl00_txtForeignId");
        private IWebElement _chkNetworkOverride => FindElementById("ctl00_Content_ctl00_chkNetworkOverride");
        private IWebElement _chkClientOverride => FindElementById("ctl00_Content_ctl00_chkClientOverride");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _pnlMessageSuccess => FindElementById("ctl00_Content_pnlMessagePopup");

        public SiteMappingFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddSiteMappingData(Field data)
        {
            EnsureMandatoryValuesAreProvided(data);

            switch (data.Name.ToLower())
            {
                case "foreignid":
                    ClearInputAndTypeValueIfRequired(_txtForeignId, data.Value);
                    break;
                case "network override":
                    SetWebformCheckBoxState(_chkNetworkOverride, bool.Parse(data.Value));
                    break;
                case "client override":
                    SetWebformCheckBoxState(_chkClientOverride, bool.Parse(data.Value));
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

        private void EnsureMandatoryValuesAreProvided(Field field)
        {
            var dataErrorFound = false;
            var siteDataErrors = new StringBuilder();
            siteDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(field.Name))
            {
                dataErrorFound = true;
                siteDataErrors.Append("\n- Field name was not set");
            }

            if (string.IsNullOrWhiteSpace(field.Value))
            {
                dataErrorFound = true;
                siteDataErrors.Append("\n- Field value was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(siteDataErrors.ToString());
        }
    }
}
