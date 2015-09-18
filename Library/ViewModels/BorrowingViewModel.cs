using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Library.ViewModels
{
    public class BorrowingViewModel : BindableBase
    {
        bool _borrowing = true;
        readonly IRegionManager _regionManager;

        public bool Borrowing {
            get { return _borrowing; } 
            set { SetProperty(ref this._borrowing, value);}
        }

        public BorrowingViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            this.BorrowCommand = new DelegateCommand<string>(Borrow).ObservesCanExecute((p) => Borrowing);
        }

        public ICommand BorrowCommand { get; set; }

        void Borrow(string uri)
        {

            Console.WriteLine("Tests");
            Borrowing = false;
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }


    }
}
