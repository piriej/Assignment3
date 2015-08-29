using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class BookHelper : IBookHelper
    {
        public IBook MakeBook(string author, string title, string callNumber, int id)
        {
            return new Book(author, title, callNumber, id);
        }

    }
}
