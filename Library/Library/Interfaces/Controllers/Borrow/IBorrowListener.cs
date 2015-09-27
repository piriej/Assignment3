using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Controllers.Borrow
{

    public interface IBorrowListener
    {
        void cancelled();
        void scansCompleted();
        void loansConfirmed();
        void loansRejected();
    }
}
