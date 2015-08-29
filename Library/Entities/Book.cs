using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Book : IBook
    {
        public Book(string author, string title, string callNumber, int bookID)
        {
            if (!sane(author, title, callNumber, bookID))
            {
                throw new ArgumentException("Member: constructor : bad parameters");
            }
            this.author = author;
            this.title = title;
            this.callNumber = callNumber;
            this.id = bookID;
            this.state = BookState.AVAILABLE;
            this.loan = null;
        }

        private bool sane(string author, string title, string callNumber, int bookID)
        {
            return (!string.IsNullOrEmpty(author) &&
                      !string.IsNullOrEmpty(title) &&
                      !string.IsNullOrEmpty(callNumber) &&
                      bookID > 0
                    );
        }


        public void Borrow(ILoan loan)
        {
            if (loan == null)
            {
                throw new ArgumentException("Book: borrow : Bad parameter: loan cannot be null");
            }
            if (!(state == BookState.AVAILABLE))
            {
                string mesg = String.Format("Illegal operation in state : {0}", state);
                throw new ApplicationException(mesg);
            }
            this.loan = loan;
            state = BookState.ON_LOAN;

        }
        private ILoan loan;
        public ILoan Loan
        {
            get { return loan; }
        }



        public void ReturnBook(bool damaged)
        {
            if (!(state == BookState.ON_LOAN || state == BookState.LOST))
            {
                throw new ApplicationException(String.Format("Illegal operation in state : {0}", state));
            }
            loan = null;
            if (damaged)
            {
                state = BookState.DAMAGED;
            }
            else
            {
                state = BookState.AVAILABLE;
            }
        }



        public void Lose()
        {
            if (!(state == BookState.ON_LOAN))
            {
                throw new ApplicationException(String.Format("Illegal operation in state : {0}", state));
            }
            state = BookState.LOST;
        }

        public void Repair()
        {
            if (!(state == BookState.DAMAGED))
            {
                throw new ApplicationException(String.Format("Illegal operation in state : {0}", state));
            }
            state = BookState.AVAILABLE;
        }

        public void Dispose()
        {
            if (!(state == BookState.AVAILABLE ||
                  state == BookState.DAMAGED ||
                  state == BookState.LOST))
            {
                throw new ApplicationException(String.Format("Illegal operation in state : {0}", state));
            }
            state = BookState.DISPOSED;
        }

        private BookState state;
        public BookState State
        {
            get { return state; }
        }

        private string author;
        public string Author
        {
            get { return author; }
        }

        private string title;
        public string Title
        {
            get { return title; }
        }

        private string callNumber;
        public string CallNumber
        {
            get { return callNumber; }
        }

        private int id;
        public int ID
        {
            get { return id; }
        }

        public override string ToString()
        {
            string cr = Environment.NewLine;
            return String.Format("{1,-20}\t{2} {0}{3,-20}\t{4} {0}{5,-20}\t{6} {0}{7,-20}\t{8}",
                                  cr,
                                  "Id:          ", id,
                                  "Call Number: ", callNumber,
                                  "Author:      ", author,
                                  "Title:       ", title);

        }
    }
}
