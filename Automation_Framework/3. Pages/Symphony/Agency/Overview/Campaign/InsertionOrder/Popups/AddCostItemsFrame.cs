using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class AddCostItemsFrame : BasePage
    {
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnAdd");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _chkSelectAllCostItems => FindElementById("ctl00_Content_ucItemList_lstScheduleItems_checkAll");

        public AddCostItemsFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectAllCostItems()
        {
            ClickElement(_chkSelectAllCostItems);
        }

        public void ClickSave()
        {
            ClickElement(_btnSave);
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
        }
    }
}
