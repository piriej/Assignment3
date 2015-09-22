using Library.ApplicationInfratructure;
using Library.Features.Borrowing;
using Library.Features.ScanBook;
using Library.Features.SwipeCard;
using Prism.Regions;
using Prism.Modularity;

namespace Library
{
    public class ContentRegionModule : IModule
    {
        readonly IRegionViewRegistry _regionViewRegistry;

        public ContentRegionModule(IRegionViewRegistry registry)
        {
            _regionViewRegistry = registry;
        }

        public void Initialize()
        {
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(BorrowingView));
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(SwipeCardView));
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(ScanBookView));
        }
    }
}
