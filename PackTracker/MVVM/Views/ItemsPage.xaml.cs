using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class ItemsPage : ContentPage
{
	public ItemsPage(Package viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}
