using System;
using System.Windows;
using Notepad_Screen_Reader.Lib;

namespace Notepad_Screen_Reader
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow Instange { get; private set; }

        public MainWindow()
        {
            InitializeComponent();

            if (Instange == null) Instange = this;

            Loaded += InitializeScreenReader;

            Closed += (s, e) => Environment.Exit(0);
        }

        public static void InitializeScreenReader(object sender, RoutedEventArgs args)
        {
            IProgress<string> progress = new Progress<string>(s =>
            {
                Instange.Log.Text += (s + "\n");
                Instange.Log.ScrollToEnd();
            });

            var screenReader = new ScreenReader()
            {
                Progress = progress
            };

            screenReader.StartEventWatcher();
        }
    }
}
