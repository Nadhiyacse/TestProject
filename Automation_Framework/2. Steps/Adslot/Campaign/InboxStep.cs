using Automation_Framework._3._Pages.Adslot.Campaigns;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Adslot.Campaign
{
    [Binding]
    public class InboxStep : BaseStep
    {
        private const string IO_PDF_EXPORT_MESSAGE = "Version {0} of the Symphony IO PDF Export is available for download. This link will expire in {1} days.";

        private InboxPage _inboxPage;

        public InboxStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _inboxPage = new InboxPage(driver, featureContext);
        }

        [Then(@"The IO PDF Export message should be visible for version (\d+) and expire in (\d+) days")]
        public void CheckIfIoPdfMessageExistsWithLink(int version, int daysUntilExpiry)
        {
            var message = string.Format(IO_PDF_EXPORT_MESSAGE, version.ToString(), daysUntilExpiry.ToString());

            _inboxPage.DoesInboxMessageExist(message);
        }

        [When(@"I click the IO PDF Export link for version (\d+) that expires in (\d+) days")]
        public void ClickLinkInIoPdfMessage(int version, int daysUntilExpiry)
        {
            var message = string.Format(IO_PDF_EXPORT_MESSAGE, version.ToString(), daysUntilExpiry.ToString());

            _inboxPage.ClickLinkInMessage(message);
        }
    }
}
