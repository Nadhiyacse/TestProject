using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Publisher.Overview
{
    public class PublisherOverviewPage : BasePage
    {
        private IWebElement _lnkSearch => FindElementById("lnktabSearch");
        private IWebElement _campaignNameInNavigationPanel => FindElementByClassName("nav-detail");
        private IWebElement _txtSearch => FindElementById("ctl00_Content_tabWidgetControl_tabSearch_txtSearchCampaign");
        private IWebElement _btnSearch => FindElementById("ctl00_Content_tabWidgetControl_tabSearch_btnSearchCampaign");
        private IWebElement _lnkSearchResult => FindElementById(SEARCH_CAMPAIGN_FIRST_RESULT_ID);
        private IWebElement _lblSearchResultCreatedDate => FindElementByXPath("//a[@id='ctl00_Content_tabWidgetControl_tabSearch_rpSearchCampaigns_ctl01_lnkCampaign']/../..//td[3]");

        private const string SEARCH_CAMPAIGN_FIRST_RESULT_ID = "ctl00_Content_tabWidgetControl_tabSearch_rpSearchCampaigns_ctl01_lnkCampaign";

        public PublisherOverviewPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SearchAndSelectCampaign(string campaignName)
        {
            WaitForElementToBeClickable(By.Id("lnktabSearch"));
            ClickElement(_lnkSearch);
            WaitForElementToBeClickable(_txtSearch);
            SetElementText(_txtSearch, campaignName);
            ClickElement(_btnSearch);

            WaitForElementToBeVisible(By.Id("ctl00_Content_tabWidgetControl_tabSearch_pnlSearchResult"));
            if (IsElementPresent(By.Id(SEARCH_CAMPAIGN_FIRST_RESULT_ID)))
            {
                Assert.AreEqual(campaignName, _lnkSearchResult.Text, "Campaign names are different");
                ClickElement(_lnkSearchResult);
                WaitForElementToBeVisible(_campaignNameInNavigationPanel);
                Assert.IsTrue(_campaignNameInNavigationPanel.Text.Contains(campaignName), "Campaign navigation is not successful");
            }
            else
            {
                Assert.Fail($"Campaign '{campaignName}' does not exist");
            }
        }
    }
}