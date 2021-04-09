Feature: Smoke_Test_Setup

Scenario: SMK01 Create agency in symphony admin
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Agencies page
    And I configure my agencies
    And I configure my agencies access control
    And I configure my agencies users
    And I configure my agencies feature control
    And I configure my agencies custom labels
    And I configure my agencies custom fields
    And I configure my agencies classifications

Scenario: SMK02 Setup agency administration, data mapping, and ext accounts
    Given I have logged in as an agency user
    And I navigate to the Administrator page
    And I navigate to the Administrator Clients page
    And I configure my clients
    And I navigate to the Administrator Access page
    And I configure my agencies access control as Administrator

    And I navigate to the Integration page
    And I navigate to the Data Mapping page
    And I configure my external applications data mapping

    And I navigate to the Ext Accounts page
    And I configure my agencies external credentials