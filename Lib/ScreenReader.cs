using System;
using System.Diagnostics;
using System.Speech.Synthesis;
using System.Windows;
using System.Windows.Automation;

namespace Notepad_Screen_Reader.Lib
{
    class ScreenReader
    {
        public Action<string> Log { private get; set; }

        private static SpeechSynthesizer Speech { get; set; }

        static ScreenReader()
        {
            Speech = new SpeechSynthesizer();
        }

        public void StartEventWatcher()
        {
            Automation.AddAutomationFocusChangedEventHandler(OnFocusChanged);
        }

        private void OnFocusChanged(object sender, AutomationFocusChangedEventArgs e)
        {
            var element = sender as AutomationElement;
            if (element == null) return;

            using (var process = Process.GetProcessById(element.Current.ProcessId))
            {
                if (process.ProcessName != "notepad") return;
            }

            if (element.Current.ControlType.ProgrammaticName != "ControlType.MenuItem") return;

            Log?.Invoke(element.Current.Name);

            Speech.SpeakAsyncCancelAll();
            Speech.SpeakAsync(element.Current.Name);
        }
    }
}
