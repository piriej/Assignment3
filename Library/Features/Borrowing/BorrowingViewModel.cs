using System;
using System.Windows.Input;
using Library.ApplicationInfratructure;
using Library.Features.CardReader;
using Library.Interfaces.Hardware;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using ICardReader = Library.Features.CardReader.ICardReader;

namespace Library.Features.Borrowing
{
    public class BorrowingViewModel : BindableBase
    {
        #region Injected Properties

        readonly IRegionManager _regionManager;
        ICardReader CardReader { get; set; }
        ICardReaderListener2 CardReaderListener { get; set; }

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

        public BorrowingViewModel(IRegionManager regionManager, ICardReader cardReader, ICardReaderListener2 cardReaderListener)
        {
            CardReader = cardReader;
            CardReaderListener = cardReaderListener;
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

            // Wait for the hardware to swipe the card, subscribe to the event.
            CardReaderListener.NotifyCardSwiped += OnCardSwipe;
        }

        public void OnCardSwipe(object source, CardReaderModel cardReaderModel)
        {
            _regionManager.RequestNavigate(RegionNames.ContentRegion, ViewNames.SwipeControl);
        }
        #endregion
    }
}

