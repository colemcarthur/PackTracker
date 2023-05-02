using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;
using CommunityToolkit.Maui.Storage;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Graphics.Platform;
using PackTracker.MVVM.Models;
using ZXing.QrCode.Internal;
using System.Drawing;
using System.Drawing.Printing;
using System.Diagnostics;

namespace PackTracker.MVVM.Views;

public partial class QRCodePageView : ContentPage
{

    public ImageSource ImageQR { get; private set; }

    private String BarcodeText { get; set; }
    private String DisplayText { get; set; }

    private CancellationToken cancellationToken = new CancellationToken();

    public QRCodePageView(string barcodeText, string displayText)
    {
        InitializeComponent();

        BarcodeText = barcodeText;
        DisplayText = displayText;

        Stream sr = App.BarcodeService.ConvertImageStream(BarcodeText, DisplayText, 200, 200);

        ImageQR = ImageSource.FromStream(() => sr);

        BindingContext = this;

    }

    async void SaveButton_Clicked(System.Object sender, System.EventArgs e)
    {

        try
        {
            Stream sr = App.BarcodeService.ConvertImageStream(BarcodeText, DisplayText, 200, 200);

            var fileSaverResult = await App.FileSaver.SaveAsync($"{DisplayText}.png", sr, cancellationToken);
            fileSaverResult.EnsureSuccess();
            await Toast.Make($"File is saved: {fileSaverResult.FilePath}").Show(cancellationToken);

        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex.Message);
        }

    }

    void PrintButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            Stream sr = App.BarcodeService.ConvertImageStream(BarcodeText, DisplayText, 400, 400, true, true);
            App.KFPrintService.Print(sr);

        }
        catch (Exception ex)
        {

            Debug.WriteLine(ex.Message);
        }
    }
}


