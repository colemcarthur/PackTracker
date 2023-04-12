
using System;
using System.IO;
using Android.Graphics;
using Microsoft.Maui.Controls.Platform;
using Newtonsoft.Json.Serialization;
using ZXing;
using ZXing.Common;
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
                    Margin = 0
                }
            };

            var bitmap = barcodeWriter.Write(text);

            // Create a new Bitmap to hold the QR code and the label
            var combinedBitmap = Bitmap.CreateBitmap(width, height + 20, Bitmap.Config.Argb8888);

            // Create a Canvas to draw on the combined Bitmap
            var canvas = new Canvas(combinedBitmap);

            // Draw the QR code on the Canvas
            canvas.DrawBitmap(bitmap, 0, 0, null);

            // Create a Paint to draw the text
            var paint = new Android.Graphics.Paint(PaintFlags.AntiAlias);
            paint.Color = Android.Graphics.Color.Black;
            paint.TextAlign = Android.Graphics.Paint.Align.Right;
            paint.TextSize = 14;

            // Calculate the width of the text
            var textWidth = paint.MeasureText(text);

            // Draw the text under the QR code
            canvas.DrawText(text, width - textWidth - 5, height + 15, paint);
            
            var stream = new MemoryStream();
            combinedBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android
     
            stream.Position = 0;
            return stream;
            
        }

    }
}

