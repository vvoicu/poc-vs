﻿Feature: Heatmap
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@mytag
Scenario: Heatmap
	Given I navigate to the login URL
	And I enter valid credentials
	When I navigate to the heatmap URL
	And I select a coloured cell
	And I check the number of each type of requirement
	And I click on the total number of requirements link
	Then I am redirected to the requirements page
	And each requirement is displayed with the correct colour
