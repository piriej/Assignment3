using Library.Features.MainWindow;
using Library.Features.ScanBook;
using Microsoft.Practices.Prism.PubSubEvents;
using Prism.Mvvm;

namespace Library.Features.SwipeCard
{
    class SwipeCardViewModel:BindableBase
    {
        public ISwipeCardController Controller { get; set; }

        public SwipeCardViewModel()
        {
            
        }

        //public SwipeCardViewModel(IEventAggregator EventAggregator)
    
    }
}
