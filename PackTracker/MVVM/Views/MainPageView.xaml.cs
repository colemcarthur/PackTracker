
using CommunityToolkit.Maui.Views;
using Microsoft.Maui.Controls;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;


namespace PackTracker.MVVM.Views;

public partial class MainPageView : ContentPage
{
    private PackageViewModel viewModel { get; set; }

    public MainPageView()
	{
		InitializeComponent();

        try
        {
            viewModel = new PackageViewModel();
            BindingContext = viewModel;
            btnPrint.IsEnabled = false;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
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
                ScanPage scanPage = new ScanPage();
                scanPage.PackageIDScanned += ScanPage_PackageIDScanned;
                await Navigation.PushModalAsync(scanPage);
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

        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    void CollectionView_SelectionChanged(System.Object sender, Microsoft.Maui.Controls.SelectionChangedEventArgs e)
    {
        try
        {
            viewModel.SelectedPackage = (e.CurrentSelection.FirstOrDefault() as Package);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
       
    }

    void TapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        try
        {
            LoadItemsPage((Package)e.Parameter);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    void DeleteSwipeItem_Invoked(System.Object sender, System.EventArgs e)
    {
        try
        {
            SwipeItem swipeItem = (SwipeItem)sender;

            viewModel.SelectedPackage = (Package)swipeItem.CommandParameter;

            viewModel.DeletePackage(viewModel.SelectedPackage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    void QRCodeTapGestureRecognizer_Tapped(System.Object sender, Microsoft.Maui.Controls.TappedEventArgs e)
    {
        try
        {
            viewModel.SelectedPackage = (Package)e.Parameter;

            string barcodeText = viewModel.SelectedPackage.Id + " - " + viewModel.SelectedPackage.Name;
            string displayText = viewModel.SelectedPackage.Name;

            QRCodePageView qrCodePage = new QRCodePageView(barcodeText, displayText);

            Navigation.PushAsync(qrCodePage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private void ScanPage_PackageIDScanned(object sender, PackageEventArgs e)
    {
        try
        {
            Package package = App.PackagesRepo.GetItemsWithChildren(e.ID);
            LoadItemsPage(package);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    public void LoadItemsPage(Package package)
    {
        try
        {
            viewModel.SelectedPackage = package;

            ItemsPage itemsPage = new(viewModel.SelectedPackage)
            {
                Title = viewModel.SelectedPackage.Name
            };

            Navigation.PushAsync(itemsPage);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    void Selection_Clicked(System.Object sender, System.EventArgs e)
    {
        if (viewModel.SelectionMode == 0)
            viewModel.SelectionMode = 1;
        else
            viewModel.SelectionMode = 0;

    }

    void Settings_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PushModalAsync(new SettingsPage());
    }

    void CheckPackage_CheckedChanged(System.Object sender, Microsoft.Maui.Controls.CheckedChangedEventArgs e)
    {
        List<Package> packages = viewModel.Packages.FindAll(x => x.isSelected == true);

        btnPrint.IsEnabled = packages.Count > 0;
    }

    void btnPrint_Clicked(System.Object sender, System.EventArgs e)
    {
        List<Package> packages = viewModel.Packages.FindAll(x => x.isSelected == true);

    }
}
