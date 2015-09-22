using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using Library.Daos;
using Library.Features.CardReader;
using ICardReader = Library.Interfaces.Hardware.ICardReader;

namespace Library.Controllers.Borrow
{
    class BorrowController : IBorrowListener, /*ICardReaderEvents,*/ IScannerListener
    {
        public ICardReaderEvents CardReaderEvents { get; set; }
        readonly IDisplay _display;
        UserControl _previousDisplay;
        readonly ABorrowControl _ui;
        ICardReader _reader;
        ICardReaderListener _previousReaderListener;
        IScanner _scanner;
        IScannerListener _previousScannerListener;
        IPrinter _printer;

        IBookDAO BookDao { get; set; }
        ILoanDAO LoanDao { get; set; }
        public IMemberDAO MemberDao { get; set; }
        //IBookDAO BookDAO;
        //ILoanDAO LoanDAO;
        //IMemberDAO MemberDAO;

        IMember _borrower;
        int scanCount = 0;
        EBorrowState _state;

        List<IBook> _bookList;
        List<ILoan> _loanList;


        public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer,
                                    ICardReaderEvents cardReaderEvents)
        {
            CardReaderEvents = cardReaderEvents;
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;

            _ui = new BorrowControl(this);

            _state = EBorrowState.CREATED;
            CardReaderEvents.NotifyCardSwiped += OnCardSwipe;
        }


        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            _previousDisplay = _display.Display;
            Console.WriteLine("BorrowController Initialising, previous display = " + _previousDisplay);
            _display.Display = _ui;
       
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
            var borrower = MemberDao.GetMemberByID(memberID);
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
            throw new ApplicationException("Not implemented yet");
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
