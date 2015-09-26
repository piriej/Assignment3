using AutoMapper;
using Prism.Events;

namespace Library.Features.CardReader
{
    class CardReaderController : ICardReaderController
    {
        public ICardReaderViewModel ViewModel { get; set; }
        public IEventAggregator EventAggregator { get; set; }

        public void CardSwiped(string borowerId)
        {
            // Get a model.
            var model = Mapper.Map<CardReaderModel>(ViewModel);

            // Notify the Borrowing screen of the borrowers details.
            EventAggregator.GetEvent<Messages.CardReaderSwipedEvent>().Publish(model);
        }
    }
}
