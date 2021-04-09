using System;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies
{
    public class AgencyCustomLabelPage : BasePage
    {
        private const string ADD_EDIT_FRAME = "Create.aspx";

        private IWebElement _btnCreate => FindElementById("ctl00_ctl00_Content_Content_btnCreate");
        private const string EDIT_OR_DELETE_BTN_XPATH = "//table[@class='fdtable fdtable-noselect']//span[contains(text(), '{0}')]/..//../td//a[contains(text(), '{1}')]";
        private const string CUSTOM_LABEL_XPATH = "//table[@class='fdtable fdtable-noselect']//span[contains(text(), '{0}')]";

        public AgencyCustomLabelPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickCreate()
        {
            ClickElement(_btnCreate);
            SwitchToFrame(ADD_EDIT_FRAME);
        }

        public void ClickEdit(string language)
        {
            var editButton = FindElementByXPath(string.Format(EDIT_OR_DELETE_BTN_XPATH, language, "Edit"));
            ClickElement(editButton);
            SwitchToFrame(ADD_EDIT_FRAME);
        }

        public void DeleteCustomLabel(string language)
        {
            var deleteButton = FindElementByXPath(string.Format(EDIT_OR_DELETE_BTN_XPATH, language, "Delete"));
            ClickElement(deleteButton);
            AcceptAlert();
        }

        public bool DoesCustomLabelOverrideExist(string language)
        {
            return IsElementPresent(By.XPath(string.Format(CUSTOM_LABEL_XPATH, language)));
        }
    }
}