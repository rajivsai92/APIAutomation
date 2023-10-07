Feature: GETCall

"GET API scenarios"

@tag1
Scenario: Verify GET API with response
	Given I Set API Call with baseurl and endpoint as /latest
	When I set <Variable> as path parameter to base url
	Then I Set Default Request headers
	Then I perform GET request
	Then Validate the response status as 200
	And Validate the GET API response for <Variable>
Examples:
	| Varaible |
	| test     |
