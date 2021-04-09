using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Publishers;
using Automation_Framework._3._Pages.Symphony.Agency.Administrator.Publishers.Popups;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Administrator
{
    [Binding]
    public class PublisherStep : BaseStep
    {
        private readonly RatecardPage _ratecardPage;
        private readonly ImportRatecardFrame _importRatecardFrame;

        public PublisherStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _ratecardPage = new RatecardPage(driver, featureContext);
            _importRatecardFrame = new ImportRatecardFrame(driver, featureContext);
        }

        [Given(@"I import my publishers default ratecard")]
        public void ImportPublishersDefaultRatecard()
        {
            if (AgencySetupData.AgencyAdministratorData.PublishersData == null)
                throw new NullReferenceException("Publishers data is not available in test data");

            if (AgencySetupData.AgencyAdministratorData.PublishersData.RatecardData == null || !AgencySetupData.AgencyAdministratorData.PublishersData.RatecardData.Any())
                throw new NullReferenceException("Ratecard data is not available in test data");

            NavigationPage.NavigateTo("Administrator Publishers Ratecard");

            foreach (var ratecard in AgencySetupData.AgencyAdministratorData.PublishersData.RatecardData)
            {
                if (_ratecardPage.DoesPublisherExist(ratecard))
                {
                    _ratecardPage.ClickEditForPublisher(ratecard.Publisher);
                    _importRatecardFrame.UploadRatecardFile(ratecard.ImportRatecardFileName);
                }
            }
        }

        [When(@"I download my publishers ratecard current file")]
        public void DownloadPublisherDefaultRatecardCurrentFile()
        {
            if (AgencySetupData.AgencyAdministratorData.PublishersData == null)
                throw new NotFoundException("AgencyAdministratorDat.PublishersData is missing in test data file");

            var ratecardData = AgencySetupData.AgencyAdministratorData.PublishersData.RatecardData;

            if (ratecardData == null || !ratecardData.Any())
                throw new NotFoundException("Publishers Ratecard data is missing in test data file");

            NavigationPage.NavigateTo("Administrator Publishers Ratecard");

            var ratecard = ratecardData.First();
            if (_ratecardPage.DoesPublisherExist(ratecard))
            {
                _ratecardPage.ClickEditForPublisher(ratecard.Publisher);
                _importRatecardFrame.DownloadCurrentFile();
            }
        }

        [Then(@"the download of the ratecard current file should be successful")]
        public void DownloadOfTheRatecardCurrentFileShouldBeSuccessful()
        {
            _importRatecardFrame.WaitUntilMessageDisplayed("All Done! Rate default has been exported.");
        }
    }
}
