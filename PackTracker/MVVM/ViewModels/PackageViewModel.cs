
using PackTracker.MVVM.Models;
using PackTracker.MVVM.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace PackTracker.MVVM.ViewModels
{

	partial class PackageViewModel : ObservableObject 
	{

        private readonly INavigation _navigation;

        [ObservableProperty]
        List<Package> packages;

        public Package SelectedPackage { get; set; }

		public PackageViewModel(INavigation navigation)
		{

            _navigation = navigation;

            Refresh();
        }

        public void AddOrUpdatePackage(Package package)
        {
            App.PackagesRepo.Save(package);

            Refresh();
        }

        public void DeletePackage(Package package)
        {
            if (package is null)
                return;

            App.PackagesRepo.Delete(package);

            Refresh();
        }
  
        public void Refresh()
        {
            Packages = App.PackagesRepo.GetItemsWithChildren();
        }
    }

}

