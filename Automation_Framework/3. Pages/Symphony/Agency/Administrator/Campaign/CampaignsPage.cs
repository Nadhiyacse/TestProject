using System;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Administrator.Campaigns;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Campaign
{
    public class CampaignsPage : BasePage
    {
        private IWebElement _ddlClient => FindElementById("ctl00_ctl00_Content_Content_ddlClients");
        private IWebElement _ddlStatus => FindElementById("ctl00_ctl00_Content_Content_ddlStatus");
        private IWebElement _btnAdd => FindElementById("ctl00_ctl00_Content_Content_btnNew");
        private IWebElement _lnkParentCampaigns => FindElementById("ctl00_ctl00_Content_LeftMenu_ucCampaign_lnkParentCampaignLabels");
        private IWebElement _lnkRemovedCampaigns => FindElementById("ctl00_ctl00_Content_LeftMenu_ucCampaign_lnkRemovedCampaigns");
        private IWebElement _tableHeaderRow => FindElementByXPath($"{PARENT_CAMPAIGNS_TABLE_XPATH}/table/thead");

        private const string TAG_NAME_TH = "th";
        private const string TAG_NAME_TD = "td";
        private const string PARENT_CAMPAIGNS_TABLE_XPATH = "//*[@id= 'ctl00_ctl00_Content_Content_gdvParentCampaigns']";
        private const string ADD_EDIT_PARENT_CAMPAIGN_FRAME = "AddEditParentCampaign.aspx";

        public CampaignsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public bool IsParentCampaignExist(ParentCampaignsData data)
        {
            EnsureMandatoryValuesAreProvided(data);

            SelectWebformDropdownValueByText(_ddlClient, data.Client);

            var isParentCampaignExist = false;

            if (IsElementPresent(By.XPath($"{PARENT_CAMPAIGNS_TABLE_XPATH}/table/thead")))
            {
                var parentCampaignNameColumnIndex = GetColumnIndexWithTableHeader("Parent Campaign Name");

                isParentCampaignExist = FindElementsByXPath($"{PARENT_CAMPAIGNS_TABLE_XPATH}//td[{parentCampaignNameColumnIndex}]").Where(x => x.Text == data.Name).Any();
            }

            return isParentCampaignExist;
        }

        public bool IsCampaignStatusCorrect(ParentCampaignsData data)
        {
            var statusColumnIndex = GetColumnIndexWithTableHeader("Status");

            var rowParentCampaignName = GetRowWithParentCampaignName(data);

            var isCampaignStatusCorrect = rowParentCampaignName.FindElement(By.XPath($"./ancestor::tr//td[{statusColumnIndex}]")).Text.Equals(data.Status.ToUpper());

            return isCampaignStatusCorrect;
        }

        public void ClickAddButton()
        {
            ScrollAndClickElement(_btnAdd);
            SwitchToFrame(ADD_EDIT_PARENT_CAMPAIGN_FRAME);
        }

        public void ClickEdit(ParentCampaignsData data)
        {
            var rowParentCampaignName = GetRowWithParentCampaignName(data);

            var _lnkEdit = rowParentCampaignName.FindElement(By.XPath($"..//ancestor::tr//a[text() = 'Edit']"));

            ScrollAndClickElement(_lnkEdit);
            SwitchToFrame(ADD_EDIT_PARENT_CAMPAIGN_FRAME);
        }

        private void EnsureMandatoryValuesAreProvided(ParentCampaignsData data)
        {
            var dataErrorFound = false;
            var campaignDataErrors = new StringBuilder();
            campaignDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(data.Name))
            {
                dataErrorFound = true;
                campaignDataErrors.Append("\n- Parent campaign name was not set");
            }

            if (string.IsNullOrWhiteSpace(data.Status))
            {
                dataErrorFound = true;
                campaignDataErrors.Append("\n- Parent campaign status was not set");
            }

            if (string.IsNullOrWhiteSpace(data.Client))
            {
                dataErrorFound = true;
                campaignDataErrors.Append("\n- Parent campaign client was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(campaignDataErrors.ToString());
        }

        private IWebElement GetRowWithParentCampaignName(ParentCampaignsData data)
        {
            var parentCampaignNameColumnIndex = GetColumnIndexWithTableHeader("Parent Campaign Name");

            var row = FindElementsByXPath($"{PARENT_CAMPAIGNS_TABLE_XPATH}//td[{parentCampaignNameColumnIndex}]").Where(x => x.Text == data.Name).FirstOrDefault();

            return row;
        }

        private int GetColumnIndexWithTableHeader(string headerName)
        {
            return _tableHeaderRow.FindElements(By.TagName(TAG_NAME_TH)).ToList().FindIndex(x => x.Text == headerName) + 1;
        }
    }
}
