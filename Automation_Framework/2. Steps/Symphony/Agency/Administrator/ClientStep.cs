using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Client;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class ClientStep : BaseStep
    {
        private readonly ClientPage _clientPage;
        private readonly CreateClientPage _createClientPage;
        private readonly ClientCustomFields _clientCustomFields;

        public ClientStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _clientPage = new ClientPage(driver, featureContext);
            _createClientPage = new CreateClientPage(driver, featureContext);
            _clientCustomFields = new ClientCustomFields(driver, featureContext);
        }

        [Given(@"I configure my clients")]
        public void CreateClients()
        {
            foreach (var client in AgencySetupData.AgencyAdministratorData.Clients)
            {
                var clientName = client.ClientName;

                if (!_clientPage.DoesClientExist(clientName))
                {
                    _clientPage.ClickNewButton();
                    _createClientPage.CreateClient(client);
                    NavigationPage.NavigateTo("Administrator Clients");
                    Assert.IsTrue(_clientPage.DoesClientExist(clientName), "The Client " + clientName + " is not created");
                }

                _clientPage.ClickEditForSpecificClient(clientName);
                NavigationPage.NavigateTo("Administrator Clients CustomFields");
                _clientCustomFields.SetClientCustomFields(client);                
                NavigationPage.NavigateTo("Administrator Clients");
            }
        }   
    }
}
