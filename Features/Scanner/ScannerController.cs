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
            int barCodeInt;
            int.TryParse(barCode ,out barCodeInt);
            _scanBookModel.Barcode = barCodeInt;
            EventAggregator.GetEvent<Messages.ScanningRecievedEvent>().Publish(_scanBookModel);
        }
    }
}