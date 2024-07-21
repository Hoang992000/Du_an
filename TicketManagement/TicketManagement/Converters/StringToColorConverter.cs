using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace RFIDApp.Converters
{
    public sealed class StringToColorConverter : IValueConverter
    {
        public string StringConvert { get; set; }
        public Brushes ToColor { get; set; }
        public StringToColorConverter()
        {


        }
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((value is string stringConvert))
            {
                return stringConvert == StringConvert ? Brushes.Green : Brushes.DarkGreen;
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
