using System;
using FluentAssertions;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Xunit.Extensions;

namespace IntegrationTests.LowLeveTests
{
    /// <summary>
    /// These tests utilise an Automocking, Autosubstituting, IOC Container i.e. If they are ddefinded in the container
    ///  It will use real entities if they exist, resolving their dependant graphs. Otherwise,
    /// if they don't it will use a substitute - the object returned by the container contains mocked data. This is useful as
    /// we can write end to end tests, and worry about providing the implementation later. This prevents the need for stubbing.
    /// </summary>

    public class LoanLowLevelTests
    {
        DateTime _today = DateTime.Today;
        DateTime _dueDate = DateTime.Today.AddDays(5);
        DateTime _overdueDate = DateTime.Today.AddDays(10);
        DateTime _notYetDueDate = DateTime.Today.AddDays(-10);

        [Theory, ContainerData]
        public void HappyCase_UserBorrowsAndReturnsBookBeforeDue(ILoanDAO loanDao,  IMember borrower, IBook book)
        {
            // Create and commit to the loan.
            var loan = loanDao.CreateLoan(borrower, book, _today, _dueDate);
            loan.State.Should().Be(LoanState.PENDING);

            // Commit the loan.
            loan.Commit(200);
            loan.State.Should().Be(LoanState.CURRENT);

            // Check if the book is overdue.
            loan.CheckOverDue(_notYetDueDate);
            loan.State.Should().Be(LoanState.CURRENT);

            // Return the book
            loan.Complete();
            loan.State.Should().Be(LoanState.COMPLETE);
            loan.IsOverDue.Should().BeFalse();

        }

        [Theory, ContainerData]
        public void OverDueCase_UserBorrowsAndReturnsBookAfterDue(ILoanDAO loanDao,  IMember borrower, IBook book)
        {
            // Create and commit to the loan.
            var loan = loanDao.CreateLoan(borrower, book, _today, _dueDate);
            loan.State.Should().Be(LoanState.PENDING);

            // Commit the loan.
            loan.Commit(200);
            loan.State.Should().Be(LoanState.CURRENT);

            // The book is now overdue.
            loan.CheckOverDue(_overdueDate);
            loan.State.Should().Be(LoanState.OVERDUE);
            loan.IsOverDue.Should().BeTrue();

            // Return the book
            loan.Complete();
            loan.State.Should().Be(LoanState.COMPLETE);
            loan.IsOverDue.Should().BeFalse();
        }

        [Theory, ContainerData]
        public void EdgeCase_CantMoveFromPendingToComplete(ILoanDAO loanDao, IMember borrower, IBook book)
        {
            // Create and commit to the loan.
            var loan = loanDao.CreateLoan(borrower, book, _today, _dueDate);
            loan.State.Should().Be(LoanState.PENDING);

            // Return the book should be prevented.
            loan.Invoking(x => x.Complete())
                .ShouldThrow<ApplicationException>();
            loan.State.Should().Be(LoanState.PENDING);
        }

        [Theory, ContainerData]
        public void EdgeCase_CantMoveFromPendingToOverdue(ILoanDAO loanDao, IMember borrower, IBook book)
        {
            // Create and commit to the loan.
            var loan = loanDao.CreateLoan(borrower, book, _today, _dueDate);
            loan.State.Should().Be(LoanState.PENDING);

            // Return the book should be prevented.
            loan.Invoking(x => x.CheckOverDue(_overdueDate))
                .ShouldThrow<ApplicationException>();
            loan.State.Should().Be(LoanState.PENDING);
        }
    }

}
