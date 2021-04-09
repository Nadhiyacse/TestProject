using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers.Popups;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Symphony_Admin
{
    [Binding]
    public class PublisherStep : BaseStep
    {
        private readonly PublisherListPage _publisherListPage;
        private readonly PublisherEditPage _publisherEditPage;
        private readonly PublisherCreatePage _publisherCreatePage;
        private readonly PublisherAccessPage _publisherAccessPage;
        private readonly PublisherUserPage _publisherUserPage;
        private readonly PublisherSitePage _publisherSitePage;
        private readonly PublisherLocationPage _publisherLocationPage;
        private readonly PublisherFormatPage _publisherFormatPage;
        private readonly PublisherMappingPage _publisherMappingPage;
        private readonly CreateUpdatePublisherUserFrame _createUpdatePublisherUserFrame;
        private readonly SiteCreatePage _siteCreatePage;
        private readonly SiteEditPage _siteEditPage;

        public PublisherStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _publisherListPage = new PublisherListPage(driver, featureContext);
            _publisherCreatePage = new PublisherCreatePage(driver, featureContext);
            _publisherEditPage = new PublisherEditPage(driver, featureContext);
            _publisherAccessPage = new PublisherAccessPage(driver, featureContext);
            _publisherUserPage = new PublisherUserPage(driver, featureContext);
            _publisherSitePage = new PublisherSitePage(driver, featureContext);
            _publisherLocationPage = new PublisherLocationPage(driver, featureContext);
            _publisherFormatPage = new PublisherFormatPage(driver, featureContext);
            _publisherMappingPage = new PublisherMappingPage(driver, featureContext);
            _createUpdatePublisherUserFrame = new CreateUpdatePublisherUserFrame(driver, featureContext);
            _siteCreatePage = new SiteCreatePage(driver, featureContext);
            _siteEditPage = new SiteEditPage(driver, featureContext);
        }

        [Given(@"I configure my publishers")]
        public void ConfigurePublishers()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                {
                    _publisherListPage.ClickPublisherName(publisherName);
                    _publisherEditPage.EditPublisher(publisher);
                    Assert.AreEqual($"{publisherName} publisher was successfully updated.", _publisherEditPage.GetPanelMessage(), "The agency was not updated.");
                }
                else
                {
                    _publisherListPage.ClickCreate();
                    _publisherCreatePage.CreatePublisher(publisher);
                    Assert.AreEqual($"{publisherName} was successfully created!", _publisherCreatePage.GetPanelMessage(), "The agency was not created.");
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publishers access control")]
        public void ConfigurePublishersAccessControl()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisherName);

                _publisherListPage.ClickPublisherName(publisherName);
                NavigationPage.NavigateTo("Symphony Admin Publishers Access");
                _publisherAccessPage.ConfigurePublisherAccessControl(publisher.Access);
                Assert.AreEqual($"Access control successfully set.", _publisherAccessPage.GetPanelMessage(), "There was an error setting access control");

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publishers users")]
        public void ConfigurePublishersUsers()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                if (publisher.Users != null && publisher.Users.Any())
                {
                    var publisherName = publisher.Name;

                    if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                        throw new Exception("The following publisher does not exist: " + publisherName);

                    _publisherListPage.ClickPublisherName(publisherName);
                    NavigationPage.NavigateTo("Symphony Admin Publishers Users");

                    foreach (var user in publisher.Users)
                    {
                        if (_publisherUserPage.IsUserEmailExist(user.Email))
                        {
                            _publisherUserPage.ClickEdit(user.Email);
                            _createUpdatePublisherUserFrame.EditAgencyUser(user);
                            Assert.AreEqual($"User: {user.Email} was successfully updated.", _createUpdatePublisherUserFrame.GetPanelMessage());
                            _createUpdatePublisherUserFrame.ClickClose();
                        }
                        else
                        {
                            _publisherUserPage.ClickCreate();
                            _createUpdatePublisherUserFrame.CreatePublisherUser(user, publisher.IsSubscriber);
                            if (!_createUpdatePublisherUserFrame.IsSaveSuccessful())
                            {
                                Assert.Fail(_createUpdatePublisherUserFrame.GetPanelMessage());
                            }
                        }
                    }

                    NavigationPage.NavigateTo("Symphony Admin Publishers");
                }
            }
        }

        [Given(@"I configure my publishers sites")]
        public void ConfigurePublishersSites()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisherName);

                _publisherListPage.ClickPublisherName(publisherName);
                NavigationPage.NavigateTo("Symphony Admin Publishers Sites");

                if (publisher.Sites != null && publisher.Sites.Any())
                {
                    foreach (var site in publisher.Sites)
                    {
                        if (_publisherSitePage.DoesSiteExist(site.SiteName))
                        {
                            _publisherSitePage.ClickSiteName(site.SiteName);
                            _siteEditPage.EditSite(site);
                            Assert.AreEqual("Success", _siteEditPage.GetPanelMessage());
                            _siteEditPage.ClickBack();
                        }
                        else
                        {
                            _publisherSitePage.ClickCreate();
                            _siteCreatePage.CreateSite(site);
                        }
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publisher parent site mappings")]
        public void ConfigurePublisherParentSiteMappings()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisher.Name, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisher.Name);

                _publisherListPage.ClickPublisherName(publisher.Name);
                NavigationPage.NavigateTo("Symphony Admin Publishers Sites");

                if (publisher.Sites != null && publisher.Sites.Any())
                {
                    foreach (var site in publisher.Sites)
                    {
                        if (site.ParentSite != null && !string.IsNullOrEmpty(site.ParentSite))
                        {
                            _publisherSitePage.ClickSiteName(site.SiteName);
                            _siteEditPage.SetParentSiteAndSave(site.ParentSite);
                            Assert.AreEqual("Success", _siteEditPage.GetPanelMessage());
                            _siteEditPage.ClickBack();
                        }
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publishers locations")]
        public void ConfigurePublishersLocations()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisherName);

                _publisherListPage.ClickPublisherName(publisherName);
                NavigationPage.NavigateTo("Symphony Admin Publishers Locations");

                if (publisher.Locations != null && publisher.Locations.Any())
                {
                    foreach (var location in publisher.Locations)
                    {
                        if (!_publisherLocationPage.DoesLocationExist(location))
                        {
                            _publisherLocationPage.AddLocation(location);
                            Assert.AreEqual("Location has been created.", _publisherLocationPage.GetAlertMessage());
                        }
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publishers formats")]
        public void ConfigurePublishersFormats()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisherName);

                _publisherListPage.ClickPublisherName(publisherName);
                NavigationPage.NavigateTo("Symphony Admin Publishers Formats");

                if (publisher.Formats != null && publisher.Formats.Any())
                {
                    foreach (var format in publisher.Formats)
                    {
                        if (!_publisherFormatPage.DoesFormatExist(format))
                        {
                            _publisherFormatPage.AddFormat(format);
                            Assert.AreEqual("Format has been created.", _publisherFormatPage.GetAlertMessage());
                        }
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }

        [Given(@"I configure my publishers mappings")]
        public void ConfigurePublishersMappings()
        {
            foreach (var publisher in GenericSetupData.SymphonyAdminData.Publishers)
            {
                var publisherName = publisher.Name;

                if (!_publisherListPage.DoesPublisherExistInCountry(publisher.Country, publisherName, publisher.IsSubscriber))
                    throw new Exception("The following publisher does not exist: " + publisherName);

                _publisherListPage.ClickPublisherName(publisherName);
                NavigationPage.NavigateTo("Symphony Admin Publishers Mappings");

                if (publisher.LocationFormatMappings != null && publisher.LocationFormatMappings.Any())
                {
                    foreach (var mapping in publisher.LocationFormatMappings)
                    {
                        if (!_publisherMappingPage.DoesMappingExist(mapping))
                        {
                            _publisherMappingPage.AddMapping(mapping);
                            Assert.AreEqual("Mapping has been created.", _publisherMappingPage.GetAlertMessage());
                        }
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Publishers");
            }
        }
    }
}