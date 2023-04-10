using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;
using PropertyChanged;

namespace PackTracker.MVVM.Views;

[AddINotifyPropertyChangedInterface]
public partial class QRCodePageView : ContentPage
{

    public ImageSource ImageQR { get; set; }

    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

    public QRCodePageView()
    {
        InitializeComponent();
        BindingContext = this;

        Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);

        var path = App.FileSaver.SaveAsync("QRCode.png", sr, cancellationTokenSource.Token);

        sr.Position = 0;

        ImageQR = ImageSource.FromStream(() => sr);
    }
}


