using FiveWordsWpfLibary;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace FiveWordsV2WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int countSolvedWords = 0;
        private List<string> words;
        public List<string> solvedWords;
        private Stopwatch stopwatch;
        private DispatcherTimer progressTimer;
        public MainWindow()
        {
            InitializeComponent();
            stopwatch = new Stopwatch();
        }
        private void Loadprogressbar()
        {
            //ProgressBar.Value = 0;
            //countSolvedWords = FiveWordsWpfLibaryClass.Solve(words);
            //solvedWords = FiveWordsWpfLibaryClass.AllCombinations;
        }

        public static List<string> AllCombinations = FiveWordsWpfLibaryClass.AllCombinations;
        public static bool FileIsChosed = false;
        private void InputFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            openFileDialog.DefaultExt = ".text"; // Default file extension
            if (openFileDialog.ShowDialog() == true)
            {
                 words = FiveWordsWpfLibaryClass.LoadWords(openFileDialog.FileName);
                 FileIsChosed = true;
            }
            InputFile.Content = "Input File";


        }

        private void PlayButtonClick(object sender, RoutedEventArgs e)
        {
            // Must choose file before doing work
            if (FileIsChosed)
            {
                Loadprogressbar();
                PlayButton.Content = "Run Solved Words";
                stopwatch.Start();
                countSolvedWords = FiveWordsWpfLibaryClass.Solve(words);

                solvedWords = FiveWordsWpfLibaryClass.AllCombinations;
            }
            else
            {
                MessageBox.Show("Please Select a file! "); 
            }

        }

        private void OutputFile_Click(object sender, RoutedEventArgs e)
        {
            // Code to save output goes here
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.DefaultExt = ".text"; // Default file extension
            saveFileDialog.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.FileName = "Output.txt";
            if (saveFileDialog.ShowDialog() == true)
            {
                OutputFile.Content = "Save Output";
                OutputFile.Click += OutputFile_Click;
                //int solvedWords = FiveWordsWpfLibaryClass.Solve(words);
                StreamWriter writer = new StreamWriter(saveFileDialog.OpenFile());
                writer.WriteLine("Number of solved words: " + countSolvedWords);
                writer.WriteLine("Elapsed time: " + stopwatch.Elapsed.Milliseconds + "ms");
                foreach (var solvedWord in solvedWords)
                {
                    writer.WriteLine(solvedWord);
                }
                stopwatch.Stop();
                writer.Close(); 
            }
        }

        private void ProgressBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {

        }
    }
}
