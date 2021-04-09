using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.FeatureToggles;
using Automation_Framework.Helpers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Feature_Toggles
{
    public class FeatureTogglesPage : BasePage
    {
        private const string CHECKBOX_COMPONENT_XPATH = "//label[text() = '{0}']/../..//input[@type = 'checkbox']";
        private IWebElement _btnSave => FindElementById("ctl00_Content_btnSave");

        public FeatureTogglesPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ConfigureFeatureToggles()
        {
            if (FeatureToggles == null || !FeatureToggles.Any())
                return;
            
            foreach (var featureToggle in FeatureToggles)
            {
                var checkboxElement = GetWebElementUsingSettingLabel(CHECKBOX_COMPONENT_XPATH, featureToggle.Feature.DisplayName());
                SetWebformCheckBoxState(checkboxElement, true);
            }

            ClickElement(_btnSave);
        }

        public void DisableAllFeatureToggles()
        {
            var featureToggleCheckboxes = FindElementsByXPath("//input[@type = 'checkbox']");

            foreach (var featureToggleCheckbox in featureToggleCheckboxes)
            {
                SetWebformCheckBoxState(featureToggleCheckbox, false);
            }
        }
    }
}