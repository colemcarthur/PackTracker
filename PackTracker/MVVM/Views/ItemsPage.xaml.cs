
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class ItemsPage : ContentPage
{


	public ItemsPage(Package package)
	{
		InitializeComponent();
		BindingContext = new ItemViewModel(package);

	}

    void AddItemButton_Clicked(System.Object sender, System.EventArgs e)
    {
		Navigation.PushModalAsync(new ItemEntryPage());
    }
}
