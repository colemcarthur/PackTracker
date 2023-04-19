using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using PackTracker.MVVM.ViewModels;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class PackagePage : ContentPage
{
    private PackageViewModel viewModel { get; set; }

	public PackagePage()
	{ 
		InitializeComponent();

        viewModel = new PackageViewModel();

		BindingContext = viewModel;
	}

    async void AddPackageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        var popup = new PackageEntryPopup();

        var result = await this.ShowPopupAsync(popup);

        if (result != null)
        {
            PackageViewModel vm = (PackageViewModel)BindingContext;

            vm.AddOrUpdatePackage(new Package()
            {
                Name = result.ToString(),
                CreationDate = DateTime.Now
            });

        }
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        
        viewModel.SelectedPackage = (e.CurrentSelection.FirstOrDefault() as Package);
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        
        viewModel.SelectedPackage = (Package)e.Parameter;

        ItemsPage itemsPage = new(viewModel.SelectedPackage)
        {
            Title = viewModel.SelectedPackage.Name
        };

        Navigation.PushAsync(itemsPage);
    }

    void DeleteSwipeItem_Invoked(System.Object sender, System.EventArgs e)
    {
        SwipeItem swipeItem = (SwipeItem)sender;

        viewModel.SelectedPackage = (Package)swipeItem.CommandParameter;

        viewModel.DeletePackage(viewModel.SelectedPackage);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        viewModel.Refresh();
    }

}
