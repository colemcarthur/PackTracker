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

        try
        {
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    protected virtual void OnPackageIDScanned(PackageEventArgs e)
    {
        try
        {
            PackageScannedEventHandler handler = PackageIDScanned;
            handler?.Invoke(this, e);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    protected void BarcodesDetected(object sender, BarcodeDetectionEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    void CameraLight_Clicked(System.Object sender, System.EventArgs e)
    {
        cameraBarcodeReaderView.IsTorchOn = !cameraBarcodeReaderView.IsTorchOn;
    }

    void Cancel_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        try
        {
            cameraBarcodeReaderView.IsDetecting = true;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }

    protected override void OnDisappearing()
    {

        base.OnDisappearing();

        try
        {
            if (Navigation.NavigationStack.LastOrDefault() == this)
            {
                cameraBarcodeReaderView.IsDetecting = false;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

}

public class PackageEventArgs : EventArgs
{
    public Int32 ID { get; set; }
}