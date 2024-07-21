using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace RFIDApp.CustomElements
{
    public class CustomRadioButtonForMenu : RadioButton
    {
        public static readonly DependencyProperty IconProperty = DependencyProperty.Register(
            "Icon", typeof(object), typeof(CustomRadioButtonForMenu), new PropertyMetadata(null));

        public object Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public static readonly DependencyProperty DisplayTextProperty = DependencyProperty.Register(
            "DisplayText", typeof(string), typeof(CustomRadioButtonForMenu), new PropertyMetadata(string.Empty));

        public string DisplayText
        {
            get => (string)GetValue(DisplayTextProperty);
            set => SetValue(DisplayTextProperty, value);
        }

        public static readonly DependencyProperty IconColorProperty = DependencyProperty.Register(
            "IconColor", typeof(Brush), typeof(CustomRadioButtonForMenu), new PropertyMetadata(Brushes.Black));

        public Brush IconColor
        {
            get => (Brush)GetValue(IconColorProperty);
            set => SetValue(IconColorProperty, value);
        }

        public static readonly DependencyProperty TextColorProperty = DependencyProperty.Register(
            "TextColor", typeof(Brush), typeof(CustomRadioButtonForMenu), new PropertyMetadata(Brushes.Black));

        public Brush TextColor
        {
            get => (Brush)GetValue(TextColorProperty);
            set => SetValue(TextColorProperty, value);
        }

        static CustomRadioButtonForMenu()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(CustomRadioButtonForMenu), new FrameworkPropertyMetadata(typeof(CustomRadioButtonForMenu)));
        }
    }
}
