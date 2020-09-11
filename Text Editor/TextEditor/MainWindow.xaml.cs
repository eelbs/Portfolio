using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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


/* Application bugs.
 * Application will only unmaximize by titile bar dragging when mouse is over title text and not the bar. 
 * Dark mode Menu bar's highlight color is light while text is also light
 * Window title text looks to be a little off center
 * Probally need a better way to deal with the text stats, need to move it to a thread, or application might slow down if there si a lot of text
 * Need to introduce more usefull text stats.
 * Need to button height light colors for minimize, maximize and close.
 * Cursor seems to lag behind while typing
 * 
 * 
 */


namespace TextEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool windowMaximized;
        Textio fileio = new Textio();
        Stats stats;
        FlowDocument text = new FlowDocument();

        bool darkmode;

        Brush darkgray = (Brush)(new BrushConverter().ConvertFrom("#1a1a1a"));




        public MainWindow()
        {
            InitializeComponent();
            TextBox.Document = text;
            darkmode = false;
            setText(string.Empty);


            this.Style = Resources["Style1"] as Style;

            //check if windows 10 uses dark mode and apply if it does

            try
            {
                var reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize");
                if ((int)reg.GetValue("AppsUseLightTheme") == 0)
                {
                    DarkMode_Click(this, null);
                    Darkmode.IsChecked = true;

                }
            }
            catch (Exception) { }

            //restore last saved settings
            getSettings();

            //To allow drag move
            MouseLeftButtonDown += delegate { DragMove(); }; ;

        }





        private void getSettings()
        {
            window.Width = Properties.Settings.Default.WindowWidth;
            window.Height = Properties.Settings.Default.WindowHeight;
        }

        private void saveSettings()
        {
            Properties.Settings.Default.WindowWidth = window.Width;
            Properties.Settings.Default.WindowHeight = window.Height;

            Properties.Settings.Default.Save();
        }


        private string getText()
        {
            return new TextRange(TextBox.Document.ContentStart, TextBox.Document.ContentEnd).Text;
        }

        private void setText(string txt)
        {
            text.Blocks.Clear();
            text.Blocks.Add(new Paragraph(new Run(txt)));
        }

        private void CloseApplication_Click(object sender, RoutedEventArgs e)
        {
            saveSettings();
            System.Windows.Application.Current.Shutdown();
        }

        private void MaximizeApplication_Click(object sender, RoutedEventArgs e)
        {
            if(WindowState == WindowState.Maximized)
            {
                
                WindowState = WindowState.Normal;
            }
            else
            {
                
                WindowState = WindowState.Maximized;
            }
        }

        private void MinimizeApplication_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            //Allow window to be unmaximized and dragged when maximized
            if ((e.ChangedButton == MouseButton.Left) && (WindowState == WindowState.Maximized))
            {
                WindowState = WindowState.Normal;
                Left = Mouse.GetPosition(this).X - (Width / 2);
                Top = Mouse.GetPosition(this).Y - 15;
            }
        }


        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            updateStats();
            
        }




        //File 


        private void NewFileButton_Click(object sender, RoutedEventArgs e)
        {
            //Clear textbox, first check if user is sure
            if (getText() != string.Empty)
            {
                if (MessageBox.Show("Are you sure?, Unsaved changes will be lost.", "Question", MessageBoxButton.YesNo,MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    setText(string.Empty);
                }
            }
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            setText(fileio.OpenFile(getText()));
            string fileName = fileio.getFileName();
            if(fileName==string.Empty)
            {
                WindowTitle.Text = "Text Editor";
            }
            else
            {
                WindowTitle.Text = "Text Editor - " + fileName;
                

                
                if(stats!=null)
                {
                    stats.changeFileStats(getText(), darkmode, fileio.getFileSize(), fileio.getSha256());
                }
            }

            
        }


        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            if(!fileio.SaveFile(getText()))
            {
                //MessageBox.Show("Error saving file", "Error");
            }
            else
            {
                stats.changeFileStats(getText(), darkmode, fileio.getFileSize(), fileio.getSha256());
            }
            
        }
        private void SaveAsButton_Click(object sender, RoutedEventArgs e)
        {
            if (!fileio.SaveAs(getText()))
            {
                MessageBox.Show("Error saving file", "Error");
            }
            string fileName = fileio.getFileName();
            if (fileName == string.Empty)
            {
                WindowTitle.Text = "Text Editor";
            }
            else
            {
                WindowTitle.Text = "Text Editor - " + fileName;
                stats.changeFileStats(getText(), darkmode, fileio.getFileSize(), fileio.getSha256());
            }
            
        }
        
        //Edit

        private void UndoButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Undo();
        }

        private void RedoButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Redo();
        }

        private void CutButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Cut();
        }
        private
            void CopyButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Copy();
        }

        private void PasteButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.Paste();
        }
        
        private void TextToUpper_Click(object sender, RoutedEventArgs e)
        {

            

            TextSelection textSelection = TextBox.Selection;
            if (textSelection.Text != string.Empty)
            {

                TextBox.Selection.Text = TextBox.Selection.Text.ToUpper();
            }
        }

        private void TextTolower_Click(object sender, RoutedEventArgs e)
        {
            TextSelection textSelection = TextBox.Selection;
            if (textSelection.Text != string.Empty)
            {
                TextBox.Selection.Text = TextBox.Selection.Text.ToLower();
            }
        }
        
        

        //Veiw




        private void IncreaseAppFont(object sender, RoutedEventArgs e)
        {
            ChangeAppFont(3);
        }

        private void DecreaseAppFont(object sender, RoutedEventArgs e)
        {
            ChangeAppFont(-3);
        }

        private void HideApplicationToolbar(object sender, RoutedEventArgs e)
        {
            ApplicationToolbar.Visibility = Visibility.Hidden;
            Thickness m = Margin;
            m.Top = 35;
            TextBox.Margin = m;

        }

        private void ShowApplicationToolbar(object sender, RoutedEventArgs e)
        {
            ApplicationToolbar.Visibility = Visibility.Visible;
            Thickness m = Margin;
            m.Top = 70;
            TextBox.Margin = m;
        }


        //Help


        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {

            //PlaceHolder
            System.Diagnostics.Process.Start("https://albertmenges.site");
        }

        private void WebsiteButton_Click(object sender, RoutedEventArgs e)
        {
            
            System.Diagnostics.Process.Start("https://albertmenges.site");
        }

        private void AboutButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Created By Albert Menges \n Copyright 2020");
        }
        //Other


        private void IncreaseFontButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.FontSize += 2;
        }

        private void DecreaseFontButton_Click(object sender, RoutedEventArgs e)
        {
            TextBox.FontSize -= 2;
        }


            private void StatsButton_Click(object sender, RoutedEventArgs e)
        {
            //if(stats!=null)
            //{
            //    stats.Close();
            //}
            //stats = new Stats(getText(),darkmode,fileio.getFileSize(),fileio.getSha256());

            if (stats == null)
            {
                stats = new Stats(getText(), darkmode, fileio.getFileSize(), fileio.getSha256());
            }
            stats.Show();

        }

        private void updateStats()
        {
            if(stats!=null)
            {
                stats.changeStats(getText(), darkmode);
            }
        }

        private void ChangeAppFont(int delta)
        {
            foreach (Button btn in ApplicationToolbar.Children.OfType<Button>())
            {
                double size = btn.FontSize;
                if((size+delta>4) && (size+delta<24))   //Limit font size so it cant be too small or bg
                {
                    btn.FontSize += delta;
                    double delta2;
                    if (delta < 0)
                    {
                        delta2 = -0.075;
                    }
                    else
                    {
                        delta2 = 0.075;
                    }
                    MenuBar.FontSize += delta2;

                }
                size = MenuBar.FontSize;
                


            }
            
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }





        private void window_KeyDown(object sender, KeyEventArgs e)
        {
            if((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.N))
            {
                NewFileButton_Click(this,null);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.O))
            {
                OpenFileButton_Click(this, null);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.S))
            {
                SaveAsButton_Click(this, null);
            }
            if (e.Key == Key.F1)
            {
                HelpButton_Click(this, null);
            }


            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.Z))
            {
                UndoButton_Click(this, null);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.X))
            {
                CutButton_Click(this, null);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.C))
            {
                CopyButton_Click(this, null);
            }
            if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.V))
            {
                PasteButton_Click(this, null);
            }



        }
    }
}
