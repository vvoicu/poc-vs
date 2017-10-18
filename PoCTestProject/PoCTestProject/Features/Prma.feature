Feature: PrmaLogin
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@retry:0
@mytag @yourtag
Scenario: Add two numbers
	Given I navigate to the login URL
	And I enter valid credentials
	When I go to heatmap

@Dia
Scenario: Steel Request
	Given I navigate to the login URL
	And I enter valid credentials

@dur
Scenario: Add my pickle
	Given I navigate to the login URL
	And I enter valid credentials
	

@mytag @someTag
Scenario Outline: Add to login
	Given I navigate to the login URL
	And I enter valid credentials

Examples: 
| 1 |
| 2 |
| 3 |