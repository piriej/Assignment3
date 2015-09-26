using System;
using System.Collections.Generic;
using System.Linq;
using Library.Entities;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class MemberDAO : IMemberDAO
    {
        public MemberDAO()
        {
            MemberList = new List<IMember>();
        }

        public MemberDAO(List<IMember> mockData )
        {
            MemberList = mockData;
        }

        public List<IMember> MemberList { get; } 

        public IMember AddMember(string firstName, string lastName, string contactPhone, string emailAddress)
        {
            var member = new Member(MemberList.Count, firstName, lastName, contactPhone, emailAddress);

            MemberList.Add(member);
            return member;
        }

        public IMember GetMemberByID(int id)
        {
            return MemberList.FirstOrDefault(members => members.ID == id);
        }

        public List<IMember> FindMembersByLastName(string lastName)
        {
            return MemberList.Where(members => members.LastName == lastName).ToList();
        }

        public List<IMember> FindMembersByEmailAddress(string emailAddress)
        {
            return MemberList.Where(members => members.EmailAddress.Equals(emailAddress)).ToList();
        }

        public List<IMember> FindMembersByNames(string firstName, string lastName)
        {
            return MemberList.Where(members => members.FirstName.Equals(firstName) && members.LastName.Equals(lastName)).ToList();
        }
    }
}
