using Library.Interfaces.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class BookDAO : IBookDAO
    {
        private IBookHelper helper;
        private Dictionary<int, IBook> bookDict;
        private int nextID;

        public BookDAO(IBookHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(
                    String.Format("BookDAO : constructor : helper cannot be null."));
            }
            this.helper = helper;
            this.bookDict = new Dictionary<int, IBook>();
            this.nextID = 1;
        }

        public IBook AddBook(string author, string title, string callNo)
        {
            int id = NextID;
            IBook book = helper.MakeBook(author, title, callNo, id);
            bookDict.Add(id, book);
            return book;
        }

        public IBook GetBookByID(int id)
        {
            if (bookDict.ContainsKey(id))
            {
                return bookDict[id];
            }
            return null;
        }

        public List<IBook> BookList
        {
            get
            {
                List<IBook> list = new List<IBook>();
                Dictionary<int, IBook>.ValueCollection tbook = bookDict.Values;
                foreach (IBook b in tbook)
                {
                    list.Add(b);
                }
                return list;
            }
        }

        public List<IBook> FindBooksByAuthor(string author)
        {
            return null;
        }

        public List<IBook> FindBooksByTitle(string title)
        {
            return null;
        }

        public List<IBook> FindBooksByAuthorTitle(string author, string title)
        {
            return null;
        }

        private int NextID
        {
            get { return nextID++; }
        }


    }
}

