using Automation_Framework.DataModels.InfrastructureData.Administrator.Campaigns;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Campaign
{
    public class AddEditParentCampaignFrame : BasePage
    {
        private IWebElement _ddlClient => FindElementById("ctl00_Content_ddlClients");
        private IWebElement _txtParentCampaignName => FindElementById("ctl00_Content_txtParentCampaignLabel");
        private IWebElement _chkEnabled => FindElementById("ctl00_Content_chkParentCampaignStatus");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");

        public AddEditParentCampaignFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void PopulateParentCampaignDetails(ParentCampaignsData data)
        {
            if (_ddlClient.Enabled)
            {
                SelectWebformDropdownValueByText(_ddlClient, data.Client);
            }

            ClearInputAndTypeValueIfRequired(_txtParentCampaignName, data.Name);

            if (_chkEnabled.Enabled)
            {
                var isEnable = false;

                if (data.Status.ToLower().Equals("enabled"))
                {
                    isEnable = true;
                }

                SetWebformCheckBoxState(_chkEnabled, isEnable);
            }

            ScrollAndClickElement(_btnSave);
            ScrollAndClickElement(_btnClose);
        }
    }
}
