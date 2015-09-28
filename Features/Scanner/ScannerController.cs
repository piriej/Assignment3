using Library.Features.ScanBook;
using Prism.Events;

namespace Library.Features.Scanner
{
    public class ScannerController : IScannerController
    {
        private ScanBookModel _scanBookModel;
        public IEventAggregator EventAggregator { get; set; }
        public IScannerViewModel ViewModel { get; set; }

        public ScannerController(IEventAggregator EventAggregator)
        {
            EventAggregator.GetEvent<Messages.ScanningEvent>().Subscribe(InitialiseScanner);
        }

        public void InitialiseScanner(ScanBookModel scanBookModel)
        {
            _scanBookModel = scanBookModel;
            ViewModel.Enabled = true;
        }

        public void Scanned(string barCode)
        {
            _scanBookModel.Barcode = int.Parse(barCode);
            EventAggregator.GetEvent<Messages.ScanningRecievedEvent>().Publish(_scanBookModel);
        }
    }
}