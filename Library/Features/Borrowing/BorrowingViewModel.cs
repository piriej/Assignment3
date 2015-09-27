using System;
using System.Windows.Input;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Features.CardReader;
using Library.Features.MainWindow;
using Library.Interfaces.Controllers.Borrow;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Events;

namespace Library.Features.Borrowing
{
    public class BorrowingViewModel : BindableBase, IBorrowingViewModel
    {
        #region Injected Properties

        readonly IRegionManager _regionManager;
        ICardReader2 CardReader { get; set; }
        //public event EventHandler<EBorrowState> setEnabled;
        public IBorrowController Controller { get; set; }

        public  IEventAggregator EventAggregator{ get; set; }
        #endregion

        #region Bound Properties

        bool _active = true;
        public bool Active
        {
            get { return _active; }
            set { SetProperty(ref this._active, value);     }
        }

        #endregion

        #region Commands
        public ICommand BorrowCommand { get; set; }
        #endregion

        #region Constructors

        public BorrowingViewModel(
             IBorrowController  controller)
        {
            Controller = controller;
            this.BorrowCommand = new DelegateCommand(controller.WaitForCardSwipe).ObservesCanExecute(p => Active);
        }

        #endregion

    }
}

