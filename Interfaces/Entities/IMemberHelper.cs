using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public interface IMemberHelper
    {
        IMember MakeMember(string firstName, string lastName, string contactPhone, string emailAddress, int id);
    }

}
