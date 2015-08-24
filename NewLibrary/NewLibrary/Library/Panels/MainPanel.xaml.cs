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

using NewLibrary.Library;

namespace NewLibrary.Library.Panels
{
    /// <summary>
    /// Interaction logic for MainPanel.xaml
    /// </summary>
    public partial class MainPanel : UserControl
    {
        private MainController controller;

        public MainPanel()
        {
            InitializeComponent();
        }

        public void setController(MainController controller)
        {
            this.controller = controller;
        }

        private void borrowButton_Click(object sender, RoutedEventArgs e)
        {
            controller.borrowSelected();
        }
    }
}
