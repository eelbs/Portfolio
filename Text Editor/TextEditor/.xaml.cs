using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TextEditor
{
    /// <summary>
    /// Interaction logic for Stats.xaml
    /// </summary>
    public partial class Stats : Window
    {


        //Stats to show
        //word, char count
        //avg wordsize
        //a measure of complexity
        // count poor adjectives like these words: good, nice, things . And potentially replaceable adjectives with better like: new, long, old,things,went, just, big
        bool windowMaximized;
        TextStats textStats;
        string textStatsString;
        string fileStatsString;
        public Stats(string text,bool darkmode ,long filesize,string sha256)
        {
            InitializeComponent();

            changeFileStats(text, darkmode, filesize, sha256);

            MouseLeftButtonDown += delegate { DragMove(); }; ;
        }

        


        private void CloseApplication_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            
        }

        private void MaximizeApplication_Click(object sender, RoutedEventArgs e)
        {
            if (windowMaximized)
            {
                windowMaximized = false;
                WindowState = WindowState.Normal;
            }
            else
            {
                windowMaximized = true;
                WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeApplication_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void WindowTitle_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        //To allow a thread to change stats
        public void changeFileStats(string text, bool darkmode, long filesize, string sha256)
        {
            if (darkmode)
            {
                DarkMode();
            }
            windowMaximized = false;

            textStats = new TextStats(text);

            

            textStatsString = string.Empty;
            fileStatsString = string.Empty;

            textStatsString += "Character count: " + textStats.GetCharCount().ToString() + '\n';
            textStatsString += "Space count: " + textStats.spaceCount().ToString() + '\n';
            textStatsString += "Word count: " + textStats.GetWordCount().ToString() + '\n';
            textStatsString += "Aproximate read time " + (textStats.GetWordCount() / 300).ToString() + " mins" + '\n'; //Average person can read about 300 words per minute
            textStatsString += textStats.PoorAdjectivesString() + '\n' + textStats.BadWordCountString() + '\n';
            if (filesize > 0)
            {
                fileStatsString += "Size on disk: " + filesize + " Bytes" + '\n';
                fileStatsString += "SHA256: " + sha256 + '\n';
            }
            StatsTextBlock.Text = textStatsString + fileStatsString;
        }

        public void changeStats(string text, bool darkmode)
        {
            if (darkmode)
            {
                DarkMode();
            }
            windowMaximized = false;

            textStats = new TextStats(text);
            textStatsString = string.Empty;
            textStatsString += "Character count: " + textStats.GetCharCount().ToString() + '\n';
            textStatsString += "Space count: " + textStats.spaceCount().ToString() + '\n';
            textStatsString += "Word count: " + textStats.GetWordCount().ToString() + '\n';
            textStatsString += "Aproximate read time " + (textStats.GetWordCount() / 300).ToString() + "mins" + '\n';
            textStatsString += textStats.PoorAdjectivesString() + '\n' + textStats.BadWordCountString() + '\n';

            StatsTextBlock.Text = textStatsString + fileStatsString;

        }

        public void DarkMode()
        {
            Brush darkgray = (Brush)(new BrushConverter().ConvertFrom("#1a1a1a"));

            MainGrid.Background = darkgray;

            CloseApplication.Background = darkgray;
            CloseApplication.BorderBrush = darkgray;
            MinimizeApplication.Background = darkgray;
            MinimizeApplication.BorderBrush = darkgray;
            MaximizeApplication.Background = darkgray;
            MaximizeApplication.BorderBrush = darkgray;

            StatsTextBlock.Background = darkgray;
            StatsTextBlock.Foreground = Brushes.White;

            WindowTitle.Foreground = Brushes.White;
            CloseApplication.Foreground = Brushes.White;
            MaximizeApplication.Foreground = Brushes.White;
            MinimizeApplication.Foreground = Brushes.White;
            BorderBrush = Brushes.White;
        }

        public void LightMode()
        {
            Brush white = Brushes.White;

            MainGrid.Background = white;

            CloseApplication.Background = white;
            CloseApplication.BorderBrush = white;
            MinimizeApplication.Background = white;
            MinimizeApplication.BorderBrush = white;
            MaximizeApplication.Background = white;
            MaximizeApplication.BorderBrush = white;

            StatsTextBlock.Background = white;
            StatsTextBlock.Foreground = Brushes.Black;

            WindowTitle.Foreground = Brushes.Black;
            CloseApplication.Foreground = Brushes.Black;
            MaximizeApplication.Foreground = Brushes.Black;
            MinimizeApplication.Foreground = Brushes.Black;
            BorderBrush = Brushes.Black;
        }

        //private void Grid_Loaded(object sender, RoutedEventArgs e)
        //{
        //    this.MouseDown += delegate { DragMove(); };

        //}

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left && WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
                Left = Mouse.GetPosition(this).X - (Width / 2);
                Top = Mouse.GetPosition(this).Y - 15;

            }
        }
    }
}
