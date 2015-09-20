using System.Windows.Input;
using Library.ApplicationInfratructure;
using Library.Features.CardReader;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Library.Features.Borrowing
{
    public class BorrowingViewModel : BindableBase
    {
 

        #region Injected Properties

        readonly IRegionManager _regionManager;
        ICardReader CardReader { get; set; }

        // Todo: Inject only the interface.
        //public CardReaderWindowViewModel CardReaderWindowViewModel { get; set; }

        #endregion

        #region Bound Properties

        bool _borrowing = true;
        public bool Borrowing {
            get { return _borrowing; } 
            set { SetProperty(ref this._borrowing, value);}
        }

        #endregion

        #region Constructors

        public BorrowingViewModel(IRegionManager regionManager, ICardReader cardReader)
        {
            CardReader = cardReader;
            _regionManager = regionManager;
            this.BorrowCommand = new DelegateCommand<string>(Borrow).ObservesCanExecute((p) => Borrowing);
        }

        #endregion

        #region Commands

        public ICommand BorrowCommand { get; set; }

        void Borrow(string uri)
        {
            //TODO: remove coupling, This should be managed via eventing.
            CardReader.Enabled = true;
            Borrowing = false;
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }

        #endregion
    }
}
