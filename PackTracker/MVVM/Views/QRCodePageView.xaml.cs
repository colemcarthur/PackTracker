
using ZXing.Net.Maui;
using PackTracker.Platforms;

namespace PackTracker.MVVM.Views;

public partial class QRCodePageView : ContentPage
{

	Image testImage { get; set; }

	public QRCodePageView()
	{
		InitializeComponent();

		Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 350, 350);

        Image image = new Image
        {
            Source = ImageSource.FromStream(() => sr),
            BackgroundColor = Colors.AliceBlue
        };
        testImage = image;
    }

}


