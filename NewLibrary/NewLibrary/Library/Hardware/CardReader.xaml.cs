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

using NewLibrary.Library.Interfaces.Hardware;

namespace NewLibrary.Library.Hardware
{
    /// <summary>
    /// Interaction logic for CardReader.xaml
    /// </summary>
    public partial class CardReader : Window, CardReaderInterface
    {
        public CardReader()
        {
            InitializeComponent();
        }

        private bool enabled;
        public bool Enabled
        {
            get
            {
                return enabled;
            }

            set
            {
                enabled = value;
            }
        }

        private CardReaderListenerInterface listener;
        public CardReaderListenerInterface Listener
        {
            get
            {
                return listener;
            }

            set
            {
                listener = value;
            }
        }

        private void swipeButton_Click(object sender, RoutedEventArgs e)
        {
            int borrowerID = 0;

            string borrowerIDstr = cardDataBox.Text;

            if (String.IsNullOrWhiteSpace(borrowerIDstr))
            {
                errorMesgLabel.Content = "Borrower ID cannot be empty or blank.";
            }
            else
            {
                try
                {
                    borrowerID = Convert.ToInt32(borrowerIDstr);
                    if (borrowerID <= 0) throw new FormatException();
                    cardDataBox.Text = "";

                    listener.cardSwiped(borrowerID);
                }
                catch (FormatException)
                {
                    errorMesgLabel.Content = "Borrower ID must be a positive integer.";
                }
                catch (OverflowException)
                {
                    errorMesgLabel.Content = "Borrower ID cannot be so big.";
                }
            }
        }
    }
}
