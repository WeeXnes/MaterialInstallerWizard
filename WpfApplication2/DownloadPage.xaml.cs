using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace WpfApplication2
{
    public partial class DownloadPage : UserControl
    {
        public DownloadContainer downloadContainer { get; set; }
        public static Types.UpdateVar<double> progressbarValue = new Types.UpdateVar<double>();
        public static double progressBarCache = 0;
        public DownloadPage(DownloadContainer _container, bool[] buttons)
        {
            InitializeComponent();
            if (buttons.Length >= 2)
            {
                btn_Back.Visibility = (buttons[0] == true) ? Visibility.Visible : Visibility.Hidden;
                btn_Continue.Visibility = (buttons[1] == true) ? Visibility.Visible : Visibility.Hidden;
            }
            this.downloadContainer = _container;
        }

        private void DownloadPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            downloadContainer._client.DownloadProgressChanged += ClientOnDownloadProgressChanged;
            progressbarValue.ValueChanged += ProgressBarChanged;
        }

        private void ClientOnDownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressbarValue.Value = e.ProgressPercentage;
        }

        private void ProgressBarChanged()
        {
            if (progressbarValue.Value < 0)
                progressbarValue.Value = 0;
            if (progressbarValue.Value > 100)
                progressbarValue.Value = 100;
            
            if(progressBarCache == progressbarValue.Value)
                return;
            
            double percent = progressbarValue.Value / 100;
            Console.WriteLine(percent);
            
            DoubleAnimation animation = new DoubleAnimation();
            animation.From = CustomProgressBar.ActualWidth;
            animation.To = CustomProgressBar_Parent.ActualWidth * percent;
            animation.Duration = TimeSpan.FromSeconds(0.1);
            /*
            QuadraticEase easeFunction = new QuadraticEase();
            easeFunction.EasingMode = EasingMode.EaseInOut;
            animation.EasingFunction = easeFunction;
            */

            CustomProgressBar.BeginAnimation(Border.WidthProperty, animation);
            progressBarCache = progressbarValue.Value;
        }

        private void ContinueButton_OnClick(object sender, RoutedEventArgs e)
        {
            downloadContainer.executeDownload();
        }
    }
}