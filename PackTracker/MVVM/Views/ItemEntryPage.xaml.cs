using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class ItemEntryPage : ContentPage
{

    private ItemViewModel viewModel { get; set; }

	public ItemEntryPage(ObservableObject viewModel)
	{
		InitializeComponent();

        this.viewModel = (ItemViewModel)viewModel;

        BindingContext = viewModel;

        btnAdd.IsEnabled = false;
	}

    void AddButton_Clicked(System.Object sender, System.EventArgs e)
    {

        viewModel.Save();
        Navigation.PopModalAsync();
    }

    void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        Navigation.PopModalAsync();
    }

    void Description_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        btnAdd.IsEnabled = string.IsNullOrEmpty(txtDescription.Text) ? false : txtDescription.Text.Length > 0 &&
                           string.IsNullOrEmpty(txtValue.Text)  ? false : txtValue.Text.Length > 0;
    }
}
