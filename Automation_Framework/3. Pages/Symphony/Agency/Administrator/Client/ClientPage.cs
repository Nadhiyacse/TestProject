using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Client
{
    public class ClientPage : BasePage
    {
        private IWebElement _btnNew => FindElementById("ctl00_Content_btnNew");
        private IWebElement _txtSearch => FindElementById("ctl00_Content_txtSearchClient");
        private IWebElement _btnSearch => FindElementById("ctl00_Content_btnSearchClient");
        private IWebElement _msgInfo => FindElementById("ctl00_Content_pnlMessage");
        private IWebElement _tblClientInfo => FindElementByCssSelector(".fdtable.fdtable-max-padding.fdtable-noselect");
        private IEnumerable<IWebElement> _lstAvailableClients => FindElementsByCssSelector(".fdtable.fdtable-max-padding.fdtable-noselect>tbody>tr td:nth-child(2)");

        public ClientPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickNewButton()
        {
            ClickElement(_btnNew);            
        }

        public bool DoesClientExist(string clientName)
        {
            var isAvailable = false;
            ClearInputAndTypeValue(_txtSearch, clientName);
            ClickElement(_btnSearch);
            if (IsElementPresent(By.CssSelector(".fdtable.fdtable-max-padding.fdtable-noselect")))
            {
                isAvailable = _lstAvailableClients.Where(item => item.Text.Equals(clientName)).Any();
            }
            return isAvailable;
        }

        public void ClickEditForSpecificClient(string clientName)
        {
            var rowClient = _lstAvailableClients.Where(item => item.Text.Equals(clientName)).First();
            var lnkEdit = rowClient.FindElement(By.XPath("..//a[text() = 'Edit']"));
            WaitForPageLoadCompleteAfterClickElement(lnkEdit);
        }
    }
}
