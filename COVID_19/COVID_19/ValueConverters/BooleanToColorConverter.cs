using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID_19
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class BooleanToColorConverter : BaseValueConverter<BooleanToColorConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {           
            if (!(value is bool))
            {
                throw new InvalidOperationException("The target must be a boolean");
            }

            return (bool)value == true ? Color.Green : Color.Transparent;
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
