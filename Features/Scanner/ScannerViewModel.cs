using Microsoft.Practices.Prism.Mvvm;
using Prism.Commands;

namespace Library.Features.Scanner
{
    class ScannerViewModel:BindableBase, IScannerViewModel
    {
        private readonly IScannerController _controller;

        public ScannerViewModel(IScannerController controller)
        {
            _controller = controller;
            ScanCommand = new DelegateCommand<string>(controller.Scanned).ObservesCanExecute(p => Enabled);
        }

        public System.Windows.Input.ICommand ScanCommand { get; set; }

        public IScannerController ScannerController { get; set; }

        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set { SetProperty(ref _barCode, value); }
        }

        private bool _enabled;
        public bool Enabled
        {
            get { return _enabled; }
            set { SetProperty(ref _enabled, value); }
        }
    }
}
