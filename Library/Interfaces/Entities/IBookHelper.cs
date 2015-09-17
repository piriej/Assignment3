using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public interface IBookHelper
    {
        IBook MakeBook(string author, string title, string callNumber, int id);
    }

}
