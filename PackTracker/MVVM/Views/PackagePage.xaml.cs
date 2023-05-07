using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.Input;
using PackTracker.MVVM.ViewModels;
using PackTracker.MVVM.Models;
using ZXing.QrCode.Internal;
using System.Diagnostics;

namespace PackTracker.MVVM.Views;

public partial class PackagePage : ContentPage
{
    private PackageViewModel viewModel { get; set; }

	internal PackagePage(PackageViewModel viewmodel)
	{ 
		InitializeComponent();

        viewModel = viewmodel;

		BindingContext = viewModel;

        searchBar.TextChanged += SearchBar_TextChanged;
    }

    async void AddPackageButton_Clicked(System.Object sender, System.EventArgs e)
    {
        var popup = new PackageEntryPopup();

        var result = await this.ShowPopupAsync(popup);

        if (result != null)
        {
 
            viewModel.AddOrUpdatePackage(new Package()
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

        if (searchBar.Text != null)
        {
            if (searchBar.Text.Length > 0)
                viewModel.PerformSearch(searchBar.Text);
            else
                viewModel.Refresh();
        }
        else
        {
            viewModel.Refresh();
        }
        
    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (e.NewTextValue.Trim().Length == 0)
            viewModel.Refresh();
    }

    void QRCodeTapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        viewModel.SelectedPackage = (Package)e.Parameter;

        string barcodeText = viewModel.SelectedPackage.Id + " - " + viewModel.SelectedPackage.Name;
        string displayText = viewModel.SelectedPackage.Name;

        QRCodePageView qrCodePage = new QRCodePageView(barcodeText, displayText);

        Navigation.PushAsync(qrCodePage);
    }

}
