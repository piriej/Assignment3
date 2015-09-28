using System;
using System.ComponentModel;
using System.Windows;
using Library.Interfaces.Hardware;

namespace Library.Features.Scanner
{
    /// <summary>
    /// Interaction logic for Scanner.xaml
    /// </summary>
    public partial class ScannerView : Window //, IScanner
    {
        public ScannerView(IScannerViewModel viewModel)
        {
            InitializeComponent();
            Show();
        }
        //private bool _enabled;
        //public bool Enabled
        //{
        //    get
        //    {
        //        return _enabled;
        //    }

        //    set
        //    {
        //        _enabled = value;
        //        barcodeDataBox.IsEnabled = _enabled;
        //        scanButton.IsEnabled = _enabled;
        //    }
        //}

        //private IScannerListener _listener;
        //public IScannerListener Listener
        //{
        //    get
        //    {
        //        return _listener;
        //    }
        //    set
        //    {
        //        Console.WriteLine("Scanner settng listener to " + value);
        //        _listener = value;
        //    }
        //}

        //private void swipeButton_Click(object sender, RoutedEventArgs e)
        //{
        //    int barcode = 0;

        //    errorMessageLabel.Content = "";
        //    string borrowerIDstr = barcodeDataBox.Text;

        //    if (String.IsNullOrWhiteSpace(borrowerIDstr))
        //    {
        //        errorMessageLabel.Content = "Barcode cannot be empty or blank.";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            barcode = Convert.ToInt32(borrowerIDstr);
        //            if (barcode <= 0) throw new FormatException();

        //            _listener.bookScanned(barcode);
        //        }
        //        catch (FormatException)
        //        {
        //            errorMessageLabel.Content = "Barcode must be a positive integer.";
        //        }
        //        catch (OverflowException)
        //        {
        //            errorMessageLabel.Content = "Barcode cannot be so big.";
        //        }
        //        barcodeDataBox.Text = "";
        //    }

        //}

        //private void Window_Closing(object sender, CancelEventArgs e)
        //{
        //    Console.WriteLine("detected Window closing");
        //    e.Cancel = true;
        //}

    }
}
