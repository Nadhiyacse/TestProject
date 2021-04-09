using System;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.CommonData;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers.Popups
{
    public class CreateUpdatePublisherUserFrame : BasePage
    {
        private IWebElement _txtName => FindElementById("ctl00_Content_txtName");
        private IWebElement _txtEmail => FindElementById("ctl00_Content_txtEmail");
        private IWebElement _chkSendPassword => FindElementById("ctl00_Content_chkSendPassword");
        private IWebElement _btnResetPassword => FindElementById("ctl00_ButtonBar_btnGeneratePassword");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private IWebElement _btnClose => FindElementById("ctl00_ButtonBar_btnClose");
        private IWebElement _lblMessagePanel => FindElementById("ctl00_Content_pnlMessage");

        private const string CHECKBOX_COMPONENT_XPATH = "//div[./label[normalize-space()='{0}']]//input[@type='checkbox']";
        private const string ERROR_MESSAGE_XPATH = "//div[@id='ctl00_Content_pnlMessage' and @class='message error']";

        public CreateUpdatePublisherUserFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreatePublisherUser(UserData userData, bool isSubscribingPublisher)
        {
            EnsureMandatoryValuesAreProvided(userData);

            ClearInputAndTypeValue(_txtName, userData.Name);
            ClearInputAndTypeValue(_txtEmail, userData.Email);

            if (isSubscribingPublisher)
            {
                SetWebformCheckBoxState(_chkSendPassword, userData.SendPassword);
            }

            if (userData.Roles != null && userData.Roles.Any())
            {
                foreach (var role in userData.Roles)
                {
                    SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, role), true);
                }
            }

            ClickElement(_btnSave);
        }

        public void EditAgencyUser(UserData userData)
        {
            EnsureMandatoryValuesAreProvided(userData);

            ClearInputAndTypeValue(_txtName, userData.Name);

            if (userData.Roles != null && userData.Roles.Any())
            {
                foreach (var role in userData.Roles)
                {
                    SetWebformCheckBoxState(GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, role), true);
                }
            }

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
    }
}
