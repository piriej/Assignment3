using System.Linq;
using AutoMapper;
using Library.Features.Borrowing;
using Library.Interfaces.Controllers.Borrow;
using Prism.Events;

namespace Library.Features.ScanBook
{
    public class ScanBookController : IScanBookController
    {
        public IEventAggregator EventAggregator { get; set; }
        public IScanBookViewModel ViewModel { get; set; }

        public ScanBookController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(ScanBook);
        }

        public void ScanBook(BorrowingModel borrowingModel)
        {
            // Display user details
            //ViewModel.BorrowerId = borrowingModel.ID;

            // Map the model onto the viewmodel.
            if (borrowingModel.BorrowingState == EBorrowState.SCANNING_BOOKS)
            {
                //borrowingModel.Loans.FirstOrDefault().
                Mapper.Map(borrowingModel, (ScanBookViewModel)ViewModel);
            }
        }
    }
}
