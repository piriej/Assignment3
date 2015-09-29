using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Library.Entities;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class LoanDAO : ILoanDAO
    {
        readonly List<ILoan> _loans;
        readonly ILoanHelper _loanHelper;

        public List<ILoan> LoanList => _loans;

        public LoanDAO()
        {
            _loans = new List<ILoan>();
        }

        public LoanDAO(ILoanHelper loanHelper) : this()
        {
            if (loanHelper == null)
                throw new ArgumentException("Error: loan helper can't be null on construction.");

            _loanHelper = loanHelper;
           
        }

        public void CommitLoan(ILoan loan)
        {
            if (loan == null)
                throw new ArgumentException("Error: loan can\'t be null.");

            loan.Commit(_loans.Count);
            _loans.Add(loan);
        }

        public ILoan CreateLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate)
        {
            if (DateTime.Compare(borrowDate, dueDate) > 0)
                throw new ArgumentException("Error borrowing date cannot be after dueDate.");

            Debug.Assert(borrower != null); // Todo: XXX Borrower null?

            if (book == null || borrower == null)
                throw new ArgumentException("Error null parameters found.");

            return _loanHelper.MakeLoan(book, borrower, borrowDate, dueDate);
        }

        public ILoan GetLoanByID(int id)
        {
            return _loans.Find(loan => loan.ID == id);
        }

        public ILoan GetLoanByBook(IBook book)
        {
            if (book == null)
                throw new ArgumentException("Error: book can\'t be null.");

            return _loans.FirstOrDefault(loan => loan.Book.Equals(book));
        }

        public List<ILoan> FindLoansByBorrower(IMember borrower)
        {
            if (borrower == null)
                throw new ArgumentException("Error: member can\'t be null.");

            return _loans.Where(loan => loan.Borrower.ID == borrower.ID).ToList();
        }

        public List<ILoan> FindLoansByBookTitle(string title)
        {
            if (title == null)
                throw new ArgumentException("Error: title can\'t be null.");

            return _loans.Where(loan => loan.Book.Title == title).ToList();
        }

        public List<ILoan> FindOverDueLoans()
        {
            return _loans.Where(loan => loan.IsOverDue).ToList();
        }

        public void UpdateOverDueStatus(DateTime currentDate)
        {
            _loans.ForEach(loan => loan.CheckOverDue(currentDate));
        }

        public void AddLoan(List<ILoan> loans)
        {
            _loans.AddRange(loans);
        }
    }
}
