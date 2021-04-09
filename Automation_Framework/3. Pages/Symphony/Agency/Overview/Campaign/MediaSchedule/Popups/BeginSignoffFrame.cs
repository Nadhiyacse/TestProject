using System;
using System.Configuration;
using Automation_Framework.DataModels.CommonData;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.MediaSchedule.Popups
{
    public class BeginSignoffFrame : BasePage
    {
        private IWebElement _txtPassword => FindElementById("password");
        private IWebElement _chkTermConditionAccepted => FindElementById("TermConditionAccepted");
        private IWebElement _btnSignoff => FindElementById("beginSignoff");
        private IWebElement _progressBar => FindElementById("progress");
        private IWebElement _lblErrorMessage => FindElementById("alert");

        public BeginSignoffFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public bool SignoffAGItems()
        {
            ClickElement(_chkTermConditionAccepted);
            ClickElement(_btnSignoff);
            if (!IsSignoffSuccessful())
            {
                return false;
            }
            SwitchToDefaultContent();
            return true;
        }

        private bool IsSignoffSuccessful()
        {
            try
            {
                var temp = driver.Manage().Timeouts().ImplicitWait;
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(3);
                Wait.Until(d => (_progressBar == null || _lblErrorMessage.Text.Contains("Error")));
                driver.Manage().Timeouts().ImplicitWait = temp;
                return (_progressBar == null);
            }
            catch
            {
                return true;
            }
        }
    }
}