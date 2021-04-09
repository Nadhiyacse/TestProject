@WorkFlowTest
Feature: GM_Austria_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: AG01 Creating the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: AG02 Checking filters on the marketplace
    When I select my campaign
    And I navigate to the Marketplace page
    Then The device filter options should be visible
    And The buy type filter options should be visible

Scenario: AG03 Adding the AG placements
    When I select my campaign
    And I navigate to the Marketplace page
    And I create all AG single placements from test data
    And I create all AG performance packages from test data
    And I navigate to the Media Schedule page
    Then The AG cost items should be present in the media schedule

Scenario: AG04 Confirming and signing off the items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all AG items
    And I signoff all items
    Then All the items should have status partsigned

Scenario: AG05 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    And I export the Billing export 'Mediacom AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'mSix AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'METS Media GmbH AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Mindshare AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Wavemaker AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported
    When I export the Billing export 'Wavemaker GmbH AT Adspace Billing XML Export' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: AG06 Publisher signs off
    Given I have logged in to Adslot publisher
    And I select my campaign in Adslot publisher
    And I navigate to the Inbox page in Adslot publisher
    Then The IO PDF Export message should be visible for version 1 and expire in 21 days
    When I click the IO PDF Export link for version 1 that expires in 21 days
    Then I should be on the AG IO Public Export Page
    And The AG IO Export Campaign Name Should Be My Current Campaign And The Version Number Should Be 1
    And I Should Be Able To Download The AG IO Export
    And I Close The Public Export Tab And Go Back To The Previous Tab
    When I navigate to the Media Schedule page in Adslot publisher
    And I sign off the items in Adslot publisher
    When I switch back to Symphony as an agency user
    And I select my campaign
    And I navigate to the Media Schedule page
    Then All the items should have status signedoff

Scenario: AG08 Export AG IO PDF Export
    When I select my campaign
    And I navigate to the Insertion Order page
    And I click on last signed off link
    And I export the insertion order export 'AG IO PDF Export (Prorata Monthly Allocations with Site Breakdown)'
    Then the insertion order export should be exported

Scenario: AG09 Edit AG items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I edit all AG single placements from test data without making any changes
    Then The version for all single placements from test data is not incremented
    When I edit all AG performance packages from test data without making any changes
    Then The version for all performance packages from test data is not incremented
    When I edit all AG single placements
    Then The version for all single placements from test data is incremented
    When I edit all AG performance packages
    Then The version for all performance packages from test data is incremented

Scenario: AG10 Recall AG IO
    When I select my campaign
    And I navigate to the Media Schedule page
    And I signoff all items
    Then All the items should have status partsigned
    Then Part signed success toast is shown

    When I navigate to the Insertion Order page
    And the AG IO is issued
    And I recall the insertion order
    And I navigate to the Media Schedule page
    Then All the items should have status confirmed

Scenario: AG11 Adslot Publisher rejects media schedule
    When I select my campaign
    And I navigate to the Media Schedule page
    And I signoff all items
    Then All the items should have status partsigned

    Given I have logged in to Adslot publisher
    And I select my campaign in Adslot publisher
    And I navigate to the Media Schedule page in Adslot publisher
    And I reject the items in Adslot publisher
    When I switch back to Symphony as an agency user
    Then All the items should have status confirmed

Scenario: AG12 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    When I export the media schedule export 'Production Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'Standard Media Schedule Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Standard Media Schedule'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM Austria Custom Media Schedule'
    Then The media schedule export should be exported

Scenario: AG13 Create AG Proto Product Single Placement
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements that will convert from RFP to AG from test data
    Then the RFP single placements should have converted to AG items

Scenario: AG14 Create AG Proto Product Performance Package
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all performance packages that will convert from RFP to AG from test data
    Then the RFP performance packages should have converted to AG items

Scenario: AG15 Create RFP Single Placements
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements that will not convert from RFP to AG from test data
    Then the RFP single placements should not have converted to AG items

Scenario: AG16 Create RFP Performance Packages
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all performance packages that will not convert from RFP to AG from test data
    Then the RFP performance packages should not have converted to AG items

Scenario: AG17 Import AG proto products
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported AG items should be present in the media schedule

Scenario: AG18 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: AG19 Add Non Media Cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I add non media cost items
    Then the non media cost items are added successfully

Scenario: AG20 Verify Cost tab of Non Media Cost items
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