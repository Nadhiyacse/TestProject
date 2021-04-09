@WorkFlowTest
Feature: GM_India_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: IN01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: IN02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: IN05 Submit for approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    And I submit for approval
    And I download the media schedule export 'GroupM India Estimate Sheet'
    Then The media schedule export should be exported
    And The approval status is Pending Approval

Scenario: IN06 Edited cost items were auto approved
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit all cost items based on test data
    Then The approval status is Pending Approval

Scenario: IN07 Campaign approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I approve all items
    Then The approval status is Approved

Scenario: IN08 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'GroupM India Release Order Export'
    Then the insertion order export should be exported
    When I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: IN09 Recall RFP insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: IN10 Subscribing publisher rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I export the insertion order export 'GroupM India Release Order Export'
    Then the insertion order export should be exported

    Given I issue the insertion order
    And I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I reject the insertion order as Publisher
    And I have logged out the current user
    And I log in as an agency user
    And I select my campaign 
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: IN11 Insertion order publisher sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'GroupM India Release Order Export'
    Then the insertion order export should be exported

    Given I issue the insertion order
    And I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)' on manage multi IO page for publisher

Scenario: IN12 Agency rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: IN13 Insertion order agency sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I export the insertion order export 'GroupM India Release Order Export'
    Then the insertion order export should be exported
    When I export the insertion order export 'GroupM India Release Order Export - Fulcrum'
    Then the insertion order export should be exported

    Given I issue the insertion order
    And I have logged out the current user
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

Scenario: IN14 Prevent locking or unlocking billing splits
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then I should not be able to lock or unlock billing splits

Scenario: IN15 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully

#@ignore
# Error - bug is documented in QA-3151
#Scenario: IN16 Trafficking deleted cost items
#    When I select my campaign
#    And I navigate to the Media Schedule page
#    And I cancel all cost items based on test data
#    And I submit for approval
#    And I download the media schedule export 'GroupM India Estimate Sheet'
#    And The media schedule export is exported
#    And I approve the version 2 of media schedule items
#    And I navigate to the Traffic page
#    Then All cost items should have correct status
#    And I traffic all cost items for all AdServers