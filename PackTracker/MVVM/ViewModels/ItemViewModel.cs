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

		public ItemViewModel(Package package, Item item = null)
		{
			try
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
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
			}

		}

        public void DeleteItem(Item item)
        {
            try
            {
                if (item is null)
                    return;

                App.ItemsRepo.Delete(item);

                Refresh();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        public void Save(Item newItem)
		{
            try
            {
                Package.Items.Add(newItem);
                App.ItemsRepo.Save(newItem);
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
                Package = App.PackagesRepo.GetItemsWithChildren(Package.Id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
	}
}

