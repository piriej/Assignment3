using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Arrange
{
    [Binding]
    public sealed class SwipeWindowArrangeSteps
    {
        [Given(@"The loan self service station prompts the user to swipe their card")]
        public void GivenTheLoanSelfServiceStationPromptsTheUserToSwipeTheirCard()
        {
            var mainWindowPageObject = new PageObjects.PageObject();

            mainWindowPageObject.IsBorrowButtonEnabled().Should().BeTrue("The borrow button is not enabled.");

            var cardReaderPageObject = mainWindowPageObject.ClickBorrowButton();

            cardReaderPageObject.IsCardDataBoxEnabled().Should().BeTrue("The user has not been prompted for the scanner, the field is disabled");

            cardReaderPageObject.SetTextOnCardDataBox("This is a test");


        }

    }
}
