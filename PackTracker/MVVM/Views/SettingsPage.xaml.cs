namespace PackTracker.MVVM.Views;

public partial class SettingsPage : ContentPage
{
	public SettingsPage()
	{
		InitializeComponent();
	}

    void Closed_Clicked(System.Object sender, System.EventArgs e)
    {
		Navigation.PopModalAsync();
    }
}
