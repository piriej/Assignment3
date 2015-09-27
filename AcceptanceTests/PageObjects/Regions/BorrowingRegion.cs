using AcceptanceTests.Help;
using AcceptanceTests.PageObjects.Pages;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects
{
    public class BorrowingRegion : ContentRegionBaseObject
    {
        const string BorrowButton = "BorrowButton";

        public BorrowingRegion() : base(ViewNames.BorrowingControl)
        {

        }

        public bool IsBorrowButtonEnabled()
        {
            return IsElementEnabled(BorrowButton);
        }

        public CardReaderPageObject ClickBorrowButton()
        {
            ClickElement(BorrowButton);
            return new CardReaderPageObject();
        }
    }
}