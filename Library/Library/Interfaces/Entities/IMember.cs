using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Interfaces.Entities
{
    public interface IMember
    {
        bool HasOverDueLoans
        {
            get;
        }

        bool HasReachedLoanLimit
        {
            get;
        }

        bool HasFinesPayable
        {
            get;
        }

        bool HasReachedFineLimit
        {
            get;
        }

        float FineAmount
        {
            get;
        }

        void AddFine(float fine);

        void PayFine(float payment);

        void AddLoan(ILoan loan);

        List<ILoan> Loans
        {
            get;
        }

        void RemoveLoan(ILoan loan);

        string FirstName
        {
            get;
        }

        string LastName
        {
            get;
        }

        string ContactPhone
        {
            get;
        }

        string EmailAddress
        {
            get;
        }

        int ID
        {
            get;
        }

        MemberState State
        {
            get;
        }

    }

}
