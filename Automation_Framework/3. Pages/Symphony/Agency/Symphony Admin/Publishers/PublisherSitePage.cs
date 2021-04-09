using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherSitePage : BasePage
    {
        private IWebElement _btnCreate => FindElementById("ctl00_ctl00_Content_Content_btnCreate");
        private IWebElement _lnkEdit(string siteName) => FindElementByXPath($"//td[contains(., \"{siteName}\")]/..//a[text()='Edit']");

        public PublisherSitePage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
        }

        public bool DoesSiteExist(string siteName)
        {
            return IsElementPresent(By.XPath($"//td[contains(., \"{siteName}\")]"));
        }

        public void ClickSiteName(string siteName)
        {
            WaitForPageLoadCompleteAfterClickElement(_lnkEdit(siteName));
        }
    }
}
