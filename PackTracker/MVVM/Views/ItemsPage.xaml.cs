

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

        try
        {
            this.package = package;

            viewModel = new ItemViewModel(package);

            BindingContext = viewModel;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

	}

    async void AddItemButton_Clicked(System.Object sender, System.EventArgs e)
    {

        try
        {
            ItemEntryPage page = new ItemEntryPage(package);

            await Navigation.PushModalAsync(page);

            Item item = await page.GetFormDataAsync();

            if (item != null)
            {

                viewModel.Save(item);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    async void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {

        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    void DeleteSwipeItem_Invoked(System.Object sender, System.EventArgs e)
    {
        try
        {
            SwipeItem swipeItem = (SwipeItem)sender;

            Item item = (Item)swipeItem.CommandParameter;

            viewModel.DeleteItem(item);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            viewModel.Refresh();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

}
