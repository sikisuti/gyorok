using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Globalization;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.ComponentModel;
using SQLConnectionLib;
using MiddleLayer;
using MiddleLayer.Representations;

namespace GyorokRentService
{
        public sealed class BooleanToMustOrderBgConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var flag = false;
                if (value is bool)
                {
                    flag = (bool)value;
                }
                else if (value is bool?)
                {
                    var nullable = (bool?)value;
                    flag = nullable.GetValueOrDefault();
                }
                if (parameter != null)
                {
                    if (bool.Parse((string)parameter))
                    {
                        flag = !flag;
                    }
                }
                if (flag)
                {
                    return new SolidColorBrush(Colors.LightGray);
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class BooleanToFGConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var flag = false;
                if (value is bool)
                {
                    flag = (bool)value;
                }
                else if (value is bool?)
                {
                    var nullable = (bool?)value;
                    flag = nullable.GetValueOrDefault();
                }
                if (parameter != null)
                {
                    if (bool.Parse((string)parameter))
                    {
                        flag = !flag;
                    }
                }
                if (flag)
                {
                    return new SolidColorBrush(Colors.Gray);
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        class MultiBooleanTrueFalseToVisibilityConverter : IMultiValueConverter
        {
            public object Convert(object[] values,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
            {
                // Check for design mode. 
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {
                    return false;
                }

                if (values.Count() == 3)
                {
                    if ((bool)values[2])
                    {
                        return System.Windows.Visibility.Hidden;
                    }
                    else
                    {
                        if (!(values[0] is bool) || !(values[1] is bool))
                        {
                            return System.Windows.Visibility.Visible;
                        }
                        else if ((bool)values[0] && !(bool)values[1])
                        {
                            return System.Windows.Visibility.Visible;
                        }
                    }
                }
                else if (values.Count() == 2)
                {
                    if (!(values[0] is bool) || !(values[1] is bool))
                    {
                        return System.Windows.Visibility.Visible;
                    }
                    else if ((bool)values[0] && !(bool)values[1])
                    {
                        return System.Windows.Visibility.Visible;
                    }
                }

                return System.Windows.Visibility.Hidden;
            }

            public object[] ConvertBack(object value,
                                        Type[] targetTypes,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        class MultiBooleanAllTrueToVisibilityConverter : IMultiValueConverter
        {
            public object Convert(object[] values,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
            {
                bool visible = true;
                foreach (object value in values)
                    if (value is bool)
                        visible = visible && (bool)value;

                if (visible)
                    return System.Windows.Visibility.Visible;
                else
                    return System.Windows.Visibility.Hidden;
            }

            public object[] ConvertBack(object value,
                                        Type[] targetTypes,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        class statusToForegroundConverter : IMultiValueConverter
        {
            public object Convert(object[] values,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
            {
                if (values.Count() == 2)
                {
                    if ((bool)values[0])
                    {
                        return new SolidColorBrush(Colors.Gray);
                    }
                    else
                    {
                        switch ((long)values[1])
                        {
                            case 5:
                                return new SolidColorBrush(Colors.DarkGreen);

                            case 6:
                                return new SolidColorBrush(Colors.DarkRed);

                            default:
                                break;
                        }
                    }
                }
                return new SolidColorBrush(Colors.Black);
            }

            public object[] ConvertBack(object value,
                                        Type[] targetTypes,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        class statusToFontWeightConverter : IMultiValueConverter
        {
            public object Convert(object[] values,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
            {
                if (values.Count() == 2)
                {
                    if ((bool)values[0])
                    {
                        return FontWeights.Bold;
                    }
                    else
                    {
                        switch ((long)values[1])
                        {
                            case 5:
                            case 6:
                                return FontWeights.Bold;

                            default:
                                break;
                        }
                    }
                }
                return FontWeights.Normal;
            }

            public object[] ConvertBack(object value,
                                        Type[] targetTypes,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class BooleanToFontWeightConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var flag = false;
                if (value is bool)
                {
                    flag = (bool)value;
                }
                else if (value is bool?)
                {
                    var nullable = (bool?)value;
                    flag = nullable.GetValueOrDefault();
                }
                if (parameter != null)
                {
                    if (bool.Parse((string)parameter))
                    {
                        flag = !flag;
                    }
                }
                if (flag)
                {
                    return FontWeights.Bold;
                }
                else
                {
                    return FontWeights.Normal;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var back = ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
                if (parameter != null)
                {
                    if ((bool)parameter)
                    {
                        back = !back;
                    }
                }
                return back;
            }
        }

        public sealed class FilledToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime)
                {
                    if (((DateTime)value).Ticks == 0)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Hidden;
                    }
                }
                else if (value is string)
                {
                    if (value == string.Empty)
                    {
                        return Visibility.Visible;
                    }
                    else
                    {
                        return Visibility.Hidden;
                    }
                }
                else if (value == null)
                {
                    return Visibility.Visible;
                }
                else
                {
                    return Visibility.Hidden;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                var back = ((value is Visibility) && (((Visibility)value) == Visibility.Visible));
                if (parameter != null)
                {
                    if ((bool)parameter)
                    {
                        back = !back;
                    }
                }
                return back;
            }
        }

        public sealed class invFilledToVisibilityConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime)
                {
                    if (((DateTime)value).Ticks == 0)
                    {
                        return Visibility.Hidden;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else if (value is string)
                {
                    if (value == string.Empty)
                    {
                        return Visibility.Hidden;
                    }
                    else
                    {
                        return Visibility.Visible;
                    }
                }
                else if (value == null)
                {
                    return Visibility.Hidden;
                }
                else
                {
                    return Visibility.Visible;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class FilledToBoolConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if (value is DateTime)
                {
                    if (((DateTime)value).Ticks == 0)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (value is string)
                {
                    if (value == string.Empty)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                else if (value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class BooleanToReadOnlyTextBgConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if ((bool)value)
                {
                    return null;
                }
                else
                {
                    return new SolidColorBrush(Colors.White);
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public sealed class BooleanToReadOnlyTextBorderConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                if ((bool)value)
                {
                    return 0;
                }
                else
                {
                    return 1;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class ToolStatusToForegroundConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                switch ((long)value)
                {
                    case 3:
                        return new SolidColorBrush(Colors.Gray);

                    default:
                        return new SolidColorBrush(Colors.Black);
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public class ToolStatusToFontWeightConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                switch ((long)value)
                {
                    case 3:
                        return FontWeights.Bold;

                    default:
                        return FontWeights.Normal;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public class ToolStatusToBoolConverter : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                switch ((long)value)
                {
                    case 3:
                        return false;

                    default:
                        return true;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public class MultiBooleanToBooleanConverter : IMultiValueConverter
        {
            public object Convert(object[] values,
                                    Type targetType,
                                    object parameter,
                                    System.Globalization.CultureInfo culture)
            {
                // Check for design mode. 
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {
                    return false;
                }

                if (values.Count() == 3)
                {
                    
                }
                else if (values.Count() == 2)
                {
                    return !(bool)values[0] && (bool)values[1];
                }

                return System.Windows.Visibility.Hidden;
            }

            public object[] ConvertBack(object value,
                                        Type[] targetTypes,
                                        object parameter,
                                        System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        public class CityIDToCity : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                // Check for design mode. 
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {
                    return false;
                }

                if (value == null)
                {
                    return string.Empty;
                }

                try
                {
                    CityRepresentation c = DataProxy.Instance.GetCityById((long)value);
                    return c.postalCode + " " + c.city;
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }

        public class IsLocalhostToBool : IValueConverter
        {
            #region IValueConverter Members

            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                // Check for design mode. 
                if ((bool)(DesignerProperties.IsInDesignModeProperty.GetMetadata(typeof(DependencyObject)).DefaultValue))
                {
                    return false;
                }

                return (value.ToString() == "." ||
                    value.ToString().ToLower() == "localhost" ||
                    value.ToString() == string.Empty);
            }

            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }

            #endregion
        }
}




