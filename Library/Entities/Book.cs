using System;
using Library.Interfaces.Entities;

namespace Library.Entities
{
    public class Book : IBook
    {
        public Book(int id, string author, string title, string callNumber)
        {
            ID = id;
            Author = author;
            Title = title;
            CallNumber = callNumber;
        }

        public void Borrow(ILoan loan)
        {
            throw new NotImplementedException();
        }

        public ILoan Loan { get; }
        public void ReturnBook(bool damaged)
        {
            throw new NotImplementedException();
        }

        public void Lose()
        {
            throw new NotImplementedException();
        }

        public void Repair()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public BookState State { get; }
        public string Author { get; }
        public string Title { get; }
        public string CallNumber { get; }
        //public string /*CallNumber*/ { get; }
        public int ID { get; }
    }
}
