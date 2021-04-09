using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals
{
    public class AgencyBriefPage : BasePage
    {
        private const string FRAME_EXPORT_POPUP = "/Export/Export.aspx?Export";

        private IWebElement _ddlPublisher => FindElementByName("ctl00$Content$CurrentlyLoadedControlId$selPublisher");
        private IWebElement _txtBudget => FindElementByName("ctl00$Content$CurrentlyLoadedControlId$txtBudget");
        private IWebElement _btnSave => FindElementById("ctl00_Content_CurrentlyLoadedControlId_btnSaveProposal");
        private IWebElement _btnSendBrief => FindElementById("ctl00_Content_CurrentlyLoadedControlId_btnSendBrief");

        public AgencyBriefPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectPublisher(string text)
        {
            SelectWebformDropdownValueByText(_ddlPublisher, text);
        }

        public void SetValueInBudget(decimal text)
        {
            SetElementText(_txtBudget, text.ToString());
        }

        public void ClickSaveButton()
        {
            ClickElement(_btnSave);
        }

        public SendBriefPage ClickSendBriefButton()
        {
            ClickElement(_btnSendBrief);
            SwitchToFrame(FRAME_EXPORT_POPUP);
            return new SendBriefPage(driver, FeatureContext);
        }
    }
}