
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


            /* ******* FOR TESTING - POSSIBLE FUTURE USE ************
             
            //UIView view = new UIView(new CGRect(0, 0, 200, 200));
            //view.BackgroundColor = UIColor.White;

            //var imageView = new UIImageView(new CGRect(0, 0, 150, 150));
            //imageView.Image = image;


            //view.AddSubview(imageView);

            // Create a new print formatter with the image
            ////var printFormatter = new UIPrintFormatter();
            //view.ViewPrintFormatter.MaximumContentWidth = 200;
            //view.ViewPrintFormatter.MaximumContentHeight = 200;
            //view.ViewPrintFormatter.StartPage = 0;
            //var printFormatter = view.ViewPrintFormatter;

            //var printPageRenderer = new UIPrintPageRenderer();
            //printPageRenderer.DrawContentForPage(0, new CGRect(0, 0, 200, 200));
            //printPageRenderer.DrawPrintFormatterForPage(view.ViewPrintFormatter, 0);

            //var printInfo = UIPrintInfo.PrintInfo;
            //printInfo.OutputType = UIPrintInfoOutputType.General;

            //printPageRenderer.AddPrintFormatter(printFormatter, 0);


            // Set the print formatter and other options
            //printController.PrintPageRenderer = printPageRenderer;
            //printController.PrintFormatter = printFormatter;
            //printController.PrintInfo = printInfo;


            //printController.ShowsPageRange = false;
            
             */


        }


    }

}

