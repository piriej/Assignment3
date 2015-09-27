using Prism.Mvvm;

namespace Library.Features.ScanBook
{
    class ScanBookViewModel : BindableBase
    {
        public IScanBookController controller;

        public ScanBookViewModel()
        {
            var tmp = controller.GetHashCode();
        }

        private int _borrowerId;
        public int Enabled
        {
            get { return _borrowerId; }
            set { SetProperty(ref _borrowerId, value); }
        }
    }
}
