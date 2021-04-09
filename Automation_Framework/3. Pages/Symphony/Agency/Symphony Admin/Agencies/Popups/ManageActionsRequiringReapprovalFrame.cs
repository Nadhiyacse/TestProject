using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies.Agency_Features;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies.Popups
{
    public class ManageActionsRequiringReapprovalFrame : BasePage
    {
        private IWebElement _btnSave => FindElementByXPath("//div[text()='Save']/ancestor::button");
        private IWebElement _btnClose => FindElementByXPath("//div[text()='Close']/ancestor::button");
        private IList<IWebElement> _lstRemoveButtons => FindElementsByXPath("//div[@data-test-selector='button-remove']/button").ToList();
        private IWebElement _btnAddAction(string actionName) => FindElementByXPath($"//span[text()='{actionName}']/ancestor::div[contains(@class, 'grid-component-row-body')]/div[@data-test-selector='button-add']/button");
        private IWebElement _btnOpenTabElements(string tabName) => FindElementByXPath($"//span[text()='{tabName}']/ancestor::div[contains(@class, 'grid-component-row-body')]/div[@data-test-selector='expander']/div");
        private IWebElement _lnkAll => FindElementByClassName("aui--breadcrumb-node-link");
        private IWebElement _lblAlertMessage => FindElementByClassName("alert");

        public ManageActionsRequiringReapprovalFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ConfigureActionsNotRequiringReapproval(List<ActionsNotRequiringReapproval> actionsNotRequiringReapproval)
        {
            RemoveAllConfiguredActions();

            foreach (var action in actionsNotRequiringReapproval)
            {
                _btnOpenTabElements(action.Tab).Click();

                foreach (var element in action.Elements)
                {
                    _btnAddAction(element).Click();
                }

                ClickElement(_lnkAll);
            }

            ClickElement(_btnSave);
            WaitUntilAlertContains("Settings saved successfully.");
            ClickElement(_btnClose);
        }

        private void RemoveAllConfiguredActions()
        {
            foreach (var removeButton in _lstRemoveButtons)
            {
                removeButton.Click();
            }
        }

        protected void WaitUntilAlertContains(string message)
        {
            Wait.Until(driver => _lblAlertMessage.Displayed);
            Assert.IsTrue(_lblAlertMessage.Text.Contains(message), $"Actual message \"{_lblAlertMessage.Text}\" does NOT contain the string \"{message}\"");
        }
    }
}
