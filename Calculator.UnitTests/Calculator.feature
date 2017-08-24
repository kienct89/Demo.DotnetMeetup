Feature: Calculator
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@add
Scenario: Add two numbers
	Given I have entered 50 into the calculator
	And I also have entered 70 into the calculator
	When I press add
	Then 1 event should be dispatched to the event bus

@add
Scenario Outline: Add two numbers (2)
	Given I have entered <first> into the calculator
	And I also have entered <second> into the calculator
	When I press add
	Then 1 event should be dispatched to the event bus

	Examples: 
		| first | second |
		| 1     | 2      |
		| 3     | 4      |