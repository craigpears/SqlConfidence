Feature: MultipleChoice
	In order to learn SQL
	As a SQL beginner
	I want to be able to practice with multiple choice exercises

@mytag
Scenario: Smoke Test
	Given I am on the multiple choice page for exercise 1
	Then I should be able to see the multiple choice page

Scenario: Multiple Choice Options
	Given I am on the multiple choice page for exercise 1
	Then I should be able to see the following multiple choice options
	| OptionDescription |
	| varchar           |
	| int               |
	| numeric           |