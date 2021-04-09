using System;
using System.IO;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._3._Pages.Symphony.Agency.Overview.Campaign.InsertionOrder.Popups
{
    public class SendForApprovalFrame : BasePage
    {
        private IWebElement _ddlAgencyBuyer => FindElementById("ctl00_Content_ddlOrgSource");
        private IWebElement _ddlApprover => FindElementById("ctl00_Content_ddlIOApprover");
        private IWebElement _ddlAgencyContact => FindElementById("ctl00_Content_selectedAgencyContacts");
        private IWebElement _ddlPublisherContact => FindElementById("ctl00_Content_rptPublishers_ctl01_selContact");
        private IWebElement _ddlLanguage => FindElementById("ctl00_Content_ddlLanguage");
        private IWebElement _btnUpload => FindElementById("ctl00_Content_CurrentUserOrgAttachment_btnUpload");
        private IWebElement _fileSelect => FindElementById("ctl00_Content_CurrentUserOrgAttachment_fileUpload");
        private IWebElement _btnSubmit => FindElementById("ctl00_ButtonBar_btnSend");
        private IWebElement _btnCancel => FindElementById("ctl00_ButtonBar_btnCancel");
        private IWebElement _lblPublisherContactSelection(string publisher)
        {
            return FindElementByXPath($"//li[contains(@class, 'selection__choice')][text()= '{publisher}']");
        }

        public SendForApprovalFrame(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
        }

        public void SelectAgencyBuyer(string buyer)
        {
            if (!string.IsNullOrEmpty(buyer))
            {
                SelectWebformDropdownValueByText(_ddlAgencyBuyer, buyer);
            }
        }

        public void SelectApprover(string approver)
        {
            if (!string.IsNullOrEmpty(approver))
            {
                SelectWebformDropdownValueByText(_ddlApprover, approver);
            }
        }

        public void SelectAgencyContact(string agencyContacts)
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

        public void SelectPublisherContact(string publisherContacts)
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

        public void ClickSubmitButton()
        {
            ClickElement(_btnSubmit);
            SwitchToDefaultContent();
        }

        public void UploadAgencyDocuments(string fileName)
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

        public void SelectLanguage(string language)
        {
            if (!string.IsNullOrEmpty(language))
            {
                SelectWebformDropdownValueByText(_ddlLanguage, language);
            }
        }
        
        //public AgencyManageInsertionOrderPage SubmitForApproval(UserData user)
        //{
        //    SelectAgencyBuyer(user.Approval_AgencyBuyer);
        //    SelectApprover(user.Approval_Appover);
        //    SelectAgencyContact(user.Approval_AgencyContact);
        //    SelectPublisherContact(user.Approval_PublisherContact);
        //    UploadAgencyDocuments(user.Approval_UploadDocuments);
        //    SelectLanguage(user.Approval_Language);
        //    ClickElement(_btnSubmit);
        //    return new AgencyManageInsertionOrderPage(driver);
        //}
    }
}
