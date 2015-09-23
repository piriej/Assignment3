using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Controllers.Borrow;

namespace Library.Features.MainWindow
{
    public interface IMainWindowController
    {
    }

    public class MainWindowController : IMainWindowController
    {
        IBorrowController BorrowController { get; set; }

        public MainWindowController(IBorrowController borrowController)
        {
            BorrowController = borrowController;
        }
    }
}
