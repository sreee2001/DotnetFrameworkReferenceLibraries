using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// returns the inverse of a boolean
    /// </summary>
    public class InverseBooleanValueConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool)
                return !((bool)value);
            return DependencyProperty.UnsetValue;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
