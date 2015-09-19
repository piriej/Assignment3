using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Library.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace Library.ViewModels
{
    class MainWindowViewModel:BindableBase
    {
        public IRegionManager RegionManager { get; set; }
        public ICommand TestCommand { get; set; }

        public MainWindowViewModel(IRegionManager regionManager)
        {
            RegionManager = regionManager;
            this.TestCommand = new DelegateCommand<string>(Test);
        }

        void Test(string uri)
        {
            RegionManager.RequestNavigate(RegionNames.ContentRegion, uri);
        }
    }
}
