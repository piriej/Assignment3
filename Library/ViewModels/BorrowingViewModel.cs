using System;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.ViewModels
{
    public class BorrowingViewModel : BindableBase
    {
        bool _borrowing = true;

        public bool Borrowing {
            get { return _borrowing; } 
            set { SetProperty(ref this._borrowing, value);}
        }

        //public DelegateCommand<object> BorrowCommand { get; }

        public BorrowingViewModel()
        {
            //this.BorrowCommand = new DelegateCommand(Borrow, CanBorrow).ObservesProperty(() => Borrowing);         
            this.BorrowCommand = new DelegateCommand(Borrow).ObservesCanExecute((p) => Borrowing);
        }

        public ICommand BorrowCommand { get; set; }

        void Borrow()
        {
            Console.WriteLine("Tests");
            Borrowing = false;
            // Change the view here...
        }


    }
}
