using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Daos
{
    public interface ILoanDAO
    {
        ILoan CreateLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate);

        void CommitLoan(ILoan loan);

        ILoan GetLoanByID(int id);

        ILoan GetLoanByBook(IBook book);

        List<ILoan> LoanList
        {
            get;
        }

        List<ILoan> FindLoansByBorrower(IMember borrower);

        List<ILoan> FindLoansByBookTitle(string title);

        void UpdateOverDueStatus(DateTime currentDate);

        List<ILoan> FindOverDueLoans();
    }

}
