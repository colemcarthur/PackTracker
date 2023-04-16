using CommunityToolkit.Maui.Views;

namespace PackTracker.MVVM.Views;

public partial class PackageEntryPopup : Popup
{
	public PackageEntryPopup()
	{
		InitializeComponent();
	}

    void OkButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Close(PackageDescription.Text);
    }
    void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Close();
    }
}
