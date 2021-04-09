using Automation_Framework._3._Pages.Symphony.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.Billing.Popups
{
    public class ExportBillingFrame : ExportFramePage
    {
        public ExportBillingFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }       

        public void DeliverBillingExport(string exportType, string deliveryMethod, string email)
        {
            if (IsElementPresent(By.Id("ctl00_Content_ddlExportProvider")))
            {
                SelectExportType(exportType);
            }

            if (IsElementPresent(By.Id("ctl00_Content_ddlDeliveryStrategy")))
            {
                SelectDeliveryMethod(deliveryMethod);

                if (deliveryMethod.ToLower().Equals("email"))
                {
                    var txtEmail = FindElementById("ctl00_Content_CurrentlyLoadedDeliveryStrategyControlId_txtEmail");
                    ClearInputAndTypeValueIfRequired(txtEmail, email);
                }
            }
            
            ScrollAndClickElement(_btnContinue);
        }
    }
}
