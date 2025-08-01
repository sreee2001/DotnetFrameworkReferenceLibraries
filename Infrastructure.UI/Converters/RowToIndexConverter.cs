using System;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Markup;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// Converts a DataGridRow to an integer index
    /// </summary>
    public class RowToIndexConverter : MarkupExtension, IValueConverter
    {
        static RowToIndexConverter converter;

        /// <summary>
        /// Converts a DataGridRow to an integer index
        /// </summary>
        /// <param name="value">DataGridRow object</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            DataGridRow row = value as DataGridRow;
            if (row != null)
                return row.GetIndex() + 1;
            else
                return -1;
        }

        /// <summary>
        /// Throws NotImplementedException
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Returns the RowToIndexConverter object
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (converter == null) converter = new RowToIndexConverter();
            return converter;
        }

        /// <summary>
        /// Empty constructor
        /// </summary>
        public RowToIndexConverter()
        {
        }
    }
}
