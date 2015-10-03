﻿using System;
using System.Linq;
using AutoMapper;
using Library.Features.Borrowing;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Entities;
using Prism.Events;

namespace Library.Features.ScanBook
{
    public class ScanBookController : IScanBookController
    {
        public IEventAggregator EventAggregator { get; set; }
        public IScanBookViewModel ViewModel { get; set; }
        public IBookDAO BookDao { get; set; }
        public ILoanDAO LoanDao { get; set; }
        public IMemberDAO MemberDao { get; set; }


        private int _numScans = 0;
        private IMember _borrower;

        public ScanBookController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;
            eventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(ScanBook);
        }

        public void ScanBook(BorrowingModel borrowingModel)
        {
            // Display user details
            //ViewModel.BorrowerId = borrowingModel.ID;

            if (borrowingModel.BorrowingState != EBorrowState.SCANNING_BOOKS) return;

            // Is this a valid user?
            _borrower = MemberDao.GetMemberByID(borrowingModel.ID);
            if (_borrower == null)
                ViewModel.ErrorMessage = $"Member {borrowingModel.ID} is not known to the system.";

            // Map the model onto the viewmodel.
         
            // Clear messages.

            //borrowingModel.Loans.FirstOrDefault().
            Mapper.Map(borrowingModel, (ScanBookViewModel) ViewModel);
                
            EventAggregator.GetEvent<Messages.ScanningRecievedEvent>().Subscribe(Scanning);
            EventAggregator.GetEvent<Messages.ScanningEvent>().Publish(new ScanBookModel());
        }

        public void Scanning(ScanBookModel scanBookModel)
        {
            ViewModel.ErrorMessage = "";

            var bookById = BookDao.GetBookByID(scanBookModel.Barcode);

            if (bookById == null)
            {
                ViewModel.ErrorMessage = $"Book {scanBookModel.Barcode} not found";
                return;
            }

            var loansPending = LoanDao.LoanList.Where(x => x.Borrower.ID == _borrower.ID && x.State == LoanState.PENDING).ToList();

            if (bookById.State != BookState.AVAILABLE)
                ViewModel.ErrorMessage = $"Book {bookById.ID} is not available: {bookById.State}";

            else if (loansPending.Any(x=>x.ID == bookById.ID))
                ViewModel.ErrorMessage = $"Book {bookById.ID} already scanned ";

            else
            {
               
                var loan = LoanDao.CreateLoan(_borrower, bookById, DateTime.Today, DateTime.Today.AddDays(14));
               
                _numScans++;

                ViewModel.CurrentBook = loan.ToString();
                ViewModel.PendingLoans = string.Join(Environment.NewLine + Environment.NewLine, loansPending);
                
                if (_numScans < 5)
                    return;

                EborrowStateManager.CurrentState.ChangeState(EBorrowState.CONFIRMING_LOANS);
            }
        }
    }
}
