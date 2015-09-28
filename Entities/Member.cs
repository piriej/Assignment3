using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Extras.Attributed;
using Library.Interfaces.Entities;

namespace Library.Entities
{
    public class Member : IMember
    {
        private const int LoanLimit = 5;
        private const double FineLimit = 10D;

        private readonly List<ILoan> _loans;

        private readonly int _id;
        private readonly string _firstName;
        private readonly string _lastName;
        private readonly string _contactPhone;
        private readonly string _emailAddress;

        private MemberState _state;

        public bool HasOverDueLoans => Loans.Any(loan => loan.IsOverDue);

        public bool HasReachedLoanLimit => _loans.Count >= LoanLimit;

        public bool HasFinesPayable => FineAmount > 0D;

        public bool HasReachedFineLimit => FineAmount >= FineLimit;

        public float FineAmount { get; private set; }

        public List<ILoan> Loans => new List<ILoan>(_loans);

        public string FirstName => _firstName;

        public string LastName => _lastName;

        public string ContactPhone => _contactPhone;

        public string EmailAddress => _emailAddress;

        public int ID => _id;

        public MemberState State => _state;

        private bool BorrowingAllowed => !(HasReachedLoanLimit || HasReachedFineLimit || HasOverDueLoans);


        public Member([WithKey("FirstName")]string firstName, [WithKey("LastName")]string lastName, [WithKey("Phone")]string contactPhone, [WithKey("Email")]string email, int memberId)
        {
            if (!(string.IsNullOrEmpty(firstName)
                  || !string.IsNullOrEmpty(lastName)
                  || string.IsNullOrEmpty(contactPhone)
                  || string.IsNullOrEmpty(email))
                || memberId < 0)
                throw new ArgumentException("Error in parameter list.");

            _firstName = firstName;
            _lastName = lastName;
            _contactPhone = contactPhone;
            _emailAddress = email;
            _id = memberId;

            _state = MemberState.BORROWING_ALLOWED;

            _loans = new List<ILoan>();

            FineAmount = 0f;
        }

        public void AddFine(float fine)
        {
            if (fine < 0D)
                throw new ArgumentException("Error: fine can't be negative.");

            FineAmount += fine;

            CheckState();
        }

        private void CheckState()
        {
            _state = BorrowingAllowed ? MemberState.BORROWING_ALLOWED : MemberState.BORROWING_DISALLOWED;
        }

        public void PayFine(float payment)
        {
            if (payment > FineAmount || payment < 0D)
                throw new ArgumentException("Error: Payment must be positive and cannot exceed the fine amount.");

            FineAmount -= payment;

            CheckState();
        }

        public void AddLoan(ILoan loan)
        {
            if (!BorrowingAllowed)
                throw new ApplicationException($"Borrowing not allowed invalid state: {_state}");

            if (loan == null)
                throw new ArgumentException("Error: Loan is null.");

            _loans.Add(loan);

            CheckState();
        }

        public void RemoveLoan(ILoan loan)
        {
            if (loan == null)
                throw new ArgumentException("Error: loan is null.");

            if (!_loans.Contains(loan))
                throw new ArgumentException("Error: Loan doesn't exist.");

            _loans.Remove(loan);

            CheckState();
        }

        public override string ToString()
        {
            var propertyList = new Dictionary<string, string>
            {
                {"Id", _id.ToString()},
                {"Name", _firstName + " " + _lastName},
                {"Contact Phone", _contactPhone},
                {"Email Address", _emailAddress},
                {"Outstanding Charges", $"{FineAmount:$0.00}"}
            };

            var result = string.Join(Environment.NewLine, propertyList.Select(row => $"{row.Key,-20}:\t{row.Value,5}"));
            return result;
        }

        /*

Entities 

Member – implements IMember
, emailAddress are null or blank  id is less than or 
equal to zero 




hasFinesPayable: boolean 
returns true if the fines owing by the Member exceeds zero 
returns false otherwise 

hasReachedFineLimit: boolean 
returns true if:
the fines owing by the Member is equals or is greater than FINE_MAX 
returns false otherwise 

getFineAmount: float 
returns the amount of fines owing by the Member 

addFine: void 
Parameters: amount:float 
Increments the fines owing by amount 
Throws IllegalArgumentException if: 
amount is negative 

payFine: void 
Parameters: amount:float 
Decrements the fines owing by amount 
Throws IllegalArgumentException if: 
amount is negative, or amount exceeds fines owing 

addLoan: void 
Parameters: loan:ILoan 
Adds loan to the Members list of current loans 
Throws IllegalArgumentException if: 
loan is null  || 
member is currently BORROWING_DISALLOWED 


getLoans: List<ILoan> 
returns a copy of the Members current loan list 

removeLoan: void 
Parameters: loan:ILoan 
Removes a loan from the Members list of current loans 
Throws IllegalArgumentException if: 
loan is null, or loan not present in current loans

getState: EMemberState
returns the current MemberState of the Member 

getFirstName: string 
returns the firstName of the Member 

getLastName: string 
returns the lastName of the Member 

getContactPhone: string 
returns the contactPhone of the Member 

getEmailAddress: string 
returns the emailAddress of the Member 

getID: int 
returns the unique id of the member 


*/
    }
}
