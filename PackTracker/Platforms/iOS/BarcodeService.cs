using System;
using System.Drawing;
using System.IO;
using UIKit;
using CoreGraphics;
using ZXing.Net.Maui;


namespace PackTracker.Platforms
{
    public class BarcodeService : IBarcodeService
    {
        public Stream ConvertImageStream(string text, int width = 300, int height = 130)
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
                    Margin = 3
                }
            };

            var bitmap = barcodeWriter.Write(text);

            var stream = bitmap.AsPNG().AsStream(); // this is the difference

            //byte[] ba = bitmap.AsPNG().ToArray();
           
            stream.Position = 0;
            
            return stream;
        }
    }
}

