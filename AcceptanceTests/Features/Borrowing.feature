Feature: Borrowing a book from the library
	In order to allow users to self-manage
	As a borrower or a staff member
	I Want to be able to find and borrow books

Scenario: A users details are presented when a library card is swiped.
	Given The loan self service station prompts the user to swipe their card
	And The borrower 'Jim' is a member of the library with a valid membership card
	When 'Jim' Swipes his card
	Then The system displays the borrowers details
