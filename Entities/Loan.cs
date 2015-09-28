using System;
using System.Collections.Generic;
using System.Linq;
using Library.Interfaces.Entities;

namespace Library.Entities
{
    public class Loan : ILoan
    {
        readonly IMember _borrower;
        readonly DateTime _borrowDate;
        readonly DateTime _dueDate;
        LoanState _state;
        int _id;
        private readonly IBook _book;

        public bool IsOverDue => _state == LoanState.OVERDUE;

        public IMember Borrower => _borrower;

        public IBook Book => _book;

        public int ID => _id;

        public LoanState State => _state;


        public Loan(IBook book, IMember borrower, DateTime borrowDate, DateTime dueDate)
        {

            if (book == null || borrower == null ||
                DateTime.Compare(borrowDate, dueDate) >= 0)
                throw new ArgumentException("Error: in constructor parameter list.");

            _id = 0;
            _state = LoanState.PENDING;
            _book = book;
            _borrower = borrower;
            _borrowDate = borrowDate;
            _dueDate = dueDate;
        }

        public void Commit(int id)
        {
            if (id < 0)
                throw new ArgumentException("Error: Loan ID must be positive integer");

            if (_state != LoanState.PENDING)
                throw new ApplicationException($"Error can't move from state : {_state} ==> {LoanState.CURRENT}");

            _state = LoanState.CURRENT;

            _id = id;

            Book.Borrow(this);

            _borrower.AddLoan(this);
        }

        public void Complete()
        {
            if (!(_state == LoanState.OVERDUE || _state == LoanState.CURRENT))
                throw new ApplicationException(
                    $"Error : Cant transition from state: {_state} ==> {LoanState.COMPLETE}");

            _state = LoanState.COMPLETE;
        }

        public bool CheckOverDue(DateTime currentDate)
        {
            if (!(_state == LoanState.CURRENT || _state == LoanState.OVERDUE))
                throw new ApplicationException($"Error : cant transition from state:{_state} ==> { LoanState.OVERDUE}");

            if (DateTime.Compare(currentDate, _dueDate) > 0)
                _state = LoanState.OVERDUE;

            return IsOverDue;
        }

        public override string ToString()
        {
            var propertyList = new Dictionary<string, string>
            {
                {"Loan ID", Book.Author},
                {"Author", Book.Title},
                {"Borrower", _borrower.FirstName + " " + _borrower.LastName},
                {"Borrow Date", _borrowDate.ToShortDateString()},
                {"Due Date", _dueDate.ToShortDateString()}
            };

            var result = string.Join(Environment.NewLine, propertyList.Select(row => $"{row.Key+":",-20}\t{row.Value,5}"));
            return result;
        }
    }
}
