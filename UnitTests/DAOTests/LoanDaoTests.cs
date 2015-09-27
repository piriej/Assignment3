using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Library.Daos;
using Library.Entities;
using Library.Interfaces.Entities;
using NSubstitute;
using Ploeh.AutoFixture;
using Ploeh.AutoFixture.AutoNSubstitute;
using Ploeh.AutoFixture.Xunit;
using Xunit.Extensions;

namespace UnitTests.DAOTests
{
    public class LoanDaoTests
    {
        private LoanDAO _loanDao;

        public LoanDaoTests()
        {
            // Arrange
            var loanHelper = new LoanHelper();
            _loanDao = new LoanDAO(loanHelper);
        }

        [Theory, AutoNSubstituteData]
        public void CommitLoan_GivenALoan_CommitsToTheList()
        {
            // Arrange.
            var loanSubstitute = Substitute.For<ILoan>();

            // Act
            _loanDao.CommitLoan(loanSubstitute);

            // Assert
            loanSubstitute.Received().Commit(Arg.Any<int>());
            _loanDao.LoanList.Any(loanRecord => loanRecord.Equals(loanSubstitute)).Should().BeTrue();

        }

        [Theory, AutoNSubstituteData]
        public void CreateLoan_WithValidParams_CreatesANewLoan(IMember borrower, IBook book)
        {
            // Arrange.

            // Act
            var loan = _loanDao.CreateLoan(borrower, book, DateTime.Today, DateTime.Today.AddDays(1));

            loan.Should().NotBeNull();

        }

        [Theory, AutoNSubstituteData]
        public void CreateLoan_IncorrectDueDate_ThrowsAnException(IMember borrower, IBook book)
        {
            // Arrange.

            // Act
            var loan = _loanDao.Invoking(x => x.CreateLoan(borrower, book, DateTime.Today, DateTime.Today.AddDays(-1)))
                 .ShouldThrow<ArgumentException>();
        }


        List<ILoan> AddLoans(IReadOnlyList<IMember> borrowers, IReadOnlyList<IBook> books)
        {
            var loanList = new List<ILoan>();
            for (var count = 0; count < borrowers.Count; count++)
            {
                var loan = _loanDao.CreateLoan(borrowers[count], books[count], DateTime.Today, DateTime.Today.AddDays(1));
                _loanDao.CommitLoan(loan);
                loanList.Add(loan);
            }

            return loanList;
        }


        [Theory, AutoNSubstituteData]
        public void GetLoanByID_WithAValidId_ReturnsALoan(List<IMember> borrowers, List<IBook> books)
        {
            // Arrange
            var loans = AddLoans(borrowers, books);

            // Act 
            var loan = _loanDao.GetLoanByID(loans[1].ID);

            // Assert
            loan.Should().Be(loans[1]);
        }

        [Theory, AutoNSubstituteData]
        public void GetLoanByID_WithAnInValidId_ReturnsNull(List<IMember> borrowers, List<IBook> books)
        {
            // Arrange
            var loans = AddLoans(borrowers, books);

            // Act 
            var loan = _loanDao.GetLoanByID(-12);

            // Assert
            loan.Should().BeNull();
        }
    }
}
