using System;
using System.Configuration;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Common
{
    public class AdslotNavigationPage : BasePage
    {
        // Campaign frames links
        private IWebElement _btnHome => FindElementByXPath("//div[@class='logo']");
        private IWebElement _btnMediaSchedule => FindElementByXPath("//button[contains(@class, 'media-schedule')]");
        private IWebElement _btnInbox => FindElementByXPath("//button[contains(@class, 'inbox')]");
        private IWebElement _lblMediaScheduleTabAlertCount => FindElementByXPath("//react-component[@ng-if='mediaScheduleTabAlertCount']");

        public AdslotNavigationPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void IsTabAlertCountPresent(string tab)
        {  
            switch (tab)
            {
                case "Media Schedule":
                    Wait.Until(d => _lblMediaScheduleTabAlertCount.Displayed);
                    break;
                default:
                    throw new ArgumentException($"{tab} is not available.");
            }
        }

        public void NavigateTo(string pageToNavigateTo)
        {
            switch (pageToNavigateTo)
            {
                case "Home":
                    ClickElement(_btnHome);
                    break;
                case "Media Schedule":
                    WaitForCampaignDashboardToLoad();
                    ClickElement(_btnMediaSchedule);
                    break;
                case "Inbox":
                    ClickElement(_btnInbox);
                    break;
                default:
                    throw new ArgumentException($"Navigating to the page {pageToNavigateTo} is not supported.");
            }
        }

        public void WaitForCampaignDashboardToLoad()
        {
            Wait.Until(d => !IsElementPresent(By.XPath("//div[@data-test-selector='loader-component']")));
            IsElementPresent(By.XPath("//react-component[@ng-if='mediaScheduleTabAlertCount']"));
        }

        public void LogoutToAdslotPublisher()
        {
            var publisherUrl = ConfigurationManager.AppSettings["AdslotPublisherUrl"];
            driver.Navigate().GoToUrl($"{publisherUrl}logout");
            SwitchToMainWindow();
        }
    }
}
