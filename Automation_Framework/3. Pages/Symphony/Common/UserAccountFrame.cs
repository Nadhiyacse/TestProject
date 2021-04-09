using System;
using Automation_Framework.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Common
{
    public class UserAccountFrame : BasePage
    {
        private IWebElement _txtSignatureUpload => FindElementById("ctl00_ctl00_Content_Content_flSignatureUpload");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_ButtonBar_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ctl00_ButtonBar_ButtonBar_btnClose");
        private IWebElement _pnlMessage => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");
        private IWebElement _imgEsignature => FindElementById("ctl00_ctl00_Content_Content_imgSignature");

        private const string FRAME_USER_ACCOUNT = "/Account/AccountInfo.aspx";

        public UserAccountFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void UploadEsignature(string esignatureFileName)
        {
            if (string.IsNullOrEmpty(esignatureFileName))
                throw new ArgumentNullException("Esignature file name missing in test data");
            
            var signatureFilePath = FileHelper.GetImageFilePath(esignatureFileName);
            TypeValueIfRequired(_txtSignatureUpload, signatureFilePath);
            ClickElement(_btnSave);
        }

        public void SwitchToUserAccountFrame()
        {
            SwitchToFrame(FRAME_USER_ACCOUNT);
        }

        public void CloseUserAccountFrame()
        {
            ClickElement(_btnClose);
            SwitchToMainWindow();
        }

        public string GetMsgText()
        {
            return _pnlMessage.Text;
        }

        public bool DoesEsignatureExist()
        {
            try
            {
                return _imgEsignature.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
    }
}
