using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class LoanHelper : ILoanHelper
    {
        public ILoan MakeLoan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate)
        {
            return new Loan(book, borrower, borrowDate, dueDate);
        }
    }
}
