using System;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Automation;

namespace Notepad_Screen_Reader.Lib
{
    static class ScreenReader
    {
        private static IProgress<string> Progress { get; set; }

        private static SpeechSynthesizer Speech { get; set; }

        static ScreenReader()
        {
            Progress = new Progress<string>(s =>
            {
                MainWindow.Instange.Log.Text += (s + "\n");
                MainWindow.Instange.Log.ScrollToEnd();
            });

            Speech = new SpeechSynthesizer();
        }

        public static void StartEventWatcher(object sender, RoutedEventArgs args)
        {
            Automation.AddAutomationFocusChangedEventHandler(OnFocusChanged);
        }

        private static void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            var element = sender as AutomationElement;
            if (element == null) return;

            var process = Process.GetProcessById(element.Current.ProcessId);
            if (process.ProcessName != "notepad") return;

            if (element.Current.ControlType.ProgrammaticName != "ControlType.MenuItem") return;

            Progress.Report(element.Current.Name);

            Speech.SpeakAsyncCancelAll();
            Speech.SpeakAsync(element.Current.Name);
        }
    }
}
