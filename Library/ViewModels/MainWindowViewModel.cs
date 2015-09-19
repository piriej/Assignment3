using System;
using System.Windows;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Library.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        public MainWindowViewModel()
        {
            this.CloseWindowCommand = new DelegateCommand(CloseWindow);
        }

        public ICommand CloseWindowCommand { get; set; }

        public Action CloseWindowDelegate { get; set; } = () =>
        {
            Application.Current.Shutdown();
        };

        void CloseWindow()
        {
            Console.WriteLine("detected Window closing");
            CloseWindowDelegate.Invoke();
        }
    }
}

