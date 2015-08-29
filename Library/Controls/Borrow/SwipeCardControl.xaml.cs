using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Controls.Borrow;
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
    /// Interaction logic for BorrowControl.xaml
    /// </summary>
    public partial class SwipeCardControl : ABorrowControl
    {
        private IBorrowListener _listener;

        public SwipeCardControl(IBorrowListener listener)
        {
            Console.WriteLine("BorrowControl creating");
            _listener = listener;
            InitializeComponent();
        }


        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            _listener.cancelled();
        }
    }
}
