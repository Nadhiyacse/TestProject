using System.Linq;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Campaigns
{
    public class CampaignsGridPage : BasePage
    {
        private IWebElement _txtSearchBox => FindElementByName("search");
        private IWebElement _tblCampaignsGrid => FindElementByXPath("//div[@class='ag__group-body']");
        private IWebElement _btnCreateCampaign => FindElementByClassName("create-campaign");

        private const string FRAME_NEW_CAMPAIGN_CREATE = "/symphony-app/campaign-create";
        private const string CAMPAIGN_GRID_ROW_XPATH = "//a[contains(@href,'CampaignSelect.ashx')]";

        public CampaignsGridPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreateCampaignButton()
        {
            ClickElement(_btnCreateCampaign);
            SwitchToFrame(FRAME_NEW_CAMPAIGN_CREATE);
        }

        public void SearchAndSelectCampaign(string campaignName)
        {
            ClearInputAndTypeValue(_txtSearchBox, campaignName);
            WaitForElementToBeInvisible(By.XPath("//div[contains(@class,'spinner-visible')]"));

            if (IsElementPresent(By.XPath("//div[@class='empty-state-heading']")))
                Assert.Fail($"Campaign Name [{campaignName}] does not exist.");

            try
            {
                var campaignLink = _tblCampaignsGrid.FindElement(By.XPath($"//a[contains(@href,'CampaignSelect.ashx') and text()='{campaignName}']"));
                ClickElement(campaignLink);
            }
            catch (NoSuchElementException)
            {
                Assert.Fail($"Campaign Name [{campaignName}] does not exist.");
            }
        }

        public void VerifyNumberOfCampaignsOnGrid(int expectedLoadedCampaigns = 50)
        {
            var actualLoadedCampaigns = _tblCampaignsGrid.FindElements(By.ClassName("ag__row--is-body")).ToList().Count;
            Assert.AreEqual(expectedLoadedCampaigns, actualLoadedCampaigns, "Number of campaigns is incorrect on Campaign Grid");
        }
    }
}
