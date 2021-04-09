using System;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Adslot.Campaigns
{
    public class InboxPage : BasePage
    {
        private const string XPATH_INBOX_MESSAGE = @"//div[contains(@class, 'conversation-content') and contains(string(), ""{0}"")]";

        public InboxPage(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        private IWebElement GetInboxContent(string message)
        {
            return FindElementByXPath(message);
        }

        public void DoesInboxMessageExist(string message)
        {
            var inboxContent = string.Format(XPATH_INBOX_MESSAGE, message);

            try
            {
                Wait.Until(d => GetInboxContent(inboxContent));
            }
            catch (Exception ex)
            {
                Assert.Fail($"The inbox message '{message}' does not exist in Adslot Publisher Inbox.\nReason:{ex.Message}\nStacktrace{ex.StackTrace}");
            }
        }

        public void ClickLinkInMessage(string message)
        {
            var inboxMessageLink = string.Format(XPATH_INBOX_MESSAGE, message) + @"//a";

            try
            {
                var link = GetInboxContent(inboxMessageLink);
                ClickElement(link);
            }
            catch (Exception ex)
            {
                Assert.Fail($"Could not click the link in the inbox message '{message}'.\nReason:{ex.Message}\nStacktrace{ex.StackTrace}");
            }

            SwitchToNewWindow(string.Empty);
        }
    }
}
