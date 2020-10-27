Feature: Search
	To perform a search in london
	Get all search displayed 
	Validate link count

@smoke
Scenario: Google Car Search
	Given I am on google page
	 And I search for 'cars in london'
	Then search result is displayed 'cars in london'
	 And I validate 'www.gumtree.com' links available is greater than '1'
	When I navigate to each 'www.gumtree.com' links and check title contains 'Gumtree'
	Then validate car count is greater than '0'

	