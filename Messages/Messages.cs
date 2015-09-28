using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Features.ScanBook;
using Library.Messages.Payload;
using Prism.Events;

namespace Library.Messages
{
        public class CardReaderSwipedEvent : PubSubEvent<CardReaderModel> { }
        public class BorrowingStateEvent : PubSubEvent<BorrowingModel> { }
        public class CloseApplicationEvent : PubSubEvent<ClosingModel>{ }
        public class ScanningRecievedEvent : PubSubEvent<ScanBookModel> { }
        public class ScanningEvent : PubSubEvent<ScanBookModel> { }
}
