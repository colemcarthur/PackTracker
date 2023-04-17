using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(Package packages)
	{
		InitializeComponent();
		BindingContext = packages;
	}

    void AddItemButton_Clicked(System.Object sender, System.EventArgs e)
    {
		Package package = (Package)BindingContext;

        Item item = new Item()
        {
            PackageID = package.Id,
            CreationDate = DateTime.Now,
            Value = 33.32,
            Description = "Lights",
            Image = new byte[] { 122, 223, 23, 2, 3, 66 }
        };

        package.Items.Add(item);

        App.ItemsRepo.Save(item);


    }
}
