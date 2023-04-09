
using System.Net.Quic;
using Microsoft.Maui.ApplicationModel;

namespace PackTracker.MVVM.Views;

public partial class MainPageView : ContentPage
{
	public MainPageView()
	{
		InitializeComponent();
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

    void GenerateButton_Clicked(System.Object sender, System.EventArgs e)
    {
        QRCodePageView qrPage = new QRCodePageView();

        // Temporary to test display of QRCode
        Navigation.PushAsync(qrPage);

        //Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 60, 60);

        Byte[] sr = App.BarcodeService.ConvertImageStream("Box 22", 60, 60);

        Image image = new Image
        {
            Source = "dotnet_bot.png", // ImageSource.FromStream(() => new MemoryStream(sr)),
            BackgroundColor = Colors.LightBlue
        };
        qrPage.TestImage = image;
    }
}
