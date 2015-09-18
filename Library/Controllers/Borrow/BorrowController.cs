using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace Library.Controllers.Borrow
{
    class BorrowController : IBorrowListener, ICardReaderListener, IScannerListener
    {
        readonly IDisplay _display;
        UserControl _previousDisplay;
        readonly ABorrowControl _ui;
        ICardReader _reader;
        ICardReaderListener _previousReaderListener;
        IScanner _scanner;
        IScannerListener _previousScannerListener;
        IPrinter _printer;

        IBookDAO _bookDAO;
        ILoanDAO _loanDAO;
        IMemberDAO _memberDAO;

        IMember _borrower;
        int scanCount = 0;
        EBorrowState _state;

        List<IBook> _bookList;
        List<ILoan> _loanList;


        public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer,
                                    IBookDAO bookDAO, ILoanDAO loanDAO, IMemberDAO memberDAO)
        {
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;

            _ui = new BorrowControl(this);

            _state = EBorrowState.CREATED;
        }


        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            _previousDisplay = _display.Display;
            Console.WriteLine("BorrowController Initialising, previous display = " + _previousDisplay);
            _display.Display = _ui;
            //setState(EBorrowState.INITIALIZED);
        }


        public void cancelled()
        {
            close();
            //setState(EBorrowState.CANCELLED);
        }

        public void cardSwiped(int memberID)
        {
            throw new ApplicationException("Not implemented yet");
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
