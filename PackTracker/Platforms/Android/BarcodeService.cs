using Android.Graphics;
using System;
using System.IO;
using ZXing.Net.Maui;


namespace PackTracker.Platforms
{
    public class BarcodeService : IBarcodeService
    {
        public Byte[] ConvertImageStream(string text, int width = 300, int height = 130)
        {
            var barcodeWriter = new BarcodeWriter
            {
          
                Format = ZXing.BarcodeFormat.QR_CODE,
                Options = new ZXing.Common.EncodingOptions
                {
                    Width = width,
                    Height = height,
                    Margin = 3
                }
            };

            var bitmap = barcodeWriter.Write(text);
            
            var stream = new MemoryStream();
            bitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
            Byte[] ba = stream.ToArray();

            stream.Position = 0;
            return ba;
            
        }
    }
}

