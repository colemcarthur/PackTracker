using PackTracker.MVVM.Views;

namespace PackTracker;

public partial class App : Application
{
	// Barcode Service for ZXing.Net
	// Platform dependent for bitmap images
	public static IBarcodeService BarcodeService { get; set; }

	public App(IBarcodeService barcodeService)
	{
		InitializeComponent();

		BarcodeService = barcodeService;

		MainPage = new NavigationPage(new MainPageView());
	}
}

