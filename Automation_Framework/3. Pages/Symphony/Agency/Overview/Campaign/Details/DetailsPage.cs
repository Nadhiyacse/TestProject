using System.Text.RegularExpressions;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Details
{
    public class DetailsPage : BasePage
    {
        private const string FRAME_EXPORT = "/Export/Export.aspx?";

        private IWebElement _lblCampaingnName => FindElementByXPath("//span[@class='nav-title']");
        private IWebElement _lblCampaignDates => FindElementByXPath("//div[@class='nav-subtitles']/span[3]");
        private IWebElement _btnAttachments => FindElementById("ctl00_Content_btnAttach");
        private IWebElement _btnExport => FindElementById("ctl00_Content_btnCampaignExports");

        public DetailsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetCampaignName()
        {
            return _lblCampaingnName.Text;
        }

        public string GetCampaignId()
        {
            Wait.Until(driver => _btnAttachments.Displayed);
            var href = _btnAttachments.GetAttribute("href");
            var campaignId = Regex.Match(href, @"CampaignId=([\d]+)").Groups[1].Value;
            return campaignId;
        }

        public string GetCampaignDates()
        {
            return _lblCampaignDates.Text;
        }

        public void ClickExportButton()
        {
            ClickElement(_btnExport);
            SwitchToFrame(FRAME_EXPORT);
        }
    }
}
