Feature: Borrowing a book from the library
	In order to allow users to self-manage
	As a borrower or a staff member
	I Want to be able to find and borrow books

Scenario: A users details are presented when a library card is swiped.
	Given The loan self service station prompts the user to scan their card
	When The borrowers card scans as '0001'
	Then The system displays the borrowers details 
