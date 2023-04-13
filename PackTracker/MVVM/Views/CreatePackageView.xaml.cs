using PackTracker.MVVM.ViewModels;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class CreatePackageView : ContentPage
{
	public CreatePackageView()
	{
		InitializeComponent();
		BindingContext = new PackageViewModel();

        List<Package> pkgs = App.PackagesRepo.GetItemsWithChildren();
    }
}
