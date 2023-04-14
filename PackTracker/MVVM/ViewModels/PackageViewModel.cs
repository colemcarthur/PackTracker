using System;
using PropertyChanged;
using PackTracker.MVVM.Models;
using System.Windows.Input;

namespace PackTracker.MVVM.ViewModels
{

	[AddINotifyPropertyChangedInterface]
	public class PackageViewModel
	{
		public Package CurrentPackage { get; set; } 

		public List<Package> Packages { get; set; }

		public ICommand AddOrUpdateCommand { get; set; } 

		public PackageViewModel()
		{

			Packages = App.PackagesRepo.GetItemsWithChildren();

			AddOrUpdateCommand = new Command( () =>
			{
				App.PackagesRepo.SaveItem(CurrentPackage);
				
			});
		}
	}
}

