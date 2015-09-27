using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public interface IBook
    {

        void Borrow(ILoan loan);

        ILoan Loan
        {
            get;
        }

        void ReturnBook(bool damaged);

        void Lose();

        void Repair();

        void Dispose();

        BookState State
        {
            get;
        }

        string Author
        {
            get;
        }

        string Title
        {
            get;
        }

        string CallNumber
        {
            get;
        }

        int ID
        {
            get;
        }
    }
}
