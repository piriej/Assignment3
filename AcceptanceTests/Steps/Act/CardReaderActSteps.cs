using AcceptanceTests.PageObjects.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Act
{
    [Binding]
    public class CardReaderActSteps
    {
        readonly CardReaderPageObject _cardReaderPageObject;

        public CardReaderActSteps(CardReaderPageObject cardReaderPageObject)
        {
            _cardReaderPageObject = cardReaderPageObject;
        }

        [When(@"The borrowers card scans as '(.*)'")]
        public void WhenTheBorrowersCardScansAs(string scannedCard)
        {
            // If the card box is enabled.
            _cardReaderPageObject.IsCardDataBoxEnabled().Should().BeTrue("The cord data box should be enabled.");

            // Enter the scanned value into the box.
            _cardReaderPageObject.SetTextOnCardDataBox(scannedCard);

            _cardReaderPageObject.ClickSwipeCard();
        }



    }
}
