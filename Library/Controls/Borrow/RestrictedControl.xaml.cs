using Library.Interfaces.Controllers.Borrow;
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

namespace Library.Controls.Borrow
{
    /// <summary>
    /// Interaction logic for RestrictedControl.xaml
    /// </summary>
    public partial class RestrictedControl : ABorrowControl
    {
        private IBorrowListener _listener;

        public RestrictedControl(IBorrowListener listener)
        {
            _listener = listener;
            InitializeComponent();
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _listener.cancelled();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
