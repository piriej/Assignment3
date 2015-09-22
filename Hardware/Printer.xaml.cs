using Library.Interfaces.Hardware;
using System;
using System.Windows;
using System.ComponentModel;

namespace Library.Hardware
{
    /// <summary>
    /// Interaction logic for Printer.xaml
    /// </summary>
    public partial class Printer : Window, IPrinter
    {
        public Printer()
        {
            InitializeComponent();
            Show();
        }
        public void print(string printData)
        {
            printBox.Text = "";
            printBox.Text = printData;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("detected Window closing");
            e.Cancel = true;
        }
    }
}
