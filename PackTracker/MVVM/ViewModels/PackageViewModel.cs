using System;
using PropertyChanged;
using PackTracker.MVVM.Models;
using System.Windows.Input;
using ZXing.OneD;

namespace PackTracker.MVVM.ViewModels
{

	[AddINotifyPropertyChangedInterface]
	public class PackageViewModel
	{
		public Package CurrentPackage { get; set; } 

		public ICommand AddOrUpdateCommand { get; set; }

		public PackageViewModel()
		{
			if (CurrentPackage == null)
				CurrentPackage = new Package();

			AddOrUpdateCommand = new Command( () =>
			{
				App.PackagesRepo.SaveItem(CurrentPackage);
				
			});
		}
	}
}

