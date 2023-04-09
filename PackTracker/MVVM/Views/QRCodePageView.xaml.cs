using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;
using PropertyChanged;

namespace PackTracker.MVVM.Views;

[AddINotifyPropertyChangedInterface]
public partial class QRCodePageView : ContentPage
{

    public ImageSource ImageQR { get; set; }

    public QRCodePageView()
    {
        InitializeComponent();
        BindingContext = this;

        //Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 60, 60);

        Byte[] sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);

        //ImageSource.FromStream(() => new MemoryStream(sr))

        ImageQR = ImageSource.FromStream(() => new MemoryStream(sr));
    }
}


