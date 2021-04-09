using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Common
{
    public class ExportFramePage : BasePage
    {
        protected IWebElement _btnContinue => FindElementById("ctl00_ButtonBar_btnContinue");
        protected IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        protected IWebElement _ddlExportType => FindElementById("ctl00_Content_ddlExportProvider");
        protected IWebElement _ddlLanguage => FindElementById("ctl00_Content_CurrentlyLoadedExportControlId_LanguageSelection_ddlLanguage");
        protected IWebElement _ddlDeliveryMethod => FindElementById("ctl00_Content_ddlDeliveryStrategy");
        protected IWebElement _ddlCountry => FindElementById("ctl00_Content_CurrentlyLoadedExportControlId_CountrySelection_dropCountryList");
        protected IWebElement _msgExportSuccess => FindElementByXPath("//*[@id = 'pnlMessage' and @class = 'message success']");
        protected IWebElement _msgCreatingExportFile => FindElementByXPath("//*[@id = 'pnlMessage' and @class = 'message info']");

        private const string FRAME_EXPORT = "/Export/Export.aspx?";

        public ExportFramePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectExportType(string exportType)
        {
            SelectWebformDropdownValueByText(_ddlExportType, exportType);
        }

        public void SelectCountry(string country)
        {
            SelectWebformDropdownValueByText(_ddlCountry, country);
        }

        public void SelectLanguage(string language)
        {
            SelectWebformDropdownValueByText(_ddlLanguage, language);
        }

        public void SelectDeliveryMethod(string method)
        {
            SelectWebformDropdownValueByText(_ddlDeliveryMethod, method);
        }

        public string GetSuccessMessage()
        {
            var msgSuccess = string.Empty;
            Wait.Until(driver => _msgExportSuccess.Displayed);
            msgSuccess = _msgExportSuccess.Text;
            ScrollAndClickElement(_btnClose);
            SwitchToMainWindow();
            return msgSuccess;
        }

        public void SwitchToExportFrame()
        {
            SwitchToFrame(FRAME_EXPORT);
        }

        public void CloseExportFrame()
        {
            ClickElement(_btnClose);
        }
    }
}
