

using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class ItemsPage : ContentPage
{

	private Package package { get; set; }
	private ItemViewModel viewModel { get; set; }

	public ItemsPage(Package package)
	{
		InitializeComponent();
		this.package = package;

		viewModel = new ItemViewModel(package);

        BindingContext = viewModel;

	}

    async void AddItemButton_Clicked(System.Object sender, System.EventArgs e)
    {
		
		ItemEntryPage page = new ItemEntryPage(package);

		await Navigation.PushModalAsync(page);

		Item item = await page.GetFormDataAsync();

		if (item != null)
		{
			
			viewModel.Save(item);
		}
    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {

        Item item = (Item)e.Parameter;

		ItemEntryPage page = new ItemEntryPage(package, item);

        await Navigation.PushModalAsync(page);

		item = await page.GetFormDataAsync();

        if (item != null)
        {

            viewModel.Save(item);
        }
    }

    void DeleteSwipeItem_Invoked(System.Object sender, System.EventArgs e)
    {
        SwipeItem swipeItem = (SwipeItem)sender;

        Item item = (Item)swipeItem.CommandParameter;

        viewModel.DeleteItem(item);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

		viewModel.Refresh();
    }

}
