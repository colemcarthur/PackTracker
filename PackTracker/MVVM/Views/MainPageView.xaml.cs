
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

    async void ScanButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            // Check to see if we are working with a physical device
            // If we are not then we need to display a message that the scan
            // feature is unavailable
            bool isVirtual = DeviceInfo.Current.DeviceType switch
            {
                DeviceType.Physical => false,
                DeviceType.Virtual => true,
                _ => false
            };

            // We also need to check to see if the user is going to allow
            // the camera to be used for this application
            PermissionStatus status = await Permissions.RequestAsync<Permissions.Camera>();

            if (isVirtual || status == PermissionStatus.Denied || status == PermissionStatus.Disabled)
            {
                await DisplayAlert("Camera not Available", "Feature not available without a Camera. Please allow or enable your camera in settings.", "OK");
            }
            else
            {
                await Navigation.PushAsync(new ScanPage());
            }
        }
        catch (PermissionException pex)
        {

            Console.WriteLine($"{pex.Message}");

            await DisplayAlert("Camera", "Feature not available without permission to use the camera", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("PackTracker Error", $"{ex.Message}", "OK");
        }
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

    //void CollectionView_Scrolled(System.Object sender, Microsoft.Maui.Controls.ItemsViewScrolledEventArgs e)
    //{

    //    if (e.VerticalDelta < 0)
    //    {
    //        var parentGrid = packageCollectionView.Parent as Grid;
    //        // set the row span of the collection view
    //        // This will create a sticky feature for the search bar
    //        Grid.SetRow(packageCollectionView, 1);
    //        Grid.SetRowSpan(packageCollectionView, 1);
    //    }

    //}
}
