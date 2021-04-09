using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.BriefsAndProposals
{
    public class SendBriefPage : BasePage
    {
        private IWebElement _ddlSendBriefTo => FindElementByCssSelector("#ctl00_Content_CurrentlyLoadedDeliveryStrategyControlId_toPublisherContact");
        private IWebElement _lnkSendBriefTo => FindElementByCssSelector("#tblExport tr:nth-child(3) td:nth-child(2) span");
        private IWebElement _btnContinue => FindElementById("ctl00_ButtonBar_btnContinue");
        private IWebElement _msgSendBrief => FindElementByCssSelector(".message.success");

        public SendBriefPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SendBrief(string text)
        {
            ClickElement(_lnkSendBriefTo);
            SelectWebformDropdownValueByText(_ddlSendBriefTo, text);
        }

        public void ClickContinueButton()
        {
            ClickElement(_btnContinue);
        }
        public string GetSendBriefMessage()
        {
            Wait.Until(driver => _msgSendBrief.Enabled);
            return _msgSendBrief.Text;
        }
    }
}