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
        private LoanHelper _loanHelper;

        public LoanDaoTests()
        {
            // Arrange
            _loanHelper = new LoanHelper();
            _loanDao = new LoanDAO(_loanHelper);
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


        private List<ILoan> AddLoans(IList<IMember> borrowers, IList<IBook> books)
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
        public void GetLoanByID_WithAValidId_ReturnsALoan(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange
            var loans = AddLoans(borrowers, books);

            // Act 
            var loan = _loanDao.GetLoanByID(loans[1].ID);

            // Assert
            loan.Should().Be(loans[1]);
        }

        [Theory, AutoNSubstituteData]
        public void GetLoanByID_WithAnInValidId_ReturnsNull(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange
            AddLoans(borrowers, books);

            // Act 
            var loan = _loanDao.GetLoanByID(-12);

            // Assert
            loan.Should().BeNull();
        }


        [Theory, AutoNSubstituteData]
        public void GetLoanByBook_WithBookOnLoan_ReturnsALoan(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange.
            var loans = AddLoans(borrowers, books);

            // Act.
            var loanFound = _loanDao.GetLoanByBook(loans[1].Book);

            // Assert.
            loanFound.Should().Be(loans[1]);
        }


        [Theory, AutoNSubstituteData]
        public void GetLoanByBook_WithBookNotOnLoan_ReturnsNull(IList<IMember> borrowers, IList<IBook> books,
            IBook bookNotOnLoan)
        {
            // Arrange.
            AddLoans(borrowers, books);

            // Act.
            var loanFound = _loanDao.GetLoanByBook(bookNotOnLoan);

            // Assert.
            loanFound.Should().BeNull();
        }


        [Theory, AutoNSubstituteData]
        public void FindLoansByBorrower_WithBorrowerHavingLoans_ReturnsTheLoans(IList<IMember> borrowers,
            IList<IBook> books)
        {

            // Arrange.
            var i = 0;
            foreach (var borrower in borrowers)
            {
                i++;
                borrower.ID.Returns(i);
            }

            AddLoans(borrowers, books);
            var loansList = AddLoans(borrowers, books);

            // Act.
            var loansFound = _loanDao.FindLoansByBorrower(borrowers[1]);

            // Assert.
            loansFound.Count.Should().Be(2);
            //loansFound[0].Should().Be(loansFound[1]);
            //loansFound[0].Should().Be(loansList[1]);
        }

        [Theory, AutoNSubstituteData]
        public void FindLoansByBorrower_WithBookNotOnLoan_ReturnsNull(IList<IMember> borrowers, IList<IBook> books,
            IBook bookNotOnLoan)
        {
            // Arrange.
            AddLoans(borrowers, books);

            // Act.
            var loanFound = _loanDao.GetLoanByBook(bookNotOnLoan);

            // Assert.
            loanFound.Should().BeNull();
        }


        [Theory, AutoNSubstituteData]
        public void FindLoansByBookTitle_WithBookOnLoan_ReturnsLoans(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange.
            var title = "Pumpkin tossing for dummies.";

            var loansList = AddLoans(borrowers, books);
            loansList[1].Book.Title.Returns(title);

            // Act.
            var loansFound = _loanDao.FindLoansByBookTitle(title);

            // Assert.
            loansFound.Should().HaveCount(1);
            loansFound.FirstOrDefault().Should().Be(loansList[1]);
        }



        [Theory, AutoNSubstituteData]
        public void FindLoansByBookTitle_WithBookNotOnLoan_ReturnsEmptyList(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange.
            var title = "Testing causes pumpkins to go splat.";

            AddLoans(borrowers, books);

            // Act.
            var loansFound = _loanDao.FindLoansByBookTitle(title);

            // Assert.
            loansFound.Should().BeEmpty();
        }


        [Theory, AutoNSubstituteData]
        public void FindOverDueLoans_WithBooksOverdue_ReturnsOverdueBooks(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange 
            var loansList = AddLoans(borrowers, books);
            loansList[1].CheckOverDue(DateTime.Today.AddDays(10));
            loansList[2].CheckOverDue(DateTime.Today.AddDays(10));

            // Act
            var loansFound = _loanDao.FindOverDueLoans();

            loansFound.Should().HaveCount(2);
            loansFound[0].Should().Be(loansList[1]);
            loansFound[1].Should().Be(loansList[2]);
        }

        [Theory, AutoNSubstituteData]
        public void UpdateOverDueStatus_WhenBooksAreOverdue_ChangesTheireStatuToOverdue(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange 
            var loansList = AddLoans(borrowers, books);

            // Act
            _loanDao.UpdateOverDueStatus(DateTime.Today.AddDays(10));

            // Assert
            // All loans are overdue.
            loansList.All(loan => loan.IsOverDue).Should().BeTrue();


        }

        [Theory, AutoNSubstituteData]
        public void UpdateOverDueStatus_WhenBooksAreNotOverdue_TheirStatusShouldNotBeOverdue(IList<IMember> borrowers, IList<IBook> books)
        {
            // Arrange 
            var loansList = AddLoans(borrowers, books);

            // Act
            _loanDao.UpdateOverDueStatus(DateTime.Today.AddDays(-10));

            // Assert
            // No loans are overdue.
            loansList.Any(loan => loan.IsOverDue).Should().BeFalse();
        }
    }
}
