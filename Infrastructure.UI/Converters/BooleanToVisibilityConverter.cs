using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// Converts a <see langword="bool"/> value to a <see cref="Visibility"/> value for use in UI bindings.
    /// </summary>
    /// <remarks>This converter maps <see langword="true"/> to <see cref="Visibility.Visible"/> and <see
    /// langword="false"/> to <see cref="Visibility.Collapsed"/>. It is commonly used in XAML bindings to control the
    /// visibility of UI elements based on a boolean property.</remarks>
    public class BooleanToVisibilityConverter : IValueConverter
    {
        /// <summary>
        /// Determines the visibility state based on the provided value.
        /// </summary>
        /// <param name="value">An object expected to be of type <see langword="bool"/>. If the value is <see langword="true"/>,  the method
        /// returns <see cref="Visibility.Visible"/>; otherwise, it returns <see cref="Visibility.Collapsed"/>.</param>
        /// <returns>A <see cref="Visibility"/> value indicating the visibility state. Returns <see cref="Visibility.Collapsed"/>
        /// if the input is not a <see langword="bool"/> or if the value is <see langword="false"/>.</returns>
        private object GetVisibility(object value)
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        /// <summary>
        /// Converts the specified value to a visibility state based on the provided parameters.
        /// </summary>
        /// <param name="value">The value to be converted. Typically represents a condition or state.</param>
        /// <param name="targetType">The type to convert the value to. This parameter is not used in this implementation.</param>
        /// <param name="parameter">An optional parameter that can influence the conversion logic. This parameter is not used in this
        /// implementation.</param>
        /// <param name="language">The language or culture information. This parameter is not used in this implementation.</param>
        /// <returns>An object representing the visibility state derived from the input value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return GetVisibility(value);
        }


        /// <summary>
        /// Converts a value back to its source type in a data binding scenario.
        /// </summary>
        /// <param name="value">The value produced by the binding target.</param>
        /// <param name="targetType">The type to convert the value back to.</param>
        /// <param name="parameter">An optional parameter to use during the conversion.</param>
        /// <param name="language">The culture information to use during the conversion.</param>
        /// <returns>The converted value, or throws an exception if the conversion is not implemented.</returns>
        /// <exception cref="NotImplementedException">Always thrown, as this method is not implemented.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts the specified value to the desired target type using the provided parameter and culture
        /// information.
        /// </summary>
        /// <param name="value">The value to be converted. This can be any object that the conversion logic supports.</param>
        /// <param name="targetType">The type to which the value should be converted. This must be a valid .NET type.</param>
        /// <param name="parameter">An optional parameter to be used during the conversion process. The usage of this parameter depends on the
        /// specific implementation.</param>
        /// <param name="culture">The culture information to use during the conversion. This can affect formatting or localization during the
        /// conversion.</param>
        /// <returns>The converted value as an object of the specified target type.</returns>
        /// <exception cref="NotImplementedException">Thrown if the method is not implemented.</exception>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return GetVisibility(value);
        }

        /// <summary>
        /// Converts a value back to its source type.
        /// </summary>
        /// <param name="value">The value produced by the binding target.</param>
        /// <param name="targetType">The type to convert the value to.</param>
        /// <param name="parameter">An optional parameter to use during the conversion.</param>
        /// <param name="culture">The culture to use during the conversion.</param>
        /// <returns>The converted value. The default implementation throws a <see cref="NotImplementedException"/>.</returns>
        /// <exception cref="NotImplementedException">Thrown to indicate that the method is not implemented.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
