using System.Windows;
using System.Windows.Controls;

namespace RFIDApp.Components
{
    /// <summary>
    /// Interaction logic for CustomTextBox.xaml
    /// </summary>
    public partial class CustomTextBox : UserControl
    {
        public static readonly DependencyProperty TextProperty =
        DependencyProperty.Register("Text", typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public static readonly DependencyProperty PlaceHolderProperty =
        DependencyProperty.Register("PlaceHolder", typeof(string), typeof(CustomTextBox), new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public CustomTextBox()
        {
            InitializeComponent();
        }
    }
}
