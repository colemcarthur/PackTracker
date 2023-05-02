
using System.Drawing;
using CoreGraphics;
using Foundation;
using UIKit;

namespace PackTracker.Platforms
{
    public class KFPrintService : IPrintService
    {


        public void Print(Stream stream)
        {
            // Load the image from the stream
            var imageData = NSData.FromStream(stream);
            var image = UIImage.LoadFromData(imageData);

            // Create a print interaction controller
            var printController = UIPrintInteractionController.SharedPrintController;
            printController.ShowsNumberOfCopies = false;
            printController.PrintingItem = image;

            // Show the print dialog
            printController.Present(true, (handler, completed, error) =>
            {
                // Handle the completion and error states
            });

        }


    }

}


