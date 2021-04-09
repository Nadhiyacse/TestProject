using Automation_Framework._3._Pages.Adslot.Common;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Adslot.Common
{
    [Binding]
    public class AdslotNavigationStep : BaseStep
    {
        public AdslotNavigationStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        [Given(@"I navigate to the (.*) page in Adslot publisher")]
        [When(@"I navigate to the (.*) page in Adslot publisher")]
        public void NavigateTo(string pageToNavigateTo)
        {
            AdslotNavigationPage.NavigateTo(pageToNavigateTo);
        }
    }
}