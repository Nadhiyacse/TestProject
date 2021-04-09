using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class RestoreSponsorshipFrame : BasePage
    {
        private IWebElement _btnConfirm => FindElementByXPath("//div[text()='Confirm']/ancestor::button");
        public RestoreSponsorshipFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickConfirm()
        {
            ClickElement(_btnConfirm);
        }
    }
}
