using System;
using System.IO;
using OpenQA.Selenium;
using Automation_Framework.Helpers;
using TechTalk.SpecFlow;
using OpenQA.Selenium.Support.UI;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class ImportMediaScheduleModal : BasePage
    {
        private IWebElement _inpFile => FindElementByXPath("//input[@name='file']");
        private IWebElement _btnImport => FindElementByCssSelector("button.aui--button.btn.btn-primary");
        private IWebElement _msgImportSuccess => FindElementByXPath("//*[contains(@class, 'alert-success')]");
        private IWebElement _btnClose => FindElementByXPath("//*[text()= 'Close']/ancestor::button");
        private IWebElement _lnkViewDetails => FindElementByXPath("//*[text()= 'View details']/ancestor::button");
        private IWebElement _btnDowload => FindElementById("btnDownload");
        private IWebElement _msgLoading => FindElementByXPath("//*[@class= 'loading-text']");
        private IWebElement _btnCancel => FindElementByXPath("//*[text()= 'Cancel']/ancestor::button");

        public const string SHORT_DATE_TIME_FORMAT = "yyyy-MM-dd";

        public ImportMediaScheduleModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ImportMediaSchedule()
        {
            var filePath = FileHelper.GetImportMediaScheduleFilePath(FeatureContext.FeatureInfo.Title);
            _inpFile.SendKeys(filePath);
            ClickElement(_btnImport);

            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
                wait.Until(driver => _msgImportSuccess.Displayed);
            }
            catch (WebDriverTimeoutException)
            {
                throw new WebDriverTimeoutException($"File '{FeatureContext.FeatureInfo.Title}' was not imported.");
            }
        }

        public void ImportMediaSchedule(string campaignName)
        {
            var fileName = $"{DateTime.Now.ToString(SHORT_DATE_TIME_FORMAT)}-symphonydataexport_{campaignName.Replace(" ", "_").Replace(":", string.Empty).Replace("/", string.Empty).ToLower()}.xlsx";
            ExcelHelper.SaveFileWithoutChanges(fileName);
            var filePath = string.Format(Path.Combine(FileHelper.GetDownloadsFolderPath(), fileName));
            _inpFile.SendKeys(filePath);
            ClickElement(_btnImport);
        }

        public string GetImportStatus()
        {
            Wait.Until(driver => _msgImportSuccess.Displayed);
            return _msgImportSuccess.Text;
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
            SwitchToDefaultContent();
            WaitForDataToBePopulated(30);
        }

        public void DownloadImportTemplate()
        {
            ClickElement(_lnkViewDetails);
            ClickElement(_btnDowload);
        }

        public string GetTextMsgLoading()
        {
            var msgLoading = string.Empty;
            WaitForLoaderSpinnerToDisappear();
            Wait.Until(driver => _msgLoading.Displayed);
            msgLoading = _msgLoading.Text.ToString();
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(120));
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.InvisibilityOfElementLocated(By.XPath("//*[@class= 'loading-text']")));
            ClickElement(_btnCancel);
            return msgLoading;
        }
    }
}
