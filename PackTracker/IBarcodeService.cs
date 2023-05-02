using System;
using System.IO;

namespace PackTracker;

public interface IBarcodeService
{
    Stream ConvertImageStream(string barodeText, string displayText, int width = 350, int height = 350,
                            bool includeTextInImage = true, bool forPrinting = false);
}

