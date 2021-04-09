using Automation_Framework._3._Pages.Symphony.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class ExportInsertionOrderFrame : ExportFramePage
    {
        public ExportInsertionOrderFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }        

        public void DownloadIOExport(string exportTye)
        {
            if (IsElementPresent(By.Id("ctl00_Content_ddlExportProvider")))
            {
                SelectExportType(exportTye);
            }
            ScrollAndClickElement(_btnContinue);
        }

        public string GetMsgSuccess()
        {
            var msgSuccess = string.Empty;
            Wait.Until(driver => _msgExportSuccess.Displayed);
            msgSuccess = _msgExportSuccess.Text;
            ScrollAndClickElement(_btnClose);
            SwitchToDefaultContent();
            return msgSuccess;
        }
    }
}