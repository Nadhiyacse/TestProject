using Automation_Framework._3._Pages.Symphony.Common;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Common
{
    [Binding]
    public class UserAccountStep : BaseStep
    {
        private readonly UserAccountFrame _userAccountFrame;

        public UserAccountStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _userAccountFrame = new UserAccountFrame(driver, featureContext);
        }

        [Given(@"I set the language to (.*)")]
        public void SetLanguage(string language)
        {
            ScenarioContext.Current.Pending();
        }

        [Given(@"I upload an esignature")]
        public void UploadEsignature()
        {
            _userAccountFrame.SwitchToUserAccountFrame();
            _userAccountFrame.UploadEsignature(AgencySetupData.AgencyLoginUserData.EsignatureFileName);
            Assert.AreEqual("Account details successfully updated.", _userAccountFrame.GetMsgText());
            Assert.IsTrue(_userAccountFrame.DoesEsignatureExist(), "Esignature is missing");
            _userAccountFrame.CloseUserAccountFrame();
        }
    }
}
