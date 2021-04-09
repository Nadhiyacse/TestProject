using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Campaigns
{
    public class MediaScheduleReviewSignoffPage : BasePage
    {
        private IWebElement _txtComment => FindElementByXPath("//textarea[@data-test-selector='bubble-comment-text-area']");
        private IWebElement _btnSignoff => FindElementByXPath("//button[@data-test-selector='campaign-section-confirm-and-send-confirm']");
        private IWebElement _txtPassword => FindElementByName("password");
        private IWebElement _chkTermsAndConditions => FindElementByName("termsCheckBox");
        private IWebElement _btnConfirm => FindElementByXPath("//button[@data-test-selector='media-schedule-sign-off-modal-confirm']");

        public MediaScheduleReviewSignoffPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void CommentAndSignOff(string comment, string password)
        {
            WaitForElementToBeVisible(By.CssSelector(".publisher-company-name"));            
            ClickElement(_txtComment);
            ClearInputAndTypeValue(_txtComment, comment);
            ClickElement(_btnSignoff);
            ClearInputAndTypeValue(_txtPassword, password);
            _chkTermsAndConditions.Click();
            ClickElement(_btnConfirm);
        }
    }
}
