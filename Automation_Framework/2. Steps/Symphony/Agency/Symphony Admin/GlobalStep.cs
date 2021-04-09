using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Global;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Global.Popups;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Symphony_Admin
{
    [Binding]
    public class GlobalStep : BaseStep
    {
        private readonly GlobalCustomFieldPage _globalCustomFieldPage;
        private readonly AddCustomFieldModal _addCustomFieldModal;

        public GlobalStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _globalCustomFieldPage = new GlobalCustomFieldPage(driver, featureContext);
            _addCustomFieldModal = new AddCustomFieldModal(driver, featureContext);
        }

        [Given(@"I configure my global custom fields")]
        public void ConfigureFeatureToggles()
        {
            if (GenericSetupData.SymphonyAdminData.GlobalCustomFields == null)
                return;

            foreach (var globalCustomField in GenericSetupData.SymphonyAdminData.GlobalCustomFields)
            {
                if (_globalCustomFieldPage.DoesCustomFieldExist(globalCustomField.ApplyTo, globalCustomField.Name))
                {
                    //TODO - Delete and re-create - Delete currently doesn't work :'( - To be fixed by GM-22821
                }
                else
                {
                    _globalCustomFieldPage.ClickAddCustomField();
                    _addCustomFieldModal.CreateCustomField(globalCustomField);
                    Assert.IsTrue(_globalCustomFieldPage.DoesCustomFieldExist(globalCustomField.ApplyTo, globalCustomField.Name));
                }
            }
        }
    }
}