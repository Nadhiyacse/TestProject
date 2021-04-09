@WorkFlowTest
Feature: GM_Indonesia_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: IDO01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: IDO02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: IDO05 Submit for approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    And I submit for approval
    Then The export is downloaded
    And The approval status is Pending Approval

Scenario: IDO06 Campaign approval
    When I select my campaign
    And I navigate to the Media Schedule page
    And I approve the items as Agency Approved on behalf of Client
    Then The approval status is Approved

 Scenario: IDO07 Billing
    When I select my campaign
    And I navigate to the Billing page
    Then The values per publisher should be based on test data
    When I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

#@ignore
# Error - bug is documented in QA-3633
#Scenario: IDO08 Imported items were auto approved
#    When I select my campaign
#    And I navigate to the Media Schedule page
#    And I export the media schedule export 'Standard Media Schedule Grid Data Export'
#    Then The media schedule export should be exported
#    When I edit all cost items based on test data
#    And I import the media schedule items from downloaded file
#    Then The approval status is Approved

Scenario: IDO09 Including auto approved items in insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I navigate to the Media Schedule page
    And I edit all cost items based on test data
    Then The approval status is Approved
    When I navigate to the Insertion Order page
    And I include cost items in insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'
    And I cancel insertion order

Scenario: IDO10 Media schedules exports
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'GroupM Indonesia Mediacom Media Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Indonesia Mindshare Media Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Indonesia Wavemaker Media Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Indonesia Digital Campaign Brief'
    Then The media schedule export should be exported

Scenario: IDO11 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: IDO12 Recall RFP insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: IDO13 Subscribing publisher rejects IO
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

Scenario: IDO14 Insertion order publisher sign off
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

Scenario: IDO15 Agency rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: IDO16 Insertion order agency sign off
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

Scenario: IDO17 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'GroupM Indonesia IO Export'
    Then the insertion order export should be exported

Scenario: IDO18 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully