@BasicMusa
Feature: UsingCalculatorBasicReliability
  In order to calculate Basic Musa reliability measurements
  As a software reliability analyst
  I want to calculate failure intensity and cumulative failures

  Scenario: Calculate current failure intensity
    Given I have a calculator
    And the initial failure intensity is 10, expected total failures is 100, and execution time is 10
    When I calculate the current failure intensity
    Then the result should be 3.6787944117144233

  Scenario: Calculate expected cumulative failures
    Given I have a calculator
    And the initial failure intensity is 10, expected total failures is 100, and execution time is 10
    When I calculate the expected cumulative failures
    Then the result should be 63.212055882855765

  Scenario: Calculate failure intensity at zero execution time
    Given I have a calculator
    And the initial failure intensity is 10, expected total failures is 100, and execution time is 0
    When I calculate the current failure intensity
    Then the result should be 10

  Scenario: Reject a non-positive initial failure intensity
    Given I have a calculator
    And the initial failure intensity is 0, expected total failures is 100, and execution time is 10
    When I calculate the current failure intensity
    Then the Basic Musa calculation should be rejected