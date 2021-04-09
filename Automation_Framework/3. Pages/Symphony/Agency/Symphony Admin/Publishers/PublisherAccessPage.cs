using System.Collections.Generic;
using System.Linq;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Symphony_Admin.Publishers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Publishers
{
    public class PublisherAccessPage : BasePage
    {
        private IWebElement _lblMessagePanel => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");

        private const string CHECKBOX_COMPONENT_XPATH = "//h2[contains(text(), '{0}')]/..//table//tbody//tr//td//label[text() = '{1}']/../..//input[@type = 'checkbox']";
        private const string RADIO_COMPONENT_XPATH = "//h2[contains(text(), '{0}')]/..//table//tbody//tr//td//label[text() = '{1}']/../..//input[@type = 'radio']";

        public PublisherAccessPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }

        public void ConfigurePublisherAccessControl(PublisherAccessData publisherAccessData)
        {
            if (publisherAccessData == null)
                return;

            ConfigureAccessControlledItems(publisherAccessData.MediaScheduleExports, "Media schedule exports");
            ConfigureAccessControlledItems(publisherAccessData.InsertionOrderExports, "Insertion Order exports");
            ConfigureAccessControlledItems(publisherAccessData.Languages, "Languages");
            
            ClickElement(_btnSave);
        }

        private void ConfigureAccessControlledItems(IEnumerable<AccessControlledItem> accessControlledItems, string sectionHeaderName)
        {
            if (accessControlledItems == null || !accessControlledItems.Any())
                return;

            foreach (var accessControlledItem in accessControlledItems)
            {
                var accessControlItemName = accessControlledItem.AccessItem.Name;
                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, accessControlItemName, CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, accessControlledItem.AccessItem.Enabled);

                if (accessControlledItem.IsDefault)
                {
                    var radioButtonElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(sectionHeaderName, accessControlItemName, RADIO_COMPONENT_XPATH);
                    ScrollAndClickElement(radioButtonElement);
                }
            }
        }

        private IWebElement GetWebElementUsingDynamicXpathByHeaderAndLabel(string sectionHeaderName, string labelName, string dynamicXPath)
        {
            var dynamicXpath = string.Format(dynamicXPath, sectionHeaderName, labelName);
            var checkboxElement = FindElementByXPath(dynamicXpath);

            return checkboxElement;
        }
    }
}