using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Messages.Payload;
using Prism.Events;

namespace Library.Messages
{
        public class CardReaderSwipedEvent : PubSubEvent<CardReaderModel> { }
        public class BorrowingStateEvent : PubSubEvent<BorrowingModel> { }
        public class CloseApplicationEvent : PubSubEvent<ClosingModel>{ }
}
