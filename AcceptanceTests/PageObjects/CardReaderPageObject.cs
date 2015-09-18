using AcceptanceTests.Help;

namespace AcceptanceTests.PageObjects
{
    public class CardReaderPageObject : PageObjectBase
    {
        const string CardReaderDataField = "cardDataBox";


        public CardReaderPageObject() : base("CardReader")
        {
           Page.DisplayChildren();
        }


        public bool IsCardDataBoxEnabled()
        {
            return IsElementEnabled(CardReaderDataField);
        }

        public void SetTextOnCardDataBox(string text)
        {
            SetElementText(CardReaderDataField, text);
        }
    }

}