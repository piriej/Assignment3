using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Library.Daos;
using Library.Entities;
using Library.Interfaces.Entities;
using Xunit.Extensions;

namespace UnitTests.DAOTests
{
    public class MemberDAOTests
    {
        public MemberDAO Dao { get; set; }

        public MemberDAOTests()
        {
            var memberHelper = new MemberHelper();
            Dao = new MemberDAO(memberHelper);
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

        [Theory, AutoNSubstituteData]
        public void FindMembersByLastName_WithAMatchingRecord_FindsTheMember(List<Member> members)
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

        [Theory, AutoNSubstituteData]
        public void FindMembersByLastName_WithMultipleMatchingRecords_FindsTheMembers(List<Member> members)
        {
            // Arrange
            IMember lastMember = null;

            // Create repeated members;
            var repeatedMembers = Enumerable.Repeat(members, 2).SelectMany(t => t).ToList();
            foreach (var member in repeatedMembers)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                lastMember = member;
            }

            //Act
            var foundMembers = Dao.FindMembersByLastName(lastMember.LastName);

            // Assert
            foundMembers.Count.Should().Be(2);
            EquivelentMembers(foundMembers, lastMember).Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void FindMembersByEmailAddress_WithAMatchingRecord_FindsTheMember(List<Member> members)
        {
            // Arrange
            string emailAddress = null;
            foreach (var member in members)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                emailAddress = member.EmailAddress;
            }

            //Act
            var foundMembers = Dao.FindMembersByEmailAddress(emailAddress);

            // Assert
            foundMembers.Count.Should().Be(1);
            EquivelentMembers(foundMembers, members.LastOrDefault()).Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void FindMembersByNames_WithMultipleMatchingRecords_FindsTheMembers(List<Member> members)
        {
            IMember lastMember = null;

            // Arrange
            var repeatedMembers = Enumerable.Repeat(members, 2).SelectMany(t => t).ToList();

            foreach (var member in repeatedMembers)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                lastMember = member;
            }

            //Act
            var foundMembers = Dao.FindMembersByNames(lastMember.FirstName, lastMember.LastName);

            // Assert
            foundMembers.Count.Should().Be(2);
            EquivelentMembers(foundMembers, lastMember).Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void FindMembersByNames_WithAMatchingRecord_FindsTheMember(List<Member> members)
        {
            // Arrange
            string aLastName = null;
            string aFirstName = null;
            foreach (var member in members)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                aLastName = member.LastName;
                aFirstName = member.FirstName;
            }

            //Act
            var foundMembers = Dao.FindMembersByNames(aFirstName, aLastName);

            // Assert
            foundMembers.Count.Should().Be(1);
            EquivelentMembers(foundMembers, members.LastOrDefault()).Should().BeTrue();
        }

        [Theory, AutoNSubstituteData]
        public void FindMembersByEmailAddress_WithMultipleMatchingRecords_FindsTheMembers(List<Member> members)
        {
            // Arrange
            var repeatedMembers = Enumerable.Repeat(members, 2).SelectMany(t => t).ToList();
            IMember lastMember = null;

            foreach (var member in repeatedMembers)
            {
                Dao.AddMember(member.FirstName, member.LastName, member.ContactPhone, member.EmailAddress);
                lastMember = member;
            }

            //Act
            var foundMembers = Dao.FindMembersByEmailAddress(lastMember.EmailAddress);

            // Assert
            foundMembers.Count.Should().Be(2);
            EquivelentMembers(foundMembers, lastMember).Should().BeTrue();
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
