using System;
using System.Diagnostics;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.Features.ScanBook
{
    public class ScanBookViewModel : BindableBase, IScanBookViewModel
    {
        #region Bound Properties

        public IScanBookController Controller { get; set; }

        #endregion

        #region constructors

        public ScanBookViewModel(IScanBookController controller)
        {
            Controller = controller;
            this.CompleteCommand = new DelegateCommand(controller.Complete).ObservesCanExecute(x => CanComplete);
        }

        #endregion

        #region Fields
        private string _existingLoan;
        public string ExistingLoan
        {
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

        private bool _hasOverDueLoans;

        public bool HasOverDueLoans
        {
            get { return _hasOverDueLoans; }
            set
            {
                SetProperty(ref _hasOverDueLoans, value);
                CanComplete = !(value || HasReachedLoanLimit || HasReachedFineLimit);
            }
        }

        private bool _hasReachedLoanLimit;

        public bool HasReachedLoanLimit
        {
            get { return _hasReachedLoanLimit; }
            set
            {
                SetProperty(ref _hasReachedLoanLimit, value);
                CanComplete = !(value || HasReachedFineLimit || HasOverDueLoans);
            }
        }

        private bool _hasReachedFineLimit;

        public bool HasReachedFineLimit
        {
            get { return _hasReachedFineLimit; }
            set
            {
                SetProperty(ref _hasReachedFineLimit, value);
                CanComplete = !(value || HasReachedLoanLimit || HasOverDueLoans);
            }
        }

        private bool _canComplete;
        public bool CanComplete
        {
            get { return _canComplete; }
            set { SetProperty(ref _canComplete, value); }
        }

        #endregion

        #region Commands

        public ICommand CompleteCommand { get; set; }
        #endregion

    }

    
}
