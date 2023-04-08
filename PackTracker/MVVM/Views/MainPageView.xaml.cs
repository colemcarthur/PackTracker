namespace PackTracker.MVVM.Views;

public partial class MainPageView : ContentPage
{
	public MainPageView()
	{
		InitializeComponent();
	}

    void ScanButton_Clicked(System.Object sender, System.EventArgs e)
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


        if (isVirtual)
        {
            DisplayAlert("Camera", "Feature not available without a Camera", "OK");
        }
        else
        {
            Navigation.PushAsync(new ScanPage());
        }
    }
}
