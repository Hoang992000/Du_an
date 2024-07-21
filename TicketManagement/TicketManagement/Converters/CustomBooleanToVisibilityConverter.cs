using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RFIDApp.Converters
{
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class CustomBooleanToVisibilityConverter : IValueConverter
    {
        public Visibility TrueValue { get; set; }
        public Visibility FalseValue { get; set; }
        enum Parameters
        {
            Normal, Inverted
        }

        public CustomBooleanToVisibilityConverter()
        {
            // set defaults
            TrueValue = Visibility.Visible;
            FalseValue = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is bool))
                return null;
            Parameters direction = Parameters.Normal;
            if (parameter != null)
                direction = (Parameters)Enum.Parse(typeof(Parameters), (string)parameter);
            if (direction == Parameters.Inverted)
                return !(bool)value ? TrueValue : FalseValue;
            return (bool)value ? TrueValue : FalseValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (Equals(value, TrueValue))
                return true;
            if (Equals(value, FalseValue))
                return false;
            return null;
        }
    }
}
