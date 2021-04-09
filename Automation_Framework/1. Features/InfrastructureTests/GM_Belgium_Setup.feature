Feature: GM_Belgium_Setup

@ConfigureUserAndAgency @GenericDataSetup
Scenario: Create agency in symphony admin
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Agencies page
    And I configure my agencies
    And I configure my agencies access control
    And I configure my agencies feature control
    And I configure my agencies users
    And I configure my agencies custom labels
    And I configure my agencies custom fields
    And I configure my agencies classifications

@ConfigureAdministrator @AgencyAdminDataSetup
Scenario: 01 Setup agency administration
    Given I have logged in as an agency user
    And I navigate to the Administrator page
    And I configure my agency default cost adjustments
    And I navigate to the Administrator Clients page
    And I configure my clients
    And I navigate to the Administrator Access page
    And I configure my agencies access control as Administrator

@DataMapping @AgencyAdminDataSetup
Scenario: 02 Setup Integration Data Mapping
    Given I have logged in as an agency user
    And I navigate to the Integration page
    And I navigate to the Data Mapping page
    And I configure my external applications data mapping

@ExtAccounts @AgencyAdminDataSetup
Scenario: 03 Setup Integration Ext Accounts
    Given I have logged in as an agency user
    And I navigate to the Integration page
    And I navigate to the Ext Accounts page
    And I configure my agencies external credentials