using Library.Interfaces.Daos;
using Xunit.Extensions;
using Library.Controllers.Borrow;
using Library.Entities;
using Library.Features.MainWindow;
using Library.Interfaces.Entities;
using Library.Interfaces.Hardware;
using NSubstitute;

namespace UnitTests.ControllerTests
{
    public class BorrowControllerTests
    {
        [Theory, AutoNSubstituteData]
        public void CardSwiped_xx_xx(IMemberDAO memberDao, ICardReaderEvents cardReaderEvents, IMember member, IMainWindowController mainWindowController)
        {
            // Retrieve a member from the DAO
            memberDao.GetMemberByID(member.ID).Returns(x => member);

            //var controller = new BorrowController(memberDao, mainWindowController /*, cardReaderEvents*/);

            //controller.cardSwiped(member.ID);


        }
    }
}
