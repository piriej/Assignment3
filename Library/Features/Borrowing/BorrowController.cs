using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Library.Features.Borrowing;
using Library.Features.CardReader;
using Library.Features.MainWindow;
using Library.Features.ScanBook;
using ICardReader = Library.Interfaces.Hardware.ICardReader;

namespace Library.Controllers.Borrow
{
    public interface IBorrowController
    {
        IMemberDAO MemberDao { get; set; }
        void ListenToCardReader();
    }

    public static class EborrowStateExtension
    {
        public static EBorrowState Evaluate(this EBorrowState state, bool overdue, bool atLoanLimit, bool hasFines, bool overFineLimit)
        {
            if (overdue || atLoanLimit || hasFines || overFineLimit)
                return EBorrowState.BORROWING_RESTRICTED;
            else
                return EBorrowState.SCANNING_BOOKS;
            //    public enum EBorrowState { CREATED, INITIALIZED, SCANNING_BOOKS, CONFIRMING_LOANS, COMPLETED, BORROWING_RESTRICTED, CANCELLED }
        }
    }

    //public class PageNavigator
    //{
    //    public UserControl
    //}

    public class BorrowController : IBorrowListener, /*ICardReaderEvents,*/ IScannerListener, IBorrowController, IBorrowEvents
    {
        private readonly IMainWindowController _mainWindowController;
        readonly IDisplay _display;
        UserControl _previousDisplay;
        readonly ABorrowControl _ui;
        ICardReader _reader;
        ICardReaderListener _previousReaderListener;
        IScanner _scanner;
        IScannerListener _previousScannerListener;
        IPrinter _printer;

        IMember _borrower;
        int scanCount = 0;
        EBorrowState _state; // EBorrowState.CREATED; - default state

        List<IBook> _bookList;
        List<ILoan> _loanList;

        #region Injected properties
        IBookDAO BookDao { get; set; }
        ILoanDAO LoanDao { get; set; }
        public IMemberDAO MemberDao { get; set; }
        public IMainWindowController MainWindowController { get; set; }
        //protected Func<UserControl, > NavigateTo { get; private set; }
        #endregion


        //public ICardReader CardReader { get; set; }
        public ICardReaderEvents CardReaderEvents { get; set; }
        public IBorrowingViewModel ViewModel { get; set; }

        public event EventHandler<EBorrowState> setEnabled;
        //public event EventHandler<EBorrowState> NotifyBorrowState;

        // TODO fix resolution to remove this constructor.
        public BorrowController( IMemberDAO memberDao,  IMainWindowController mainWindowController/*, ICardReaderEvents cardReaderEvents*/) //: this(null, reader,null,null,cardReaderEvents)
        {
            MainWindowController = mainWindowController;
            // _state = EBorrowState.CREATED; - default state


        }

        //public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer,
        //                            ICardReaderEvents cardReaderEvents)
        //{
        //    CardReaderEvents = cardReaderEvents;
        //    _display = display;
        //    _reader = reader;
        //    _scanner = scanner;
        //    _printer = printer;

        //    _ui = new BorrowControl(this);

        //    _state = EBorrowState.CREATED;
        //    CardReaderEvents.NotifyCardSwiped += OnCardSwipe;
        //}


        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            _previousDisplay = _display.Display;
            Console.WriteLine("BorrowController Initialising, previous display = " + _previousDisplay);
            _display.Display = _ui;

        }

        public void ListenToCardReader()
        {
            CardReaderEvents.NotifyCardSwiped += OnCardSwipe;
        }

        public void cancelled()
        {
            close();
        }

        // Keeping this function so I don't change too much.
        public void OnCardSwipe(object sender, CardReaderModel cardReaderModel)
        {
            int memberId;
            bool parsed = Int32.TryParse(cardReaderModel.BorrowerId, out memberId);
            cardSwiped(memberId);
        }

        // Keeping this function so I don't change too much, should all be in oncardswipe.
        public void cardSwiped(int memberID)
        {
            MainWindowController.NavigateTo<ScanBookView>();
            var borrower = MemberDao.GetMemberByID(memberID);
            if (borrower == null)
            {
                // Notify the user that couldn't be found.
                return;
            }

            var overdue = borrower.HasOverDueLoans;
            var atLoanLimit = borrower.HasReachedLoanLimit;
            var hasFines = borrower.HasFinesPayable;
            var overFineLimit = borrower.HasReachedFineLimit;

            setState(_state.Evaluate(overdue, atLoanLimit, hasFines, overFineLimit));

            
            MainWindowController.NavigateTo<ScanBookView>();
            //DisplayMemberDetails(borrower.ID, borrower.FirstName + " " + borrower.LastName, borrower.ContactPhone);

        }


        public void bookScanned(int barcode)
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void scansCompleted()
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void loansConfirmed()
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void loansRejected()
        {
            throw new ApplicationException("Not implemented yet");
        }


        private void setState(EBorrowState state)
        {
            _state = state;
            setEnabled?.Invoke(this, _state);
        }


        public void close()
        {
            _display.Display = _previousDisplay;
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
