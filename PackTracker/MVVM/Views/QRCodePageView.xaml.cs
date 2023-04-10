using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;
using PropertyChanged;
using CommunityToolkit.Maui.Storage;
using System.Threading;
using CommunityToolkit.Maui.Alerts;

namespace PackTracker.MVVM.Views;

[AddINotifyPropertyChangedInterface]
public partial class QRCodePageView : ContentPage
{

    public ImageSource ImageQR { get; set; }

    private CancellationToken cancellationToken = new CancellationToken();

    public QRCodePageView()
    {
        InitializeComponent();
        BindingContext = this;

        Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);

        ImageQR = ImageSource.FromStream(() => sr);
    }

    async void SaveButton_Clicked(System.Object sender, System.EventArgs e)
    {

        Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);
       
        var fileSaverResult = await App.FileSaver.SaveAsync("Box 22.png", sr, cancellationToken);
        fileSaverResult.EnsureSuccess();
        await Toast.Make($"File is saved: {fileSaverResult.FilePath}").Show(cancellationToken);

    }
}


