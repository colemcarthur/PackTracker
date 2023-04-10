using CommunityToolkit.Maui.Storage;
using PackTracker.MVVM.Views;

namespace PackTracker;

public partial class App : Application
{
	// Barcode Service for ZXing.Net
	// Platform dependent for bitmap images
	public static IBarcodeService BarcodeService { get; set; }

	// .Net Community Toolkit File Saver
	public static IFileSaver FileSaver { get; set; }

	public App(IBarcodeService barcodeService, IFileSaver fileSaver)
	{
		InitializeComponent();

		BarcodeService = barcodeService;
		FileSaver = fileSaver;

		MainPage = new NavigationPage(new MainPageView());
	}
}

