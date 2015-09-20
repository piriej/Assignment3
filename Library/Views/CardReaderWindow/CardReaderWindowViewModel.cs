using System;
using System.Windows.Input;
using Library.ViewModels;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Library.Views.CardReaderWindow
{
    public class CardReaderWindowViewModel : BindableBase
    {
        readonly IRegionManager _regionManager;
        // Injected properties.
        //IRegionManager RegionManager { get; set; }

        public CardReaderWindowViewModel(IRegionManager RegionManager)
        {
            _regionManager = RegionManager;
            //RegionManager = regionManager;
            this.ScanCommand = new DelegateCommand<string>(Scan).ObservesCanExecute((p) => Enabled);
        }

        // View Model Properties
        bool _enabled = false;
        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref this._enabled, value); }
        }

        string _cardData;
        public string CardData
        {
            get { return _cardData; }
            set { SetProperty(ref this._cardData, value); }
        }

        // Commands
        public ICommand CloseWindowCommand { get; set; }

        public Action CloseWindowDelegate { get; set; } = () =>
        {
            // Do Nothing
        };

        void CloseWindow()
        {
            Console.WriteLine(@"detected Window closing");
            CloseWindowDelegate.Invoke();
        }

        public ICommand ScanCommand { get; set; }
        void Scan(string uri)
        {
            Enabled = false;
            _regionManager.RequestNavigate(RegionNames.ContentRegion, uri);
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

