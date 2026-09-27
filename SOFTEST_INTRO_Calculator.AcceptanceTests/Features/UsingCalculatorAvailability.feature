@Availability
Feature: UsingCalculatorAvailability
  In order to calculate MTBF and Availability
  As a software reliability analyst
  I want to use the calculator for reliability calculations

  Scenario: Calculate MTBF
    Given I have a calculator
    When I have entered 1000 and 5 into the calculator and press MTBF
    Then the result should be 200

  Scenario: Calculate Availability
    Given I have a calculator
    When I have entered 90 and 10 into the calculator and press Availability
    Then the result should be 0.9

  Scenario: Reject an invalid failure count
    Given I have a calculator
    When I have entered 1000 and 0 into the calculator and press MTBF
    Then the reliability calculation should be rejected

  Scenario: Calculate Availability from named reliability values
    Given I have a calculator
    And the reliability values are
      | MTBF | MTTR |
      |   90 |   10 |
    When I calculate Availability from these values
    Then the result should be 0.9
