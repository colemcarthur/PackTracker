using CommunityToolkit.Maui.Storage;
using PackTracker.MVVM.Views;
using PackTracker.MVVM.Models;
using PackTracker.Repositories;

namespace PackTracker;

public partial class App : Application
{
	// Barcode Service for ZXing.Net
	// Platform dependent for bitmap images
	public static IBarcodeService BarcodeService { get; set; }

	// .Net Community Toolkit File Saver
	public static IFileSaver FileSaver { get; set; }

	// Database Repositories
	public static BaseRepository<Containers> Containers { get; private set; }

	public App(IBarcodeService barcodeService, IFileSaver fileSaver,
			   BaseRepository<Containers> containers)
	{
		InitializeComponent();

		BarcodeService = barcodeService;
		Containers = containers;
		FileSaver = fileSaver;

		MainPage = new NavigationPage(new MainPageView());
	}
}

