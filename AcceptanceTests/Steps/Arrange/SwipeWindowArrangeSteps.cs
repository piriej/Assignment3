using System;
using System.Windows.Automation;
using AcceptanceTests.PageObjects;
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

           


        }

    }
}
