using System;
using System.Collections.Generic;
using Autofac;
using Library.Daos;
using Library.Entities;
using Library.Interfaces.Entities;

namespace Library.ApplicationInfratructure.Modules
{
    public class MockDataProviderModule : Module
    {
        private LoanDAO _loanDAO;
        private List<IBook> _books;
        private List<IMember> _members;
        private List<ILoan> _loans; 
        private DateTime _borrowDate;
        private TimeSpan _loanPeriod;
        private DateTime _dueDate;
        private LoanHelper _loanHelper;
        private MemberHelper _memberHelper;
        private MemberDAO _memberDAO;
        private BookDAO _bookDao;
        private BookHelper _bookHelper;


        public MockDataProviderModule()
        {
            _memberHelper = new MemberHelper();
            _memberDAO = new MemberDAO(_memberHelper);
            _loanHelper = new LoanHelper();
            _loanDAO = new LoanDAO(_loanHelper);
            _bookHelper = new BookHelper();
            _bookDao = new BookDAO(_bookHelper);

            // Setup dates for test data
            _borrowDate = DateTime.Now;
            _loanPeriod = new TimeSpan(LoanConstants.LOAN_PERIOD, 0, 0, 0);
            _dueDate = _borrowDate.Add(_loanPeriod);
          
            SetupBookTestData();
            SetupMemberTestData();
            SetUpLoanTestData();
            SetUpTestData();
        }

        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterInstance(_books).SingleInstance().AsImplementedInterfaces();
            builder.RegisterInstance(_members).SingleInstance().AsImplementedInterfaces();
            builder.RegisterInstance(_loans).SingleInstance().AsImplementedInterfaces();

            builder.RegisterType<Book>().AsImplementedInterfaces();
            builder.RegisterType<Loan>().AsImplementedInterfaces();
            builder.RegisterType<Member>().AsImplementedInterfaces();

            builder.RegisterInstance(_loanDAO).SingleInstance().AsImplementedInterfaces();
            builder.RegisterInstance(_memberDAO).SingleInstance().AsImplementedInterfaces();
            builder.RegisterInstance(_bookDao).SingleInstance().AsImplementedInterfaces();
            //builder.RegisterType<MemberDAO>().AsImplementedInterfaces().SingleInstance().PropertiesAutowired(PropertyWiringOptions.AllowCircularDependencies);

            builder.RegisterType<LoanHelper>().AsImplementedInterfaces();
            builder.RegisterType<BookHelper>().AsImplementedInterfaces();
            builder.RegisterInstance<LoanHelper>(_loanHelper).AsImplementedInterfaces();
        }
        
        private void SetupBookTestData()
        {
            _books = new List<IBook>
            {
                new Book("author1", "title1", "callNo1"    , 1 ),
                new Book("author1", "title2", "callNo2"    , 2 ),
                new Book("author1", "title3", "callNo3"    , 3 ),
                new Book("author1", "title4", "callNo4"    , 4 ),
                new Book("author2", "title5", "callNo5"    , 5 ),
                new Book("author2", "title6", "callNo6"    , 6 ),
                new Book("author2", "title7", "callNo7"    , 7 ),
                new Book("author2", "title8", "callNo8"    , 8 ),
                new Book("author3", "title9", "callNo9"    , 9 ),
                new Book( "author3", "title10", "callNo10" , 10),
                new Book( "author4", "title11", "callNo11" , 11),
                new Book( "author4", "title12", "callNo12" , 12),
                new Book( "author5", "title13", "callNo13" , 13),
                new Book( "author5", "title14", "callNo14" , 14),
                new Book( "author5", "title15", "callNo15" , 15)
            };
            _bookDao.AddBooks(_books);
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
            _memberDAO.AddMembers(_members);
        }

        private void SetUpLoanTestData()
        {
            var checkDate = _dueDate.AddDays(1);

            for (var i = 0; i < 2; i++)
            {
                var loan = _loanDAO.CreateLoan(_members[1], _books[i], _borrowDate, _dueDate);
                _loanDAO.CommitLoan(loan);
            }
            _loanDAO.UpdateOverDueStatus(checkDate);
            _loans = _loanDAO.LoanList;
            //_loans = new List<ILoan>
            //{
            //    new Loan( _books[1], _members[1], _borrowDate, _dueDate),
            //    new Loan( _books[2] ,_members[1], _borrowDate, _dueDate)
            //};

            ////_loans[0].Commit(0);
            ////_loans[1].Commit(1);

            //_loanDAO.AddLoan(_loans);
        }

        private void SetUpTestData()
        {
            

         

            //create a member with maxed out unpaid fines
            _members[2].AddFine(10.0f);

            //create a member with maxed out loans
            for (int i = 2; i < 7; i++)
            {
                var loan = _loanDAO.CreateLoan(_members[3], _books[i], _borrowDate, _dueDate);
                _loanDAO.CommitLoan(loan);
            }

            //a member with a fine, but not over the limit
            _members[4].AddFine(5.0f);

            //a member with a couple of loans but not over the limit
            for (var i = 7; i < 9; i++)
            {
                var loan = _loanDAO.CreateLoan(_members[5], _books[i], _borrowDate, _dueDate);
                _loanDAO.CommitLoan(loan);
            }
        }
    }
}