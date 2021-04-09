using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyListPage : BasePage
    {
        private IWebElement _btnCreate => FindElementById("ctl00_Content_btnCreate");
        private IWebElement _ddlCountry => FindElementById("ctl00_Content_ddlCountry");
        private IWebElement _lnkNext => FindElementByXPath("//a[@class = 'pager-page-item pager-page-item-next']");

        public AgencyListPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
        }

        public bool IsAgencyExistInCountry(string country, string agencyName)
        {
            SelectWebformDropdownValueByText(_ddlCountry, country);
            WaitForPageLoadComplete();

            bool isFound = false;
            bool isNextLinkPresent;

            do
            {
                if (IsElementPresent(By.LinkText(agencyName)))
                {
                    isFound = true;
                    break;
                }

                isNextLinkPresent = IsElementPresent(By.XPath("//a[@class = 'pager-page-item pager-page-item-next']"));
                if (isNextLinkPresent)
                {
                    ClickElement(_lnkNext);
                    WaitForPageLoadComplete();
                }
            }
            while (!isFound && isNextLinkPresent);

            return isFound;
        }

        public void ClickAgencyName(string name)
        {
            WaitForPageLoadCompleteAfterClickElement(FindElementByLinkText(name));
        }
    }
}