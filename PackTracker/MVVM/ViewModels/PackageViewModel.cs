
using PackTracker.MVVM.Models;
using PackTracker.MVVM.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using SQLite;

namespace PackTracker.MVVM.ViewModels
{

    partial class PackageViewModel : ObservableObject, INotifyPropertyChanged
	{

        [ObservableProperty]
        List<Package> packages;

        public Int32 Count
        {
            get
            {
                // Your custom logic here
                return Packages.Count;

            }
        }

        private Double totalValue = 0;

        public Double TotalValue
        {
            get
            {
                return totalValue;
            }
            private set
            {
                totalValue = value;
            }
        }

        public Package SelectedPackage { get; set; }

		public PackageViewModel()
		{
            Refresh();
        }

        public void AddOrUpdatePackage(Package package)
        {
            App.PackagesRepo.Save(package);

            Refresh();
        }

        public void AddOrUpdateItem(Item item)
        {
            SelectedPackage.Items.Add(item);
            App.ItemsRepo.Save(item);

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
            TotalValue = App.PackagesRepo.TotalValue();
        }

    }

}

