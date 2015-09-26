using Library.Features.Borrowing;
using Library.Messages.Payload;
using Prism.Events;

namespace Library.Features.ScanBook
{
    class ScanBookController
    {
        public IEventAggregator EventAggregator { get; set; }

        public ScanBookController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(ScanBook);
        }

        public void ScanBook(BorrowingModel borrowingModel)
        {
        }
    }
}
