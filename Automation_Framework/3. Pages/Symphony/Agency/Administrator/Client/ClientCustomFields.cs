using System.Linq;
using Automation_Framework.DataModels.InfrastructureData.Administrator;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Client
{
    public class ClientCustomFields : BasePage
    {
        private IWebElement _btnSave => FindElementById("saveButton");
        private IWebElement _btnResetToDefault => FindElementById("restoreDefaultButton");
        private IWebElement _pnlMessage => FindElementByXPath("//div[@role='alert']");

        public ClientCustomFields(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SetClientCustomFields(ClientData clientData)
        {
            if (clientData.CustomFields != null && clientData.CustomFields.Any())
            {
                foreach (var customFieldData in clientData.CustomFields)
                {
                    var customFieldElement = FindElementByXPath($"//label[text() = '{customFieldData.Name}']");
                    SetCustomFieldElement(customFieldElement, customFieldData.Type, customFieldData.Values);
                }

                ClickElement(_btnSave);
                Assert.AreEqual("Custom Fields were successfully saved.", GetMsgText());
            }
        }

        public string GetMsgText()
        {
            return _pnlMessage.Text;
        }
    }
}
