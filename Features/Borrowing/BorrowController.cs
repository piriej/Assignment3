using System;
using System.Windows.Controls;
using AutoMapper;
using Library.Controllers.Borrow;
using Library.Features.CardReader;
using Library.Features.MainWindow;
using Library.Features.ScanBook;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Daos;
using Library.Interfaces.Hardware;
using Prism.Events;

namespace Library.Features.Borrowing
{
    public class BorrowController : IBorrowListener, /*ICardReaderEvents,*/ IScannerListener, IBorrowController, IBorrowEvents
    {
        readonly IDisplay _display;
        UserControl _previousDisplay;

        EBorrowState _state; // EBorrowState.CREATED; - default state

        #region Injected properties

        public IMemberDAO MemberDao { get; set; }
        public IMainWindowController MainController { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        #endregion

        public IBorrowingViewModel ViewModel { get; set; }

        public event EventHandler<EBorrowState> setEnabled;


        public void cancelled()
        {
            close();
        }

        public void WaitForCardSwipe()
        {
            // Move from CREATED to INITIALISED State, and create a message payload.
            var nextState = EborrowStateManager.CurrentState.ChangeState();

            // Create a new borrowing model
            var borrowingModel = new BorrowingModel();

            // Raise the change of state event to let listeners know that the borrow button
            // has been pressed.
            EventAggregator.GetEvent<Messages.BorrowingStateEvent>().Publish(borrowingModel);

            // Listen for the card reader swipe event.
            EventAggregator.GetEvent<Messages.CardReaderSwipedEvent>().Subscribe(CardSwiped);

            // Disable the button whilst waiting.
            ViewModel.Active = false;
        }

        public void CardSwiped(ICardReaderModel cardReaderModel)
        {
            var borrowerId = int.Parse(cardReaderModel.BorrowerId);

            // Prepare a message payload containing the users member.
            var member = MemberDao.GetMemberByID(borrowerId);

            if (member == null)
            {
                ViewModel.ErrorMessage = $"Member ID {borrowerId} not found";
                EventAggregator.GetEvent<Messages.CardReaderSwipedEvent>().Subscribe(CardSwiped);
                return;
            }

            // Map the member to a borrowing model.
            var model = Mapper.Map<BorrowingModel>(member);

            // Decide the next state based on the members borrowing status, deliver the payload and navigate as appropriate.
            var nextState = EborrowStateManager.CurrentState.ChangeState(member);
            //switch (nextState)
            //{
            //    case EBorrowState.SCANNING_BOOKS:
            EventAggregator.GetEvent<Messages.BorrowingStateEvent>().Publish(model);
            MainController.NavigateTo<ScanBookView>();
                //    break;
                //case EBorrowState.BORROWING_RESTRICTED:
                //    //TODO: Where to then? - BorrowingRestrictredview
                //    break;
            }

        public void bookScanned(int barcode)
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void scansCompleted()
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void loansConfirmed()
        {
            throw new ApplicationException("Not implemented yet");
        }

        public void loansRejected()
        {
            throw new ApplicationException("Not implemented yet");
        }


        private void setState(EBorrowState state)
        {
            _state = state;
            setEnabled?.Invoke(this, _state);
        }


        public void close()
        {
            _display.Display = _previousDisplay;
        }


        //private string buildLoanListDisplay(List<ILoan> loanList)
        //{
        //    StringBuilder bld = new StringBuilder();
        //    foreach (ILoan loan in loanList)
        //    {
        //        if (bld.Length > 0)
        //        {
        //            bld.Append("\n\n");
        //        }
        //        bld.Append(loan.ToString());
        //    }
        //    return bld.ToString();
        //}
    }
}
