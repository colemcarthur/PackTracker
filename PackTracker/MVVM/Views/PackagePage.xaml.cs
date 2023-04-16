using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class PackagePage : ContentPage
{
	public PackagePage()
	{ 
		InitializeComponent();
		BindingContext = new PackageViewModel(Navigation);
	}
}
