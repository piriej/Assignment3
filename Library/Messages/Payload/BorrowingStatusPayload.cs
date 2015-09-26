using Library.Interfaces.Controllers.Borrow;
using Library.Interfaces.Entities;

namespace Library.Messages.Payload
{
    public class BorrowingStatusPayload 
    {
        public EBorrowState BorrowState { get; set; }
        public IMember Member { get; set; }
    }
}