using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Campaign;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class CampaignStep : BaseStep
    {
        private readonly CampaignsPage _campaignsPage;
        private readonly AddEditParentCampaignFrame _addEditParentCampaignFrame;

        public CampaignStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _campaignsPage = new CampaignsPage(driver, featureContext);
            _addEditParentCampaignFrame = new AddEditParentCampaignFrame(driver, featureContext);
        }

        [Given(@"I configure my agencies parent campaign settings as an Agency Administrator")]
        public void ConfigureMyAgenciesParentCampaignSettingsAsAnAgencyAdministrator()
        {
            if (AgencySetupData.AgencyAdministratorData.Campaigns == null)
                throw new ArgumentNullException("Campaigns data is empty");

            if (AgencySetupData.AgencyAdministratorData.Campaigns.ParentCampaignsData == null || !AgencySetupData.AgencyAdministratorData.Campaigns.ParentCampaignsData.Any())
                throw new ArgumentNullException("Parent campaigns data is empty");

            foreach (var campaignDetailsData in AgencySetupData.AgencyAdministratorData.Campaigns.ParentCampaignsData)
            {
                NavigationPage.NavigateTo("Administrator Campaigns Parent Campaigns");

                if (!_campaignsPage.IsParentCampaignExist(campaignDetailsData))
                {
                    _campaignsPage.ClickAddButton();
                    _addEditParentCampaignFrame.PopulateParentCampaignDetails(campaignDetailsData);
                    Assert.IsTrue(_campaignsPage.IsParentCampaignExist(campaignDetailsData));
                }
                else if (_campaignsPage.IsParentCampaignExist(campaignDetailsData) && !_campaignsPage.IsCampaignStatusCorrect(campaignDetailsData))
                {
                    _campaignsPage.ClickEdit(campaignDetailsData);
                    _addEditParentCampaignFrame.PopulateParentCampaignDetails(campaignDetailsData);
                    Assert.IsTrue(_campaignsPage.IsCampaignStatusCorrect(campaignDetailsData));
                }
            }
        }
    }
}
