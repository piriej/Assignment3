using System;
using System.Collections.Generic;
using System.Windows;

using NewLibrary;
using NewLibrary.Library.Hardware;
using NewLibrary.Library.Interfaces.Hardware;

namespace NewLibrary.Library
{

    public class MainController : CardReaderListenerInterface
    {
        private MainWindow mainWindow;
        private CardReader cardReader;


        public MainController(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            mainWindow.Left = 500;
            mainWindow.Top = 50;
            this.cardReader = new CardReader();
            cardReader.Listener = this;
            cardReader.Left = 100;
            cardReader.Top = 50;
        }


        public void Initialise()
        {
            mainWindow.MainPanel.Visibility = Visibility.Visible;
            cardReader.Show();

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

        public void cardSwiped(int cardData)
        {
            Console.WriteLine("Got borrower Id: {0}", cardData);
        }

        public void shutdown()
        {
            cardReader.Close();
            //mainWindow.Close();
        }
    }

}
