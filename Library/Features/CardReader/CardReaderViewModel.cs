using System;
using System.Windows;
using Library.Features.Borrowing;
using Library.Interfaces.Controllers.Borrow;
using Library.Messages;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;

namespace Library.Features.CardReader
{
    public class CardReaderViewModel : ValidatedBindableBase,  ICardReader2, ICardReaderViewModel
    {

        #region Injected Properties

        public IRegionManager RegionManager { get; set; } 
        //public IBorrowEvents BorrowEvents { get; set; }
        public IEventAggregator EventAggregator { get; set; }
        ICardReaderController Controller { get; set; }

        #endregion

        #region constructors

        public CardReaderViewModel(IEventAggregator eventAggregator, ICardReaderController cardReaderController)
        {
            Controller = cardReaderController;
            EventAggregator = eventAggregator;

            // Subscribe to setEnabled event from the borrower. 
            // In the event that the Borrowers current state is initialised, Enables this control, otherwise disables it.
            eventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(borrowModel => Enabled = borrowModel.BorrowingState == EBorrowState.INITIALIZED);

            // Listen to Swipe button press 
            CardSwipedCmd = new DelegateCommand<string>(cardReaderController.CardSwiped).ObservesCanExecute(p => Enabled);

            // Listen to window close event.
            CloseWindowCommand = new DelegateCommand(CloseWindow, () => false);
        }

        #endregion

        #region View Model Properties

        bool _enabled;


        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }

        private string _borrowerId;

        public string BorrowerId
        {
            get { return _borrowerId; }
            set
            {
                SetProperty(ref _borrowerId, value);
                ValidateModelProperty(value, "BorrowerId");
            }
        }

        #endregion

        #region Commands

        public System.Windows.Input.ICommand CloseWindowCommand { get; set; }
        void CloseWindow()
        {
            Application.Current.Shutdown();
            EventAggregator.GetEvent<CloseApplicationEvent>().Publish(new ClosingModel());
        }

        public System.Windows.Input.ICommand CardSwipedCmd { get; set; }
     

        public event EventHandler<CardReaderModel> NotifyCardSwiped;

        protected virtual void OnNotifyCardSwiped(CardReaderModel model)
        {
            NotifyCardSwiped?.Invoke(this, model);
        }

        #endregion

    }
}

