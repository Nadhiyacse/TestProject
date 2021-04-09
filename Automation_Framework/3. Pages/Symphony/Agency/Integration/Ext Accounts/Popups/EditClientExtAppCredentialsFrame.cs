using System;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Integration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.Ext_Accounts.Popups
{
    public class EditClientExtAppCredentialsFrame : BasePage
    {
        private IWebElement _txtUsername => FindElementById("ctl00_Content_ucExtApp_txtUserName");
        private IWebElement _txtPassword => FindElementById("ctl00_Content_ucExtApp_txtPassword");
        private IWebElement _txtAuthorisationKey => FindElementById("ctl00_Content_ucExtApp_txtAuthkey");
        private IWebElement _txtNote => FindElementById("ctl00_Content_ucExtApp_txtNote");
        private IWebElement _ddlClient => FindElementById("ctl00_Content_ucExtApp_ddlClient");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _txtSuccess => FindElementById("ctl00_Content_pnlMessage");
        public EditClientExtAppCredentialsFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SetExternalApplicationCredentials(ClientOverrideData clientOverrideData, string externalApplicationAccountName)
        {
            EnsureMandatoryValuesAreProvidedForClientOverrideData(clientOverrideData, externalApplicationAccountName);
            SelectWebformDropdownValueByText(_ddlClient, clientOverrideData.Name);

            SetElementText(_txtUsername, clientOverrideData.Username);
            SetElementText(_txtPassword, clientOverrideData.Password);

            if (!string.IsNullOrEmpty(clientOverrideData.AuthorisationKey))
            {                
                if (IsOctopusVariable(clientOverrideData.AuthorisationKey))
                    throw new ArgumentException($"Errors in test data found: \n{clientOverrideData.AuthorisationKey}");                    
                
                SetElementText(_txtAuthorisationKey, clientOverrideData.AuthorisationKey);
            }

            if (!string.IsNullOrEmpty(clientOverrideData.Note))
            {
                SetElementText(_txtNote, clientOverrideData.Note);
            }

            ClickElement(_btnSave);
        }

        public void ClickClose()
        {
            WaitForPageLoadCompleteAfterClickElement(_btnClose);
        }

        public string GetSuccessMessage()
        {
            return _txtSuccess.Text;
        }

        private void EnsureMandatoryValuesAreProvidedForClientOverrideData(ClientOverrideData clientOverrideData, string externalApplicationAccountName)
        {
            var errors = new StringBuilder();

            if (string.IsNullOrEmpty(clientOverrideData.Name))
            {
                errors.Append("Client missing from test data for AdServer " + externalApplicationAccountName + " \n");
            }
            
            if (string.IsNullOrEmpty(clientOverrideData.Username) || IsOctopusVariable(clientOverrideData.Username))
            {
                errors.Append("Please verify the Username from test data for AdServer " + externalApplicationAccountName + " \n");
            }

            if (string.IsNullOrEmpty(clientOverrideData.Password) || IsOctopusVariable(clientOverrideData.Password))
            {
                errors.Append("Please verify the Password from test data for AdServer " + externalApplicationAccountName + " \n");
            }

            if (!string.IsNullOrEmpty(errors.ToString()))
            {
                throw new ArgumentException($"Errors in test data found: \n{errors.ToString()}");
            }
        }
    }
}
