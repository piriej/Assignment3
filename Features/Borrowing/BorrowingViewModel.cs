using System;
using System.Windows.Input;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Features.CardReader;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Hardware;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ICardReader = Library.Features.CardReader.ICardReader;

namespace Library.Features.Borrowing
{
    public interface IBorrowingViewModel
    {
    }

    public class BorrowingViewModel : BindableBase, IBorrowingViewModel
//, IBorrowEvents
    {
        #region Injected Properties

        readonly IRegionManager _regionManager;
        ICardReader CardReader { get; set; }
        //public event EventHandler<EBorrowState> setEnabled;
        public IBorrowController Controller { get; set; }
        #endregion

        #region Bound Properties

        bool _borrowing = true;
        public bool Borrowing
        {
            get { return _borrowing; }
            set { SetProperty(ref this._borrowing, value); }
        }

        #endregion

        #region Constructors

        public BorrowingViewModel(
            IRegionManager regionManager
            , ICardReader cardReader
            , IBorrowController  controller)
        {
            CardReader = cardReader;
            Controller = controller;
            _regionManager = regionManager;
            this.BorrowCommand = new DelegateCommand<string>(Borrow).ObservesCanExecute((p) => Borrowing);
        }

        #endregion

        #region Commands

        public ICommand BorrowCommand { get; set; }

        void Borrow(string uri)
        {
            // Enable the card reader, and disable the borrowing button.
            CardReader.Enabled = true;
            Borrowing = false;

            // Navigate to the "Waiting" view.
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);

            Controller.ListenToCardReader();
            // Wait for the hardware to swipe the card, subscribe to the event.
           // CardReaderEvents.NotifyCardSwiped += OnCardSwipe;
        }

        //public void OnCardSwipe(object source, CardReaderModel cardReaderModel)
        //{
        //    CardReaderListener.cardSwiped();
        //    _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.SwipeControl);
        //}
        #endregion

    }
}

