using System;
using AutoMapper;
using Library.ApplicationInfratructure;
using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Hardware;
using Prism.Commands;
using Prism.Regions;

namespace Library.Features.CardReader
{
    public class CardReaderViewModel : ValidatedBindableBase, ICardReaderEvents,  ICardReader
    {

        #region Injected Properties

        readonly IRegionManager _regionManager;
        public IBorrowEvents BorrowEvents { get; set; }

        #endregion

        #region constructors

        public CardReaderViewModel(IRegionManager regionManager/*, IBorrowEvents borrowEvents*/)
        {
            // Subscribe to setEnabled event from the borrower. 
            // In the event that the Borrowers current state is initialised, Enables this control, otherwise disables it.
            //BorrowEvents = borrowEvents;
          

            _regionManager = regionManager;
            CardSwipedCmd = new DelegateCommand<string>(CardSwiped)
                .ObservesCanExecute(p => Enabled);

            CloseWindowCommand = new DelegateCommand(CloseWindow, () => false);
        }

        #endregion

        public void ListenToBorrower(IBorrowEvents borrowEvents)
        {
            BorrowEvents = borrowEvents;
            BorrowEvents.setEnabled += (obj, currentState) => Enabled = currentState == EBorrowState.INITIALIZED;
        }


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
            Console.WriteLine(@"detected Window closing");
        }

        public System.Windows.Input.ICommand CardSwipedCmd { get; set; }
        void CardSwiped(string uri)
        {
            // Get a model.
            Console.WriteLine(this.BorrowerId);
            var model = Mapper.Map<CardReaderModel>(this);

            OnNotifyCardSwiped(model);

            Enabled = false;

            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }

        public event EventHandler<CardReaderModel> NotifyCardSwiped;

        protected virtual void OnNotifyCardSwiped(CardReaderModel model)
        {
            NotifyCardSwiped?.Invoke(this, model);
        }

        #endregion

    }
}

