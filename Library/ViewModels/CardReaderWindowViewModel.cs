using System;
using System.ComponentModel;
using System.Linq.Expressions;
using Prism.Mvvm;

namespace Library.ViewModels
{
    class CardReaderWindowViewModel : BindableBase
    {
        bool _enabled = true;

        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref this._enabled, value); }
        }

        //protected void OnPropertyChanged(params Expression<Func<T>>[] propertyExpressions)
        //{
        //    PropertyChanged.Raise(propertyExpressions);
        //}

        //public bool Enabled
        //{
        //    get
        //    {
        //        return _enabled;
        //    }

        //    set
        //    {
        //        _enabled = value;
        //        cardDataBox.IsEnabled = _enabled;
        //        swipeButton.IsEnabled = _enabled;
        //    }
        //}


        //private ICardReaderListener _listener;
        //public ICardReaderListener Listener
        //{
        //    get
        //    {
        //        return _listener;
        //    }
        //    set
        //    {
        //        Console.WriteLine("CardReader setting listener to " + value);
        //        _listener = value;
        //    }
        //}


        //private void swipeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int borrowerID = 0;

        //    errorMessageLabel.Content = "";
        //    string borrowerIDstr = cardDataBox.Text;

        //    if (String.IsNullOrWhiteSpace(borrowerIDstr))
        //    {
        //        errorMessageLabel.Content = "Borrower ID cannot be empty or blank.";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            borrowerID = Convert.ToInt32(borrowerIDstr);
        //            if (borrowerID <= 0) throw new FormatException();

        //            _listener.cardSwiped(borrowerID);
        //        }
        //        catch (FormatException)
        //        {
        //            errorMessageLabel.Content = "Borrower ID must be a positive integer.";
        //        }
        //        catch (OverflowException)
        //        {
        //            errorMessageLabel.Content = "Borrower ID cannot be so big.";
        //        }
        //        cardDataBox.Text = "";
        //    }
        //}


        //private void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    Console.WriteLine("detected Window closing {0}", sender);
        //    e.Cancel = true;
        //}

    }
}

