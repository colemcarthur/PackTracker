using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;
using PropertyChanged;
using CommunityToolkit.Maui.Storage;
using System.Threading;
using CommunityToolkit.Maui.Alerts;
using Microsoft.Maui.Graphics.Platform;
using PackTracker.MVVM.Models;

namespace PackTracker.MVVM.Views;

[AddINotifyPropertyChangedInterface]
public partial class QRCodePageView : ContentPage
{

    public ImageSource ImageQR { get; set; }

    private CancellationToken cancellationToken = new CancellationToken();

    public QRCodePageView()
    {
        InitializeComponent();
        BindingContext = this;

        // ********* TEST ************
        //Package package = new Package()
        //{
        //    Name = "Box 22",
        //    CreationDate = DateTime.Now,

        //};

        //package.Items = new List<Item>()
        //{
        //    new Item
        //    {
        //        CreationDate = DateTime.Now,
        //        Value = 33.32,
        //        Description = "Lights",
        //        Image = new byte[] {122, 223, 23, 2, 3, 66}
        //    },
        //    new Item
        //    {
        //        CreationDate = DateTime.Now,
        //        Value = 33.32,
        //        Description = "Ordiment",
        //        Image = new byte[] {122, 33, 23, 2, 3, 66}
        //    }
        //};

        // App.Packages.SaveItemWithChildren(package);
        
        //List<Package> pkgs = App.PackagesRepo.GetItemsWithChildren();


        Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);

        ImageQR = ImageSource.FromStream(() => sr);

        // ******************************************
    }

    async void SaveButton_Clicked(System.Object sender, System.EventArgs e)
    {

        Stream sr = App.BarcodeService.ConvertImageStream("Box 22", 200, 200);
       
        var fileSaverResult = await App.FileSaver.SaveAsync("Box 22.png", sr, cancellationToken);
        fileSaverResult.EnsureSuccess();
        await Toast.Make($"File is saved: {fileSaverResult.FilePath}").Show(cancellationToken);


    }
}


