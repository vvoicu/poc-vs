Feature: Heatmap
	In order to avoid silly mistakes
	As a math idiot
	I want to be told the sum of two numbers

@prma @heatmap
Scenario: Heatmap
	Given I navigate to the login URL
	And I enter valid credentials
	When I navigate to the heatmap
	And I select a coloured cell with '3' colors
	And I check the number of each requirement

#@prma @heatmap
#Scenario Outline: Heatmap
#	Given I navigate to the login URL
#	And I enter valid credentials
#	When I navigate to the heatmap
#	And I select a coloured cell with '<colorScheme>' colors
#	And I check the number of each requirement
#
#Examples:
#| colorScheme |
#| 1           |
#| 2           |