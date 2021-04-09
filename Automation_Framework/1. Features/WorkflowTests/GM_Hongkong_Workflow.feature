@WorkFlowTest
Feature: GM_Hongkong_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: HK01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: HK02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: HK03 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: HK04 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: HK05 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: HK06 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: HK07 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'M2 Export (GroupM Hong Kong) V2.1' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: HK08 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: HK09 Insertion order publisher sign off
    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher    
    And I have logged out the current user
    And I log in as an agency user
    And I select my campaign 
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)'

Scenario: HK10 Insertion order agency sign off
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'datesignedoff (V1)'

Scenario: HK11 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then Excluded cost items are not visible
    When I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: HK12 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM (HK) Standard Media Schedule'
    Then The media schedule export should be exported