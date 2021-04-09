@WorkFlowTest
Feature: GM_Vietnam_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: VN01 Create the campaign
    When I create a campaign
    Then The campaign should be created successfully

Scenario: VN02 Adding the cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I create all single placements from test data
    And I create all performance packages from test data
    And I create all sponsorship packages from test data
    Then The cost items should be present in the media schedule

Scenario: VN03 Download import template
    When I select my campaign
    And I navigate to the Media Schedule page
    And I download the import template
    Then the import template should be downloaded

Scenario: VN04 Import cost items from file
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

Scenario: VN05 Confirm all cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I confirm all items
    Then All the items should have status confirmed

Scenario: VN06 Billing
    When I select my campaign
    And I navigate to the Billing page
    And I open the Custom Billing page for the first publisher displayed on the UI
    Then The totals values per item are as per test data

Scenario: VN07 Billing Export
    When I select my campaign
    And I navigate to the Billing page
    When I export the Billing export 'M2 Export (GroupM Vietnam) V2' with delivery method as 'Download'
    Then the Billing export should be exported

Scenario: VN08 Trafficking
    When I select my campaign
    And I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: VN09 Export cost items
    When I select my campaign
    And I navigate to the Media Schedule page
    And I export the media schedule export 'Standard Media Schedule Grid Data Export'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (VN) IO Export (V2)'
    Then The media schedule export should be exported
    When I export the media schedule export 'GroupM (VN) IO Export (V3)'
    Then The media schedule export should be exported