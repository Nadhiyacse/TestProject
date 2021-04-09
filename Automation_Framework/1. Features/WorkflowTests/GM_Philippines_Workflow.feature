@WorkFlowTest
Feature: GM_Philippines_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: PH01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: PH02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: PH03 Cost Item Version History
    When I select my campaign
    And I navigate to the Media Schedule page
    And I open the version history of the first single placement
    Then the datetime of the 1st row should be based on the agency time zone

Scenario: PH04 Version not increment for single placement edit
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit the first single placement from test data without making any changes
    Then The version for the first single placement from test data is not incremented

Scenario: PH05 Version not increment for performance package edit
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit the first performance package from test data without making any changes
    Then The version for the first performance package from test data is not incremented

Scenario: PH06 Version not increment for sponsorship package edit
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit the first placement inside the first sponsorship package from test data without making any changes
    Then The version for the first sponsorship package from test data is not incremented

Scenario: PH09 Submit for approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    And I submit for approval
    Then The export is downloaded
    And The approval status is Pending Approval

Scenario: PH10 Campaign approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I approve the items as Agency Approved on behalf of Client
    Then The approval status is Approved

Scenario: PH11 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: PH12 Recall RFP insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: PH13 Subscribing publisher rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click the Pending IO status link
    And I issue the insertion order

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

Scenario: PH14 Insertion order publisher sign off
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order

    Given I have logged out the current user
    And I have logged in as a publisher user
    And I select my campaign as a publisher user
    And I navigate to the Insertion Order page
    When I sign off the insertion order as Publisher
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Part Signed (V1)' on manage multi IO page for publisher

Scenario: PH15 Agency rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: PH16 Insertion order agency sign off
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

Scenario: PH17 Publishers have correct data on Billing Landing Page
    When I select my campaign
    And I navigate to the Billing page
    And I select Buy (Multiple) from the currency dropdown on the Billing Landing page for the country Philippines
    Then The values per publisher should be based on test data
    When I select Base from the currency dropdown on the Billing Landing page for the country Philippines
    Then The values per publisher should be based on test data

Scenario: PH18 Publisher has correct data on Custom Billing page
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    And I select Buy (Multiple) from the currency dropdown on the Custom Billing page for the country Philippines
    And I customise my billing values based on test data
    Then The totals values per item are as per test data
    When I select Base from the currency dropdown on the Custom Billing page for the country Philippines
    Then The totals values per item are as per test data

Scenario: PH19 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    Then I traffic all cost items for all AdServers

#@ignore
# Error - bug is documented in QA-3323
#Scenario: PH20 Edited cost items were auto approved
#    When I select my campaign
#    And I navigate to the Media Schedule page
#    And I edit all cost items based on test data
#    Then The approval status is Approved

Scenario: PH21 Media schedules exports
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM PH Cost Summary Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM PH Cost Estimate Export (Fee Basis)'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM PH Cost Estimate Export (Comm Basis)'
    Then The media schedule export should be exported

Scenario: PH22 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'GM Philippines Booking Order'
    Then the insertion order export should be exported

Scenario: PH23 Export Billing
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'GroupM Philippines BMD Export V1' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: PH24 Export Classification Filter
    Given I navigate to the Administrator page
    And I navigate to Classification Filter tab
    And I export my classification filters
