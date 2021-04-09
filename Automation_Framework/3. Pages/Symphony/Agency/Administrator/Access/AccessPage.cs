using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using Automation_Framework.DataModels.CommonData;
using Automation_Framework.DataModels.InfrastructureData.Administrator;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Administrator.Access
{
    public class AccessPage : BasePage
    {
        private IWebElement _lnkMediaScheduleExports => FindElementById("liMEDIASCHEDULE_EXPORT");
        private IWebElement _lnkBillingExports => FindElementById("liBILLING_EXPORT");
        private IWebElement _lnkInsertionOrderExports => FindElementById("liIO_AGENCY_EXPORT");
        private IWebElement _lnkTraffickingExport => FindElementById("liTRAFFIC_EXPORT");
        private IWebElement _lnkPublisherExport => FindElementById("liPUBLISHER_EXPORT");
        private IWebElement _lnkDataMapping => FindElementById("liDATA_MAPPING");
        private IWebElement _lnkThirdPartyAsServers => FindElementById("liADSERVER");
        private IWebElement _lnkFourthPartyTracking => FindElementById("liFOURTHPARTYTRACKING");
        private IWebElement _lnkLanguage => FindElementById("liLANGUAGE");
        private IWebElement _lnkMediaScheduleView => FindElementById("liMEDIASCHEDULE_VIEW");
        private IWebElement _lnkPurchaseType => FindElementById("liPURCHASE_TYPE");
        private IWebElement _lnkBillingAllocation => FindElementById("liBILLING_ALLOCATION");
        private IWebElement _btnSave => FindElementById("ctl00_ctl00_Content_Content_btnSave");
        private IWebElement _lblMessagePanel => FindElementById("ctl00_ctl00_Content_Content_pnlMessage");

        private const string CHECKBOX_COMPONENT_XPATH = "//table//tbody//tr//td//label[text() = '{0}']/../..//input[@type = 'checkbox']";
        private const string RADIO_COMPONENT_XPATH = "//table//tbody//tr//td//label[text() = '{0}']/../..//input[@type = 'radio']";
        private const string SUCCESS_MESSAGE = "Access control successfully set";

        public AccessPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ConfigureAccess(AccessData accessData)
        {
            if (accessData == null)
                throw new ArgumentNullException("Access data is empty");

            PropertyInfo[] properties = typeof(AccessData).GetProperties();
            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case nameof(AccessData.MediaScheduleExports):
                        WaitForPageLoadCompleteAfterClickElement(_lnkMediaScheduleExports);
                        ConfigureAdministrationAccessAndSave(accessData.MediaScheduleExports, nameof(AccessData.MediaScheduleExports));
                        break;
                    case nameof(AccessData.InsertionOrderExports):
                        WaitForPageLoadCompleteAfterClickElement(_lnkInsertionOrderExports);
                        ConfigureAdministrationAccessAndSave(accessData.InsertionOrderExports, nameof(AccessData.InsertionOrderExports));
                        break;
                    case nameof(AccessData.BillingExports):
                        WaitForPageLoadCompleteAfterClickElement(_lnkBillingExports);
                        ConfigureAdministrationAccessAndSave(accessData.BillingExports, nameof(AccessData.BillingExports));
                        break;
                    case nameof(AccessData.TraffickingExports):
                        WaitForPageLoadCompleteAfterClickElement(_lnkTraffickingExport);
                        ConfigureAdministrationAccessAndSave(accessData.TraffickingExports, nameof(AccessData.TraffickingExports));
                        break;
                    case nameof(AccessData.PublisherExports):
                        WaitForPageLoadCompleteAfterClickElement(_lnkPublisherExport);
                        ConfigureAdministrationAccessAndSave(accessData.PublisherExports, nameof(AccessData.PublisherExports));
                        break;
                    case nameof(AccessData.DataMapping):
                        WaitForPageLoadCompleteAfterClickElement(_lnkDataMapping);
                        ConfigureAdministrationAccessAndSave(accessData.DataMapping, nameof(AccessData.DataMapping));
                        break;
                    case nameof(AccessData.ThirdPartyAdServers):
                        WaitForPageLoadCompleteAfterClickElement(_lnkThirdPartyAsServers);
                        ConfigureAdministrationAccessAndSave(accessData.ThirdPartyAdServers, nameof(AccessData.ThirdPartyAdServers));
                        break;
                    case nameof(AccessData.FourthPartyTracking):
                        WaitForPageLoadCompleteAfterClickElement(_lnkFourthPartyTracking);
                        ConfigureAdministrationAccessAndSave(accessData.FourthPartyTracking, nameof(AccessData.FourthPartyTracking));
                        break;
                    case nameof(AccessData.Languages):
                        WaitForPageLoadCompleteAfterClickElement(_lnkLanguage);
                        ConfigureAdministrationAccessAndSave(accessData.Languages, nameof(AccessData.Languages));
                        break;
                    case nameof(AccessData.MediaScheduleViews):
                        WaitForPageLoadCompleteAfterClickElement(_lnkMediaScheduleView);
                        ConfigureAdministrationAccessAndSave(accessData.MediaScheduleViews, nameof(AccessData.MediaScheduleViews));
                        break;
                    case nameof(AccessData.PurchaseTypes):
                        WaitForPageLoadCompleteAfterClickElement(_lnkPurchaseType);
                        ConfigureAdministrationAccessAndSave(accessData.PurchaseTypes, nameof(AccessData.PurchaseTypes));
                        break;
                    case nameof(AccessData.BillingAllocations):
                        WaitForPageLoadCompleteAfterClickElement(_lnkBillingAllocation);
                        ConfigureAdministrationAccessAndSave(accessData.BillingAllocations, nameof(AccessData.BillingAllocations));
                        break;
                }
            }
        }

        private void ConfigureAdministrationAccessAndSave(IEnumerable<AccessControlledItem> accessControlledItems, string accessControlName)
        {
            if (accessControlledItems == null || !accessControlledItems.Any())
                return;

            foreach (var accessControlledItem in accessControlledItems)
            {
                var accessItemName = accessControlledItem.AccessItem.Name;
                if (accessItemName.Equals("GroupM (BE) Technical Specifications Export") && !IsFeatureToggleEnabled(FeatureToggle.BEProdScheduleChanges))
                    continue;

                var checkboxElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(accessItemName, CHECKBOX_COMPONENT_XPATH);
                SetWebformCheckBoxState(checkboxElement, accessControlledItem.AccessItem.Enabled);

                if (accessControlledItem.IsDefault)
                {
                    var radioButtonElement = GetWebElementUsingDynamicXpathByHeaderAndLabel(accessItemName, RADIO_COMPONENT_XPATH);
                    ScrollAndClickElement(radioButtonElement);
                }
            }

            WaitForPageLoadCompleteAfterClickElement(_btnSave);
            Assert.AreEqual(SUCCESS_MESSAGE, GetPanelMessage(), $"The access control {accessControlName} was not set successfully");
        }

        private IWebElement GetWebElementUsingDynamicXpathByHeaderAndLabel(string labelName, string dynamicXPath)
        {
            var dynamicXpath = string.Format(dynamicXPath, labelName);
            var checkboxElement = FindElementByXPath(dynamicXpath);

            return checkboxElement;
        }

        public string GetPanelMessage()
        {
            Wait.Until(driver => _lblMessagePanel.Displayed);
            return _lblMessagePanel.Text;
        }
    }
}
