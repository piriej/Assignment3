using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTests.PageObjects;
using AcceptanceTests.PageObjects.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;

namespace AcceptanceTests.Steps.Assert
{
    [Binding]
    public sealed class BorrowingAssertions
    {
        ScanBookRegion ScanBookRegion { get; set; }

        public BorrowingAssertions(ScanBookRegion scanBookRegion)
        {
            ScanBookRegion = scanBookRegion;
        }

        [Then(@"The system displays the borrowers details as Id of '(.*)', Name '(.*)', Contact: '(.*)'")]
        public void ThenTheSystemDisplaysTheBorrowersDetailsAsIdOfNameContact(string id, string name, string contact)
        {
            ScanBookRegion.BorrowerId.Should().Be(id);
            ScanBookRegion.BorrowerName.Should().Be(name);
            ScanBookRegion.ContactNumber.Should().Be(contact);
        }

    }
}
