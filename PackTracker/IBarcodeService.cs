using System;
using System.IO;

namespace PackTracker;

public interface IBarcodeService
{
    Byte[] ConvertImageStream(string text, int width = 350, int height = 350);
}

