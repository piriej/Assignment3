using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Loan : ILoan
    {
        private IMember borrower;
        private IBook book;
        private DateTime borrowDate;
        private DateTime dueDate;
        private LoanState state;


        public Loan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate)
        {
            if (!sane(book, borrower, borrowDate, dueDate))
            {
                throw new ArgumentException("Loan: constructor : bad parameters");
            }
            this.book = book;
            this.borrower = borrower;
            this.borrowDate = borrowDate;
            this.dueDate = dueDate;
            this._id = 0;
            this.state = LoanState.PENDING;

        }


        private bool sane(IBook book, IMember borrower, DateTime borrowDate, DateTime returnDate)
        {
            return (book != null &&
                      borrower != null &&
                      borrowDate != null &&
                      returnDate != null &&
                      DateTime.Compare(borrowDate, returnDate) <= 0);
        }


        public void Commit(int id)
        {
            if (!(state == LoanState.PENDING))
            {
                throw new ApplicationException(
                        String.Format("Loan : commit : incorrect state transition  : {0} -> {1}\n",
                                state, LoanState.CURRENT));
            }
            if (id <= 0)
            {
                throw new ApplicationException(
                        String.Format("Loan : commit : loan ID must be positive integer\n"));
            }
            _id = id;
            state = LoanState.CURRENT;
            book.Borrow(this);
            borrower.AddLoan(this);
        }


        public void Complete()
        {
            if (!(state == LoanState.CURRENT ||
                  state == LoanState.OVERDUE))
            {
                throw new ApplicationException(
                        String.Format("Loan : complete : incorrect state transition  : {0} -> {1}\n",
                                state, LoanState.COMPLETE));
            }
            state = LoanState.COMPLETE;
        }

        public bool IsOverDue
        {
            get
            {
                return (state == LoanState.OVERDUE);
            }
        }


        public bool CheckOverDue(DateTime currentDate)
        {
            if (!(state == LoanState.CURRENT || state == LoanState.OVERDUE))
            {
                throw new ApplicationException(
                        String.Format("Loan : checkOverDue : incorrect state transition  :{0} -> {1}\n",
                                state, LoanState.OVERDUE));
            }
            if (DateTime.Compare(currentDate, dueDate) > 0)
            {
                state = LoanState.OVERDUE;
            }
            return IsOverDue;
        }

        public IMember Borrower
        {
            get { return borrower; }
        }

        public IBook Book
        {
            get { return book; }
        }

        private int _id;
        public int ID
        {
            get { return _id; }
        }

        public LoanState State
        {
            get { return state; }
        }

        LoanState ILoan.State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public override string ToString()
        {
            string cr = Environment.NewLine;
            return (String.Format("{1,-20}\t{2} " +
                                  "{0}{3,-20}\t{4} " +
                                  "{0}{5,-20}\t{6} " +
                                  "{0}{7,-20}\t{8} {9} " +
                                  "{0}{10,-20}\t{11:d} " +
                                  "{0}{12,-20}\t{13:d}",
                    cr,
                    "Loan ID:     ", _id,
                    "Author:      ", book.Author,
                    "Title:       ", book.Title,
                    "Borrower:    ", borrower.FirstName, borrower.LastName,
                    "Borrow Date: ", borrowDate,
                    "Due Date:    ", dueDate));
        }

    }
}
