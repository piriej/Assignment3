using System;
using FluentAssertions;
using Library.Daos;
using Library.Entities;
using Library.Interfaces.Entities;
using Ploeh.AutoFixture;
using Xunit.Extensions;

namespace UnitTests.DAOTests
{
    class LoanDaoTests
    {
        [Theory, AutoNSubstituteData]
        public void Commit_WithValidLoanId_BorrowsABookAgainstTheCurrentUser(Book book, Member member, LoanHelper loanHelper)
        {
            // Arrange
            loanHelper.MakeLoan(book, member, DateTime.Today, DateTime.Today.AddDays(1));
            var loanDao = new LoanDAO(loanHelper);

            // Act


            // Assert

            // The book should be on loan.
            book.State.Should().Be(BookState.ON_LOAN);

            // The book should be on loan to the member.
            member.Loans.
        }
    }
}
