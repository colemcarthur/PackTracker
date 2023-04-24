using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;

namespace PackTracker.MVVM.Views;

public partial class ItemEntryPage : ContentPage
{

    private TaskCompletionSource<Item> _completionSource;

	public ItemEntryPage(Package package, Item item = null) 
    {
		InitializeComponent();

        BindingContext = new ItemViewModel(package, item);

        bool enablebutton = false;

        if (item != null)
        {
            btnAdd.Text = "Save";
            enablebutton = true;
        }
        btnAdd.IsEnabled = enablebutton;
  
	}

    public Task<Item> GetFormDataAsync()
    {
        _completionSource = new TaskCompletionSource<Item>();
        return _completionSource.Task;
    }

    private async void AddButton_Clicked(System.Object sender, System.EventArgs e)
    {

        Item item = ((ItemViewModel)BindingContext).Item;
        item.CreationDate = DateTime.Now;

        _completionSource.SetResult(item);
        await Navigation.PopModalAsync();
    }

    private async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        _completionSource.SetResult(null);
        await Navigation.PopModalAsync();
    }

    void Description_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        btnAdd.IsEnabled = string.IsNullOrEmpty(txtDescription.Text) ? false : txtDescription.Text.Length > 0 &&
                           string.IsNullOrEmpty(txtPurchasePrice.Text)  ? false : txtPurchasePrice.Text.Length > 0;
    }
}
