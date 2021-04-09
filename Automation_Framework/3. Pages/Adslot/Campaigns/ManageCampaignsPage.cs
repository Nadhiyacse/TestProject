using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Campaigns
{
    public class ManageCampaignsPage : BasePage
    {
        private IWebElement _txtSearch => FindElementByName("search");
        private IWebElement _lnkCampaignName(string campaignName)
        {
            return FindElementByXPath($"*//a[text()='{campaignName}']");
        }
        private const int MAX_ATTEMPTS = 5;
        public ManageCampaignsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SearchAndSelectCampaign(string campaignName)
        {
            bool isFound = false;
            int attempts = 0;

            while (!isFound && attempts < MAX_ATTEMPTS)
            {
                try
                {
                    Wait.Until(d => FindElementByName("search"));
                    ClearInputAndTypeValue(_txtSearch, campaignName);
                    Wait.Until(d => !IsElementPresent(By.XPath("//div[contains(@class,'loader-wrapper')]")));
                    ClickElement(_lnkCampaignName(campaignName));
                    isFound = true;
                }
                catch
                {
                    driver.Navigate().Refresh();
                }
                attempts++;
            }

            if (!isFound)
            {
                Assert.Fail($"The campaign '{campaignName}' does not exist in Adslot Publisher");
            }
        }
    }
}
