using ZXing.Net.Maui;
using PackTracker.MVVM.Models;
using System.ComponentModel;

namespace PackTracker.MVVM.Views;

public partial class ScanPage : ContentPage
{
    
    public delegate void PackageScannedEventHandler(object sender, PackageEventArgs e);

    public event PackageScannedEventHandler PackageIDScanned;

    public ScanPage() 
    {
        InitializeComponent();

        if (cameraBarcodeReaderView != null)
        {
            cameraBarcodeReaderView.Options = new BarcodeReaderOptions
            {
                Formats = BarcodeFormats.All,
                AutoRotate = true,
                Multiple = true
            };
        }
    }

    protected virtual void OnPackageIDScanned(PackageEventArgs e)
    {
        PackageScannedEventHandler handler = PackageIDScanned;
        handler?.Invoke(this, e);
    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        cameraBarcodeReaderView.IsDetecting = false;
        
        MainThread.BeginInvokeOnMainThread(() =>
        {
            lblData.Text = $"{e.Results[0].Format}->{e.Results[0].Value}";
            lblMessage.Text = "";
            string id = e.Results[0].Value.Split("-")[0].ToString();
            Navigation.PopModalAsync();

            OnPackageIDScanned(new PackageEventArgs() { ID = Convert.ToInt32(id) });

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

        cameraBarcodeReaderView.IsDetecting = true;
    }

    protected override void OnDisappearing()
    {

        base.OnDisappearing();

        if (Navigation.NavigationStack.LastOrDefault() == this)
        {
            cameraBarcodeReaderView.IsDetecting = false;
        }
    }
}

public class PackageEventArgs : EventArgs
{
    public Int32 ID { get; set; }
}