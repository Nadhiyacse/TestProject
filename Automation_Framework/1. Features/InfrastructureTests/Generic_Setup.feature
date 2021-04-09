@GenericDataSetup
Feature: Generic_Setup

@ConfigureFeatureToggles
Scenario: 00 Configure Feature Toggles
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Feature Toggles page
    And I configure my feature toggles

@ConfigureGlobalCustomFields
Scenario: 01 Configure Global Custom Fields
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Global page
    And I configure my global custom fields

@ConfigureGenericVendor
Scenario: 02 Configure Generic Vendor
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Vendors page
    And I configure my vendors

@ConfigureGenericPublisher
Scenario: 03 Configure Generic Publisher
    Given I have logged in as a symphony admin user
    And I navigate to the Symphony Admin page
    And I navigate to the Symphony Admin Publishers page
    And I configure my publishers
    And I configure my publishers access control
    And I configure my publishers users
    And I configure my publishers sites
    And I configure my publisher parent site mappings
    And I configure my publishers locations
    And I configure my publishers formats
    And I configure my publishers mappings