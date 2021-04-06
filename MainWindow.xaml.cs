using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Automation;
using System.Speech.Synthesis;
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

            Loaded += ScreenReader.StartEventWatcher;

            Closed += (s, e) => Environment.Exit(0);
        }
    }
}
