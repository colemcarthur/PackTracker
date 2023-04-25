using System;
using System.Globalization;

namespace PackTracker.Converters
{
	public class ImageSourceConverter : IValueConverter
	{
		public ImageSourceConverter()
		{
		}

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Byte[] image = (Byte[])value;

            ImageSource imageSource = ImageSource.FromStream(() => new MemoryStream(image));

            return imageSource;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

