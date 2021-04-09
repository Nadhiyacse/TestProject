using Automation_Framework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Publishers.Popups
{
    public class ImportRatecardFrame : BasePage
    {
        private IWebElement _btnDownloadCurrentFile => FindElementByXPath("//div[@class='download-button']/button");
        private IWebElement _inpUploadRatecardFile => FindElementByXPath("//input[@name='file']");
        private IWebElement _btnSave => FindElementByXPath("//*[text()= 'Save']/ancestor::button");
        private IWebElement _btnCancel => FindElementByXPath("//*[text()= 'Cancel']/ancestor::button");
        private IWebElement _btnClose => FindElementByXPath("//button[@class='aui--button btn-cross btn btn-xs btn-default']");
        private IWebElement _msgStatus => FindElementByXPath("//div[@class='status-box']/span");

        public ImportRatecardFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void UploadRatecardFile(string filename)
        {
            var filePath = FileHelper.GetImportRatecardFilePath(filename);
            _inpUploadRatecardFile.SendKeys(filePath);
            ClickElement(_btnSave);
            WaitUntilMessageDisplayed("All Done! Rate default has been imported.");
            ClickElement(_btnClose);
            SwitchToMainWindow();
        }

        public void DownloadCurrentFile()
        {
            ClickElement(_btnDownloadCurrentFile);
        }

        public void WaitUntilMessageDisplayed(string message)
        {
            try
            {
                Wait.Until(driver => _msgStatus.Displayed);
                Wait.Until(driver => _msgStatus.Text.Equals(message));
            }
            catch
            {
                Assert.Fail("Success message not found.");
            }
        }
    }
}
