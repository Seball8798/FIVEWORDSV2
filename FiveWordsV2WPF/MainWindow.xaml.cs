using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using FiveWordsWpfLibary;
using Microsoft.Win32;

namespace FiveWordsV2WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Loadprogressbar()
        {
            ProgressBar.Value = 0;
            ProgressBar.Maximum = 100;

            Duration duration = new Duration(TimeSpan.FromSeconds(2));
            DoubleAnimation dblanim = new DoubleAnimation(100, duration);
            
            ProgressBar.ValueChanged += (s, e) =>
            {
                if (ProgressBar.Value == 100)
                {
                    return;
                }
            };
            ProgressBar.BeginAnimation(ProgressBar.ValueProperty, dblanim);
        }
        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            Loadprogressbar();
        }

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
                File.ReadAllText(openFileDialog.FileName);
        }

        private void OutputFile_Click(object sender, RoutedEventArgs e)
        {
            string hej = "fgsdfs";

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".text"; // Default file extension
            saveFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            saveFileDialog.FileName = "hej.txt";
            if (saveFileDialog.ShowDialog() == true)
                saveFileDialog.DefaultExt = ".txt"; ;

            saveFileDialog.InitialDirectory = @"c:\documents\";
;            File.WriteAllText(saveFileDialog.FileName, hej);
        }
    }
}
