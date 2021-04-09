using Automation_Framework.DataModels.InfrastructureData.Administrator.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Publishers
{
    public class RatecardPage : BasePage
    {
        private IWebElement _ddlCountry => FindElementById("ctl00_ctl00_Content_Content_ddlCountry");
        private IWebElement _ddlStatus => FindElementById("ctl00_ctl00_Content_Content_ddlStatus");
        private IWebElement _btnExport => FindElementById("ctl00_ctl00_Content_Content_btnExport");
        private IWebElement _lnkEdit(string publisher) => FindElementByXPath($"//td[contains(text(), '{publisher}')]/..//a[text()='Edit']");

        private const string FRAME_IMPORT_RATECARD = "/symphony-app/ratecard-default-import";

        public RatecardPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickExport()
        {
            ClickElement(_btnExport);
        }

        public bool DoesPublisherExist(RatecardData ratecardData)
        {
            SelectWebformDropdownValueIfRequired(_ddlCountry, ratecardData.Country);
            SelectWebformDropdownValueIfRequired(_ddlStatus, ratecardData.Status);
            return IsElementPresent(By.XPath($"//td[contains(text(), '{ratecardData.Publisher}')]"));
        }

        public void ClickEditForPublisher(string publisher)
        {
            ClickElement(_lnkEdit(publisher));
            SwitchToFrame(FRAME_IMPORT_RATECARD);
        }
    }
}
