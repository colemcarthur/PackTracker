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
        public Stream ConvertImageStream(string barcodeText, string displayText, int width = 300, int height = 300, bool includeTextInImage = true, bool forPrinting = false)
        {

            Int32 extraHeight = 0;
            Int32 canvasWidth = width;
            Int32 canvasHeight = height;
            Double xPosition = 0;
            Double yPosition = 0;

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

            var bitmap = barcodeWriter.Write(barcodeText);

            if (includeTextInImage)
            {
                if (forPrinting)
                    extraHeight = 60;
                else
                    extraHeight = 20;
            }

            if (forPrinting)
            {
                canvasWidth = 1000;
                canvasHeight = 1000;
                xPosition = (canvasWidth - width) / 2;
                yPosition = (canvasHeight - height) / 2;
            }
            else
            {
                canvasHeight += extraHeight;
            }

            // Create a UIView to hold the QR code and the label
            UIView view = new UIView(new CGRect(0, 0, canvasWidth, canvasHeight));
            view.BackgroundColor = UIColor.White;

            // Create a UIImageView to display the QR Code
            var imageView = new UIImageView(new CGRect(xPosition, yPosition, width, height));
            imageView.Image = UIImage.LoadFromData(NSData.FromArray(bitmap.AsPNG().ToArray()));

            // Create a UILabel to  display the text
            var label = new UILabel(new CGRect(xPosition, yPosition + height, width - 5, extraHeight));
            label.Text = displayText;
            label.TextAlignment = UITextAlignment.Right;
            if (forPrinting)
                label.Font = UIFont.SystemFontOfSize(48);
            else
                label.Font = UIFont.SystemFontOfSize(14);

            // Add the UIImageView and UILabel to the UIView
            view.AddSubview(imageView);

            if (includeTextInImage)
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

