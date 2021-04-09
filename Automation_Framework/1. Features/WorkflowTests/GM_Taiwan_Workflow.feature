@WorkFlowTest
Feature: GM_Taiwan_Workflow

Background:
    Given I have logged in as an agency user

Scenario: TW01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: TW02 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: TW03 Confirm all items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items

Scenario: TW04 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM Taiwan Media Schedule'
    Then The media schedule export should be exported

Scenario: TW05 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: TW06 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'M2 Export (GroupM Taiwan)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2016/03)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2017/03)' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: TW07 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: TW08 Insertion order publisher sign off
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

Scenario: TW09 Insertion order agency sign off
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'DateSignedOff (V1)'

Scenario: TW10 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully 

#@ignore
# Error - bug is documented in QA-3120
#Scenario: TW11 Edit Cancelled Placements
#    When I select my campaign
#    And I navigate to the Media Schedule page
#    And I cancel all my cost items
#    Then All the items should be cancelled
#    When I edit all single placements
#    And I edit all performance packages
#    And I edit all sponsorship packages
#    Then All the items should be not cancelled

Scenario: TW12 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export'
    Then the insertion order export should be exported