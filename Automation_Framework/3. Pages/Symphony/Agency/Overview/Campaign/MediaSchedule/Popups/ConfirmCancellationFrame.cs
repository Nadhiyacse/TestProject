using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class ConfirmCancellationFrame : BasePage
    {
        private IWebElement _btnConfirm => FindElementById("confirmCancel");
        public ConfirmCancellationFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickConfirm()
        {
            ClickElement(_btnConfirm);
        }
    }
}
