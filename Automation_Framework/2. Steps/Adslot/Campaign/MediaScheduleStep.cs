using Automation_Framework._3._Pages.Adslot.Campaigns;
using Automation_Framework._3._Pages.Adslot.Campaigns.MediaSchedule;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Adslot.Campaign
{
    [Binding]
    public class MediaScheduleStep : BaseStep
    {
        private readonly MediaSchedulePage _mediaSchedulePage;
        private readonly ManageCampaignsPage _manageCampaignsPage;
        private readonly MediaScheduleReviewSignoffPage _mediaScheduleReviewSignoffPage;

        public MediaScheduleStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _manageCampaignsPage = new ManageCampaignsPage(driver, featureContext);
            _mediaSchedulePage = new MediaSchedulePage(driver, featureContext);
            _mediaScheduleReviewSignoffPage = new MediaScheduleReviewSignoffPage(driver, featureContext);
        }

        [Given(@"I sign off the items in Adslot publisher")]
        [When(@"I sign off the items in Adslot publisher")]
        public void SignOffItemsInAdslotPublisher()
        {
            _mediaSchedulePage.ClickApproveButton();
            _mediaScheduleReviewSignoffPage.CommentAndSignOff("Signing off for test campaign", WorkflowTestData.PublisherLoginUserData.Password);
        }

        [Given(@"I reject the items in Adslot publisher")]
        public void RejectItemsInAdslotPublisher()
        {
            _mediaSchedulePage.ClickRejectButton();
            _mediaScheduleReviewSignoffPage.CommentAndSignOff("Test campaign is rejected because I don't like it", WorkflowTestData.PublisherLoginUserData.Password);
        }

        [When(@"I sign off the changes in Adslot publisher")]
        public void SignOffChangesInAdslotPublisher()
        {
            _mediaSchedulePage.ClickSignOffButton();
            _mediaScheduleReviewSignoffPage.CommentAndSignOff("Signing off for test campaign", WorkflowTestData.PublisherLoginUserData.Password);            
        }

        [When(@"I switch back to Symphony as an agency user")]
        public void WhenISwitchBackToSymphonyAsAnAgencyUser()
        {
            _mediaSchedulePage.SwitchToMainWindow();
        }
    }
}
