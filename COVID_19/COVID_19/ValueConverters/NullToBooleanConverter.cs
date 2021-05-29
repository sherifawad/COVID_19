using System;
using System.Globalization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace COVID_19
{
    /// <summary>
    /// A converter that takes in a boolean and inverts it
    /// </summary>
    public class NullToBooleanConverter : BaseValueConverter<NullToBooleanConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>  value == null ? false : true;

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
