@Performance
Feature: Performance

Background: 
    Given I have logged in as an agency user

Scenario: Load Time of Overview Page with Watch List
    Then The page should be loaded within 12 seconds

Scenario: Load time of Campaign Search Grid with Watch List
    When I create 50 campaigns via Public API if they don't exist
    And I navigate to the Campaigns page
    Then The page should be loaded within 10 seconds
    And The campaigns are rendered on Campaign Search Grid page

Scenario: Load Time of Media Schedule items in the Grid
    When I create a campaign
    When I select my campaign
    And I navigate to the Media Schedule page
    And I import 3501 media schedule items
    And I navigate to the Media Schedule Grid page
    Then all media schedule items should be loaded within 240 seconds
