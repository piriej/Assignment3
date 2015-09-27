﻿using Library.Features.Borrowing;
using Prism.Events;

namespace Library.Features.ScanBook
{
    public class ScanBookController : IScanBookController
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
