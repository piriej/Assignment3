using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Attributed;
using Library.Interfaces.Entities;

namespace Library.Entities
{
    public class Book : IBook
    {
        ILoan _loan;
        BookState _state;
        readonly int _id;
        readonly string _author;
        readonly string _title;
        readonly string _callNumber;

        public ILoan Loan => _loan;

        public BookState State => _state;

        public string Author => _author;

        public string Title => _title;

        public string CallNumber => _callNumber;

        public int ID => _id;

        public Book([WithKey("Name")]string author, [WithKey("Title")]string title, [WithKey("Number")]string callNumber, int bookId)
        {
            if (string.IsNullOrEmpty(author) 
                || string.IsNullOrEmpty(title) 
                || string.IsNullOrEmpty(callNumber) 
                || bookId <= 0)
                throw new ArgumentException("Error Constructor parameters invalid");

            _author = author;
            _title = title;
            _callNumber = callNumber;
            _id = bookId;
            _state = BookState.AVAILABLE;
        }

        public void Borrow(ILoan loan)
        {
            if (loan == null)
                throw new ArgumentException("Error loan cant be null.");

            if (_state != BookState.AVAILABLE)
                throw new ApplicationException($"Error: Guard against wrong state : {_state}");

            _loan = loan;

            _state = BookState.ON_LOAN;
        }

        public void ReturnBook(bool isDamaged)
        {
            if (_state != BookState.ON_LOAN && this._state != BookState.LOST)
                throw new ApplicationException($"Error: Guard against wrong state: {_state}");

            NothingOnLoan();

            this._state = isDamaged ? BookState.DAMAGED : BookState.AVAILABLE;
        }

        private void NothingOnLoan()
        {
            _loan = null;
        }

        public void Lose()
        {
            if (_state != BookState.ON_LOAN)
                throw new ApplicationException($"Error: Guard against wrong state : {_state}");

            _state = BookState.LOST;
        }

        public void Repair()
        {
            if (_state != BookState.DAMAGED)
                throw new ApplicationException($"Error: Guard against wrong state : {_state}");

            _state = BookState.AVAILABLE;
        }

        public void Dispose()
        {
            if (_state != BookState.DAMAGED && _state != BookState.LOST && _state != BookState.AVAILABLE )
                throw new ApplicationException($"Error: Guard against wrong state : {_state}");
            _state = BookState.DISPOSED;
        }

        public override string ToString()
        {
            var propertyList = new Dictionary<string, string>
            {
                {"Id", _id.ToString()},
                {"CallNumber", CallNumber},
                {"Author", Author},
                {"Title", Title}
            };

            var result = string.Join(Environment.NewLine, propertyList.Select(row => $"{row.Key,-20}:\t{row.Value,5}"));

            return result;
        }
    }
}
