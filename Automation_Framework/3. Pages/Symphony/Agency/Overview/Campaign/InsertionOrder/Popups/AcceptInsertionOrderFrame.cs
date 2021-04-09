using System;
using System.Configuration;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class AcceptInsertionOrderFrame : BasePage
    {
        private IWebElement _txtPassword => FindElementById("ctl00_Content_txtPassword");
        private IWebElement _ddlNotify => FindElementById("ctl00_Content_ddlContacts");
        private IWebElement _chkTermAndConditions => FindElementById("ctl00_Content_chkTermsAndConditions");
        private IWebElement _fileSelect => FindElementById("ctl00_Content_CurrentUserOrgNewAttachment_fileUpload");
        private IWebElement _btnUpload => FindElementById("ctl00_Content_CurrentUserOrgNewAttachment_btnUpload");
        private IWebElement _btnCancel => FindElementById("ctl00_ButtonBar_btnCancel");
        private IWebElement _btnSignOff => FindElementById("ctl00_ButtonBar_btnAccept");

        public AcceptInsertionOrderFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void UploadAgencyDocuments(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var binpath = path.Substring(0, path.LastIndexOf("bin"));
                var filelocation = Path.Combine(binpath, $@"Documents\\{fileName}");
                _fileSelect.SendKeys(filelocation);
                ClickElement(_btnUpload);
            }
        }

        public void SignOffIO()
        {
            if (!_chkTermAndConditions.Selected)
            {
                ClickElement(_chkTermAndConditions);
            }

            ClickElement(_btnSignOff);
            SwitchToMainWindow();
        }
    }
}