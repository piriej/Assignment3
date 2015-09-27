using AcceptanceTests.PageObjects;
using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Arrange
{
    [Binding]
    public sealed class SwipeWindowArrangeSteps
    {
        readonly IObjectContainer objectContainer;

        public SwipeWindowArrangeSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }


        [Given(@"The loan self service station prompts the user to scan their card")]
        public void GivenTheLoanSelfServiceStationPromptsTheUserToScanTheirCard()
        {
            // Get the page object for the current page.
            var mainWindowPageObject = new PageObjects.Pages.MainWindowPageObject();

            // Ensure the borrow region is visible.
            mainWindowPageObject.ContentRegionIs<BorrowingRegion>().Should().BeTrue();

            // Ensure the borrow button is enabled before it is clicked.
            mainWindowPageObject.ContentRegion<BorrowingRegion>().IsBorrowButtonEnabled().Should().BeTrue("The borrow button is not enabled.");

            // Click the borrow button.
            var cardReaderPageObject = mainWindowPageObject.ContentRegion<BorrowingRegion>().ClickBorrowButton();

            // Check that the main window prompts the user to swipe their card.
            //mainWindowPageObject.ContentRegionIs<SwipeCardRegion>().Should().BeTrue();

            // Ensure the user is prompted to swipe their card.
            cardReaderPageObject.IsCardDataBoxEnabled().Should().BeTrue("The user has not been prompted for the scanner, the field is disabled");


            // Store these objects in the container for later reference.
            objectContainer.RegisterInstanceAs(cardReaderPageObject);
            objectContainer.RegisterInstanceAs(mainWindowPageObject);

        }

    }
}
