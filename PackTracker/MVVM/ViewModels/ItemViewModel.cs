using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.ViewModels
{
	partial class ItemViewModel : ObservableObject
	{

		[ObservableProperty]
		Package package;

		[ObservableProperty]
		String description;

		[ObservableProperty]
		Double purchasePrice;

		public ItemViewModel(Package package)
		{
			Package = package;
		}

		public void AddNewItem()
		{

			Description = "";
			PurchasePrice = 0.00;
		}

		public void Save()
		{

			Item item = new Item()
			{
                Description = Description,
                PurchasePrice = PurchasePrice,
                PackageID = Package.Id,
                CreationDate = DateTime.Now
            };

			Package.Items.Add(item);

			App.ItemsRepo.Save(item);
		}
	}
}

