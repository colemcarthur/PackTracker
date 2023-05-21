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

	// Platform specific printing service
	public static IPrintService KFPrintService { get; set; }

	// .Net Community Toolkit File Saver
	public static IFileSaver FileSaver { get; set; }

	// Database Repositories
	public static BaseRepository<Package> PackagesRepo { get; private set; }
	public static BaseRepository<Item> ItemsRepo { get; private set; }

	public App(IBarcodeService barcodeService, IPrintService kfPrintService,
			  IFileSaver fileSaver,
			   BaseRepository<Package> packages,
			   BaseRepository<Item> items)
	{

		InitializeComponent();

		// Set the local static member properties
		BarcodeService = barcodeService;
		KFPrintService = kfPrintService;
		PackagesRepo = packages;
		ItemsRepo = items;
		FileSaver = fileSaver;

		// Create the Navigation Stack
		MainPage = new AppContainer();

		
	}
}

