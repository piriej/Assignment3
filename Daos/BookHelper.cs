using Library.Entities;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class BookHelper : IBookHelper
    {
        public IBook MakeBook(string author, string title, string callNumber, int id)
        {
            return (IBook) new Book(author, title, callNumber, id);
        }
    }
}
