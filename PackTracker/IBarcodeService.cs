using System;
using System.IO;
using PackTracker.MVVM.Models;

namespace PackTracker;

public interface IBarcodeService
{
    Stream ConvertImageStream(string barodeText, string displayText, int width = 350, int height = 350,
                            bool includeTextInImage = true, bool forPrinting = false);

    Stream ConvertImageStream(List<Package> packages, int width = 350, int height = 350);
}

