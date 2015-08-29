using Library.Interfaces.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Entities
{
    public class Member : IMember
    {
        public Member(String firstName, String lastName, String contactPhone,
                      String email, int memberID)
        {
            if (!sane(firstName, lastName, contactPhone, email, memberID))
            {
                throw new ArgumentException("Member: constructor : bad parameters");
            }
            this.firstName = firstName;
            this.lastName = lastName;
            this.contactPhone = contactPhone;
            this.emailAddress = email;
            this.id = memberID;
            this.loanList = new List<ILoan>();
            this.fineAmount = 0.0f;
            this._state = MemberState.BORROWING_ALLOWED;
        }

        private bool sane(string firstName, string lastName, string contactPhone,
                string emailAddress, int memberID)
        {
            return (!string.IsNullOrEmpty(firstName) &&
                    !string.IsNullOrEmpty(lastName) &&
                    !string.IsNullOrEmpty(contactPhone) &&
                    !string.IsNullOrEmpty(emailAddress) &&
                     memberID > 0
                    );
        }

        public bool HasOverDueLoans
        {
            get
            {
                foreach (ILoan loan in loanList)
                {
                    if (loan.IsOverDue) return true;
                }
                return false;
            }
        }

        public bool HasReachedLoanLimit
        {
            get { return loanList.Count >= MemberConstants.LOAN_LIMIT; }
        }

        public bool HasFinesPayable
        {
            get { return fineAmount > 0.0f; }
        }

        public bool HasReachedFineLimit
        {
            get { return fineAmount >= MemberConstants.FINE_LIMIT; }
        }

        private float fineAmount;
        public float FineAmount
        {
            get { return fineAmount; }
        }

        public void AddFine(float fine)
        {
            if (fine < 0)
            {
                throw new ArgumentException("Member: AddFine : fine cannot be negative");
            }
            fineAmount += fine;
            updateState();
        }

        public void PayFine(float payment)
        {
            if (payment < 0 || payment > fineAmount)
            {
                throw new ArgumentException("Member: PayFine : payment cannot be negative, or greater than amount owed");
            }
            fineAmount -= payment;
            updateState();
        }

        public void AddLoan(ILoan loan)
        {
            if (loan == null)
            {
                throw new ArgumentException("Member: AddLoan : loan cannot be null");
            }
            if (!BorrowingAllowed)
            {
                string mesg = String.Format("Member: AddLoan : illegal operation in state: {0}", _state);
                throw new ApplicationException(mesg);
            }
            loanList.Add(loan);
            updateState();
        }

        private List<ILoan> loanList;
        public List<ILoan> Loans
        {
            get
            {
                return new List<ILoan>(loanList);
            }
        }

        public void RemoveLoan(ILoan loan)
        {
            if (loan == null)
            {
                throw new ArgumentException("Member: RemoveLoan : loan cannot be null");
            }
            if (!loanList.Contains(loan))
            {
                throw new ArgumentException("Member: RemoveLoan : loan not present");
            }
            loanList.Remove(loan);
            updateState();

        }

        private string firstName;
        public string FirstName
        {
            get { return firstName; }
        }

        private string lastName;
        public string LastName
        {
            get { return lastName; }
        }

        private string contactPhone;
        public string ContactPhone
        {
            get { return contactPhone; }
        }

        private string emailAddress;
        public string EmailAddress
        {
            get { return emailAddress; }
        }

        private int id;
        public int ID
        {
            get { return id; }
        }

        private MemberState _state;
        public MemberState State
        {
            get { return _state; }
        }

        private bool BorrowingAllowed
        {
            get
            {
                return !HasReachedFineLimit && !HasReachedLoanLimit && !HasOverDueLoans;
            }
        }

        MemberState IMember.State
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        private void updateState()
        {
            if (BorrowingAllowed)
            {
                _state = MemberState.BORROWING_ALLOWED;
            }
            else
            {
                _state = MemberState.BORROWING_DISALLOWED;
            }
        }

        public override string ToString()
        {
            string cr = Environment.NewLine;
            return String.Format("{1,-20}\t{2} {0}{3,-20}\t{4}{5} {0}{6,-20}\t{7} {0}{8,-20}\t{9} {0}{10,-20}\t{11:$0.00}",
                                  cr,
                                  "Id:                  ", id,
                                  "Name:                ", firstName, lastName,
                                  "Contact Phone:       ", contactPhone,
                                  "Email Address:       ", emailAddress,
                                  "Outstanding Charges: ", fineAmount);
        }


    }
}
