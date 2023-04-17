using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using PackTracker.MVVM.ViewModels;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class PackagePage : ContentPage
{
	public PackagePage()
	{ 
		InitializeComponent();
		BindingContext = new PackageViewModel(Navigation);
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
        PackageViewModel vm = (PackageViewModel)BindingContext;
        vm.SelectedPackage = (e.CurrentSelection.FirstOrDefault() as Package);
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {

        PackageViewModel vm = (PackageViewModel)BindingContext;
        vm.SelectedPackage = (Package)e.Parameter;

        ItemsPage itemsPage = new(vm.SelectedPackage)
        {
            Title = vm.SelectedPackage.Name
        };

        Navigation.PushAsync(itemsPage);
    }

    void DeleteSwipeItem_Invoked(System.Object sender, System.EventArgs e)
    {
        SwipeItem swipeItem = (SwipeItem)sender;

        PackageViewModel vm = (PackageViewModel)BindingContext;

        vm.SelectedPackage = (Package)swipeItem.CommandParameter;

        vm.DeletePackage(vm.SelectedPackage);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //PackageViewModel vm = (PackageViewModel)BindingContext;
        //vm.Refresh();
    }
}
