using Library.Features.Borrowing;
using Library.Features.ScanBook;
using Library.Interfaces.Controllers.Borrow;
using Prism.Events;

namespace Library.Features.Scanner
{
    public class ScannerController : IScannerController
    {
        private ScanBookModel _scanBookModel;
        private IBorrowingModel _borrower;
        public IEventAggregator EventAggregator { get; set; }
        public IScannerViewModel ViewModel { get; set; }

        public ScannerController(IEventAggregator EventAggregator)
        {
            EventAggregator.GetEvent<Messages.ScanningEvent>().Subscribe(InitialiseScanner);
            EventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(DisableScanner);
        }

        public void DisableScanner(IBorrowingModel borrowingModel)
        {
            _borrower = borrowingModel;
            ViewModel.Enabled = borrowingModel.BorrowingState == EBorrowState.SCANNING_BOOKS;
        }

        public void InitialiseScanner(ScanBookModel scanBookModel)
        {
            _scanBookModel = scanBookModel;
            ViewModel.Enabled = true;
        }

        public void Scanned(string x)
        {
            var barCode = ViewModel.BarCode;
            int barCodeInt;
            int.TryParse(barCode ,out barCodeInt);
            _scanBookModel.Barcode = barCodeInt;

            EventAggregator.GetEvent<Messages.ScanningRecievedEvent>().Publish(_scanBookModel);

            // Todo: Exception when unborrowed book 
        }
    }
}