using System.Collections.Generic;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class VersionHistoryModal : BasePage
    {
        private IWebElement _btnClose => FindElementByXPath("//div[@class='modal-content']//button[@class='aui--button btn-cross btn btn-xs btn-default']");
        private IWebElement _tblStatusHistory => FindElementByXPath("//div[@class='modal-content']//div[@class='ag__grid ag__grid--is-bordered']");

        public VersionHistoryModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetRowDateTime(int rowNumber)
        {
            Wait.Until(driver => FindElementByCssSelector(".modal-content").Displayed);

            var headerColumnIndexByName = CreateHeaderColumnIndexByName();
            var selectedRow = GetRow(rowNumber);
            var selectedRowColumns = selectedRow.FindElements(By.CssSelector(".ag__cell"));
            var dateTime = selectedRowColumns[headerColumnIndexByName["Date"]];
            return dateTime.Text;
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
        }

        private Dictionary<string, int> CreateHeaderColumnIndexByName()
        {
            var header = _tblStatusHistory.FindElement(By.XPath(".//div[@class='ag__header ag__with-fix-and-free']"));
            var headerColumns = header.FindElements(By.XPath(".//div[@class='ag__cell--wrap-text']"));

            var headerColumnIndexByName = new Dictionary<string, int>();
            for (var i = 0; i < headerColumns.Count; i++)
            {
                headerColumnIndexByName.Add(headerColumns[i].Text, i);
            }

            return headerColumnIndexByName;
        }

        private IWebElement GetRow(int rowNumber)
        {
            var rows = _tblStatusHistory.FindElements(By.XPath(".//div[@class='ag__row ag__row--is-body']"));
            return rows[rowNumber - 1];
        }
    }
}
