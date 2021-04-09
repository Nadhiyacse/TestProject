@WorkFlowTest
Feature: GM_Australia_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: RFP01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: RFP02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: RFP04 Lock cost items in RFP Grid mode
    When I select my campaign
    And I navigate to the Media Schedule page

    When I check all cost items
    And I expand all placements
    And I click the lock button
    Then Lock success toast is shown
    And All items should be locked
    And The add and import buttons should be disabled

Scenario: RFP05 Unlock cost item in RFP Grid mode
    When I select my campaign
    And I navigate to the Media Schedule page

    When I check all cost items
    And I expand all placements
    And I click the unlock button
    Then Unlock success toast is shown
    And All items should be unlocked
    And The add and import buttons should be enabled

Scenario: RFP06 Duplicate cost items in RFP Grid mode
    When I select my campaign
    And I navigate to the Media Schedule page

    When I select a single placement cost item
    And I click the duplicate button
    Then The single placement should be duplicated with success toast

    When I navigate to the Media Schedule page
    And I select a sponsorship package header row
    And I click the duplicate button
    And I enter a new duplicate sponsorship package name and click save
    Then The sponsorship package should be duplicated with success toast

    When I navigate to the Media Schedule page
    And I select a performance package header row
    And I click the duplicate button
    Then The performance package should be duplicated with success toast

    When I navigate to the Media Schedule page
    And I expand all placements
    And I select a sponsorship package child item
    And I click the duplicate button
    Then The sponsorship package child item should be duplicated with success toast

    When I uncheck all cost items
    And I dismiss all toast notifications
    And I select a performance package child item
    And I click the duplicate button
    Then The performance package child item should be duplicated with success toast

    When I uncheck all cost items
    And I expand all placements
    And I select multiple single placement cost items
    And I select multiple performance package header rows
    And I select multiple performance package placements
    And I select multiple sponsorship package child items
    And I click the duplicate button
    Then The selected items should bulk duplicate with a success toast message with 14 item count

Scenario: RFP07 Replace cost items in RFP Grid mode
    When I select my campaign
    And I navigate to the Media Schedule page

    When I select a single placement cost item
    And I click the replace button
    And I replace the single placement
    Then The single placement should be replaced

    When I uncheck all cost items
    And I select a performance package header row
    And I click the replace button
    And I replace the performance package header row
    Then The performance package header row should be replaced

    When I uncheck all cost items
    And I expand a performance package header row
    And I select a performance package child item
    And I click the replace button
    And I replace the performance package child item
    Then The performance package child item should be replaced

    When I uncheck all cost items
    And I expand a sponsorship package header row
    And I select a sponsorship package child item
    And I click the replace button
    And I replace the sponsorship package child item
    Then The sponsorship package child item should be replaced

Scenario: RFP08 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: RFP09 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: RFP10 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: RFP11 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: RFP12 Issuing insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I create insertion order
    And I issue the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Issued (V1)'

Scenario: RFP13 Recall RFP insertion order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I recall the insertion order
    And I navigate to the Insertion Order page
    Then The insertion order status should be 'Saved (V2)'

Scenario: RFP14 Subscribing publisher rejects IO
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

Scenario: RFP15 Insertion order publisher sign off
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

Scenario: RFP16 Agency rejects IO
    When I select my campaign
    And I navigate to the Insertion Order page
    And I reject the insertion order as Agency
    And I navigate to the Insertion Order page
    Then The insertion order should no longer appear

Scenario: RFP17 Insertion order agency sign off
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

Scenario: RFP18 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items for all AdServers

Scenario: RFP19 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    When I export the media schedule export 'Maxus Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'MCOM Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'Mindshare Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'MEC Australia Media Schedule'
    Then The media schedule export should be exported

Scenario: RFP20 Export Insertion Order
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'IO PDF Export'
    Then the insertion order export should be exported

Scenario: RFP21 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'BCC Media Schedule' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2017/09)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2018/08)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2018/09)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2018/11)' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Standard Billing XML Export (2019/03)' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: RFP22 Group Media Schedule Grid
    When I select my campaign
    And I navigate to the Media Schedule page
    And I group media schedule grid by 'Cost Type'
    Then Media schedule group 'Media' are displayed

Scenario: RFP24 Manage columns in RFP Grid Mode
    When I select my campaign
    And I navigate to the Media Schedule page

    When I select columns to hide
    Then I should not see the columns on the page

    When I select columns to display
    Then I should see the columns on the page