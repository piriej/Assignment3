using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Controls.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Library.Controllers.Borrow
{
    class BorrowController : IBorrowListener, ICardReaderListener, IScannerListener
    {
        private IDisplay _display;
        private UserControl _previousDisplay;
        private ABorrowControl _ui;
        private ICardReader _reader;
        private ICardReaderListener _previousReaderListener;
        private IScanner _scanner;
        private IScannerListener _previousScannerListener;
        private IPrinter _printer;

        private IBookDAO _bookDAO;
        private ILoanDAO _loanDAO;
        private IMemberDAO _memberDAO;

        private IMember _borrower;
        private int scanCount = 0;
        private EBorrowState _state;

        private List<IBook> _bookList;
        private List<ILoan> _loanList;


        public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer,
                                    IBookDAO bookDAO, ILoanDAO loanDAO, IMemberDAO memberDAO)
        {
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;

            _bookDAO = bookDAO;
            _loanDAO = loanDAO;
            _memberDAO = memberDAO;

            _ui = new BorrowControl(this);
        }


        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            _previousReaderListener = _reader.Listener;
            _previousScannerListener = _scanner.Listener;
            _previousDisplay = _display.Display;
            Console.WriteLine("BorrowController Initialising, previous display = " + _previousDisplay);
            _display.Display = _ui;
            setState(EBorrowState.INITIALIZED);
        }


        public void cancelled()
        {
            setState(EBorrowState.CANCELLED);
        }

        public void cardSwiped(int memberID)
        {
            Console.WriteLine("detected card swipe: {0}", memberID);
            _scanner.Enabled = true;
            _reader.Enabled = false;

            if (_state != EBorrowState.INITIALIZED)
            {
                throw new ApplicationException(
                        String.Format("BorrowUC_CTL : cardSwiped : illegal operation in state: {0}", _state));
            }
            _borrower = _memberDAO.GetMemberByID(memberID);
            if (_borrower == null)
            {
                _ui.DisplayErrorMessage(String.Format("Member ID {0} not found", memberID));
                _reader.Enabled = true;
                return;
            }
            bool overdue = _borrower.HasOverDueLoans;
            bool atLoanLimit = _borrower.HasReachedLoanLimit;
            bool hasFines = _borrower.HasFinesPayable;
            bool overFineLimit = _borrower.HasReachedFineLimit;
            bool borrowing_restricted = (overdue || atLoanLimit || overFineLimit);

            if (borrowing_restricted)
            {
                setState(EBorrowState.BORROWING_RESTRICTED);
            }
            else
            {
                setState(EBorrowState.SCANNING_BOOKS);
            }

            //display member details
            int mID = _borrower.ID;
            string mName = _borrower.FirstName + " " + _borrower.LastName;
            String mContact = _borrower.ContactPhone;
            _ui.DisplayMemberDetails(mID, mName, mContact);

            if (overdue)
            {
                _ui.DisplayOverDueMessage();
            }
            if (atLoanLimit)
            {
                _ui.DisplayAtLoanLimitMessage();
            }
            if (hasFines)
            {
                float amountOwing = _borrower.FineAmount;
                _ui.DisplayOutstandingFineMessage(amountOwing);
            }

            if (overFineLimit)
            {
                Console.WriteLine("State: " + _state);
                float amountOwing = _borrower.FineAmount;
                _ui.DisplayOverFineLimitMessage(amountOwing);
            }

            //display existing loans
            foreach (ILoan ln in _borrower.Loans)
            {
                _ui.DisplayExistingLoan(ln.ToString());
            }
        }

        public void bookScanned(int barcode)
        {
            Console.WriteLine("bookScanned: got " + barcode);
            if (_state != EBorrowState.SCANNING_BOOKS)
            {
                throw new ApplicationException(
                        String.Format("BorrowUC_CTL : bookScanned : illegal operation in state: {0}", _state));
            }
            _ui.DisplayErrorMessage("");
            IBook book = _bookDAO.GetBookByID(barcode);
            if (book == null)
            {
                _ui.DisplayErrorMessage(
                    String.Format("Book {0} not found", barcode));
                return;
            }

            if (book.State != BookState.AVAILABLE)
            {
                _ui.DisplayErrorMessage(
                    String.Format("Book {0} is not available: {1}", book.ID, book.State));
                return;
            }
            if (_bookList.Contains(book))
            {
                _ui.DisplayErrorMessage(
                    String.Format("Book {0} already scanned: ", book.ID));
                return;
            }

            DateTime borrowDate = DateTime.Now;
            TimeSpan loanPeriod = new TimeSpan(LoanConstants.LOAN_PERIOD, 0, 0, 0);
            DateTime dueDate = borrowDate.Add(loanPeriod);
            ILoan loan = _loanDAO.CreateLoan(_borrower, book, borrowDate, dueDate);

            scanCount++;
            _bookList.Add(book);
            _loanList.Add(loan);
            Console.WriteLine("scancount = {0}", scanCount);

            //display current book
            _ui.DisplayScannedBookDetails(book.ToString());
            //display pending loans
            _ui.DisplayPendingLoan(buildLoanListDisplay(_loanList));


            if (scanCount >= MemberConstants.LOAN_LIMIT)
            {
                setState(EBorrowState.CONFIRMING_LOANS);
            }
        }

        public void scansCompleted()
        {
            Console.WriteLine("detected scans completed");
            setState(EBorrowState.CONFIRMING_LOANS);
        }

        public void loansConfirmed()
        {
            setState(EBorrowState.COMPLETED);
        }

        public void loansRejected()
        {
            setState(EBorrowState.SCANNING_BOOKS);
        }


        private void setState(EBorrowState state)
        {
           Console.WriteLine("Setting state: " + state);

            this._state = state;
            _ui.State = state;

            switch (state)
            {
                case EBorrowState.INITIALIZED:
                    _reader.Listener = this;
                    _scanner.Listener = this;
                    _reader.Enabled = true;
                    _scanner.Enabled = false;
                    break;

                case EBorrowState.SCANNING_BOOKS:
                    _reader.Enabled = false;
                    _scanner.Enabled = true;
                    _bookList = new List<IBook>();
                    _loanList = new List<ILoan>();
                    scanCount = _borrower.Loans.Count;

                    _ui.DisplayScannedBookDetails("");
                    _ui.DisplayPendingLoan("");

                    break;

                case EBorrowState.BORROWING_RESTRICTED:
                    _reader.Enabled = false;
                    _scanner.Enabled = false;
                    _ui.DisplayErrorMessage(String.Format("Member {0} cannot borrow at this time.", _borrower.ID));
                    break;

                case EBorrowState.CONFIRMING_LOANS:
                    _reader.Enabled = false;
                    _scanner.Enabled = false;
                    //display pending loans
                    _ui.DisplayConfirmingLoan(buildLoanListDisplay(_loanList));
                    break;

                case EBorrowState.COMPLETED:
                    _reader.Enabled = false;
                    _scanner.Enabled = false;
                    foreach (ILoan loan in _loanList)
                    {
                        _loanDAO.CommitLoan(loan);
                    }
                    _printer.print(buildLoanListDisplay(_loanList));
                    close();
                    return;

                case EBorrowState.CANCELLED:
                    _reader.Enabled = false;
                    _scanner.Enabled = false;
                    close();
                    return;

                default:
                    throw new ApplicationException("Unknown state");
            }
        }


        public void close()
        {
            _display.Display  = _previousDisplay;
            _reader.Listener  = _previousReaderListener;
            _scanner.Listener = _previousScannerListener;
        }


        private string buildLoanListDisplay(List<ILoan> loanList)
        {
            StringBuilder bld = new StringBuilder();
            foreach (ILoan loan in loanList)
            {
                if (bld.Length > 0)
                {
                    bld.Append("\n\n");
                }
                bld.Append(loan.ToString());
            }
            return bld.ToString();
        }
    }
}
