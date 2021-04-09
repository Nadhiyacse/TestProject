using System;
using Automation_Framework.Translations;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class ConfirmMediaSchedulePopUp : BasePage
    {
        private IWebElement _btnConfirm => FindElementById("ctl00_ButtonBar_btnConfirm");

        public ConfirmMediaSchedulePopUp(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void ClickConfirm()
        {
            ClickElement(_btnConfirm);

            var alert = driver.SwitchTo().Alert();
            if (!alert.Text.Contains(Strings.LineItemsSuccessfullyConfirmedMsg))
                throw new Exception("Media Schedule items were not confirmed properly");

            AcceptAlert();
            SwitchToMainWindow();
        }
    }
}