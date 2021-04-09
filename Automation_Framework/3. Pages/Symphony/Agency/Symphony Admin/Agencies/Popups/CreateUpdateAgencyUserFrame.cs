using System;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.CommonData;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies.Popups
{
    public class CreateUpdateAgencyUserFrame : BasePage
    {
        private IWebElement _txtName => FindElementById(TXT_USERNAME_ID);
        private IWebElement _txtEmail => FindElementById("agencyAdminUserEmail");
        private IWebElement _chkSendPassword => FindElementByXPath("//input[@id = 'agencyAdminUserSendPassword']/../div[@class = 'checkbox-component-icon']");
        private IWebElement _btnResetPassword => FindElementByXPath("//div[text() = 'Reset Password']/ancestor::button");
        private IWebElement _btnSave => FindElementByXPath("//div[text() = 'Save']/ancestor::button");
        private IWebElement _btnClose => FindElementByXPath("//div[text() = 'Close']/ancestor::button");
        private IWebElement _lblMessagePanel => FindElementByXPath("//div[contains(@class, 'alert-success')]/span");

        private const string CHECKBOX_COMPONENT_XPATH = "//div[text() = '{0}']/../div[@class = 'checkbox-component-icon']";
        private const string RADIO_COMPONENT_XPATH = "//div[text() = '{0}']/..//span";
        private const string ERROR_MESSAGE_XPATH = "//div[contains(@class, 'alert-danger')]/span";
        private const string TXT_USERNAME_ID = "agencyAdminUserName";

        public CreateUpdateAgencyUserFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateAgencyUser(UserData userData, bool isEnableAdminSplit = false)
        {
            EnsureMandatoryValuesAreProvided(userData);

            WaitForElementToBeVisible(By.Id(TXT_USERNAME_ID));
            ClearInputAndTypeValue(_txtName, userData.Name);
            ClearInputAndTypeValue(_txtEmail, userData.Email);
            SetWebformCheckBoxState(_chkSendPassword, userData.SendPassword);
            AssignUserRolesIfAvailable(userData, isEnableAdminSplit);

            ClickElement(_btnSave);
        }

        public void EditAgencyUser(UserData userData, bool isEnableAdminSplit = false)
        {
            EnsureMandatoryValuesAreProvided(userData);

            WaitForDataToBePopulated();
            ClearInputAndTypeValue(_txtName, userData.Name);
            AssignUserRolesIfAvailable(userData, isEnableAdminSplit);

            ClickElement(_btnSave);
        }

        public void ClickClose()
        {
            ClickElement(_btnClose);
        }

        private void EnsureMandatoryValuesAreProvided(UserData userData)
        {
            var dataErrorFound = false;
            var userDataErrors = new StringBuilder();
            userDataErrors.Append("The feature file " + FeatureContext.FeatureInfo.Title + " has the following data issues:");

            if (string.IsNullOrWhiteSpace(userData.Name))
            {
                dataErrorFound = true;
                userDataErrors.Append("\n- User Name was not set");
            }

            if (string.IsNullOrWhiteSpace(userData.Email))
            {
                dataErrorFound = true;
                userDataErrors.Append("\n- User Email was not set");
            }

            if (dataErrorFound)
                throw new ArgumentException(userDataErrors.ToString());
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }

        public bool IsSaveSuccessful()
        {
            return !IsElementPresent(By.XPath(ERROR_MESSAGE_XPATH));
        }

        private void AssignUserRolesIfAvailable(UserData userData, bool isEnableAdminSplit)
        {
            if (userData.Roles != null && userData.Roles.Any())
            {
                foreach (var role in userData.Roles)
                {
                    if (isEnableAdminSplit && (role.Equals("Agency Administrator") || role.Equals("User Administrator")))
                    {
                        SetReactCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, "Administrator"), isEnableAdminSplit);
                        ClickElement(GetWebElementUsingSettingLabel(RADIO_COMPONENT_XPATH, role));
                    }
                    else
                    {
                        SetReactCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, role), true);
                    }
                }
            }
        }
    }
}
