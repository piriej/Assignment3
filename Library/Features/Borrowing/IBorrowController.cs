using Library.Interfaces.Daos;

namespace Library.Controllers.Borrow
{
    public interface IBorrowController
    {
        IMemberDAO MemberDao { get; set; }
        void WaitForCardSwipe();
    }
}