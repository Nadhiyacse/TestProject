using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherListPage : BasePage
    {
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnCreate");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _ddlStatus => FindElementById("ctl00_Content_ddlStatus");
        private IWebElement _lnkNext => FindElementByXPath("//a[@class = 'pager-page-item pager-page-item-next']");

        public PublisherListPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
        }

        public bool DoesPublisherExistInCountry(string country, string publisherName, bool isSubscriber)
        {
            SelectWebformDropdownValueByText(_ddlCountry, country);
            SelectWebformDropdownValueByText(_ddlStatus, isSubscriber ? "Subscriber" : "Non-subscriber");

            bool isFound = false;
            bool isNextLinkPresent;

            do
            {
                if (IsElementPresent(By.LinkText(publisherName)))
                {
                    isFound = true;
                    break;
                }

                isNextLinkPresent = IsElementPresent(By.XPath("//a[@class = 'pager-page-item pager-page-item-next']"));
                if (isNextLinkPresent)
                {
                    ClickElement(_lnkNext);
                }
            }
            while (!isFound && isNextLinkPresent);

            return isFound;
        }

        public void ClickPublisherName(string publisherName)
        {
            WaitForPageLoadCompleteAfterClickElement(FindElementByLinkText(publisherName));
        }
    }
}