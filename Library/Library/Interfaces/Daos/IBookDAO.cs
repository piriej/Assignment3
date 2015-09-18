using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Daos
{
    public interface IBookDAO
    {
        IBook AddBook(string author, string title, string callNo);

        IBook GetBookByID(int id);

        List<IBook> BookList
        {
            get;
        }

        List<IBook> FindBooksByAuthor(string author);

        List<IBook> FindBooksByTitle(string title);

        List<IBook> FindBooksByAuthorTitle(string author, string title);
    }

}
