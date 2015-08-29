using Library.Controllers.Borrow;
using Library.Controls;
using Library.Interfaces.Controllers;
using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Controllers
{
    class MainMenuController : IMainMenuListener
    {
        private IDisplay _display;
        private ICardReader _reader;
        private IScanner _scanner;
        private IPrinter _printer;

        public MainMenuController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer)
        {
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;
        }

        public void initialise()
        {
            MainMenuControl mainControl = new MainMenuControl(this);
            _display.Display = mainControl;
        }

        public void borrowBook()
        {
            BorrowController borrowController = new BorrowController(_display, _reader, _scanner, _printer);
            borrowController.initialise();
        }
    }
}
