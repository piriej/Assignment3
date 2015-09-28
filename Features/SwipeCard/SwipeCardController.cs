using Library.Features.MainWindow;
using Library.Interfaces.Daos;
using Prism.Events;



namespace Library.Features.SwipeCard
{
    internal interface ISwipeCardController
    {
    }

    class SwipeCardController : ISwipeCardController
    {
        public IEventAggregator EventAggregator { get; set; }
        public IMainWindowController MainController { get; set; }
        public IMemberDAO MemberDao { get; set; }

        public SwipeCardController(IEventAggregator eventAggregator)
        {
            EventAggregator = eventAggregator;

            //EventAggregator.GetEvent<Messages.BorrowingStateEvent>().Subscribe(CardSwiped);
        }


    }
}
