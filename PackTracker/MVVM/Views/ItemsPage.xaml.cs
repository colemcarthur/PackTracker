
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

    void AddItemButton_Clicked(System.Object sender, System.EventArgs e)
    {
		viewModel.AddNewItem();
		Navigation.PushModalAsync(new ItemEntryPage(viewModel));
    }

}
