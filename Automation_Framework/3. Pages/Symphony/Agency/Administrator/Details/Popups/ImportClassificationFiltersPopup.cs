using Automation_Framework.Helpers;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Details.Popups
{
    public class ImportClassificationFiltersPopup : BasePage
    {
        private IWebElement _inpFile => FindElementByXPath("//input[@name='file']");
        private IWebElement _btnImport => FindElementByCssSelector("button.aui--button.btn.btn-primary");

        public ImportClassificationFiltersPopup(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ImportClassificationFilters()
        {
            var filePath = FileHelper.GetImportClassificationFilterFilePath(FeatureContext.FeatureInfo.Title);
            _inpFile.SendKeys(filePath);
            ClickElement(_btnImport);
            Assert.IsTrue(IsSuccessNotificationShownWithMessage("Classification filter mappings have been imported"), "Success toast was not shown.");
        }
    }
}
