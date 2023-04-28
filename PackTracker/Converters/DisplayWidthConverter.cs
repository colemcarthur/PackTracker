using System;
using System.Globalization;
using Microsoft.Maui.Devices;

namespace PackTracker.Converters
{
	public class DisplayWidthConverter : IValueConverter
	{
		public DisplayWidthConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return DeviceDisplay.MainDisplayInfo.Width;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

