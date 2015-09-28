using Library.Entities;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class MemberHelper : IMemberHelper
    {
        public IMember MakeMember(string firstName, string lastName, string contactPhone, string emailAddress, int id)
        {
            return (IMember)new Member(firstName, lastName, contactPhone, emailAddress, id);
        }
    }
}
