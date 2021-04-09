using System;
using System.Linq;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies;
using Automation_Framework._3._Pages.Symphony.Agency.Symphony_Admin.Agencies.Popups;
using Automation_Framework._3._Pages.Symphony.Common.Enums;
using NUnit.Framework;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Automation_Framework._2._Steps.Symphony.Agency.Symphony_Admin
{
    [Binding]
    public class AgencyStep : BaseStep
    {
        private readonly AgencyListPage _agencyListPage;
        private readonly AgencyCreatePage _agencyCreatePage;
        private readonly AgencyEditPage _agencyEditPage;
        private readonly AgencyAccessPage _agencyAccessPage;
        private readonly AgencyUserPage _agencyUserPage;
        private readonly AgencyFeaturesPage _agencyFeaturesPage;
        private readonly CreateUpdateAgencyUserFrame _createUpdateAgencyUserFrame;
        private readonly AgencyCustomLabelPage _agencyCustomLabelPage;
        private readonly AddEditCustomLabelFrame _addEditCustomLabelFrame;
        private readonly AgencyCustomFieldPage _agencyCustomFieldPage;
        private readonly AgencyClassificationsPage _agencyClassificationsPage;
        private readonly ManageActionsRequiringReapprovalFrame _manageActionsRequiringReapprovalFrame;

        public AgencyStep(IWebDriver driver, FeatureContext featureContext) : base(driver, featureContext)
        {
            _agencyListPage = new AgencyListPage(driver, featureContext);
            _agencyCreatePage = new AgencyCreatePage(driver, featureContext);
            _agencyEditPage = new AgencyEditPage(driver, featureContext);
            _agencyAccessPage = new AgencyAccessPage(driver, featureContext);
            _agencyUserPage = new AgencyUserPage(driver, featureContext);
            _agencyFeaturesPage = new AgencyFeaturesPage(driver, featureContext);
            _createUpdateAgencyUserFrame = new CreateUpdateAgencyUserFrame(driver, featureContext);
            _agencyCustomLabelPage = new AgencyCustomLabelPage(driver, featureContext);
            _addEditCustomLabelFrame = new AddEditCustomLabelFrame(driver, featureContext);
            _agencyCustomFieldPage = new AgencyCustomFieldPage(driver, featureContext);
            _agencyClassificationsPage = new AgencyClassificationsPage(driver, featureContext);
            _manageActionsRequiringReapprovalFrame = new ManageActionsRequiringReapprovalFrame(driver, featureContext);
        }

        [Given(@"I configure my agencies")]
        public void ConfigureAgencies()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                var agencyName = agency.Name;
                var agencyAction = "created"; 

                if (_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                {
                    _agencyListPage.ClickAgencyName(agencyName);
                    _agencyEditPage.EditAgency(agency);
                    agencyAction = "updated";
                }
                else
                {
                    _agencyListPage.ClickCreate();
                    _agencyCreatePage.CreateAgency(agency);
                }

                Assert.AreEqual($"{agencyName} agency was successfully {agencyAction}.", _agencyAccessPage.GetPanelMessage(), $"The agency was not {agencyAction}.");
                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }

        [Given(@"I configure my agencies access control")]
        public void ConfigureAgencyAccessControl()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                var agencyName = agency.Name;

                if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                    throw new Exception("The following agency does not exist: " + agencyName);

                _agencyListPage.ClickAgencyName(agencyName);
                NavigationPage.NavigateTo("Symphony Admin Agencies Access");
                _agencyAccessPage.ConfigureAgencyAccessControl(agency.Access);
                Assert.AreEqual($"Access control successfully set.", _agencyAccessPage.GetPanelMessage(), "There was an error setting access control");
                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }

        [Given(@"I configure my agencies users")]
        public void CreateAgencyUsers()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                if (agency.Users != null && agency.Users.Any())
                {
                    var agencyName = agency.Name;
                    
                    if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                        throw new Exception("The following agency does not exist: " + agencyName);

                    _agencyListPage.ClickAgencyName(agencyName);
                    NavigationPage.NavigateTo("Symphony Admin Agencies Users");
                        
                    var isEnableAdminSplit = _agencyFeaturesPage.IsAgencyFeatureEnabledInList(agency.Features.AdministratorRoles, "Enable Administrator Split");

                    foreach (var user in agency.Users)
                    {
                        if (_agencyUserPage.IsUserEmailExist(user.Email))
                        {
                            _agencyUserPage.ClickEdit(user.Email);
                            _createUpdateAgencyUserFrame.EditAgencyUser(user, isEnableAdminSplit);
                            Assert.AreEqual($"User: {user.Email} was successfully updated.", _createUpdateAgencyUserFrame.GetPanelMessage());
                        }
                        else
                        {
                            _agencyUserPage.ClickCreate();
                            _createUpdateAgencyUserFrame.CreateAgencyUser(user, isEnableAdminSplit);
                            Assert.AreEqual($"An account has been created for {user.Email}", _createUpdateAgencyUserFrame.GetPanelMessage());
                        }

                        _createUpdateAgencyUserFrame.ClickClose();
                    }

                    NavigationPage.NavigateTo("Symphony Admin Agencies");
                }
            }
        }

        [Given(@"I configure my agencies feature control")]
        public void ConfigureAgencyFeatures()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                var agencyName = agency.Name;

                if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                    throw new Exception("The following agency does not exist: " + agencyName);

                _agencyListPage.ClickAgencyName(agencyName);
                NavigationPage.NavigateTo("Symphony Admin Agencies Features");
                _agencyFeaturesPage.ConfigureAgencyFeatures(agency.Features);

                if (agency.Features.MediaScheduleApproval != null)
                {
                    var actionsNotRequiringReapproval = agency.Features.MediaScheduleApproval.ActionsNotRequiringReapproval;

                    if (actionsNotRequiringReapproval != null && actionsNotRequiringReapproval.Any())
                    {
                        _agencyFeaturesPage.NavigateToActionsNotRequiringReapprovalFrame();
                        _manageActionsRequiringReapprovalFrame.ConfigureActionsNotRequiringReapproval(actionsNotRequiringReapproval);
                    }
                }

                _agencyFeaturesPage.ClickSaveButton();
                Assert.AreEqual("Feature control successfully set.", _agencyAccessPage.GetPanelMessage(), "There was an error setting agency features");
                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }

        [Given(@"I configure my agencies custom labels")]
        public void ConfigureAgencyCustomLabels()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                if (agency.CustomLabels == null)
                {
                    continue;
                }

                var agencyName = agency.Name;

                if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                    throw new Exception("The following agency does not exist: " + agencyName);

                _agencyListPage.ClickAgencyName(agencyName);
                NavigationPage.NavigateTo("Symphony Admin Agencies Custom Labels");
                
                foreach (var agencyCustomLabel in agency.CustomLabels)
                {
                    if (_agencyCustomLabelPage.DoesCustomLabelOverrideExist(agencyCustomLabel.Language))
                    {
                        _agencyCustomLabelPage.ClickEdit(agencyCustomLabel.Language);
                    }
                    else
                    {
                        _agencyCustomLabelPage.ClickCreate();
                    }
                    _addEditCustomLabelFrame.CreateOrUpdateCustomLabel(agencyCustomLabel);
                }

                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }

        [Given(@"I configure my agencies custom fields")]
        public void ConfigureAgencyCustomFields()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                if (agency.CustomFields == null)
                {
                    continue;
                }

                var agencyName = agency.Name;

                if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                    throw new Exception("The following agency does not exist: " + agencyName);

                _agencyListPage.ClickAgencyName(agencyName);
                NavigationPage.NavigateTo("Symphony Admin Agencies Custom Fields");

                foreach (var agencyCustomField in agency.CustomFields)
                {
                    if (_agencyCustomFieldPage.DoesCustomFieldExist(agencyCustomField))
                    {
                        _agencyCustomFieldPage.EditCustomField(agencyCustomField);
                    }
                    else
                    {
                        _agencyCustomFieldPage.AddCustomField(agencyCustomField);
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }

        [Given(@"I configure my agencies classifications")]
        public void ConfigureAgencyClassifications()
        {
            foreach (var agency in AgencySetupData.SymphonyAdminData.Agencies)
            {
                if (agency.Classifications == null)
                {
                    continue;
                }

                var agencyName = agency.Name;

                if (!_agencyListPage.IsAgencyExistInCountry(agency.Country, agencyName))
                    throw new Exception("The following agency does not exist: " + agencyName);

                _agencyListPage.ClickAgencyName(agencyName);
                NavigationPage.NavigateTo("Symphony Admin Agencies Classifications");

                foreach (var agencyClassification in agency.Classifications)
                {
                    if (!_agencyClassificationsPage.IsTechnicalNameExist(agencyClassification))
                    {
                        _agencyClassificationsPage.AddClassification(agencyClassification);
                    }
                }

                NavigationPage.NavigateTo("Symphony Admin Agencies");
            }
        }
    }
}