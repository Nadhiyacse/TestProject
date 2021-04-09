@WorkFlowTest
Feature: GM_China_Workflow

 Background:
    Given I have logged in as an agency user

 Scenario: CN01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

 Scenario: CN02 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

 Scenario: CN03 Confirm all items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    
 Scenario: CN04 Submit IO for Approval
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I submit Insertion Order for approval
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Pending Approval (V1)'

Scenario: CN05 Approver rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject submitted insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V1)'

Scenario: CN06 Approve Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I submit Insertion Order for approval
    And I approve submitted insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)'

Scenario: CN07 Export IO PDF Approved
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I export the insertion order export 'IO PDF Export (Approved)'
    Then the insertion order export should be exported

Scenario: CN08 Recall Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: CN09 Publisher rejects IO
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

Scenario: CN10 Insertion order publisher sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I submit Insertion Order for approval
    And I approve submitted insertion order
    And I export the insertion order export 'IO PDF Export (Approved)'
    Then the insertion order export should be exported
    When I export the insertion order export 'IO PDF Export'
    Then the insertion order export should be exported

    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'DateSignedOff (V1)' on manage multi IO page for publisher

Scenario: CN11 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I override the billing allocation method per publisher based on test data
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: CN12 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'Minder Packaged Export V3' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Minder Packaged Export V2' with delivery method as 'Download'
    Then the Billing export should be exported

 Scenario: CN13 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items for all AdServers

Scenario: CN14 Trafficking modified cost items
   When I select my campaign
   And I navigate to the Media Schedule page
   And I edit all cost items based on test data
   And I cancel all cost items based on test data
   And I navigate to the Traffic page
   Then All cost items should have correct status
   And I traffic all cost items for all AdServers

Scenario: CN15 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    When I export the media schedule export 'GroupM (CN) Maxus Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (CN) MEC Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (CN) MCOM Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (CN) MCOM Volkswagen Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (CN) Mindshare Media Schedule'
    Then The media schedule export should be exported

Scenario: CN18 Download campaign exports
    When I select my campaign
    And I export the campaign export 'Campaign Data Mapping'
    Then The campaign export should be exported
