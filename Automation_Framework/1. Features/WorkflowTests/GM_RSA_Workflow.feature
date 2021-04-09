@WorkFlowTest
Feature: GM_RSA_Workflow

 Background:
    Given I have logged in as an agency user

 Scenario: RSA01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

 Scenario: RSA02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

 Scenario: RSA03 Confirm all items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    
 Scenario: RSA04 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'GroupM RSA Insertion Order Export'
    Then the insertion order export should be exported
 
 Scenario: RSA07 Submit IO for Approval
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I submit Insertion Order for approval
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Pending Approval (V1)'

Scenario: RSA08 Approver rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject submitted insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V1)'

Scenario: RSA09 Approve Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I submit Insertion Order for approval
    And I approve submitted insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)'

Scenario: RSA10 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I export the insertion order export 'GroupM RSA Insertion Order Export'
    Then the insertion order export should be exported

Scenario: RSA11 Recall Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: RSA12 Publisher rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I submit Insertion Order for approval
    And I approve submitted insertion order

    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I reject the insertion order as Publisher
    And I have logged out the current user
    And I log in as an agency user
    And I select my campaign 
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: RSA13 Insertion order publisher sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'GroupM RSA Insertion Order Export'
    And the insertion order export is exported
    And I submit Insertion Order for approval
    And I approve submitted insertion order

    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'DateSignedOff (V1)' on manage multi IO page for publisher

 Scenario: RSA14 Publishers have correct data on Billing Landing Page
    When I select my campaign
    And I navigate to the Billing page
    Then The values per publisher should be based on test data

 Scenario: RSA15 Publisher has correct data on Custom Billing page
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

 Scenario: RSA16 Prevent locking or unlocking billing splits
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then I should not be able to lock or unlock billing splits

Scenario: RSA17 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

 Scenario: RSA18 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: RSA19 Media schedules exports
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM RSA Media Schedule Export'
    Then The media schedule export should be exported

Scenario: RSA20 Download current ratecard file
    When I navigate to the Administrator page
    And I navigate to the Administrator Publishers page
    And I download my publishers ratecard current file
    Then the download of the ratecard current file should be successful

Scenario: RSA21 Download campaign exports
    When I select my campaign
    And I export the campaign export 'Campaign Data Mapping'
    Then The campaign export should be exported

# Manage Rate Card - (demonstrate value populated from Rate Card)this will happen in Create media schedule step
# Cost flooring to be implemented - which needs API to lock the after the billing splits are selected - out of scope for UI tests. Have to make sure we have this implemented in API tests
# Show non-sub publisher in IO - cannot be verified as part of UI test (as this involves testing exported file).