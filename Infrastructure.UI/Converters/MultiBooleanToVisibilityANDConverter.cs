using System;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// return Visible when all the parameters are true
    /// </summary>
    public class MultiBooleanToVisibilityANDConverter : IMultiValueConverter
    {
        #region Implementation of IMultiValueConverter

        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            bool result = values.OfType<bool>().Aggregate(true, (current, value) => current && value);
            return result ? Visibility.Visible : Visibility.Collapsed;
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
