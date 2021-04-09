using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class HistoryLineItemStatusFrame : BasePage
    {
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _tblStatusHistory => FindElementById("ctl00_Content_gvHistory");

        private Dictionary<string, int> _statusHistoryTableColumnHeaders = new Dictionary<string, int>();

        public HistoryLineItemStatusFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetRowDateTime(int rowNumber)
        {
            var headerColumnIndexByName = CreateHeaderColumnIndexByName();
            var selectedRow = GetRow(rowNumber);
            var selectedRowColumns = selectedRow.FindElements(By.TagName("td"));
            var dateTime = selectedRowColumns[headerColumnIndexByName["Date/Time"]];
            return dateTime.Text;
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
            SwitchToMainWindow();
        }

        private Dictionary<string, int> CreateHeaderColumnIndexByName()
        {
            var header = _tblStatusHistory.FindElement(By.XPath("./table/thead/tr"));
            var headerColumns = header.FindElements(By.TagName("th"));

            var headerColumnIndexByName = new Dictionary<string, int>();
            for (var i = 0; i < headerColumns.Count; i++)
            {
                headerColumnIndexByName.Add(headerColumns[i].Text, i);
            }

            return headerColumnIndexByName;
        }

        private IWebElement GetRow(int rowNumber)
        {
            var rows = _tblStatusHistory.FindElements(By.XPath("./table/tbody/tr"));
            return rows[rowNumber - 1];
        }
    }
}
