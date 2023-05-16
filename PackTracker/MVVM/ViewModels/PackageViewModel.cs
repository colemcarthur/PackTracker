
using PackTracker.MVVM.Models;
using PackTracker.MVVM.Views;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel;
using SQLite;
using System.Diagnostics;

namespace PackTracker.MVVM.ViewModels
{

    partial class PackageViewModel : ObservableObject, INotifyPropertyChanged
	{

        [ObservableProperty]
        List<Package> packages;

        [RelayCommand]
        public void PerformSearch(String query)
        {
            try
            {
                // Get the list of pacakges held in the database
                List<Package> currentPackages = App.PackagesRepo.GetItemsWithChildren();

                // Find out what items have the same name
                List<Item> filterItems = currentPackages.SelectMany(p => p.Items)
                    .Where(i => i.Description.ToLower().Contains(query.ToLower())).ToList();

                // Build the list of packages from the found items
                List<Package> filteredPackages = new List<Package>();

                Package foundPackage;

                foreach (Item item in filterItems)
                {
                    // Find the package to add to the filtered list
                    foundPackage = currentPackages.Find(p => p.Id == item.PackageID);

                    if (foundPackage != null)
                    {
                        // Double check if package hasn't already been added
                        if (filteredPackages.Find(p => p.Id == foundPackage.Id) == null)
                            filteredPackages.Add(foundPackage);
                    }
                }

                Packages = filteredPackages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
           

        }

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
            try
            {
                Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        public void AddOrUpdatePackage(Package package)
        {

            try
            {
                App.PackagesRepo.Save(package);

                Refresh();
            } catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void AddOrUpdateItem(Item item)
        {
            try
            {
                SelectedPackage.Items.Add(item);
                App.ItemsRepo.Save(item);

                Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void DeletePackage(Package package)
        {
            try
            {
                if (package is null)
                    return;

                App.PackagesRepo.Delete(package);

                Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
  
        public void Refresh()
        {
            try
            {
                Packages = App.PackagesRepo.GetItemsWithChildren();

                TotalValue = App.PackagesRepo.TotalValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }

}

