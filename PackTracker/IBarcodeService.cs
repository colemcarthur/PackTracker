using System;
using System.IO;

namespace PackTracker
{
	public interface IBarcodeService
	{
        Stream ConvertImageStream(string text, int width = 300, int height = 130);
    }
}

