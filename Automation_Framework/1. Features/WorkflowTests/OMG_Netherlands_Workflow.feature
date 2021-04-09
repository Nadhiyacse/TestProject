@WorkFlowTest
Feature: OMG_Netherlands_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: NL01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: NL02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: NL03 Add Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I add non media cost items
    Then the non media cost items are added successfully

Scenario: NL04 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: NL05 Insertion order publisher sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'OMG Insertion Order Export'
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

 Scenario: NL06 Publishers have correct data on Billing Landing Page
    When I select my campaign
    And I navigate to the Billing page
    Then The values per publisher should be based on test data

 Scenario: NL07 Publisher has correct data on Custom Billing page
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: NL08 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then I traffic all cost items for all AdServers

Scenario: NL09 Media schedules exports
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'OMG Media Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'OMG Media Schedule Export - Actuals'
    Then The media schedule export should be exported

Scenario: NL09 Billed Goal
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    And I enter my Billed Goals based on test data
    And I navigate to the Media Schedule page
    And I select columns to display
    Then I should see the columns on the page
    And I should see the Billed Goals under Delivered column in the media schedule

Scenario: NL10 Verify Cost tab of Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I open the first non media cost item

    And I switch to Costs tab
    And I set cost adjustment values
    Then the cost summaries in cost tab are as expected
    When I switch to Classifications tab
    And I set classification values
    And I save the cost item

    When I open the first non media cost item
    And I switch to Costs tab
    Then the cost summaries in cost tab are as expected
    When I switch to Classifications tab
    Then the classifications in Classifications tab are as expected

Scenario: NL11 Cancel Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I cancel Non Media costs based on test data
    Then Non Media costs were cancelled