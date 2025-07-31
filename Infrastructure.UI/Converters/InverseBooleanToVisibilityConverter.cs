using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// use the inverse of a boolean value to control visibility
    /// </summary>
    public class InverseBooleanToVisibilityConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool flag = false;
            if (value is bool)
                flag = !(bool)value;
            else if (value is bool?)
            {
                bool? nullable = (bool?)value;
                flag = nullable.HasValue && !nullable.Value;
            }
            return (object)(Visibility)(flag ? 0 : 2);
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Visibility)
                return (object)((Visibility)value != Visibility.Visible);
            return (object)true;
        }

        #endregion
    }
}
