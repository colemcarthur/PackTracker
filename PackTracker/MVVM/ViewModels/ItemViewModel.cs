using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.ViewModels
{
	partial class ItemViewModel : ObservableObject
	{

		[ObservableProperty]
		Package package;

		[ObservableProperty]
		Item item;

		public ItemViewModel(Package package)
		{
			Package = package;

			Item = new Item()
			{
				PackageID = package.Id,
				Description = "",
				PurchasePrice = 0,
				PurchaseLocation = ""
			};
			
		}

		public void Save(Item newItem)
		{
            Package.Items.Add(newItem);
			App.ItemsRepo.Save(newItem);
		}

		public void Refresh()
		{
			Package = App.PackagesRepo.GetItemsWithChildren(Package.Id);
        }
	}
}

