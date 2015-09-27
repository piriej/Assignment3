using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AcceptanceTests.PageObjects;
using AcceptanceTests.PageObjects.Pages;
using FluentAssertions;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

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

        [Then(@"The system displays the borrowers existing loan details as:")]
        public void ThenTheSystemDisplaysTheBorrowersExistingLoanDetailsAs(Table table)
        {
            //var x = new List<string>
            //{
            //    "Loan ID:            	2",
            //    "Author:    author1",
            //    "Title:              	title2",
            //    "Borrower:           	fName2 lName2",
            //    "Borrow Date:        	27 / 09 / 2015",
            //    "Due Date:           	11 / 10 / 2015"
            //};

            var set = string.Join("\n",table.Rows.Select(row => row.Values.FirstOrDefault()));

            ScanBookRegion.ExistingLoans.Should().Be(set);

        }

    }
}
