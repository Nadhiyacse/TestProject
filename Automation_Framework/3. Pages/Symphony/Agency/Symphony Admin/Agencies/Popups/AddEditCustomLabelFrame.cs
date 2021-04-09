using System.Linq;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Agencies;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies.Popups
{
    public class AddEditCustomLabelFrame : BasePage
    {
        private IWebElement _ddlLanguage => FindElementById("ctl00_Content_ddlLanguages");
        private IWebElement _btnSave => FindElementById("ctl00_ButtonBar_btnSave");
        private const string DYNAMIC_LABEL_XPATH = "//div[@class='form-group in-line']//span[contains(text(), '{0}')]//..//input";

        public AddEditCustomLabelFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CreateOrUpdateCustomLabel(CustomLabelData customLabels)
        {
            if (customLabels?.CustomLabelOverrides == null || !customLabels.CustomLabelOverrides.Any())
                return;

            SelectSingleValueFromReactDropdownByText(_ddlLanguage, customLabels.Language);

            foreach (var customLabelsCustomLabelOverride in customLabels.CustomLabelOverrides)
            {
                var labelInputElement = FindElementByXPath(string.Format(DYNAMIC_LABEL_XPATH, customLabelsCustomLabelOverride.LabelToOverride));

                ClearInputAndTypeValueIfRequired(labelInputElement, customLabelsCustomLabelOverride.OverrideValue);
            }

            ClickElement(_btnSave);
        }
    }
}