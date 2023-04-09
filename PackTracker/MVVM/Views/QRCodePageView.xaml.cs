//using Microsoft.Maui.Platform;
//using Microsoft.Maui.Graphics.Platform;
//using Microsoft.Maui.Controls;
//using Microsoft.Maui.Graphics;
//using Microsoft.Maui.Controls.PlatformConfiguration;
//using Microsoft.Maui.Controls.Compatibility.Platform;

using ZXing.Net.Maui;
//using ZXing.QrCode;
//using ZXing.QrCode.Internal;
//using ZXing.Common;

using PackTracker.Platforms;

namespace PackTracker.MVVM.Views;

public partial class QRCodePageView : ContentPage
{

	Image testImage { get; set; }

	public QRCodePageView()
	{
		InitializeComponent();

		Stream sr = App.BarcodeService.ConvertImageStream("Hell", 400, 400);

        Image image = new Image
        {
            Source = ImageSource.FromStream(() => sr)
        };
        testImage = image;
    }

}


