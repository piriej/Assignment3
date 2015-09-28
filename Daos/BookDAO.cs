using System;
using System.Collections.Generic;
using System.Linq;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class BookDAO : IBookDAO
    {
        private readonly IBookHelper _helper;
        private readonly List<IBook> _books;

        public List<IBook> BookList => _books;

        public BookDAO(IBookHelper helper)
        {
            if (helper == null) throw new ArgumentException("bookhelper is null.");
            _helper = helper;
            _books = new List<IBook>();
        }

        public IBook AddBook(string author, string title, string callNo)
        {
            var nextid = _books.Count;
            var book = _helper.MakeBook(author, title, callNo, nextid);

            _books.Add(book);
            return book;
        }

        public IBook GetBookByID(int id)
        {
            return _books.Count > id ? _books[id] : null;
        }

        public List<IBook> FindBooksByAuthor(string author)
        {
            return _books.Where(book => book.Author == author).ToList();
        }

        public List<IBook> FindBooksByTitle(string title)
        {
            return _books.Where(book => book.Title == title).ToList();
        }

        public List<IBook> FindBooksByAuthorTitle(string author, string title)
        {
            return _books.Where(book => book.Title == title && book.Author == author).ToList();
        }

        public void AddBooks(List<IBook> books)
        {
            _books.AddRange(books);
        }
    }
}
