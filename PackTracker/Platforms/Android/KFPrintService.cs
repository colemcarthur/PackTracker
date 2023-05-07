using System;
using Android.Print;
using Android.Util;

namespace PackTracker.Platforms
{
	public class KFPrintService : IPrintService 
	{
		public KFPrintService()
		{
		}

        public async void Print(Stream stream)
        {
           Log.Debug("PackTracker", "Not Implemented");
        }
    }
}

