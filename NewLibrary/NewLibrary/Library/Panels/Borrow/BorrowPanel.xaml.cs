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

namespace NewLibrary.Library.Panels.Borrow
{
    /// <summary>
    /// Interaction logic for BorrowPanel.xaml
    /// </summary>
    public partial class BorrowPanel : UserControl
    {
        private MainController controller;

        public BorrowPanel()
        {
            InitializeComponent();
        }


        public void setController(MainController controller)
        {
            this.controller = controller;
        }


        private void mainButton_Click(object sender, RoutedEventArgs e)
        {
            controller.mainSelected();

        }
    }
}
