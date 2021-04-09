using System;
using System.IO;
using Automation_Framework.DataModels.WorkflowTestData.InsertionOrder;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class IssueInsertionOrderFrame : BasePage
    {
        private IWebElement _ddlAgencyBuyer => FindElementById("ctl00_Content_ddlOrgSource");
        private IWebElement _ddlAgencyContact => FindElementById("ctl00_Content_selectedAgencyContacts");
        private IWebElement _ddlPublisherContact => FindElementById("ctl00_Content_rptIssueInsertionOrder_ctl01_selContact");
        private IWebElement _btnUpload => FindElementById("ctl00_Content_CurrentUserOrgAttachment_btnUpload");
        private IWebElement _fileSelect => FindElementById("ctl00_Content_CurrentUserOrgAttachment_fileUpload");
        private IWebElement _chkClientApproved => FindElementById("ctl00_Content_chkClientApproved");
        private IWebElement _btnSend => FindElementById("ctl00_ButtonBar_btnSend");
        private IWebElement _btnCancel => FindElementById("ctl00_ButtonBar_btnCancel");
        private IWebElement _lblPublisherContactSelection(string publisher)
        {
            return FindElementByXPath($"//li[contains(@class, 'selection__choice')][text()= '{publisher}']");
        }

        public IssueInsertionOrderFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        private void SelectAgencyBuyer(string buyer)
        {
            if (!string.IsNullOrEmpty(buyer))
            {
                SelectWebformDropdownValueByText(_ddlAgencyBuyer, buyer);
            }
        }

        private void SelectAgencyContact(string agencyContacts)
        {
            if (!string.IsNullOrEmpty(agencyContacts))
            {
                var agencyContactList = agencyContacts.Split(',');
                foreach (string agencyContact in agencyContactList)
                {
                    SelectWebformDropdownValueByText(_ddlAgencyContact, agencyContact);
                }
            }
        }

        private void SelectPublisherContact(string publisherContacts)
        {
            if (!string.IsNullOrEmpty(publisherContacts))
            {
                var publisherContactList = publisherContacts.Split(',');
                foreach (string publisherContact in publisherContactList)
                {
                    SelectWebformDropdownValueByText(_ddlPublisherContact, publisherContact);
                    Wait.Until(driver => _lblPublisherContactSelection(publisherContact).Displayed);
                }
            }
        }

        private void UploadAgencyDocuments(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                var path = AppDomain.CurrentDomain.BaseDirectory;
                var binpath = path.Substring(0, path.LastIndexOf("bin"));
                var filelocation = Path.Combine(binpath, $@"Documents\\{fileName}");
                _fileSelect.SendKeys(filelocation);
                ClickElement(_btnUpload);
            }
        }        

        private void ClientApproved(bool isClientApproved)
        {
            // This element is controlled by 'Client approval required before issuing IO' agency feature 
            if (!IsElementPresent(By.Id("ctl00_Content_chkClientApproved")))
                return;

            SetWebformCheckBoxState(_chkClientApproved, isClientApproved);
        }

        public void SendIOToPublisher(InsertionOrderIssueData issueData)
        {
            SelectAgencyBuyer(issueData.AgencyBuyer);
            SelectAgencyContact(issueData.AgencyContact);
            SelectPublisherContact(issueData.PublisherContact);
            UploadAgencyDocuments(issueData.UploadDocuments);
            ClientApproved(issueData.IsClientApproved);
            ClickElement(_btnSend);
            SwitchToMainWindow();
        }
    }
}