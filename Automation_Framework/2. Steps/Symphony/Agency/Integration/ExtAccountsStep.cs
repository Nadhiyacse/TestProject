using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Integration.Ext_Accounts;
using Automation_Framework._3._Pages.Symphony.Agency.Integration.Ext_Accounts.Popups;
using Automation_Framework.DataModels.InfrastructureData.Integration;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Integration
{
    [Binding]
    public class ExtAccountsStep : BaseStep
    {
        private readonly ExtAccountsPage _extAccountsPage;
        private readonly EditClientExtAppCredentialsFrame _editClientExtAppCredentialsFrame;
        private readonly GoogleSignInPopUp _googleSignInPopUp;

        public ExtAccountsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _extAccountsPage = new ExtAccountsPage(driver, featureContext);
            _editClientExtAppCredentialsFrame = new EditClientExtAppCredentialsFrame(driver, featureContext);
            _googleSignInPopUp = new GoogleSignInPopUp(driver, featureContext);
        }

        [Given(@"I configure my agencies external credentials")]
        public void ConfigureMyAgenciesExternalCredentials()
        {
            foreach (var externalAccount in AgencySetupData.IntegrationData.ExternalAccounts)
            {
                _extAccountsPage.GoTolnkExtAccountSetting(externalAccount.Name);

                switch (externalAccount.Name)
                {
                    case "Sizmek MDX":
                        ConfigureExternalAccountWithEditFrame(externalAccount);
                        break;
                    case "Sizmek SAS":
                        ConfigureExternalAccountWithEditFrame(externalAccount);
                        break;
                    case "Facebook":
                        _extAccountsPage.ConfigureFacebook(externalAccount);
                        break;
                    case "AdMonitor":
                        ConfigureExternalAccountWithEditFrame(externalAccount);
                        break;
                    case "DCM":
                        if (_extAccountsPage.DoesDcmGoogleAccountLinkExist())
                        {
                            _extAccountsPage.ClickDcmGoogleAccountSignInLink();
                            _googleSignInPopUp.SignInToGoogle(externalAccount);
                        }
                        break;
                    case "Google AdWords":
                        _extAccountsPage.ConfigureGoogleAdwords(externalAccount);
                        _extAccountsPage.ClickGoogleAdwordsAccountSignInLink();
                        _googleSignInPopUp.SignInToGoogleAndAllowAccess(externalAccount);
                        _extAccountsPage.ClickSaveButton();
                        break;
                    case "Adform":
                        _extAccountsPage.ConfigureAdform(externalAccount);
                        break;
                    default:
                        throw new ArgumentException($"{externalAccount.Name} is not supported for configuration");
                }
            }
        }

        private void ConfigureExternalAccountWithEditFrame(ExternalAccountData externalAccountData)
        {
            if (externalAccountData.ClientOverrideData.Any())
            {
                _extAccountsPage.RemoveAllClientOverridesIfTheyExist();

                foreach (var clientOverride in externalAccountData.ClientOverrideData)
                {
                    _extAccountsPage.ClickNewClientOverride();
                    _editClientExtAppCredentialsFrame.SetExternalApplicationCredentials(clientOverride, externalAccountData.Name);
                    var successMessage = _editClientExtAppCredentialsFrame.GetSuccessMessage();
                    Assert.AreEqual("External credentials successfully saved.", successMessage, "Failed to save credentials");
                    _editClientExtAppCredentialsFrame.ClickClose();
                }
            }
        }
    }
}
