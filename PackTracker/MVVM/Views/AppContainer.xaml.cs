
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

public partial class AppContainer : TabbedPage
{
  
    public AppContainer()
	{
		InitializeComponent();
	}

    protected override void OnCurrentPageChanged()
    {
        base.OnCurrentPageChanged();

        /*
        if (CurrentPage is ScannerHostPage)
        {
            Task<bool> haveCamera = IsCameraAvailableAsync();

            scannerPage = (ScannerHostPage)CurrentPage;

            if (CurrentPage == scannerHostPageView && haveCamera.Result == true)
            {
                scannerPage.PushScanner();
            }
        }
        */
    }

    private static async Task<bool> IsCameraAvailableAsync()
    {
        bool result = true;

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
                result = false;
                //await DisplayAlert("Camera not Available", "Feature not available without a Camera. Please allow or enable your camera in settings.", "OK");
            }

        }
        catch (PermissionException pex)
        {

            Console.WriteLine($"{pex.Message}");
            result = false;
            //await DisplayAlert("Camera", "Feature not available without permission to use the camera", "OK");
        }
        catch (Exception ex)
        {
            result = false;
            // await DisplayAlert("PackTracker Error", $"{ex.Message}", "OK");
        }

        return result;
    }
}
