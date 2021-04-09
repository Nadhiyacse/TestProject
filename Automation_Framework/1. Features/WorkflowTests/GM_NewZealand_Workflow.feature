@WorkFlowTest
Feature: GM_NewZealand_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: NZ01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: NZ02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: NZ03 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: NZ04 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: NZ05 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: NZ06 Insertion order sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order

    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher

    Given I have logged out the current user
    And I have logged in as an agency user
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'DateSignedOff (V1)'

Scenario: NZ07 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: NZ08 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items for all AdServers

Scenario: NZ09 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    When I export the media schedule export 'Standard Media Schedule Grid Data Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (NZ) Standard Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'Production Schedule Export'
    Then The media schedule export should be exported
    
Scenario: NZ10 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export'
    Then the insertion order export should be exported

Scenario: NZ11 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    When I export the Billing export 'Standard Billing Export' with delivery method as 'Download'
    Then the Billing export should be exported