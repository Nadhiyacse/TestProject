using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class AddNonMediaCostItemsFrame : BasePage
    {
        private IWebElement _btnSave => FindElementById("ctl00_Content_ucItemList_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_Content_ucItemList_btnClose");
        private IWebElement _chkSelectAllNonMediaCostItems => FindElementById("ctl00_Content_ucItemList_gvOtherCost_ctl01_checkAll");
        private IWebElement _pnlMessage => FindElementById("ctl00_Content_ucItemList_pnlMessage");

        public AddNonMediaCostItemsFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void AddAllNonMediaCosts()
        {
            ClickElement(_chkSelectAllNonMediaCostItems);
            ClickElement(_btnSave);
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
        }

        public string GetMsgText()
        {
            Wait.Until(driver => _pnlMessage.Displayed);
            return _pnlMessage.Text;
        }
    }
}
