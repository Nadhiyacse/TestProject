using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals.Popups;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals
{
    public class AgencyProposalPage : BasePage
    {
        private IWebElement _chkBriefItem => FindElementById("ctl00_Content_CurrentlyLoadedControlId_ucSchedule_gvSchedule_ctl02_chkSchedule");
        private IWebElement _chkSelectAllBriefItems => FindElementById("ctl00_Content_CurrentlyLoadedControlId_ucSchedule_gvSchedule_ctl01_checkAll");
        private IWebElement _btnSend => FindElementById("ctl00_Content_CurrentlyLoadedControlId_ucProposalRating_btnSave");
        private IWebElement _msgSaveRating => FindElementById("save-rating-message");
        private IWebElement _iconCreativityRating => FindElementByCssSelector("#creativity-rating img:nth-child(5)");
        private IWebElement _iconValueForMoneyRating => FindElementByCssSelector("#valueformoney-rating img:nth-child(5)");
        private IWebElement _iconOnTargetRating => FindElementByCssSelector("#ontarget-rating img:nth-child(5)");
        private IWebElement _iconOnTimeRating => FindElementByCssSelector("#ontime-rating img:nth-child(5)");
        private IWebElement _proposalRatingModal => FindElementByCssSelector(".simplemodal-wrap");
        private IWebElement _btnImport => FindElementById("ctl00_Content_CurrentlyLoadedControlId_btnImport");
        private IWebElement _btnRating => FindElementById("ctl00_Content_CurrentlyLoadedControlId_ucProposalRating_btnRating");
        private IWebElement _msgProposalRatingSavedSuccessfully => FindElementByXPath("//*[text() = 'Proposal ratings saved successfully']");

        public AgencyProposalPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ImportAgencyProposal()
        {
            ClickElement(_chkSelectAllBriefItems);
            var parentWindowHandle = driver.CurrentWindowHandle;
            ClickElement(_btnImport);

            var lstWindow = driver.WindowHandles.ToList();
            foreach (var handle in lstWindow)
            {
                driver.SwitchTo().Window(handle);
                if (driver.Title.Contains("Proposal Rating"))
                {
                    var p = new ProposalRatingPopUpPage(driver, FeatureContext);
                    Assert.IsTrue(p.GetProposalRatingMessage().Contains("succ"));
                    p.SendProposalRating(parentWindowHandle);
                }
            }
        }

        public void ImportNCMAgencyProposal()
        {
            ClickElement(_chkSelectAllBriefItems);
            if (_btnRating.GetAttribute("class").Contains("fd-rating-off"))
            {
                ClickElement(_btnImport);
                Assert.IsTrue(_msgSaveRating.Text.Contains("succ"));
                ClickElement(_iconCreativityRating);
                ClickElement(_iconValueForMoneyRating);
                ClickElement(_iconOnTargetRating);
                ClickElement(_iconOnTimeRating);
                ClickElement(_btnSend);
                Assert.IsTrue(_msgProposalRatingSavedSuccessfully.Displayed);
                Wait.Until(driver => !(IsElementPresent(By.CssSelector(".simplemodal-wrap"))));
            }
            else
            {
                ClickElement(_btnImport);
                var alert = driver.SwitchTo().Alert();
                Assert.AreEqual("Items successfully imported to the media schedule", alert.Text);
                alert.Accept();
            }
        }
    }
}
