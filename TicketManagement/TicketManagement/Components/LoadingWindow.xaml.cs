using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RFIDApp.Components
{
    /// <summary>
    /// Interaction logic for LoadingWindow.xaml
    /// </summary>
    public partial class LoadingWindow : Window
    {
        private CancellationTokenSource Token;
        private static TimeSpan duration = TimeSpan.FromSeconds(1);
        public LoadingWindow(CancellationTokenSource cancellationToken)
        {
            InitializeComponent();
            Token = cancellationToken;
        }

        public void SetProgressBarValue(double value)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                DoubleAnimation animation = new DoubleAnimation(value, duration);
                progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
            });

        }

        public void ResetValueProgressBar()
        {
            progressBar.BeginAnimation(ProgressBar.ValueProperty, null);
            progressBar.Value = 0.00;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Token.Cancel();
                Close();

            });
        }

        public void ShowLoading()
        {
            Show();
        }

        public void SetTitle(string title)
        {
            txtTitle.Text = title;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                DragMove();
            }
        }
    }
}
