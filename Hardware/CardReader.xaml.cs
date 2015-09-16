using Library.Interfaces.Hardware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;

namespace Library.Hardware
{
    /// <summary>
    /// Interaction logic for CardReader.xaml
    /// </summary>
    public partial class CardReader : Window, ICardReader
    {


        public CardReader()
        {
            InitializeComponent();
        }


        private bool _enabled;
        public bool Enabled
        {
            get
            {
                return _enabled;
            }

            set
            {
                _enabled = value;
                cardDataBox.IsEnabled = _enabled;
                swipeButton.IsEnabled = _enabled;
            }
        }


        private ICardReaderListener _listener;
        public ICardReaderListener Listener
        {
            get
            {
                return _listener;
            }
            set
            {
                Console.WriteLine("CardReader setting listener to " + value);
                _listener = value;
            }
        }


        private void swipeButton_Click(object sender, RoutedEventArgs e)
        {
            int borrowerID = 0;

            errorMessageLabel.Content = "";
            string borrowerIDstr = cardDataBox.Text;

            if (String.IsNullOrWhiteSpace(borrowerIDstr))
            {
                errorMessageLabel.Content = "Borrower ID cannot be empty or blank.";
            }
            else
            {
                try
                {
                    borrowerID = Convert.ToInt32(borrowerIDstr);
                    if (borrowerID <= 0) throw new FormatException();

                    _listener.cardSwiped(borrowerID);
                }
                catch (FormatException)
                {
                    errorMessageLabel.Content = "Borrower ID must be a positive integer.";
                }
                catch (OverflowException)
                {
                    errorMessageLabel.Content = "Borrower ID cannot be so big.";
                }
                cardDataBox.Text = "";
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("detected Window closing {0}", sender);
            e.Cancel = true;
        }

    }
}
