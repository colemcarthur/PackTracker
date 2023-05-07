using ZXing.Net.Maui;

namespace PackTracker.MVVM.Views;

public partial class ScanPage : ContentPage
{
    public ScanPage()
    {
        InitializeComponent();

    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        
        MainThread.BeginInvokeOnMainThread(() => {
            lblData.Text = $"{e.Results[0].Format}->{e.Results[0].Value}";
            lblMessage.Text = "";
            cameraBarcodeReaderView.IsDetecting = false;
        });
        
    }

    void Button_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
    }

    void Button_Clicked_1(System.Object sender, System.EventArgs e)
    {
        lblMessage.Text = "Scan Barcode...";
        lblData.Text = "";
        cameraBarcodeReaderView.IsDetecting = true;
    }

    void Button_Clicked_2(System.Object sender, System.EventArgs e)
    {
        cameraBarcodeReaderView.CameraLocation = cameraBarcodeReaderView.CameraLocation == CameraLocation.Rear ? CameraLocation.Front : CameraLocation.Rear;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        
        if (cameraBarcodeReaderView != null)
        {
            cameraBarcodeReaderView.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormats.All,
                AutoRotate = true,
                Multiple = true
            };
        }
        cameraBarcodeReaderView.IsDetecting = true;
        
    }
    protected override void OnDisappearing()
    {

        base.OnDisappearing();
        cameraBarcodeReaderView.IsDetecting = false;

    }


}
