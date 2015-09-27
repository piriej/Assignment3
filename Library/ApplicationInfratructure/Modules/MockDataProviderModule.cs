using System;
using System.Collections.Generic;
using Autofac;
using Library.Entities;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;

namespace Library.ApplicationInfratructure.Modules
{
    public class MockDataProviderModule : Module
    {
        private ILoanDAO LoanDAO { get; set; }
        private List<IBook> _books;
        private List<IMember> _members;
        private List<ILoan> _loans; 
        private DateTime _borrowDate;
        private TimeSpan _loanPeriod;
        private DateTime _dueDate;

        public MockDataProviderModule(/*ILoanDAO loanDao*/)
        {
            // Setup dates for test data
            _borrowDate = DateTime.Now;
            _loanPeriod = new TimeSpan(LoanConstants.LOAN_PERIOD, 0, 0, 0);
            _dueDate = _borrowDate.Add(_loanPeriod);
          
            SetupBookTestData();
            SetupMemberTestData();
            SetUpLoanTestData();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_books);
            builder.RegisterInstance(_members);
            builder.RegisterInstance(_loans);
        }

    
        private void SetupBookTestData()
        {
            _books = new List<IBook>
            {
                new Book(1, "author1", "title1", "callNo1"),
                new Book(2, "author1", "title2", "callNo2"),
                new Book(3, "author1", "title3", "callNo3"),
                new Book(4, "author1", "title4", "callNo4"),
                new Book(5, "author2", "title5", "callNo5"),
                new Book(6, "author2", "title6", "callNo6"),
                new Book(7, "author2", "title7", "callNo7"),
                new Book(8, "author2", "title8", "callNo8"),
                new Book(9, "author3", "title9", "callNo9"),
                new Book(10, "author3", "title10", "callNo10"),
                new Book(11, "author4", "title11", "callNo11"),
                new Book(12, "author4", "title12", "callNo12"),
                new Book(13, "author5", "title13", "callNo13"),
                new Book(14, "author5", "title14", "callNo14"),
                new Book(15, "author5", "title15", "callNo15")
            };
        }

        private void SetupMemberTestData()
        {
            _members = new List<IMember>
            {
                new Member( "fName1", "lName1", "0001", "email1", 1),
                new Member( "fName2", "lName2", "0002", "email2", 2),
                new Member( "fName3", "lName3", "0003", "email3", 3),
                new Member( "fName4", "lName4", "0004", "email4", 4),
                new Member( "fName5", "lName5", "0005", "email5", 5),
                new Member( "fName6", "lName6", "0006", "email6", 6)
            };
        }

        private void SetUpLoanTestData()
        {
            _loans = new List<ILoan>
            {
                new Loan( _books[1], _members[1], _borrowDate, _dueDate),
                new Loan( _books[2] ,_members[1], _borrowDate, _dueDate)
            };
        }

        private void SetUpTestData()
        {
            
            //var checkDate = _dueDate.Add(new TimeSpan(1, 0, 0, 0));
            //LoanDAO.UpdateOverDueStatus(checkDate);

            ////create a member with maxed out unpaid fines
            //member[2].AddFine(10.0f);

            ////create a member with maxed out loans
            //for (int i = 2; i < 7; i++)
            //{
            //    ILoan loan = LoanDAO.CreateLoan(member[3], book[i], _borrowDate, _dueDate);
            //    LoanDAO.CommitLoan(loan);
            //}

            ////a member with a fine, but not over the limit
            //member[4].AddFine(5.0f);

            ////a member with a couple of loans but not over the limit
            //for (int i = 7; i < 9; i++)
            //{
            //    ILoan loan = LoanDAO.CreateLoan(member[5], book[i], _borrowDate, _dueDate);
            //    LoanDAO.CommitLoan(loan);
            //}
        }
    }
}