using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule
{
    public class ManageColumnsModal : BasePage
    {
        private IWebElement _btnSave => FindElementByCssSelector(".modal-dialog .btn-primary div");

        public ManageColumnsModal(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ShowColumns(IEnumerable<string> columnNames)
        {
            Wait.Until(driver => FindElementByCssSelector(".modal-content").Displayed);

            foreach (var columnName in columnNames)
            {
                var checkboxLabel = FindElementsByXPath($"//div[@class='modal-content']//div[not(contains(@class, 'checked')) and contains(@class, 'checkbox-component')]//div[contains(text(), '{columnName}')]").FirstOrDefault();

                if (checkboxLabel != null)
                {
                    var checkbox = checkboxLabel.FindElement(By.XPath("../.."));

                    Wait.Until(driver => { SetReactCheckBoxState(checkboxLabel, true); return checkbox.GetAttribute("class").Contains("checked"); });
                }
            }

            _btnSave.Click();
        }

        public void HideColumns(IEnumerable<string> columnNames)
        {
            Wait.Until(driver => FindElementByCssSelector(".modal-content").Displayed);

            foreach (var columnName in columnNames)
            {
                var checkboxLabel = FindElementsByXPath($"//div[@class='modal-content']//div[contains(@class, 'checked') and contains(@class, 'checkbox-component')]//div[contains(text(), '{columnName}')]").FirstOrDefault();
             

                if (checkboxLabel != null)
                {
                    var checkbox = checkboxLabel.FindElement(By.XPath("../.."));

                    Wait.Until(driver => { SetReactCheckBoxState(checkboxLabel, false); return !checkbox.GetAttribute("class").Contains("checked"); });
                }
            }

            _btnSave.Click();
        }
    }
}
