using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace COVID_19
{
    public class StringNullOrEmptyBoolConverter : BaseValueConverter<StringNullOrEmptyBoolConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var stringValue = value as string;

            return !string.IsNullOrEmpty(stringValue);
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
