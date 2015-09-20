using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Library.Interfaces.Hardware;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.Views.MainWindow
{
    public class MainWindowViewModel : BindableBase, IDisplay
    {
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

