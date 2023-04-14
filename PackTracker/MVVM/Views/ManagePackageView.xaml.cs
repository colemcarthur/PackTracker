using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class ManagePackageView : ContentPage
{
	public ManagePackageView()
	{ 
		InitializeComponent();
		BindingContext = new PackageViewModel();
	}
}
