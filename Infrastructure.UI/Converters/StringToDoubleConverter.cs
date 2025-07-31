using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Infrastructure.UI.Converters
{
    /// <summary>
    /// Provides functionality to convert string values to double representations and vice versa, with support for
    /// custom formatting and culture-specific parsing.
    /// </summary>
    /// <remarks>This class implements the <see cref="IValueConverter"/> interface, enabling its use in data
    /// binding scenarios, such as in WPF applications. The <c>ConvertBack</c> method includes logic to handle numeric
    /// strings with special characters, trimming invalid characters, and preserving formatting details like negative
    /// signs and trailing periods.</remarks>
    public class StringToDoubleConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        /// <inheritdoc />
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string)
            {
                string input = (string)value;
                input = input?.Trim();

                if (string.IsNullOrWhiteSpace(input))
                    return null;

                bool isNegative = input.StartsWith("-");
                string trimmedValue = Regex.Replace(input, "[^0-9.]", "");

                // trim all repeated periods '.'
                trimmedValue = TrimRepeatedCharacters(trimmedValue, '.');
                trimmedValue = TrimRepeatedCharacters(trimmedValue, '-');

                bool endsinPeriod = trimmedValue.EndsWith(".");

                if (endsinPeriod)
                    trimmedValue = trimmedValue.TrimEnd('.');
                if (isNegative)
                    trimmedValue = trimmedValue.TrimStart('-');

                string returnValue;

                if (string.IsNullOrWhiteSpace(trimmedValue))
                {
                    returnValue = string.Empty;
                }
                else
                {
                    bool parseResult = double.TryParse(trimmedValue, out double result);
                    returnValue = !parseResult ? trimmedValue : result.ToString();
                }

                if (isNegative)
                {
                    if (string.IsNullOrWhiteSpace(returnValue))
                        returnValue = "-";
                    else
                        returnValue = "-" + returnValue;
                }
                if (endsinPeriod)
                    returnValue += ".";

                return string.IsNullOrWhiteSpace(returnValue) ? null : returnValue;
            }
            return value;
        }

        private string TrimRepeatedCharacters(string input, char charToMatch)
        {
            string result = input;
            int firstIndexofChar = result.IndexOf(charToMatch);
            if (firstIndexofChar != -1)
            {
                int repeatIndex;
                do
                {
                    repeatIndex = result.IndexOf(charToMatch, firstIndexofChar + 1);
                    if (repeatIndex != -1)
                        result = result.Remove(repeatIndex, 1);
                } while (repeatIndex != -1);
            }

            return result;
        }

        #endregion
    }
}
