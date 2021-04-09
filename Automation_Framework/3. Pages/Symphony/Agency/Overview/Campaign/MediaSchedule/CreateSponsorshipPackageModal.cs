using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class CreateSponsorshipPackageModal : BasePage
    {
        private IWebElement _txtCreateSponsorshipPackageTextbox => FindElementById("save-sponsorship-name-input");
        private IWebElement _btnSave => FindElementByClassName("save-button");

        public CreateSponsorshipPackageModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateSponsorshipPackage(string sponsorshipPackageName)
        {
            ClearInputAndTypeValue(_txtCreateSponsorshipPackageTextbox, sponsorshipPackageName);
            ClickElement(_btnSave);
        }
    }
}