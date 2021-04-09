using System.Configuration;
using Automation_Framework._3._Pages.Adslot.Common;
using Automation_Framework._3._Pages.Symphony.Common;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Common
{
    [Binding]
    public class LoginStep : BaseStep
    {
        private readonly LoginPage _loginPage;
        private readonly AdslotLoginPage _adslotLoginPage;

        public LoginStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _loginPage = new LoginPage(driver, featureContext);
            _adslotLoginPage = new AdslotLoginPage(driver, featureContext);
        }

        [Given(@"I have logged in as an agency user")]
        [When(@"I log in as an agency user")]
        public void LoginAsAgencyUser()
        {
            if (WorkflowTestData != null)
            {
                _loginPage.LoginToApplication(WorkflowTestData.AgencyLoginUserData);
            }
            else
            {
                _loginPage.LoginToApplication(AgencySetupData.AgencyLoginUserData);
            }
        }

        [Given(@"I have logged in as a publisher user")]
        [When(@"I log in as a publisher user")]
        public void LoginAsPublisherUser()
        {
            _loginPage.LoginToApplication(WorkflowTestData.PublisherLoginUserData);
        }

        [Given(@"I have logged in as a symphony admin user")]
        public void LoginAsSymphonyAdminUser()
        {
            _loginPage.LoginToApplication(GenericSetupData.SymphonyAdminLoginUserData);
        }

        [When(@"I have logged in as an agency approver user")]
        public void LogInAsAnAgencyApproverUser()
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I have logged in to Adslot publisher")]
        [When(@"I log in to Adslot publisher")]
        public void LogInToAdslotPublisher()
        {
            _adslotLoginPage.LoginToAdslotPublisher(WorkflowTestData.PublisherLoginUserData);
        }
    }
}