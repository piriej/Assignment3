using System;
using System.Collections.Generic;
using Library.Interfaces.Entities;

namespace Library.Entities
{
    public class Member : IMember
    {

        // hasOverDueLoans: boolean
        // returns true if: 
        // any existing loan is overdue
        // returns false otherwise
        public bool HasOverDueLoans { get; }

        
        // hasReachedLoanLimit: boolean 
        // returns true if: 
        // number of existing loans equal LOAN_LIMIT 
        // returns false otherwise 
        public bool HasReachedLoanLimit { get; }


        public bool HasFinesPayable { get; }
        public bool HasReachedFineLimit { get; }
        public float FineAmount { get; }
        public int ID { get; }
        public MemberState State { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactPhone { get; set; }
        public string EmailAddress { get; set; }
        
        //        Constructor : Member
        //Parameters: 
        //firstName:string, lastName:string, contactPhone :string, emailAddress:string, id:int
        //throws IllegalArgumentException if: 
        //any of firstName, lastName, contactPhone
        public Member(int id, string firstName, string lastName, string contactPhone, string emailAddress)
        {
            FirstName = firstName;
            LastName = lastName;
            ContactPhone = contactPhone;
            EmailAddress = emailAddress;
            ID = id;
        }

        public void AddFine(float fine)
        {
            throw new NotImplementedException();
        }

        public void PayFine(float payment)
        {
            throw new NotImplementedException();
        }

        public void AddLoan(ILoan loan)
        {
            throw new NotImplementedException();
        }

        public List<ILoan> Loans { get; }
        public void RemoveLoan(ILoan loan)
        {
            throw new NotImplementedException();
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
