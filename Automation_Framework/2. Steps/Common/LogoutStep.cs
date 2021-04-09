using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Common
{
    [Binding]
    public class LogoutStep : BaseStep
    {
        public LogoutStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        [Given(@"I have logged out the current user")]
        [When(@"I have logged out the current user")]
        public void LogOutTheCurrentUser()
        {
            NavigationPage.LogoutCurrentUser();
        }

        [When(@"I log out from Adslot publisher")]
        public void LogOutFromAdslotPublisher()
        {
            AdslotNavigationPage.LogoutToAdslotPublisher();
        }
    }
}
