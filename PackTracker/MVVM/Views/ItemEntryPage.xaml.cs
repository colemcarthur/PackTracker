using CommunityToolkit.Mvvm.ComponentModel;
using PackTracker.MVVM.Models;
using PackTracker.MVVM.ViewModels;
using Microsoft.Maui.Graphics.Platform;
using Microsoft.Maui.Graphics;

namespace PackTracker.MVVM.Views;

public partial class ItemEntryPage : ContentPage
{

    private TaskCompletionSource<Item> _completionSource;

	public ItemEntryPage(Package package, Item item = null) 
    {
		InitializeComponent();

        try
        {
            bool enablebutton = false;

            if (item != null)
            {

                btnAdd.Text = "Save";
                enablebutton = true;
            }
            btnAdd.IsEnabled = enablebutton;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        BindingContext = new ItemViewModel(package, item);

        if (DeviceInfo.Current.Platform == DevicePlatform.iOS)
            imgItem.RotateTo(90);
	}

    public Task<Item> GetFormDataAsync()
    {
        try
        {
            _completionSource = new TaskCompletionSource<Item>();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        return _completionSource.Task;
    }

    private async void AddButton_Clicked(System.Object sender, System.EventArgs e)
    {

        try
        {
            Item item = ((ItemViewModel)BindingContext).Item;
            item.CreationDate = DateTime.Now;

            _completionSource.SetResult(item);
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    private async void CancelButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            _completionSource.SetResult(null);
            await Navigation.PopModalAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        
        
    }

    void Description_TextChanged(System.Object sender, Microsoft.Maui.Controls.TextChangedEventArgs e)
    {
        try
        {
            btnAdd.IsEnabled = string.IsNullOrEmpty(txtDescription.Text) ? false : txtDescription.Text.Length > 0 &&
                          string.IsNullOrEmpty(txtPurchasePrice.Text) ? false : txtPurchasePrice.Text.Length > 0;
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
       
    }

    async void TakePhotoButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            if (MediaPicker.Default.IsCaptureSupported)
            {
                Item item = ((ItemViewModel)BindingContext).Item;

                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // save the file into local storage
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();

                    Microsoft.Maui.Graphics.IImage image;
                    image = PlatformImage.FromStream(sourceStream);
                    //Microsoft.Maui.Graphics.IImage newImage = image.Downsize(100, true);
                    Double width = image.Width * .30;
                    Double height = image.Height * .30;
                    Microsoft.Maui.Graphics.IImage newImage = image.Resize((float)width, (float)height, ResizeMode.Fit, true);
    
                    item.Image = newImage.ToPlatformImage().AsBytes();

                    //item.Image = StreamToByteArray(sourceStream);

                    // Convert the byte array to an ImageSource object
                    ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(item.Image));

                    imgItem.Source = imageSource;
                    
                    //using FileStream localFileStream = File.OpenWrite(localFilePath);

                    //await sourceStream.CopyToAsync(localFileStream);

                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
  
    }

    public static byte[] StreamToByteArray(Stream stream)
    {
        using (var memoryStream = new MemoryStream())
        {
            try
            {
                stream.CopyTo(memoryStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return memoryStream.ToArray();
        }
    }
}
