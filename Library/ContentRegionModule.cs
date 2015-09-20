using Prism.Regions;
using Library.ViewModels;
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
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.Borrowing.Borrowing));
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.SwipeCard));
            _regionViewRegistry.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(Views.ScanBookControl));
        }
    }
}
