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

        public static string word = fiveWordsWpfLibary.word;
        public static string AllCombinations = fiveWordsWpfLibary.AllCombinations;

        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog()
            {
                Filter = "txt files (*.txt)|*.txt"
            };

            var hasSelectedFile = openFileDialog.ShowDialog() == true;
            if (hasSelectedFile)
            {
                WordDisplayLog.Text = "Current File: " + openFileDialog.SafeFileName;
                WordDisplayLog.Visibility = Visibility.Visible;
                word = openFileDialog.FileName;
            }
        }

        public void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            var words = fiveWordsWpfLibary.LoadWords(word);
            Loadprogressbar();
        }

        private void OutputFile_Click(object sender, RoutedEventArgs e)
        {

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".text"; // Default file extension
            saveFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            saveFileDialog.FileName = "Output.txt";
            saveFileDialog.InitialDirectory = @"c:\documents\";
            if (saveFileDialog.ShowDialog() == true)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile());
                writer.Write(fiveWordsWpfLibary.AllCombinations);
                writer.Close(); 
            }
        }
    }
}
