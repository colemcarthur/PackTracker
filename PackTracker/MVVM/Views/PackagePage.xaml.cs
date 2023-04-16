using CommunityToolkit.Maui.Views;
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

}
