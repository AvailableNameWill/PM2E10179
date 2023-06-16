using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Xamarin.Forms;

namespace PM2E10179.Controllers
{
    public class imageConverter : IValueConverter{
        
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture){
            ImageSource image = null;

            if (value != null){
                byte[] imgByte = (byte[])value;
                var stream = new MemoryStream(imgByte);
                image = ImageSource.FromStream(()=> stream);
            }

            return image;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture){
            throw new NotImplementedException();
        }
    }
}
