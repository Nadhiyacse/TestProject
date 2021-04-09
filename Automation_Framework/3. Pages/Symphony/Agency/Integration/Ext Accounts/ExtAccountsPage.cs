using System;
using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.InfrastructureData.Integration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Integration.Ext_Accounts
{
    public class ExtAccountsPage : BasePage
    {
        private IWebElement _lnkExtAccount(string serverName) => FindElementByXPath($"//div[@id='ctl00_ctl00_Content_LeftMenu_pnlThirdLevelMenu']/ul//a[contains(text(),'{serverName}')]");
        private IWebElement _btnNewClientOverride => FindElementById("ctl00_ctl00_Content_Content_btnNew");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");
        private IWebElement _ddlClient => FindElementById(DDL_CLIENT_ID);
        private IWebElement _btnDelete => FindElementById("ctl00_ctl00_Content_Content_btnRemove");
        private IWebElement _txtAdAcountId => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_textAdAccountId");
        private IWebElement _txtAccessToken => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_textAccessToken");
        private IEnumerable<IWebElement> _lstClientNameInClientOverride => FindElementsByXPath("//span[@class='client-name']");
        private IWebElement _lnkDcmGoogleAccountSignIn => FindElementById(LNK_DCM_GOOGLE_ACCOUNT_SIGN_IN);
        private IWebElement _txtClientId => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_txtClientId");
        private IWebElement _txtClientSecret => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_txtClientSecret");
        private IWebElement _lnkGoogleAdwordsAccountSignIn => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_lnkLinkGoogleAccount");
        private IWebElement _txtManagerAccountId => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_txtManagerAccountId");
        private IWebElement _txtCustomerId => FindElementById("ctl00_ctl00_Content_Content_ucExtApp_txtCustomerId");

        private const string EXT_APP_CREDETIALS_FRAME = "EditClientExternalCredentials";
        private const string SIGN_IN_GOOGLE_POPUP_TITLE = "Sign in";
        private const string DDL_CLIENT_ID = "ctl00_ctl00_Content_Content_ucExtApp_ddlClient";
        private const string LNK_DCM_GOOGLE_ACCOUNT_SIGN_IN = "ctl00_ctl00_Content_Content_ucExtApp_lnkCreateCredential";

        public ExtAccountsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public bool DoesClientOverrideExist(string clientName)
        {
            var isAvailable = false;
            if (IsElementPresent(By.XPath("//span[@class='client-name']")))
            {
                isAvailable = _lstClientNameInClientOverride.Where(item => item.Text.Equals(clientName)).Any();
            }
            return isAvailable;
        }

        public void GoTolnkExtAccountSetting(string accountName)
        {
            WaitForPageLoadCompleteAfterClickElement(_lnkExtAccount(accountName));
        }

        public void ClickNewClientOverride()
        {
            ClickElement(_btnNewClientOverride);
            if (IsFrameVisible(EXT_APP_CREDETIALS_FRAME))
            {
                SwitchToFrame(EXT_APP_CREDETIALS_FRAME);
            }
        }

        public void ConfigureFacebook(ExternalAccountData externalAccountData)
        {
            if (!_txtAdAcountId.Enabled)
            {
                RemoveAllClientOverridesIfTheyExist();
                ClickElement(_btnDelete);
                AcceptAlert();
            }

            if (IsOctopusVariable(externalAccountData.ExternalAccountId) || IsOctopusVariable(externalAccountData.AccessToken))
                throw new ArgumentException($"Errors in test data found: \n{externalAccountData.ExternalAccountId} and/or {externalAccountData.AccessToken}");
                     
            SetElementText(_txtAdAcountId, externalAccountData.ExternalAccountId);
            SetElementText(_txtAccessToken, externalAccountData.AccessToken);
            ClickSaveButton();

            if (externalAccountData.ClientOverrideData != null && externalAccountData.ClientOverrideData.Any())
            {
                foreach (var client in externalAccountData.ClientOverrideData)
                {
                    if (IsOctopusVariable(client.externalAccountId) || IsOctopusVariable(client.AccessToken))
                        throw new ArgumentException($"Errors in test data found: \n{client.externalAccountId} and/or {client.AccessToken}");
                    
                    if (!DoesClientOverrideExist(client.Name))
                    {
                        ClickNewClientOverride();
                        SelectWebformDropdownValueByText(_ddlClient, client.Name);
                        SetElementText(_txtAdAcountId, client.externalAccountId);
                        SetElementText(_txtAccessToken, client.AccessToken);
                        ClickSaveButton();
                    }
                }
            }
        }

        public void RemoveAllClientOverridesIfTheyExist()
        {
            while (_lstClientNameInClientOverride.Count() > 0)
            {
                var lnkRemove = _lstClientNameInClientOverride.First().FindElement(By.XPath("//a[@class = 'remove-client-override-link delete-link']"));
                ClickElement(lnkRemove);
                AcceptAlert();
            }
        }

        public bool DoesDcmGoogleAccountLinkExist()
        {
            return IsElementPresent(By.Id(LNK_DCM_GOOGLE_ACCOUNT_SIGN_IN));
        }

        public void ClickDcmGoogleAccountSignInLink()
        {
            ClickElement(_lnkDcmGoogleAccountSignIn);
            SwitchToNewWindow(SIGN_IN_GOOGLE_POPUP_TITLE);
        }

        public void ClickGoogleAdwordsAccountSignInLink()
        {
            ClickElement(_lnkGoogleAdwordsAccountSignIn);
            SwitchToNewWindow(SIGN_IN_GOOGLE_POPUP_TITLE);
        }

        public void ConfigureGoogleAdwords(ExternalAccountData externalAccountData)
        {
            ClearInputAndTypeValue(_txtClientId, externalAccountData.ClientId);
            ClearInputAndTypeValue(_txtClientSecret, externalAccountData.ClientSecret);
            ClearInputAndTypeValueIfRequired(_txtManagerAccountId, externalAccountData.ManagerAccountId);
            ClearInputAndTypeValue(_txtCustomerId, externalAccountData.CustomerId);
        }

        public void ClickSaveButton()
        {
            WaitForPageLoadCompleteAfterClickElement(_btnSave);
        }

        public void ConfigureAdform(ExternalAccountData externalAccountData)
        {
            if (!_txtClientId.Enabled)
            {
                RemoveAllClientOverridesIfTheyExist();
                ClickElement(_btnDelete);
                AcceptAlert();
            }

            if (IsOctopusVariable(externalAccountData.ClientId) || IsOctopusVariable(externalAccountData.ClientSecret))
                throw new ArgumentException($"Errors in test data found: \n{externalAccountData.ClientId} and/or {externalAccountData.ClientSecret}");

            SetElementText(_txtClientId, externalAccountData.ClientId);
            SetElementText(_txtClientSecret, externalAccountData.ClientSecret);
            ClickSaveButton();

            if (externalAccountData.ClientOverrideData != null && externalAccountData.ClientOverrideData.Any())
            {
                foreach (var client in externalAccountData.ClientOverrideData)
                {
                    if (IsOctopusVariable(client.ClientId) || IsOctopusVariable(client.ClientSecret))
                        throw new ArgumentException($"Errors in test data found: \n{client.ClientId} and/or {client.ClientSecret}");

                    if (!DoesClientOverrideExist(client.Name))
                    {
                        ClickNewClientOverride();
                        SelectWebformDropdownValueByText(_ddlClient, client.Name);
                        ClearInputAndTypeValue(_txtClientId, client.ClientId);
                        ClearInputAndTypeValue(_txtClientSecret, client.ClientSecret);
                        ClickSaveButton();
                    }
                }
            }
        }
    }
}
