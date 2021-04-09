using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Automation_Framework._3._Pages.Symphony.Agency.Campaigns;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Campaigns
{
    [Binding]
    public class CampaignsStep : BaseStep
    {
        private readonly CampaignsGridPage _campaignsGridPage;

        public CampaignsStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _campaignsGridPage = new CampaignsGridPage(driver, featureContext);
        }

        [Then(@"The campaigns are rendered on Campaign Search Grid page")]
        public void CampaignsAreRenderedOnTheGrid()
        {
            _campaignsGridPage.VerifyNumberOfCampaignsOnGrid();
        }
    }
}
