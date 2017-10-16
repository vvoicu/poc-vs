@retry:0
Feature: PrmaLogin
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag @yourtag
Scenario: Add two numbers
	Given I navigate to the login URL
	And I enter valid credentials
	When I go to heatmap