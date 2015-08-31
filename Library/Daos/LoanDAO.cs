using Library.Interfaces.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Entities;

namespace Library.Daos 
{
    public class LoanDAO : ILoanDAO
    {
        private ILoanHelper helper;
        private Dictionary<int, ILoan> loanDict;


        public LoanDAO(ILoanHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(
                    String.Format("LoanDAO : constructor : helper cannot be null."));
            }
            this.helper = helper;
            this.loanDict = new Dictionary<int, ILoan>();
            this.NextID = 1;
        }




        public ILoan CreateLoan(IMember borrower, IBook book, DateTime borrowDate, DateTime dueDate)
        {
            if (borrower == null || book == null || borrowDate == null || dueDate == null)
            {
                throw new ArgumentException(
                    String.Format("LoanMapDAO : CreatePendingLoan : parameters cannot be null."));
            }
            if (DateTime.Compare(borrowDate, dueDate) > 0)
            {
                throw new ArgumentException(
                    String.Format("LoanDAO : createPendingLoan : borrowDate cannot be after dueDate."));
            }
            ILoan loan = helper.MakeLoan(book, borrower, borrowDate, dueDate);
            return loan;
        }




        public void CommitLoan(ILoan loan)
        {
            if (loan == null)
            {
                throw new ArgumentException(
                    String.Format("LoanDAO : commitLoans : loan cannot be null."));
            }
            loan.Commit(NextID);
            loanDict.Add(loan.ID, loan);
        }


        public ILoan GetLoanByID(int id)
        {
            if (loanDict.ContainsKey(id))
            {
                return loanDict[id];
            }
            return null;
        }


        public ILoan GetLoanByBook(IBook book)
        {
            if (book == null)
            {
                throw new ArgumentException(
                    String.Format("LoanMapDAO : getLoanByBook : book cannot be null."));
            }
            foreach (ILoan loan in loanDict.Values)
            {
                IBook tempBook = loan.Book;
                if (book.Equals(tempBook))
                {
                    return loan;
                }
            }
            return null;
        }

        public List<ILoan> LoanList
        {
            get
            {
                List<ILoan> list = new List<ILoan>();
                Dictionary<int, ILoan>.ValueCollection tloan = loanDict.Values;
                foreach (ILoan b in tloan)
                {
                    list.Add(b);
                }
                return list;
            }
        }


        public List<ILoan> FindLoansByBorrower(IMember borrower)
        {
            List<ILoan> list = new List<ILoan>();
            Dictionary<int, ILoan>.ValueCollection tloan = loanDict.Values;
            foreach (ILoan b in tloan)
            {
                if (b.Borrower.Equals(borrower))
                {
                    list.Add(b);
                }
            }
            return list;
        }


        public List<ILoan> FindLoansByBookTitle(string title)
        {
            List<ILoan> list = new List<ILoan>();
            Dictionary<int, ILoan>.ValueCollection tloan = loanDict.Values;
            foreach (ILoan b in tloan)
            {
                if (b.Book.Title.Equals(title))
                {
                    list.Add(b);
                }
            }
            return list;
        }

        public void UpdateOverDueStatus(DateTime currentDate)
        {
            Dictionary<int, ILoan>.ValueCollection tloan = loanDict.Values;
            foreach (ILoan loan in tloan)
            {
                loan.CheckOverDue(currentDate);
            }
        }


        public List<ILoan> FindOverDueLoans()
        {
            List<ILoan> list = new List<ILoan>();
            Dictionary<int, ILoan>.ValueCollection tloan = loanDict.Values;
            foreach (ILoan loan in tloan)
            {
                if (loan.IsOverDue)
                {
                    list.Add(loan);
                }
            }
            return list;
        }

        private int _nextID;
        private int NextID
        {
            get { return _nextID++; }
            set { _nextID = value; }
        }

    }
}
