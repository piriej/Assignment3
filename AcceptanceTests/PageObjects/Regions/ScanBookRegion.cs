using AcceptanceTests.Help;
using AcceptanceTests.PageObjects.Pages;
using Library.ApplicationInfratructure;

namespace AcceptanceTests.PageObjects
{
    public class ScanBookRegion : ContentRegionBaseObject
    {
        private const string BorrowerIdLabelName = "BorrowerIdLabel";
        private const string ContactLabelName = "ContactLabel";
        private const string NameLabelName = "NameLabel";

        public ScanBookRegion() : base(ViewNames.ScanBookControl)
        {

        }

        public object BorrowerId => this.GetLabelText(BorrowerIdLabelName);
        public object BorrowerName => this.GetLabelText(NameLabelName);
        public object ContactNumber => this.GetLabelText(ContactLabelName);

    }
}