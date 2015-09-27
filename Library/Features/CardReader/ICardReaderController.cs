using Library.Messages.Payload;

namespace Library.Features.CardReader
{
    public interface ICardReaderController
    {
        void CardSwiped(string borowerId);
    }
}