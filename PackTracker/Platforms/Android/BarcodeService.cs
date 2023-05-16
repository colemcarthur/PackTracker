
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
        public Stream ConvertImageStream(string barcodeText, string displayText, int width = 300, int height = 130, bool includeTextInImage = true, bool forPrinting = false)
        {
            var stream = new MemoryStream();

            try
            {
                Int32 extraHeight = 0;
                Int32 canvasWidth = width;
                Int32 canvasHeight = height;
                float xPosition = 0;
                float yPosition = 0;

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

                // Create a new Bitmap to hold the QR code and the label
                var combinedBitmap = Bitmap.CreateBitmap(canvasWidth, canvasHeight, Bitmap.Config.Argb8888);

                // Create a Canvas to draw on the combined Bitmap
                var canvas = new Canvas(combinedBitmap);

                // Draw the QR code on the Canvas
                canvas.DrawBitmap(bitmap, xPosition, yPosition, null);

                // Create a Paint to draw the text
                var paint = new Android.Graphics.Paint(PaintFlags.AntiAlias);
                paint.Color = Android.Graphics.Color.Black;
                paint.TextAlign = Android.Graphics.Paint.Align.Right;
                if (forPrinting)
                    paint.TextSize = 48;
                else
                    paint.TextSize = 14;

                // Calculate the width of the text
                var textWidth = paint.MeasureText(displayText);

                // Draw the text under the QR code
                if (includeTextInImage)
                    canvas.DrawText(displayText, width - textWidth - 5, height + 15, paint);


                combinedBitmap.Compress(Bitmap.CompressFormat.Png, 100, stream);  // this is the diff between iOS and Android

                stream.Position = 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
         
            return stream;
            
        }

    }
}

