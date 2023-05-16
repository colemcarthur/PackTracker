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
        try
        {
            Close(PackageDescription.Text);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
    }
    void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}
