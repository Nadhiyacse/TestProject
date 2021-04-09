using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Campaigns.MediaSchedule
{
    public class MediaSchedulePage : BasePage
    {
        private const string PURCHASE_TYPE_XPATH = ".//div[@data-test-selector='campaign-section-line-item-buy-type']";
        private const string EDIT_GOAL_XPATH = ".//div[@data-test-selector='campaign-section-line-item-booked-impressions']/div[1]";
        private const string EDIT_RATE_XPATH = ".//div[@data-test-selector='campaign-section-line-item-booked-rate']/div[1]";

        private IWebElement _btnApprove => FindElementByXPath("//button[@data-test-selector='campaign-section-media-schedule-approve-changes']");
        private IWebElement _btnReject => FindElementByXPath("//button[@data-test-selector='campaign-section-media-schedule-reject-changes']");
        private IList<IWebElement> _lstMediaScheduleItems => FindElementsByCssSelector(".ag__grid--is-rimless .ag__row--is-body").ToList();
        private IWebElement _btnSignOff => FindElementByXPath("//button[@data-test-selector='campaign-section-media-schedule-sign-off']");

        public MediaSchedulePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickApproveButton()
        {
            ClickElement(_btnApprove);
        }

        public void ClickRejectButton()
        {
            ClickElement(_btnReject);
        }

        public void ClickSignOffButton()
        {
            ClickElement(_btnSignOff);
        }
    }
}
