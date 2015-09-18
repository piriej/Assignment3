using System;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.ViewModels
{
    class BorrowViewModel : BindableBase
    {
        bool _borrowing = true;

        public bool Borrowing {
            get { return _borrowing; } 
            set { SetProperty(ref this._borrowing, value);}
        }

        public DelegateCommand<object> BorrowCommand { get; }

        public BorrowViewModel()
        {
            this.BorrowCommand = new DelegateCommand<object>(this.borrow);
        }

        void borrow(object obj)
        {
            Console.WriteLine("Tests");
            // Notify that the order was saved.
           // this.OnSaved(new DataEventArgs<OrderPresentationModel>(this));
        }
    }
}
