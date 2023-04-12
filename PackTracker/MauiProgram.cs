using Microsoft.Extensions.Logging;
using ZXing.Net.Maui.Controls;
using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Storage;
using PackTracker.Repositories;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.Views;
using PackTracker.Platforms;

namespace PackTracker;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseBarcodeReader()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		builder.Services.AddTransient<IBarcodeService, BarcodeService>();
		builder.Services.AddSingleton<IFileSaver>(FileSaver.Default);
        builder.Services.AddSingleton<BaseRepository<Containers>>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

