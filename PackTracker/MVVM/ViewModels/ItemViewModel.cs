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

		[ObservableProperty]
		Int32 purchaseLocationLabelWidth = 100;

		[ObservableProperty]
		Int32 purchasePriceLabelWidth = 0;

		public ItemViewModel(Package package, Item item = null)
		{
			Package = package;

			if (item == null)
			{
				Item = new Item()
				{
					PackageID = package.Id,
					Description = "",
					PurchasePrice = 0,
					PurchaseLocation = ""
				};
			}
			else
			{
				Item = item;
			}

		}

        public void DeleteItem(Item item)
        {
            if (item is null)
                return;

            App.ItemsRepo.Delete(item);

            Refresh();
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

