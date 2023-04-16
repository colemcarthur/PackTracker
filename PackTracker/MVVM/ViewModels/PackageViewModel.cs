
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

        public Package CurrentPackage { get; set; }

        [ObservableProperty]
        List<Package> packages;

		public ICommand AddOrUpdateCommand { get; set; } 

		public PackageViewModel(INavigation navigation)
		{

            _navigation = navigation;

			Packages = App.PackagesRepo.GetItemsWithChildren();

			AddOrUpdateCommand = new Command( () =>
			{
				App.PackagesRepo.SaveItem(CurrentPackage);
            });

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
    }

}

