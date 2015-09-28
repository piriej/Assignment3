using Prism.Mvvm;

namespace Library.Features.ScanBook
{
    public class ScanBookViewModel : BindableBase, IScanBookViewModel
    {
        public IScanBookController Controller { get; set; }

        public ScanBookViewModel(IScanBookController controller)
        {
            Controller = controller;
        }

        private string _existingLoan;
        public string ExistingLoan {
            get { return _existingLoan; }
            set { SetProperty(ref _existingLoan, value); }
        }

        private int _borrowerId;
        public int BorrowerId
        {
            get { return _borrowerId; }
            set { SetProperty(ref _borrowerId, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _contact;
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }


        private string _erorMessage;
        public string ErrorMessage
        {
            get { return _erorMessage; }
            set { SetProperty(ref _erorMessage, value); }
        }

        private string _pendingLoans;
        public string PendingLoans
        {
            get { return _pendingLoans; }
            set { SetProperty(ref _pendingLoans, value); }
        }

        private string _currentBook;
        public string CurrentBook
        {
            get { return _currentBook; }
            set { SetProperty(ref _currentBook, value); }
        }

    }
}
