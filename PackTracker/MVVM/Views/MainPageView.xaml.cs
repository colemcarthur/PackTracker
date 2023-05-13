
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.ApplicationModel;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;
using System.Diagnostics;
using System.Text;

namespace PackTracker.MVVM.Views;

public partial class MainPageView : ContentPage
{
    private PackageViewModel viewModel { get; set; }

    public MainPageView()
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

            viewModel.AddOrUpdatePackage(new Package()
            {
                Name = result.ToString(),
                CreationDate = DateTime.Now
            });

            packageCollectionView.ScrollTo(viewModel.Count - 1, position: ScrollToPosition.MakeVisible, animate: false);

        }
    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        viewModel.SelectedPackage = (e.CurrentSelection.FirstOrDefault() as Package);
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        LoadItemsPage((Package)e.Parameter);
    }

    public void LoadItemsPage(Package package)
    {
        viewModel.SelectedPackage = package ;

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
        {
            var parentGrid = packageCollectionView.Parent as Grid;
            // set the row span of the collection view
            // This will create a sticky feature for the search bar
            Grid.SetRow(packageCollectionView, 0);
            Grid.SetRowSpan(packageCollectionView, 2);
            viewModel.Refresh();
        }
    }

    void QRCodeTapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        viewModel.SelectedPackage = (Package)e.Parameter;

        string barcodeText = viewModel.SelectedPackage.Id + " - " + viewModel.SelectedPackage.Name;
        string displayText = viewModel.SelectedPackage.Name;

        QRCodePageView qrCodePage = new QRCodePageView(barcodeText, displayText);

        Navigation.PushAsync(qrCodePage);
    }

    void ScanButton_Clicked(System.Object sender, System.EventArgs e)
    {
        ScanPage scanPage = new ScanPage();

        scanPage.PackageIDScanned += ScanPage_PackageIDScanned1;
        Navigation.PushModalAsync(scanPage);
    }

    private void ScanPage_PackageIDScanned1(object sender, PackageEventArgs e)
    {
        Package package = App.PackagesRepo.GetItemsWithChildren(e.ID);
        LoadItemsPage(package);
    }

}
