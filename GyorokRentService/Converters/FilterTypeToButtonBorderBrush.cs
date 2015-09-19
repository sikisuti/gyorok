using Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace GyorokRentService.Converters
{
    public class FilterTypeToButtonBorderBrush : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FilterTypeEnum v = (FilterTypeEnum)value;
            FilterTypeEnum p = (FilterTypeEnum)(int.Parse(parameter.ToString()));

            if (v == p)
                return null;
            else
                return Brushes.Black;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
