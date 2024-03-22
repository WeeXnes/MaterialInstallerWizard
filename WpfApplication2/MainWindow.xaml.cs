using System;
using System.Windows;
using System.Windows.Input;

namespace WpfApplication2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public static Types.UpdateVar<string> WindowTitle = new Types.UpdateVar<string>();
        public MainWindow()
        {
            InitializeComponent();
            WindowTitle.ValueChanged += TitleChanged;
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            Content_Frame.Child = new DownloadPage(new DownloadContainer("https://dl.discordapp.net/distro/app/stable/win/x86/1.0.9027/DiscordSetup.exe"), new []{true,true});
            WindowTitle.Value = "Setup";
        }

        private void TitleChanged()
        {
            this.Title = WindowTitle.Value;
            label_title.Content = WindowTitle.Value;
        }

        private void MainWindow_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
        
        private void CloseButton_OnClick(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }private void MinimizeButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}