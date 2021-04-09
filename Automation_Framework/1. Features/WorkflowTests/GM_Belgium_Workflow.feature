@WorkFlowTest
Feature: GM_Belgium_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: BE01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: BE02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: BE03 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: BE04 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: BE05 Add Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I add non media cost items
    Then the non media cost items are added successfully

Scenario: BE06 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: BE07 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: BE08 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: BE09 Insertion order publisher sign off
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

Scenario: BE10 Insertion order agency sign off
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'datesignedoff (V1)'

Scenario: BE11 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then Excluded cost items are not visible
    When I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: BE12 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM (BE) Production Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (BE) Media Plan Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (BE) Trafficking Sheet'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (BE) Campaign Report'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (BE) Technical Specifications Export'
    Then The media schedule export should be exported

Scenario: BE13 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export'
    Then the insertion order export should be exported
    When I export the insertion order export 'IO PDF Export (Prorata Monthly Allocations)'
    Then the insertion order export should be exported
    When I export the insertion order export 'IO PDF Export (Prorata Monthly Allocations with Site Breakdown)'
    Then the insertion order export should be exported

Scenario: BE14 Edit Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit Non Media cost based on test data
    Then The version of Non Media cost is incremented
    When I navigate to the Media Schedule page
    And I edit Non Media cost based without making any changes
    Then The version of Non Media cost is not incremented

Scenario: BE15 Verify Cost tab of Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I open the first non media cost item
    And I switch to Costs tab
    And I set cost adjustment values
    Then the cost summaries in cost tab are as expected
    And I save the cost item

    When I open the first non media cost item
    And I switch to Costs tab
    Then the cost summaries in cost tab are as expected

Scenario: BE16 Cancel Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I cancel Non Media costs based on test data
    Then Non Media costs were cancelled

Scenario: BE17 VerifyForecastUnitcost
    When I select my campaign
    And I navigate to the Media Schedule page
    And I open the first Single Placement 
    And I switch to Forecast tab
    Then the values of estimate fields in forecast tab is as expected