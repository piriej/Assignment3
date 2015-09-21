using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Library.Daos;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using NSubstitute.Core;
using Xunit.Extensions;

namespace UnitTests.DAOTests
{
    public class MemberDAOTests
    {
        public MemberDAO Dao { get; set; }

        public MemberDAOTests()
        {
            Dao = new MemberDAO();
        }

        [Theory, AutoNSubstituteData]
        public void MemberList_WhenConstructed_ShouldBeEmpty()
        {
            Dao.MemberList.Should().BeEmpty();
        }

        [Theory, AutoNSubstituteData]
        public void AddMember_WithValidData_AddsAMemberToTheList(IMember member)
        {
            // Act
            Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);

            // Assert
            EquivelentMembers(Dao.MemberList, member).Should().BeTrue();
        }

        public void FindMembersByLastName_WithAMatchingRecord_FindsTheMember(List<IMember> members)
        {
            // Arrange
            string aLastName = null;
            foreach (var member in members)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                aLastName = member.LastName;
            }
       
            //Act
            var foundMembers = Dao.FindMembersByLastName(aLastName);
            
            // Assert
            foundMembers.Count.Should().Be(1);
            EquivelentMembers(foundMembers, members.LastOrDefault()).Should().BeTrue();
        }

        public void FindMembersByLastName_WithMultipleMatchingRecords_FindsTheMembers(List<IMember> members)
        {
            // Arrange
            string aLastName = null;
            foreach (var member in members)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                aLastName = member.LastName;
            }
       
            //Act
            var foundMembers = Dao.FindMembersByLastName(aLastName);
            
            // Assert
            foundMembers.Count.Should().Be(2);
            EquivelentMembers(foundMembers, members.FirstOrDefault()).Should().BeTrue();
            EquivelentMembers(foundMembers, members.LastOrDefault()).Should().BeTrue();
        }

        bool EquivelentMembers(List<IMember> memberList, IMember member)
        {
           return memberList.Any(m =>
                m.FirstName.Equals(member.FirstName) &&
                m.LastName.Equals(member.LastName) &&
                m.ContactPhone.Equals(member.ContactPhone) &&
                m.EmailAddress.Equals(member.EmailAddress));
        }
    }
}
