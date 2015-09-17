using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace GyorokRentService.Converters
{
    public class ObjectExistsToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = value != null;
            if (parameter == null || parameter.GetType() != typeof(bool))
            {
                parameter = false;
            }
            if ((bool)parameter)
            {
                flag = !flag;
            }
            return flag;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
