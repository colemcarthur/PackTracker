﻿namespace PackTracker.MVVM.Views;

public partial class AppContainer : TabbedPage
{
	public AppContainer()
	{
		InitializeComponent();
	}

    private async void ScanPage_Tapped(object sender, EventArgs e)
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
}
