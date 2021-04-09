using System;
using Automation_Framework._3._Pages.Symphony.Common;
using Automation_Framework.Hooks;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Common
{
    [Binding]
    public class NavigationStep : BaseStep
    {
        private readonly NavigationPage _navigationPage;

        public NavigationStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _navigationPage = new NavigationPage(driver, featureContext);
        }

        [Given(@"I navigate to the (.*) page")]
        [When(@"I navigate to the (.*) page")]
        public void NavigateTo(string pageToNavigateTo)
        {
            _navigationPage.NavigateTo(pageToNavigateTo);
        }

        [Given(@"I click the help link")]
        public void GivenIClickTheHelpLink()
        {
            _navigationPage.ClickHelpLink();
        }

        [Given(@"I open the User Profile Settings and Privacy Page")]
        public void GivenIOpenTheUserProfileSettingsAndPrivacyPage()
        {
            _navigationPage.UserProfileActions("Settings and Privacy");
        }

        [Given(@"I open the User Profile Resource Centre Page")]
        public void GivenIOpenTheUserProfileResourceCentrePage()
        {
            _navigationPage.UserProfileActions("Resource Centre");
        }

        [Given(@"I open the User Profile Colour Code Page")]
        public void GivenIOpenTheUserProfileColourCodePage()
        {
            _navigationPage.UserProfileActions("Colour Code");
        }

        [Then(@"The page should be loaded within (\d+) seconds")]
        public void PageIsLoadedWithinSeconds(int seconds)
        {
            var actualTime = (TimeSpan)FeatureContext[ContextStrings.ElapsedTime];
            var expectedTime = TimeSpan.FromSeconds(seconds);
            Assert.LessOrEqual(actualTime, expectedTime);
            Console.WriteLine($"Actual load time: {actualTime}");
        }
    }
}