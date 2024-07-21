using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RFIDApp.Converters
{
    public sealed class BooleanToStringConverter : IValueConverter
    {
        public string TrueValue { get; set; }
        public string FalseValue { get; set; }
        public BooleanToStringConverter()
        {
            TrueValue = "True";
            FalseValue = "False";
        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is bool isTrue))
            {
                return isTrue ? TrueValue : FalseValue;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
