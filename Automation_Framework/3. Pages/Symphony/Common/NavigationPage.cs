using System;
using System.Configuration;
using System.Diagnostics;
using Automation_Framework.Hooks;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Common
{
    public class NavigationPage : BasePage
    {
        // Overview Page Links
        private IWebElement _lnkOverview => FindElementByLinkText("Overview");
        private IWebElement _lnkCampaignDetails => FindElementByLinkText("Details");
        private IWebElement _lnkMarketplace => FindElementByLinkText("Marketplace");
        private IWebElement _lnkBriefsAndProposals => FindElementByLinkText("Briefs and Proposals");
        private IWebElement _lnkMediaSchedule => FindElementByLinkText("Media Schedule");
        private IWebElement _btnMediaScheduleBeta => FindElementById("ctl00_Content_btnGridBeta");
        private IWebElement _btnMediaScheduleClassic => FindElementByCssSelector(".notification-right-content-ms a");
        private IWebElement _lnkInsertionOrder => FindElementByLinkText("Insertion Order");
        private IWebElement _lnkBilling => FindElementByLinkText("Billing");
        private IWebElement _lnkTraffic => FindElementByLinkText("Traffic");
        private IWebElement _lnkReports => FindElementByLinkText("Reports");

        // Campaigns Page Links
        private IWebElement _lnkCampaigns => FindElementByLinkText("Campaigns");
        private IWebElement _lnkArchivedCampaigns => FindElementByLinkText("Archived Campaigns");

        // Administrator Page Links
        private IWebElement _lnkAdministrator => FindElementByLinkText("Administrator");
        private IWebElement _lnkAdministratorDetails => FindElementByLinkText("Details");
        private IWebElement _lnkAdministratorClients => FindElementByLinkText("Clients");
        private IWebElement _lnkAdministratorPublishers => FindElementByLinkText("Publishers");
        private IWebElement _lnkAdministratorCreativeAgency => FindElementByLinkText("Creative Agency");
        private IWebElement _lnkAdministratorUsers => FindElementByLinkText("Users");
        private IWebElement _lnkAdministratorAccess => FindElementByLinkText("Access");
        private IWebElement _lnkAdministratorCampaigns => FindElementByXPath("//div[@class='nav-level-two']//a[text()='Campaigns']");
        private IWebElement _lnkAdministratorIOAdmin => FindElementByLinkText("IO Admin");
        private IWebElement _lnkAdministratorSiteDetails => FindElementByLinkText("Site Details");

        // Administrator Details Page Links
        private IWebElement _lnkFeatureSettings => FindElementByLinkText("Feature Settings");

        // Administrator Publishers Page Links
        private IWebElement _lnkRatecard => FindElementByLinkText("Ratecard");
        
        // Administrator Campaigns Page Links
        private IWebElement _lnkParentCampaigns => FindElementByLinkText("Parent Campaigns");

        // Integration Page Links
        private IWebElement _lnkIntegration => FindElementByLinkText("Integration");
        private IWebElement _lnkDataMapping => FindElementByLinkText("Data Mapping");
        private IWebElement _lnkExtAccounts => FindElementByLinkText("Ext Accounts");

        // Symphony Admin Page Links
        private IWebElement _lnkSymphonyAdmin => FindElementByLinkText("Symphony Admin");
        private IWebElement _lnkSymphonyAdminAgencies => FindElementByLinkText("Agencies");
        private IWebElement _lnkSymphonyAdminPublishers => FindElementByLinkText("Publishers");
        private IWebElement _lnkSymphonyAdminVendors => FindElementByLinkText("Vendors");
        private IWebElement _lnkSymphonyAdminCampaigns => FindElementByLinkText("Campaigns");
        private IWebElement _lnkSymphonyAdminFeatureToggles => FindElementByLinkText("Feature Toggles");
        private IWebElement _lnkSymphonyAdminGlobal => FindElementByLinkText("Global");

        // Symphony Admin Agencies Page Links
        private IWebElement _lnkEdit => FindElementByLinkText("Edit");
        private IWebElement _lnkAccess => FindElementByLinkText("Access");
        private IWebElement _lnkUsers => FindElementByLinkText("Users");
        private IWebElement _lnkFeatures => FindElementByLinkText("Features");
        private IWebElement _lnkCustomLabels => FindElementByLinkText("Custom Labels");
        private IWebElement _lnkCustomFields => FindElementByLinkText("Custom Fields");
        private IWebElement _lnkClassifications => FindElementByLinkText("Classifications");

        // Symphony Admin Publishers Page Links
        private IWebElement _lnkSites => FindElementByLinkText("Sites");
        private IWebElement _lnkLocations => FindElementByLinkText("Locations");
        private IWebElement _lnkFormats => FindElementByLinkText("Formats");
        private IWebElement _lnkMappings => FindElementByLinkText("Mappings");

        private IWebElement _lnkHelp => FindElementByClassName("aui--svg-symbol-component aui--svg-symbol-component-clickable");
        private IWebElement _userProfile => FindElementByClassName("avatar-component-initials");
        private IWebElement _lnkSettingsAndPrivacy => FindElementByLinkText("Settings & Privacy");
        private IWebElement _lnkResourceCentre => FindElementByLinkText("Resource Centre");
        private IWebElement _lnkColourCode => FindElementByLinkText("Colour Code");
        private IWebElement _lnkLogout => FindElementByLinkText("Logout");
        private IWebElement _divProfilePopover => FindElementById("profilePopover");

        public NavigationPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void NavigateTo(string pageToNavigateTo)
        {
            var stopwatch = Stopwatch.StartNew();
            switch (pageToNavigateTo)
            {
                // Overview Links
                case "Overview":
                    ClickElement(_lnkOverview);
                    break;
                case "Campaign Details":
                    ClickElement(_lnkCampaignDetails);
                    break;
                case "Marketplace":
                    ClickElement(_lnkMarketplace);
                    break;
                case "Briefs and Proposals":
                    ClickElement(_lnkBriefsAndProposals);
                    break;
                case "Media Schedule":
                    ClickElement(_lnkMediaSchedule);
                    break;
                case "Media Schedule Grid":
                    ClickElement(_btnMediaScheduleBeta);
                    AcceptAlert(1);
                    break;
                case "Media Schedule Classic":
                    ClickElement(_btnMediaScheduleClassic);
                    break;
                case "Insertion Order":
                    ClickElement(_lnkInsertionOrder);
                    break;
                case "Billing":
                    ClickElement(_lnkBilling);
                    break;
                case "Traffic":
                    ClickElement(_lnkTraffic);
                    break;
                case "Reports":
                    ClickElement(_lnkReports);
                    break;

                // Campaigns Links
                case "Campaigns":
                    ClickElement(_lnkCampaigns);
                    WaitForElementToBeInvisible(By.XPath("//div[contains(@class,'spinner-large')]"));
                    break;
                case "Archived Campaigns":
                    ClickElement(_lnkArchivedCampaigns);
                    break;

                // Administrator Links
                case "Administrator":
                    ClickElement(_lnkAdministrator);
                    break;
                case "Administrator Details":
                    ClickElement(_lnkAdministratorDetails);
                    break;
                case "Administrator Clients":
                    ClickElement(_lnkAdministratorClients);
                    break;
                case "Administrator Publishers":
                    ClickElement(_lnkAdministratorPublishers);
                    break;
                case "Administrator Creative Agency":
                    ClickElement(_lnkAdministratorCreativeAgency);
                    break;
                case "Administrator Users":
                    ClickElement(_lnkAdministratorUsers);
                    break;
                case "Administrator Access":
                    ClickElement(_lnkAdministratorAccess);
                    break;
                case "Administrator Campaigns":
                    ClickElement(_lnkAdministratorCampaigns);
                    break;
                case "Administrator IO Admin":
                    ClickElement(_lnkAdministratorIOAdmin);
                    break;
                case "Administrator Site Details":
                    ClickElement(_lnkAdministratorSiteDetails);
                    break;

                // Administrator Details Links
                case "Administrator Details Feature Settings":
                    ClickElement(_lnkFeatureSettings);
                    if (IsElementPresent(By.XPath("//a[text()='Agency Fees']")))
                    {
                        WaitForElementToBeVisible(By.CssSelector(".creativeColumn"));
                    }
                    break;

                // Administrator Client Links
                case "Administrator Clients CustomFields":
                    ClickElement(_lnkCustomFields);
                    break;

                // Administrator Publishers Links
                case "Administrator Publishers Ratecard":
                    ClickElement(_lnkRatecard);
                    break;

                // Administrator Campaigns Links
                case "Administrator Campaigns Parent Campaigns":
                    ClickElement(_lnkParentCampaigns);
                    break;

                // Integration Links
                case "Integration":
                    ClickElement(_lnkIntegration);
                    break;
                case "Data Mapping":
                    ClickElement(_lnkDataMapping);
                    break;
                case "Ext Accounts":
                    ClickElement(_lnkExtAccounts);
                    break;

                // Symphony Admin Links
                case "Symphony Admin":
                    ClickElement(_lnkSymphonyAdmin);
                    break;
                case "Symphony Admin Agencies":
                    ClickElement(_lnkSymphonyAdminAgencies);
                    break;
                case "Symphony Admin Publishers":
                    ClickElement(_lnkSymphonyAdminPublishers);
                    break;
                case "Symphony Admin Vendors":
                    ClickElement(_lnkSymphonyAdminVendors);
                    break;
                case "Symphony Admin Campaigns":
                    ClickElement(_lnkSymphonyAdminCampaigns);
                    break;
                case "Symphony Admin Feature Toggles":
                    ClickElement(_lnkSymphonyAdminFeatureToggles);
                    break;
                case "Symphony Admin Global":
                    ClickElement(_lnkSymphonyAdminGlobal);
                    break;

                // Symphony Admin Agencies Links
                case "Symphony Admin Agencies Edit":
                    ClickElement(_lnkEdit);
                    break;
                case "Symphony Admin Agencies Access":
                    ClickElement(_lnkAccess);
                    break;
                case "Symphony Admin Agencies Users":
                    ClickElement(_lnkUsers);
                    break;
                case "Symphony Admin Agencies Features":
                    ClickElement(_lnkFeatures);
                    break;
                case "Symphony Admin Agencies Custom Labels":
                    ClickElement(_lnkCustomLabels);
                    break;
                case "Symphony Admin Agencies Custom Fields":
                    ClickElement(_lnkCustomFields);
                    break;
                case "Symphony Admin Agencies Classifications":
                    ClickElement(_lnkClassifications);
                    break;

                // Symphony Admin Publishers Links
                case "Symphony Admin Publishers Edit":
                    ClickElement(_lnkEdit);
                    break;
                case "Symphony Admin Publishers Access":
                    ClickElement(_lnkAccess);
                    break;
                case "Symphony Admin Publishers Users":
                    ClickElement(_lnkUsers);
                    break;
                case "Symphony Admin Publishers Sites":
                    ClickElement(_lnkSites);
                    break;
                case "Symphony Admin Publishers Locations":
                    ClickElement(_lnkLocations);
                    break;
                case "Symphony Admin Publishers Formats":
                    ClickElement(_lnkFormats);
                    break;
                case "Symphony Admin Publishers Mappings":
                    ClickElement(_lnkMappings);
                    break;

                default:
                    throw new ArgumentException($"Navigating to the page {pageToNavigateTo} is not supported.");
            }

            WaitForPageLoadComplete();
            stopwatch.Stop();
            FeatureContext[ContextStrings.ElapsedTime] = stopwatch.Elapsed;
        }

        public void ClickHelpLink()
        {
            ClickElement(_lnkHelp);
        }

        public void UserProfileActions(string action)
        {
            Wait.Until(driver => _userProfile.Displayed);
            new Actions(driver).MoveToElement(_userProfile).Build().Perform();
            switch (action)
            {
                case "Settings and Privacy":
                    Wait.Until(driver => _divProfilePopover.Displayed);
                    ScrollAndClickElement(_lnkSettingsAndPrivacy);
                    break;
                case "Resource Centre":
                    Wait.Until(driver => _divProfilePopover.Displayed);
                    ScrollAndClickElement(_lnkResourceCentre);
                    break;
                case "Colour Code":
                    Wait.Until(driver => _divProfilePopover.Displayed);
                    ScrollAndClickElement(_lnkColourCode);
                    break;
                case "Logout":
                    Wait.Until(driver => _divProfilePopover.Displayed);
                    ScrollAndClickElement(_lnkLogout);
                    break;
                default:
                    throw new ArgumentException($"Action {action} is not supported.");
            }
        }

        public void LogoutCurrentUser()
        {
            var environmentUrl = ConfigurationManager.AppSettings["EnvironmentUrl"];
            driver.Navigate().GoToUrl($"{environmentUrl}Account/Logout");
        }
    }
}