using System;
using System.Collections.Generic;
using System.Windows;

using NewLibrary;


namespace NewLibrary.Library
{

    public class MainController
    {
        private MainWindow mainWindow;


        public MainController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
        }


        public void Initialise()
        {
            mainWindow.MainPanel.Visibility = Visibility.Visible;
        }

        public void borrowSelected()
        {
            mainWindow.MainPanel.Visibility = Visibility.Hidden;
            mainWindow.BorrowPanel.Visibility = Visibility.Visible;
        }


        public void mainSelected()
        {
            mainWindow.BorrowPanel.Visibility = Visibility.Hidden;
            mainWindow.MainPanel.Visibility = Visibility.Visible;
        }
    }

}
