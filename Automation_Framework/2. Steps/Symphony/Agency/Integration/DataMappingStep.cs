using Automation_Framework._3._Pages.Symphony.Agency.Integration.DataMapping;
using Automation_Framework.DataModels.InfrastructureData.Integration.DataMapping;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Integration
{
    [Binding]
    public class DataMappingStep : BaseStep
    {
        private readonly ClientMappingFrame _clientMappingFrame;
        private readonly SiteMappingFrame _siteMappingFrame;
        private readonly VendorMappingFrame _vendorMappingFrame;
        private readonly ManageDataMappingPage _managePage;
        private const string EXPECTED_SUCCESS_MESSAGE = "Mapping was successfully saved";

        public DataMappingStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _clientMappingFrame = new ClientMappingFrame(driver, featureContext);
            _siteMappingFrame = new SiteMappingFrame(driver, featureContext);
            _vendorMappingFrame = new VendorMappingFrame(driver, featureContext);
            _managePage = new ManageDataMappingPage(driver, featureContext);
        }

        [Given(@"I configure my external applications data mapping")]
        public void ConfigureMyDataMapping()
        {
            if (AgencySetupData.IntegrationData.MappingData == null)
                return;

            foreach (var data in AgencySetupData.IntegrationData.MappingData.ExternalApplications)
            {
                _managePage.SelectApplication(data.Name);

                foreach (var dataMapping in data.Types)
                {
                    _managePage.SelectType(dataMapping.TypeName);

                    switch (dataMapping.TypeName.ToLower())
                    {
                        case "agency":
                            AgencyDataMapping(dataMapping);
                            Assert.AreEqual(_managePage.GetMessage(), EXPECTED_SUCCESS_MESSAGE);
                            break;
                        case "client":
                            ClientDataMapping(dataMapping);
                            break;
                        case "site":
                            SiteDataMapping(dataMapping);
                            break;
                        case "vendor":
                            VendorDataMapping(dataMapping);
                            break;
                    }
                }
            }
        }

        private void AgencyDataMapping(Type data)
        {
            _managePage.ClickDisplay();
            foreach (var field in data.Fields)
            {
                _managePage.MapAgencyData(field);
            }
            _managePage.ClickSave();
        }

        private void ClientDataMapping(Type data)
        {
            _managePage.SelectStatus(data.Status);
            _managePage.ClickDisplay();
            foreach (var client in data.Clients)
            {
                var isClientSelected = _managePage.SelectClientToMapData(client);
                if (isClientSelected)
                {
                    foreach (var field in client.Fields)
                    {
                        _clientMappingFrame.AddClientMappingData(field);
                    }
                    _clientMappingFrame.ClickSave();
                    Assert.AreEqual(_clientMappingFrame.GetMessage(), EXPECTED_SUCCESS_MESSAGE);
                    _clientMappingFrame.CloseDataMappingFrame();
                }
            }
        }

        private void SiteDataMapping(Type data)
        {
            foreach (var publisher in data.Publishers)
            {
                _managePage.SelectCountry(publisher.Country);
                _managePage.SelectStatus(publisher.Status);
                _managePage.SelectPublisher(publisher.PublisherName);
                foreach (var site in publisher.Sites)
                {
                    _managePage.SearchItemName(site.Name);
                    var isSiteSelected = _managePage.SelectSiteToMapData(site);
                    if (isSiteSelected)
                    {
                        foreach (var field in site.Fields)
                        {
                            _siteMappingFrame.AddSiteMappingData(field);
                        }
                        _siteMappingFrame.ClickSave();
                        Assert.AreEqual(_siteMappingFrame.GetMessage(), EXPECTED_SUCCESS_MESSAGE);
                        _siteMappingFrame.CloseDataMappingFrame();
                    }
                    _managePage.ClickBrowseTab();
                }
            }
        }

        private void VendorDataMapping(Type data)
        {
            foreach (var vendor in data.Vendors)
            {
                _managePage.SearchItemName(vendor.Name);

                var isVendorSelected = _managePage.SelectVendorToMapData(vendor);
                if (isVendorSelected)
                {
                    foreach (var field in vendor.Fields)
                    {
                        _vendorMappingFrame.AddVendorMappingData(field);
                    }
                    _vendorMappingFrame.ClickSave();
                    Assert.AreEqual(_vendorMappingFrame.GetMessage(), EXPECTED_SUCCESS_MESSAGE);
                    _vendorMappingFrame.CloseDataMappingFrame();
                }
            }
        }
    }
}
