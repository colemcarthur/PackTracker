using System;
using System.Drawing;
using System.IO;
using UIKit;
using CoreGraphics;
using ZXing.Net.Maui;
using Foundation;

namespace PackTracker.Platforms
{
    public class BarcodeService : IBarcodeService
    {
        public Stream ConvertImageStream(string text, int width = 300, int height = 300)
        {

            var barcodeWriter = new BarcodeWriter
            {
                ForegroundColor = Colors.Black,
                BackgroundColor = Colors.Transparent,
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 0
                }
            };

            var bitmap = barcodeWriter.Write(text);

            // Create a UIView to hold the QR code and the label
            UIView view = new UIView(new CGRect(0, 0, width, height + 20));
            view.BackgroundColor = UIColor.White;

            // Create a UIImageView to display the QR Code
            var imageView = new UIImageView(new CGRect(0, 0, width, height));
            imageView.Image = UIImage.LoadFromData(NSData.FromArray(bitmap.AsPNG().ToArray()));

            // Create a UILabel to  display the text
            var label = new UILabel(new CGRect(0, height, width - 5, 20));
            label.Text = text;
            label.TextAlignment = UITextAlignment.Right;
            label.Font = UIFont.SystemFontOfSize(14);

            // Add the UIImageView and UILabel to the UIView
            view.AddSubview(imageView);
            view.AddSubview(label);

            // Convert the UIView to a UIImage and return it as a stream
            UIGraphics.BeginImageContextWithOptions(view.Bounds.Size, view.Opaque, 0.0f);
            view.Layer.RenderInContext(UIGraphics.GetCurrentContext());
            var image = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();

            Stream stream = new MemoryStream();
            image.AsPNG().AsStream().CopyTo(stream);
            //var stream = bitmap.AsPNG().AsStream(); // this is the difference
            stream.Position = 0;

            return stream;
        }
    }
}

