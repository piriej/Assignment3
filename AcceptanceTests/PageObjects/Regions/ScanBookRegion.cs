using AcceptanceTests.Help;
using AcceptanceTests.PageObjects.Pages;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects
{
    public class ScanBookRegion : ContentRegionBaseObject
    {
        const string BorrowerIdLabelName = "BorrowerIdLabel";
        const string ContactLabelName = "ContactLabel";
        const string NameLabelName = "NameLabel";
        const string ExistingLoanBox = "existingLoanBox";

        public ScanBookRegion() : base(ViewNames.ScanBookControl)
        {

        }

        public object BorrowerId => this.GetLabelText(BorrowerIdLabelName);
        public object BorrowerName => this.GetLabelText(NameLabelName);
        public object ContactNumber => this.GetLabelText(ContactLabelName);
        public object ExistingLoans => this.GetLabelText(ExistingLoanBox);


    }
}