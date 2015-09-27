using Prism.Mvvm;

namespace Library.Features.ScanBook
{
    public interface IScanBookViewModel
    {
        int BorrowerId { get; set; }
        string Name { get; set; }
        string Contact { get; set; }
    }

    public class ScanBookViewModel : BindableBase, IScanBookViewModel
    {
        public IScanBookController Controller { get; set; }

        public ScanBookViewModel(IScanBookController controller)
        {
            Controller = controller;
        }

        private int _borrowerId;
        public int BorrowerId
        {
            get { return _borrowerId; }
            set { SetProperty(ref _borrowerId, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private string _contact;
        public string Contact
        {
            get { return _contact; }
            set { SetProperty(ref _contact, value); }
        }
    }
}
