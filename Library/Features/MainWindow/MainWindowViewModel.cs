using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Library.Interfaces.Hardware;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.Features.MainWindow
{
    public interface IMainWindowViewModel
    {
    }

    public class MainWindowViewModel : BindableBase, IDisplay, IMainWindowViewModel
    {
        #region Injected Properties

        public ICardReader CardReader { get; set; }
        public IScanner Scanner { get; set; }
        public IPrinter Printer { get; set; }

        #endregion


        public MainWindowViewModel()
        {
            CloseWindowCommand = new DelegateCommand(CloseWindow);
        }

        public ICommand CloseWindowCommand { get; set; }

        public Action CloseWindowDelegate { get; set; } = () =>
        {
            Application.Current.Shutdown();
        };

        void CloseWindow()
        {
            Console.WriteLine(@"detected Window closing");
            CloseWindowDelegate.Invoke();
        }

        public UserControl Display { get; set; }
    }


}

