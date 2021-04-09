using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals.Popups
{
    public class ProposalRatingPopUpPage : BasePage
    {
        public IWebElement _btnSend => FindElementById("ctl00_Content_CurrentlyLoadedControlId_ucProposalRating_btnSave");
        public IWebElement _msgSaveRating => FindElementById("save-rating-message");
        public IWebElement _iconCreativityRating => FindElementByCssSelector("#creativity-rating img:nth-child(5)");
        public IWebElement _iconValueForMoneyRating => FindElementByCssSelector("#valueformoney-rating img:nth-child(5)");
        public IWebElement _iconOnTargetRating => FindElementByCssSelector("#ontarget-rating img:nth-child(5)");
        public IWebElement _iconOnTimeRating => FindElementByCssSelector("#ontime-rating img:nth-child(5)");

        public ProposalRatingPopUpPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetProposalRatingMessage()
        {
            return _msgSaveRating.Text;
        }

        public void SendProposalRating(string handle)
        {
            ClickElement(_iconCreativityRating);
            ClickElement(_iconValueForMoneyRating);
            ClickElement(_iconOnTargetRating);
            ClickElement(_iconOnTimeRating);
            ClickElement(_btnSend);
            driver.SwitchTo().Window(handle);
        }
    }
}