using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Updater.Internal;

namespace Updater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Image? _image167;
        private Image? _image168;
        private Image? _image169;
        private Image? _image170;
        private Image? _image185;
        private Image? _image187;
        private Image? _image188;

        public MainWindow()
        {
            InitializeComponent();
            Button1.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Button1_Click), true);
            Button2.AddHandler(PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(Button2_Click), true);
        }

        private void Window1_Initialized(object sender, EventArgs e)
        {
            _image167 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap167.bmp");
            _image168 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap168.bmp");
            _image169 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap169.bmp");
            _image170 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap170.bmp");
            _image185 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap185.bmp");
            _image187 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap187.bmp");
            _image188 = Utils.ImageFromManifestResource("Updater.Resources.Bitmap.Bitmap188.bmp");
        }

        private void Window1_Loaded(object sender, RoutedEventArgs e)
        {
            Window1.Background = new ImageBrush(_image167?.Source);
            Button1.Content = _image185;
            Button2.Content = _image168;
            WebBrowser1.Navigate("https://127.0.0.1"); // to-do: hangs. needs more work.
            App.BackgroundWorker.RunWorkerAsync();
        }

        private void Window1_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }

        private void Button1_Click(object sender, RoutedEventArgs e)
        {
            if (App.BackgroundWorker.IsBusy)
                return;

            Environment.Exit(0);
        }

        private void Button1_MouseEnter(object sender, MouseEventArgs e)
        {
            Button1.Content = _image187;
        }

        private void Button1_MouseLeave(object sender, MouseEventArgs e)
        {
            Button1.Content = _image185;
        }

        private void Button1_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button1.Content = _image188;
        }

        private void Button1_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button1.Content = _image187;
        }

        private void Button2_Click(object sender, RoutedEventArgs e)
        {
            if (App.BackgroundWorker.IsBusy)
                return;

            try
            {
                Process.Start("game.exe", "start game");
                Environment.Exit(0);
            }
            catch (Exception ex)
            {
                TextBox1.Text = "Error";
                Program.Log(ex.ToString());
            }
        }

        private void Button2_MouseEnter(object sender, MouseEventArgs e)
        {
            Button2.Content = _image169;
        }

        private void Button2_MouseLeave(object sender, MouseEventArgs e)
        {
            Button2.Content = _image168;
        }

        private void Button2_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Button2.Content = _image170;
        }

        private void Button2_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Button2.Content = _image169;
        }
    }
}
