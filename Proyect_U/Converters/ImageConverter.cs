using Proyect_U.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Xamarin.Forms;

namespace Proyect_U.Converters
{
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return "vehicle.png";
            }
            value = new ImageService().ConvertImageFromBase64ToImageSource(value.ToString());
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
