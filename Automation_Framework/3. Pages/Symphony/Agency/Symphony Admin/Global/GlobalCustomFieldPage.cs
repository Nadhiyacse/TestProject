using Automation_Framework._3._Pages.Symphony.Common.Enums;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Global
{
    public class GlobalCustomFieldPage : BasePage
    {
        private IWebElement _btnAddCustomField => FindElementByXPath("//button//div[contains(text(), 'Add Custom Field')]");
        private const string DYNAMIC_CUSTOM_FIELD_XPATH = "//div[@class='custom-field-template-list']//div[@class='grid-component-cell grid-component-cell-2 grid-component-cell-body grid-component-cell-site-cell'][contains(text(), '{0}')]//..//div[@class='grid-component-cell grid-component-cell-6-5 grid-component-cell-body grid-component-cell-site-cell'][contains(text(), '{1}')]";

        public GlobalCustomFieldPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickAddCustomField()
        {
            ClickElement(_btnAddCustomField);
        }

        public bool DoesCustomFieldExist(CustomFieldApplyTo applyTo, string customFieldName)
        {
            var dynamicCustomFieldXpath = string.Format(DYNAMIC_CUSTOM_FIELD_XPATH, applyTo.ToString(), customFieldName);
            return IsElementPresent(By.XPath(dynamicCustomFieldXpath));
        }
    }
}