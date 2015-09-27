using System;
using System.Linq;
using System.Windows.Controls;
using Library.ApplicationInfratructure;
using Library.Controllers.Borrow;
using Library.Interfaces.Controllers.Borrow;
using Prism.Regions;

namespace Library.Features.MainWindow
{
    public interface IMainWindowController
    {
        T NavigateTo<T>() where T : UserControl;
        EBorrowState BorrowState { get; set; }
    }

    public class MainWindowController : IMainWindowController
    {
        // Moving this from ctor to prop injection IIBorrowController should autowire properties.
        public IBorrowController BorrowController { get; set; }
        public IRegionManager Regionmanager { get; set; }
        UserControl Region { get; set; }
        public EBorrowState BorrowState { get; set; }


        public MainWindowController(IRegionManager regionmanager)
        {
            Regionmanager = regionmanager;
        }

  
        /// <summary>
        /// Navigate to the user control of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns>The view requested.</returns>
        public T NavigateTo<T>() where T : UserControl
        {
            // Name of the view (it's url).
            var name = typeof(T).Name;

            // Navigate to it.
            Regionmanager.RequestNavigate(RegionNames.ContentRegion, name);

            // Get the content region.
            var region = Regionmanager.Regions[RegionNames.ContentRegion];

            // Construct the pack URI for the required view.
            var uriPath = $"/features/{name.Replace("View", "")}/{name}";
            var uri = new Uri(uriPath, UriKind.Relative);

            // Add the navigation to the navigation journal so we can come back later and ispect the "last" navigation.
            region.NavigationService.Journal.RecordNavigation(new RegionNavigationJournalEntry { Uri = uri });

            // Return the currently active view.
            var currentView = (T)Regionmanager.Regions[RegionNames.ContentRegion].ActiveViews.FirstOrDefault();
            
            return currentView;
        }

    }
}
