using AcceptanceTests.PageObjects;
using AcceptanceTests.PageObjects.Pages;
using BoDi;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Act
{
    [Binding]
    public class CardReaderActSteps
    {
        private readonly IObjectContainer _objectContainer;
        MainWindowPageObject _mainWindowPageObject;
        CardReaderPageObject _cardReaderPageObject;

        public CardReaderActSteps(IObjectContainer objectContainer, CardReaderPageObject cardReaderPageObject, MainWindowPageObject mainWindowPageObject)
        {
            _objectContainer = objectContainer;
            _mainWindowPageObject = mainWindowPageObject;
            _cardReaderPageObject = cardReaderPageObject;
        }

        [When(@"The borrowers card scans as '(.*)'")]
        public void WhenTheBorrowersCardScansAs(string scannedCard)
        {
            // If the card box is enabled.
            _cardReaderPageObject.IsCardDataBoxEnabled().Should().BeTrue("The cord data box should be enabled.");

            // Enter the scanned value into the box.
            _cardReaderPageObject.SetTextOnCardDataBox(scannedCard);

            // Click the swipe button.
            _cardReaderPageObject.ClickSwipeCard();

            // The main window should now have the scan book view.
            _mainWindowPageObject.ContentRegionIs<ScanBookRegion>().Should().BeTrue();

            var scanBookRegion = _mainWindowPageObject.ContentRegion<ScanBookRegion>();

            // Save the current scan book region in the di container.
            _objectContainer.RegisterInstanceAs(scanBookRegion);
        }
    }
}
