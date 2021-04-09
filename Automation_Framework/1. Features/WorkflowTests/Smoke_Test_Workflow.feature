Feature: Smoke_Test_Workflow

Background: 
    Given I have logged in as an agency user

Scenario: SMK01 RFP smoke
    When I create a campaign
    Then The campaign should be created successfully

    When I navigate to the Media Schedule page
    And I import the media schedule items
    Then The imported cost items should be present in the media schedule

    When I confirm all items
    Then All the items should have status confirmed

    When I export the media schedule export 'Standard Media Schedule Grid Data Export'
    Then The media schedule export should be exported

    When I navigate to the Billing page
    And I export the Billing export 'Standard Billing XML Export (2018/11)' with delivery method as 'Email'
    Then the Billing export should be delivered
    
    When I navigate to the Traffic page
    And I traffic all cost items
    Then All cost items should be trafficked successfully

Scenario: SMK02 AG smoke
    When I create a campaign
    Then The campaign should be created successfully

    When I navigate to the Marketplace page
    And I create all AG single placements from test data
    And I create all AG performance packages from test data
    And I navigate to the Media Schedule page
    Then The AG cost items should be present in the media schedule

    When I confirm all AG items
    And I signoff all items
    Then All the items should have status partsigned

    Given I have logged in to Adslot publisher
    And I select my campaign in Adslot publisher
    When I navigate to the Media Schedule page in Adslot publisher
    And I sign off the items in Adslot publisher
    When I switch back to Symphony as an agency user
    And I select my campaign
    And I navigate to the Media Schedule page
    Then All the items should have status signedoff
