using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherUserPage : BasePage
    {
        private const string FRAME_CREATE_USER = "Create.aspx";
        private const string FRAME_EDIT_USER = "Edit.aspx";

        private IWebElement _btnCreate => FindElementById("ctl00_ctl00_Content_Content_btnCreate");

        public PublisherUserPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
            SwitchToFrame(FRAME_CREATE_USER);
        }

        public void ClickEdit(string userEmail)
        {
            var relativeEditButtonPath = $"//*[@class='fdtable fdtable-max-padding fdtable-noselect']/tbody/tr/td[contains(text(), '{userEmail}')]/..//a[contains(@href, 'Edit.aspx')]";
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