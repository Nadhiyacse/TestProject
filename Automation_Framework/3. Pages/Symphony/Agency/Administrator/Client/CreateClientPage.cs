using System;
using System.Text;
using OpenQA.Selenium;
using Automation_Framework.Helpers;
using TechTalk.SpecFlow;
using Automation_Framework.DataModels.InfrastructureData.Administrator;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Client
{
    public class CreateClientPage : BasePage
    {
        private IWebElement _txtClientName => FindElementById("ctl00_Content_txtClientName");
        private IWebElement _txtFileLogoUpload => FindElementById("ctl00_Content_fileLogoUpload");
        private IWebElement _txtClientContactMarketing => FindElementById("ctl00_Content_txtContactMark");
        private IWebElement _txtClientContactMarketingEmail => FindElementById("ctl00_Content_txtEmail1");
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnSave");

        public CreateClientPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateClient(ClientData clientData)
        {
            EnsureMandatoryValuesAreProvided(clientData);
            if (!string.IsNullOrEmpty(clientData.ClientLogoName))
            {
                var clientLogoFilePath = FileHelper.GetImageFilePath(clientData.ClientLogoName);
                TypeValueIfRequired(_txtFileLogoUpload, clientLogoFilePath);
            }
            ClearInputAndTypeValue(_txtClientName, clientData.ClientName);            
            ClearInputAndTypeValue(_txtClientContactMarketing, clientData.ClientContactMarketing);
            ClearInputAndTypeValue(_txtClientContactMarketingEmail, clientData.ClientContactMarketingEmail);
            ScrollAndClickElement(_btnCreate);
        }

        private void EnsureMandatoryValuesAreProvided(ClientData clientsData)
        {
            var dataErrorFound = false;
            var clientDataErrors = new StringBuilder();
            clientDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(clientsData.ClientName))
            {
                dataErrorFound = true;
                clientDataErrors.Append("\n- Client name was not set");
            }

            if (string.IsNullOrWhiteSpace(clientsData.ClientContactMarketing))
            {
                dataErrorFound = true;
                clientDataErrors.Append("\n- Client Contact Marketing was not set");
            }

            if (string.IsNullOrWhiteSpace(clientsData.ClientContactMarketingEmail))
            {
                dataErrorFound = true;
                clientDataErrors.Append("\n- Client Contact Marketing Email was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(clientDataErrors.ToString());
        }
    }
}
