using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace RFIDApp.Converters
{
    public class POStatusToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool status)
            {
                return status ? "Hoàn thành" : "Lưu nháp";
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
