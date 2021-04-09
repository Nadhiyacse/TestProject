@WorkFlowTest
Feature: GM_Turkey_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: TR01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: TR02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: TR03 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: TR04 Add Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I add non media cost items
    Then the non media cost items are added successfully

Scenario: TR05 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: TR06 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: TR07 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: TR08 Insertion order publisher sign off
    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    And I check non media costs in insertion order as Publisher
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I have logged out the current user
    And I log in as an agency user
    And I select my campaign 
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)'

Scenario: TR09 Insertion order agency sign off
    When I select my campaign 
    And I navigate to the Insertion Order page
    And I sign off the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'datesignedoff (V1)'

Scenario: TR10 Edit Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit Non Media cost based on test data
    Then The version of Non Media cost is incremented
    When I navigate to the Media Schedule page
    And I edit Non Media cost based without making any changes
    Then The version of Non Media cost is not incremented

#@ignore
# Error - bug is documented in QA-3122
#Scenario: TR11 Revise and sign off insertion order
#    When I select my campaign
#    And I navigate to the Billing page
#    And I override the billing allocation method per publisher based on test data
#    And I navigate to the Insertion Order page
#    And I revise and check Non Media Cost in insertion order as Agency
#    And I issue the insertion order
#    And I navigate to the Insertion Order page
#    Then The insertion order status should be 'Issued (V3)'
#
#    Given I have logged out the current user
#    And I have logged in as a publisher user
#    And I select my campaign as a publisher user
#    And I navigate to the Insertion Order page
#    When I sign off the insertion order as Publisher
#    And I navigate to the Insertion Order page
#    Then The insertion order status should be 'Part Signed (V3)' on manage multi IO page for publisher
#
#    Given I have logged out the current user
#    And I have logged in as an agency user
#    When I select my campaign 
#    And I navigate to the Insertion Order page
#    And I sign off the insertion order as Agency
#    And I navigate to the Insertion Order page
#    Then The insertion order status should be 'DateSignedOff (V3)'

Scenario: TR12 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then Excluded cost items are not visible
    And I traffic all cost items for all AdServers

Scenario: TR13 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM Turkey Custom Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Maxus Custom Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Mec Custom Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Wavemaker Custom Media Schedule V2'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Mediacom Custom Media Schedule V2'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Mindshare Custom Media Schedule V2'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Turkey Garantiban Custom Media Schedule V2'
    Then The media schedule export should be exported

Scenario: TR14 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: TR15 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export (Net Cost)'
    Then the insertion order export should be exported
    When I export the insertion order export 'IO PDF Export (Prorata Monthly Allocations)'
    Then the insertion order export should be exported

Scenario: TR16 Download current ratecard file
    When I navigate to the Administrator page
    And I navigate to the Administrator Publishers page
    And I download my publishers ratecard current file
    Then the download of the ratecard current file should be successful

Scenario: TR17 Verify Cost tab of Non Media Cost items
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

Scenario: TR18 Cancel Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I cancel Non Media costs based on test data
    Then Non Media costs were cancelled
