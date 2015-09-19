using AcceptanceTests.PageObjects;
using FluentAssertions;
using Library.ViewModels;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Arrange
{
    [Binding]
    public sealed class SwipeWindowArrangeSteps
    {
        [Given(@"The loan self service station prompts the user to swipe their card")]
        public void GivenTheLoanSelfServiceStationPromptsTheUserToSwipeTheirCard()
        {
            // Get the page object for the current page.
            var mainWindowPageObject = new PageObjects.Pages.PageObject();

            // Ensure the borrow region is visible.
            mainWindowPageObject.ContentRegionIs<BorrowingRegion>().Should().BeTrue();

            // Ensure the borrow button is enabled before it is clicked.
            mainWindowPageObject.ContentRegion<BorrowingRegion>().IsBorrowButtonEnabled().Should().BeTrue("The borrow button is not enabled.");

            // Click the borrow button.
            var cardReaderPageObject = mainWindowPageObject.ContentRegion<BorrowingRegion>().ClickBorrowButton();

            // Check that the main window prompts the user to swipe their card.
            mainWindowPageObject.ContentRegionIs<SwipeCardRegion>().Should().BeTrue();

            // Ensure the user is prompted to swipe their card.
            cardReaderPageObject.IsCardDataBoxEnabled().Should().BeTrue("The user has not been prompted for the scanner, the field is disabled");

            // Swipe card...
            cardReaderPageObject.SetTextOnCardDataBox("This is a test");
        }

    }
}
