using Library.Controls.Borrow;
using Library.Interfaces.Controllers.Borrow;
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
        private ICardReader _reader;
        private ICardReaderListener _previousReaderListener;
        private IScanner _scanner;
        private IScannerListener _previousScannerListener;
        private IPrinter _printer;


        public BorrowController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer)
        {
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;
        }


        public void initialise()
        {
            Console.WriteLine("BorrowController Initialising");
            SwipeCardControl borrowControl = new SwipeCardControl(this);
            _previousDisplay = _display.Display;
            _display.Display = borrowControl;

            _previousReaderListener = _reader.Listener;
            _reader.Listener = this;
            _reader.Enabled = true;
            _scanner.Enabled = false;

            _previousScannerListener = _scanner.Listener;
            _scanner.Listener = this;
        }


        public void cancelled()
        {
            _display.Display = _previousDisplay;
            _reader.Listener = _previousReaderListener;
            _scanner.Listener = _previousScannerListener;
        }

        public void cardSwiped(int cardData)
        {
            Console.WriteLine("detected card swipe: {0}", cardData);
            ScanBookControl scanBookControl = new ScanBookControl(this);
            //RestrictedControl restrictedControl = new RestrictedControl(this);
            _display.Display = scanBookControl;
            _scanner.Enabled = true;
            _reader.Enabled = false;
        }

        public void bookScanned(int barcode)
        {
            Console.WriteLine("detected book scanned: {0}", barcode);
        }

        public void scansCompleted()
        {
            Console.WriteLine("detected scans completed");
            ConfirmLoanControl scanBookControl = new ConfirmLoanControl(this);
            _display.Display = scanBookControl;
            _scanner.Enabled = false;
        }

        public void loansConfirmed()
        {
            _printer.print("Loan slip printed");
            _display.Display = _previousDisplay;
            _reader.Listener = _previousReaderListener;
            _scanner.Listener = _previousScannerListener;
        }

        public void loansRejected()
        {
            ScanBookControl scanBookControl = new ScanBookControl(this);
            _display.Display = scanBookControl;
            _scanner.Enabled = true;
            _reader.Enabled = false;
        }


    }
}
