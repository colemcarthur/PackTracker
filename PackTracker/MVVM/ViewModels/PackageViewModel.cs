
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
            App.PackagesRepo.Delete(package);

            Refresh();
        }

        [RelayCommand]
        public async Task GoToItemsAsync(Package package)
        {

            if (package is null)
                return;

            ItemsPage itemsPage = new(package)
            {
                Title = package.Name
            };

            await _navigation.PushAsync(itemsPage);
        }


        private void Refresh()
        {
            Packages = App.PackagesRepo.GetItemsWithChildren();

        }
    }

}

