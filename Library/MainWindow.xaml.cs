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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

using Library.Interfaces.Hardware;
using Library.Controllers;
using Library.Hardware;

namespace Library
{


    public partial class MainWindow : Window, IDisplay
    {
        private UserControl _currentControl;
        private CardReader _reader;
        private Scanner _scanner;
        private Printer _printer;

        public MainWindow()
        {
            _reader = new CardReader();
            _scanner = new Scanner();
            _printer = new Printer();
            InitializeComponent();
            _reader.Show();
            _scanner.Show();
            _printer.Show();

            MainMenuController mainController =
                new MainMenuController(this, _reader, _scanner, _printer);
            mainController.initialise();
        }


        public UserControl Display
        {
            get
            {
                Console.WriteLine("Getting Display");
                return _currentControl;
            }
            set
            {
                Console.WriteLine("Setting Display");
                Panel.Children.Remove(_currentControl);
                _currentControl = value;
                Panel.Children.Add(_currentControl);

            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Console.WriteLine("detected Window closing");
            Application.Current.Shutdown();
            //_reader.Close();
            //_scanner.Close();
        }


    }
}
