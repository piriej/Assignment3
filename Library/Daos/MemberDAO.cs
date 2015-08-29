using Library.Interfaces.Daos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.Interfaces.Entities;

namespace Library.Daos
{
    public class MemberDAO : IMemberDAO
    {
        private IMemberHelper helper;
        private Dictionary<int, IMember> memberDict;
        private int nextID;

        public MemberDAO(IMemberHelper helper)
        {
            if (helper == null)
            {
                throw new ArgumentException(
                    String.Format("MemberDAO : constructor : helper cannot be null."));
            }
            this.helper = helper;
            this.memberDict = new Dictionary<int, IMember>();
            this.nextID = 1;
        }


        public IMember AddMember(string firstName, string lastName, string contactPhone, string emailAddress)
        {
            int id = NextID;
            IMember mem = helper.MakeMember(firstName, lastName, contactPhone, emailAddress, id);
            memberDict.Add(id, mem);
            return mem;
        }

        public IMember GetMemberByID(int id)
        {
            if (memberDict.ContainsKey(id))
            {
                return memberDict[id];
            }
            return null;
        }

        public List<IMember> MemberList
        {
            get
            {
                List<IMember> list = new List<IMember>();
                Dictionary<int, IMember>.ValueCollection tmem = memberDict.Values;
                foreach (IMember m in tmem)
                {
                    list.Add(m);
                }
                return list;
            }
        }

        public List<IMember> FindMembersByLastName(string lastName)
        {
            return null;
        }

        public List<IMember> FindMembersByEmailAddress(string emailAddress)
        {
            return null;
        }

        public List<IMember> FindMembersByNames(string firstName, string lastName)
        {
            return null;
        }

        private int NextID
        {
            get { return nextID++; }
        }
    }
}
