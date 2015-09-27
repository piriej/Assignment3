Feature: Borrowing a book from the library
	In order to allow users to self-manage
	As a borrower or a staff member
	I Want to be able to find and borrow books

Scenario: A users details are presented when a library card is swiped.
	Given The loan self service station prompts the user to scan their card
	When The borrowers card scans as '0001'
	Then The system displays the borrowers details as Id of '1', Name 'fName1 lName1', Contact: '0001' 


Scenario: Display existing loan details when a library card is swiped.
	Given The loan self service station prompts the user to scan their card
	When The borrowers card scans as '0002'
	Then The system displays the borrowers existing loan details as:
		| Row                                 |
		| Loan ID:            	2             |
		| Author:             	author1       |
		| Title:              	title2        |
		| Borrower:           	fName2 lName2 |
		| Borrow Date:        	27/09/2015    |
		| Due Date:           	11/10/2015    | 
