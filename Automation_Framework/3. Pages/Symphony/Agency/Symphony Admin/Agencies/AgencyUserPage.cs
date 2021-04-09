using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyUserPage : BasePage
    {
        private const string FRAME_CREATE_USER = "symphony-admin-user-details-create";
        private const string FRAME_EDIT_USER = "symphony-admin-user-details-edit";

        private IWebElement _btnCreate => FindElementById("ctl00_ctl00_Content_Content_btnCreate");

        public AgencyUserPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
            SwitchToFrame(FRAME_CREATE_USER);
        }

        public void ClickEdit(string userEmail)
        {
            var relativeEditButtonPath = $"//*[@class='fdtable fdtable-max-padding fdtable-noselect']//td[contains(text(), '{userEmail}')]/ancestor::tr//a[text() = 'Edit']";

            var btnEdit = FindElementByXPath(relativeEditButtonPath);

            ClickElement(btnEdit);
            SwitchToFrame(FRAME_EDIT_USER);
        }

        public bool IsUserEmailExist(string email)
        {
            return IsElementPresent(By.XPath($".//tr/td[2][contains(.,'{email}')]"));
        }
    }
}
