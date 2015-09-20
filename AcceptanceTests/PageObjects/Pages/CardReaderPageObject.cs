using AcceptanceTests.PageObjects.Infrastructure;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects.Pages
{
    public class CardReaderPageObject : PageObjectBase
    {
        const string CardReaderDataField = "cardDataBox";
        const string CardReaderSwipeCardButton = "swipeButton";


        public CardReaderPageObject() : base(ViewNames.CardReaderControl)
        {
        }

        public bool IsCardDataBoxEnabled()
        {
            return IsElementEnabled(CardReaderDataField);
        }

        public void SetTextOnCardDataBox(string text)
        {
            SetElementText(CardReaderDataField, text);
        }

        public void ClickSwipeCard()
        {
            ClickElement(CardReaderSwipeCardButton);
        }
    }

}