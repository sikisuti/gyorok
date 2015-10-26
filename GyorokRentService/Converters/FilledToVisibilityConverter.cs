using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace GyorokRentService.Converters
{
    public class FilledToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool rtn;
            if (value is DateTime)
            {
                if (((DateTime)value).Ticks == 0)
                {
                    rtn = false;
                }
                else
                {
                    rtn = true;
                }
            }
            else if (value is string)
            {
                if (value == string.Empty)
                {
                    rtn = false;
                }
                else
                {
                    rtn = true;
                }
            }
            else if (value == null)
            {
                rtn = false;
            }
            else
            {
                rtn = true;
            }

            if (parameter != null && bool.Parse(parameter.ToString()))
            {
                rtn = !rtn;
            }

            if (rtn)
            {
                return Visibility.Visible;
            }
            else
            {
                return Visibility.Collapsed;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
