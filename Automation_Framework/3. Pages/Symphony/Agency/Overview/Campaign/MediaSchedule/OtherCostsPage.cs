using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class OtherCostsPage : BasePage
    {
        private const string FRAME_ADD_OTHER_COST = "EditOtherCosts";

        private IWebElement _btnAdd => FindElementByCssSelector("#main-button-bar a.fd-add");
        private IWebElement _btnBack => FindElementById("btnBack");

        public OtherCostsPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickAddButton()
        {
            ClickElement(_btnAdd);
            SwitchToFrame(FRAME_ADD_OTHER_COST);
        }

        public void ClickBackButton()
        {
            ClickElement(_btnBack);
        }
    }
}
