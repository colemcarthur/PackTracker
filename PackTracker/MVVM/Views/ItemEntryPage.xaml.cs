namespace PackTracker.MVVM.Views;

public partial class ItemEntryPage : ContentPage
{
	public ItemEntryPage()
	{
		InitializeComponent();
	}

    void AddButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }
}
