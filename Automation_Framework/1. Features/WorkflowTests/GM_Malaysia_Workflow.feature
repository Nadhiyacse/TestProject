@WorkFlowTest
Feature: GM_Malaysia_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: MY01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: MY02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: MY03 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: MY04 Media schedules exports
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM Standard Media Schedule (MY)'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (MY) Mindshare Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (MY) Maxus Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (MY) MediaCom Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (MY) Mediaedge Media Schedule'
    Then The media schedule export should be exported

Scenario: MY05 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: MY06 Insertion order publisher sign off
    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)' on manage multi IO page for publisher

Scenario: MY07 Insertion order agency sign off
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'datesignedoff (V1)'

Scenario: MY08 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export (Tax)'
    Then the insertion order export should be exported

Scenario: MY09 Billing
    When I select my campaign
    And I navigate to the Billing page
    Then The values per publisher should be based on test data
    When I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: MY10 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then Excluded cost items are not visible
    When I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: MY11 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'GroupM Malaysia BMD Export V2' with delivery method as 'Download'
    Then the Billing export should be exported