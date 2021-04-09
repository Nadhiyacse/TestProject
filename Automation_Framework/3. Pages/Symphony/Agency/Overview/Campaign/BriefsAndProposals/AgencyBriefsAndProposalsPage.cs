using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals
{
    public class AgencyBriefsAndProposalsPage : BasePage
    {
        private IWebElement _ddlCountry => FindElementByName("ctl00$Content$CurrentlyLoadedControlId$ddlCountry");
        private IWebElement _btnAddBrief => FindElementById("ctl00_Content_CurrentlyLoadedControlId_btnNew");
        private IWebElement _lnkViewBrief(string publisher)
        {
            return FindElementByXPath($"//a[text()='{publisher}']/ancestor::tr//a[contains(@id,'ViewBrief')]");
        }
        private IWebElement _lnkViewProposal(string publisher)
        {
            return FindElementByXPath($"//a[text()='{publisher}']/ancestor::tr//a[contains(@id,'ViewProposal')]");
        }
        private IWebElement _lnkPublisher(string publisher)
        {
            return FindElementByXPath($"//a[text()= '{publisher}']");
        }

        public AgencyBriefsAndProposalsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectCountry(string text)
        {
            SelectWebformDropdownValueByText(_ddlCountry, text);
        }

        public AgencyBriefPage ClickAddBriefButton()
        {
            ClickElement(_btnAddBrief);
            return new AgencyBriefPage(driver, FeatureContext);
        }

        public AgencyBriefPage ClickViewBriefLink(string publisher)
        {
            var strPublisher = publisher.Split('(');
            ClickElement(_lnkViewBrief(strPublisher[0].Trim()));
            return new AgencyBriefPage(driver, FeatureContext);
        }

        public AgencyProposalPage ClickViewProposalLink(string publisher)
        {
            var strPublisher = publisher.Split('(');
            ClickElement(_lnkViewProposal(strPublisher[0].Trim()));
            return new AgencyProposalPage(driver, FeatureContext);
        }

        public bool IsPublisherDisplayed(string publisher)
        {
            var strPublisher = publisher.Split('(');
            var isDisplayed = false;
            try
            {
                isDisplayed = _lnkPublisher(strPublisher[0].Trim()).Displayed;
            }
            catch (NoSuchElementException)
            {
            }

            return isDisplayed;
        }
    }
}
