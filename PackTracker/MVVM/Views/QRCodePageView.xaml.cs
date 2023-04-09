
using ZXing.Net.Maui;
using PackTracker.Platforms;
using System.ComponentModel;

namespace PackTracker.MVVM.Views;

public partial class QRCodePageView : ContentPage, INotifyPropertyChanged
{
    private Image testImage;

    public Image TestImage
    {
        get => testImage;
        set
        {
            testImage = value;
            OnPropertyChanged();
        }
    }

    public QRCodePageView()
    {
        InitializeComponent();

        BindingContext = this;



    }

    

}


