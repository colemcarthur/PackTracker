using PackTracker.MVVM.Views;

namespace PackTracker;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new NavigationPage(new MainPageView());
	}
}

