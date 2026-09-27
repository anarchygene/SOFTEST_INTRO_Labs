@Factorial
Feature: UsingCalculatorFactorial
 In order to conquer factorials
 As a factorial enthusiast
 I want to understand a variety of factorial operations

  Scenario Outline: Find the factorial of a number
    Given I have a calculator
    When I have entered <number> into the calculator and press factorial
    Then the factorial result should be <result>

    Examples:
      | number | result |
      |      0 |      1 |
      |      5 |    120 |

  Scenario Outline: Reject invalid factorial inputs
    Given I have a calculator
    When I have entered <value1> into the calculator and press factorial
    Then factorial should be rejected

    Examples:
      | value1 |
      |     -1 |
