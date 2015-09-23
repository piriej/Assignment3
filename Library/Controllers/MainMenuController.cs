using Library.Controllers.Borrow;
using Library.Controls;
using Library.Interfaces.Controllers;
using Library.Interfaces.Daos;
using Library.Interfaces.Hardware;

namespace Library.Controllers
{
    public class MainMenuController : IMainMenuListener
    {
        public ICardReaderEvents CardReaderEvents { get; set; }
        readonly IDisplay _display;
        readonly ICardReader _reader;
        readonly IScanner _scanner;
        readonly IPrinter _printer;

        readonly IBookDAO _bookDao;
        readonly ILoanDAO _loanDao;
        readonly IMemberDAO _memberDao;
                

        public MainMenuController(IDisplay display, ICardReader reader, IScanner scanner, IPrinter printer,
                                    IBookDAO bookDao, ILoanDAO loanDao, IMemberDAO memberDao, ICardReaderEvents cardReaderEvents)
        {
            CardReaderEvents = cardReaderEvents;
            _display = display;
            _reader = reader;
            _scanner = scanner;
            _printer = printer;

            _bookDao = bookDao;
            _loanDao = loanDao;
            _memberDao = memberDao;
        }

        public void initialise()
        {
            MainMenuControl mainMenuControl = new MainMenuControl(this);
            //_display.Display = mainMenuControl;
        }

        public void borrowBook()
        {
            //TODO: Fix borrow controller
            //BorrowController borrowController = new BorrowController(_display, _reader, _scanner, _printer,
            //                                                         _bookDao, _loanDao, _memberDao, CardReaderEvents);  

            //BorrowController borrowController = new BorrowController();
            //borrowController.initialise();
        }
    }
}
