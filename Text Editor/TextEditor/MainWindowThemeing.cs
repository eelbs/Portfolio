using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace TextEditor
{
    public partial class MainWindow 
    {
        //Seperate file to handle most of the applications' colors so that MainWindow.xaml.cs is not too cluttered




        private void DarkMode_Click(object sender, RoutedEventArgs e)
        {

            darkmode = true;

            //make sure stats has been initilized
            if (stats != null)
            {
                stats.DarkMode();
            }


            //change various colors

            MainGrid.Background = darkgray;

            CloseApplication.Background = darkgray;
            CloseApplication.BorderBrush = darkgray;
            MinimizeApplication.Background = darkgray;
            MinimizeApplication.BorderBrush = darkgray;
            MaximizeApplication.Background = darkgray;
            MaximizeApplication.BorderBrush = darkgray;

            NewFileButton.Background = darkgray;
            NewFileButton.BorderBrush = darkgray;
            OpenFileButton.Background = darkgray;
            OpenFileButton.BorderBrush = darkgray;
            SaveFileButton.Background = darkgray;
            SaveFileButton.Background = darkgray;
            SaveFileButton.BorderBrush = darkgray;
            SaveAsButton.Background = darkgray;
            SaveAsButton.BorderBrush = darkgray;
            UndoButton.Background = darkgray;
            UndoButton.BorderBrush = darkgray;
            RedoButton.Background = darkgray;
            RedoButton.BorderBrush = darkgray;
            IncreaseFontButton.Background = darkgray;
            IncreaseFontButton.BorderBrush = darkgray;
            DecreaseFontButton.Background = darkgray;
            DecreaseFontButton.BorderBrush = darkgray;
            CutButton.BorderBrush = darkgray;
            CutButton.Background = darkgray;
            CopyButton.Background = darkgray;
            CopyButton.BorderBrush = darkgray;
            PasteButton.Background = darkgray;
            PasteButton.BorderBrush = darkgray;
            StatsButton.Background = darkgray;
            StatsButton.BorderBrush = darkgray;

            TextBox.BorderBrush = darkgray;
            TextBox.Background = darkgray;






            MenuBar.Background = darkgray;
            MenuBar.BorderBrush = darkgray;

            foreach (MenuItem item in MenuBar.Items)
            {
                item.Background = darkgray;
                item.BorderBrush = darkgray;
                foreach (MenuItem subitem in item.Items)
                {
                    subitem.Background = darkgray;
                    subitem.BorderBrush = darkgray;
                    subitem.Foreground = Brushes.White;
                }
            }

            MenuBar.Foreground = Brushes.White;
            WindowTitle.Foreground = Brushes.White;
            TextBox.Foreground = Brushes.White;
            CloseApplication.Foreground = Brushes.White;
            MaximizeApplication.Foreground = Brushes.White;
            MinimizeApplication.Foreground = Brushes.White;


            NewFileButton.Foreground = Brushes.White;
            OpenFileButton.Foreground = Brushes.White;
            SaveFileButton.Foreground = Brushes.White;
            SaveAsButton.Foreground = Brushes.White;
            UndoButton.Foreground = Brushes.White;
            RedoButton.Foreground = Brushes.White;
            IncreaseFontButton.Foreground = Brushes.White;
            DecreaseFontButton.Foreground = Brushes.White;
            CutButton.Foreground = Brushes.White;
            CopyButton.Foreground = Brushes.White;
            PasteButton.Foreground = Brushes.White;
            StatsButton.Foreground = Brushes.White;
            OpenFileButton.Foreground = Brushes.White;
            OpenFileButton.Foreground = Brushes.White;
            OpenFileButton.Foreground = Brushes.White;
            BorderBrush = Brushes.White;


        }

        private void LightMode_Click(object sender, RoutedEventArgs e)
        {
            darkmode = false;

            MainGrid.Background = Brushes.White;

            if (stats != null)
            {
                stats.LightMode();
            }


            CloseApplication.Background = Brushes.White;
            CloseApplication.BorderBrush = Brushes.White;
            MinimizeApplication.Background = Brushes.White;
            MinimizeApplication.BorderBrush = Brushes.White;
            MaximizeApplication.Background = Brushes.White;
            MaximizeApplication.BorderBrush = Brushes.White;

            NewFileButton.Background = Brushes.White;
            NewFileButton.BorderBrush = Brushes.White;
            OpenFileButton.Background = Brushes.White;
            OpenFileButton.BorderBrush = Brushes.White;
            SaveFileButton.Background = Brushes.White;
            SaveFileButton.BorderBrush = Brushes.White;
            SaveAsButton.Background = Brushes.White;
            SaveAsButton.BorderBrush = Brushes.White;
            UndoButton.Background = Brushes.White;
            UndoButton.BorderBrush = Brushes.White;
            RedoButton.Background = Brushes.White;
            RedoButton.BorderBrush = Brushes.White;
            IncreaseFontButton.Background = Brushes.White;
            IncreaseFontButton.BorderBrush = Brushes.White;
            DecreaseFontButton.Background = Brushes.White;
            DecreaseFontButton.BorderBrush = Brushes.White;
            CutButton.BorderBrush = Brushes.White;
            CutButton.Background = Brushes.White;
            CopyButton.Background = Brushes.White;
            CopyButton.BorderBrush = Brushes.White;
            PasteButton.Background = Brushes.White;
            PasteButton.BorderBrush = Brushes.White;
            StatsButton.Background = Brushes.White;
            StatsButton.BorderBrush = Brushes.White;

            TextBox.Background = Brushes.White;
            TextBox.Background = Brushes.White;


            MenuBar.Background = Brushes.White;



            foreach (MenuItem item in MenuBar.Items)
            {
                item.Background = Brushes.White;
                item.BorderBrush = Brushes.White;
                foreach (MenuItem subitem in item.Items)
                {
                    subitem.Background = Brushes.White;
                    subitem.BorderBrush = Brushes.White;
                    subitem.Foreground = Brushes.Black;
                }
            }


            MenuBar.Foreground = Brushes.Black;
            WindowTitle.Foreground = Brushes.Black;
            TextBox.Foreground = Brushes.Black;
            CloseApplication.Foreground = Brushes.Black;
            MaximizeApplication.Foreground = Brushes.Black;
            MinimizeApplication.Foreground = Brushes.Black;


            NewFileButton.Foreground = Brushes.Black;
            OpenFileButton.Foreground = Brushes.Black;
            SaveFileButton.Foreground = Brushes.Black;
            SaveAsButton.Foreground = Brushes.Black;
            UndoButton.Foreground = Brushes.Black;
            RedoButton.Foreground = Brushes.Black;
            IncreaseFontButton.Foreground = Brushes.Black;
            DecreaseFontButton.Foreground = Brushes.Black;
            CutButton.Foreground = Brushes.Black;
            CopyButton.Foreground = Brushes.Black;
            PasteButton.Foreground = Brushes.Black;
            StatsButton.Foreground = Brushes.Black;
            OpenFileButton.Foreground = Brushes.Black;
            OpenFileButton.Foreground = Brushes.Black;
            OpenFileButton.Foreground = Brushes.Black;
            BorderBrush = Brushes.Black;

        }



        private void NewFileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            //NewFileButton.FontWeight = FontWeights.Bold;
            NewFileButton.FontSize += 2.5;
            if (darkmode)
            {
                NewFileButton.Foreground = Brushes.AntiqueWhite;
                //NewFileButton.Background = Brushes.RoyalBlue;

            }
            else
            {
                //NewFileButton.Background = Brushes.LightBlue;
            }


        }

        private void NewFileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            //NewFileButton.FontWeight = FontWeights.Normal;
            NewFileButton.FontSize -= 2.5;
            if (darkmode)
            {
                NewFileButton.Foreground = Brushes.White;
                NewFileButton.Background = darkgray;

            }
            else
            {
                NewFileButton.Background = Brushes.White;
            }
        }

        private void OpenFileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            OpenFileButton.FontSize += 2.5;
            if (darkmode)
            {
                OpenFileButton.Foreground = Brushes.White;
                OpenFileButton.Background = darkgray;

            }
            else
            {
                OpenFileButton.Background = Brushes.White;
            }
        }

        private void OpenFileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            OpenFileButton.FontSize -= 2.5;
            if (darkmode)
            {
                OpenFileButton.Foreground = Brushes.White;
            }
        }

        private void SaveFileButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveFileButton.FontSize += 2.5;
            if (darkmode)
            {
                SaveFileButton.Foreground = Brushes.White;
            }
        }

        private void SaveFileButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveFileButton.FontSize -= 2.5;
            if (darkmode)
            {
                SaveFileButton.Foreground = Brushes.White;
            }
        }

        private void SaveAsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SaveAsButton.FontSize += 2.5;
            if (darkmode)
            {
                SaveAsButton.Foreground = Brushes.White;
            }
        }

        private void SaveAsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SaveAsButton.FontSize -= 2.5;
            if (darkmode)
            {
                SaveAsButton.Foreground = Brushes.White;
            }

        }

        private void UndoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            UndoButton.FontSize += 2.5;
            if (darkmode)
            {
                UndoButton.Foreground = Brushes.White;
            }
        }

        private void UndoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            UndoButton.FontSize -= 2.5;
            if (darkmode)
            {
                UndoButton.Foreground = Brushes.White;
            }

        }

        private void RedoButton_MouseEnter(object sender, MouseEventArgs e)
        {
            RedoButton.FontSize += 2.5;
            if (darkmode)
            {
                RedoButton.Foreground = Brushes.White;
            }
        }

        private void RedoButton_MouseLeave(object sender, MouseEventArgs e)
        {
            RedoButton.FontSize -= 2.5;
            if (darkmode)
            {
                RedoButton.Foreground = Brushes.White;
            }

        }

        private void IncreaseFontButton_MouseEnter(object sender, MouseEventArgs e)
        {
            IncreaseFontButton.FontSize += 2.5;
            if (darkmode)
            {
                IncreaseFontButton.Foreground = Brushes.White;
            }
        }

        private void IncreaseFontButton_MouseLeave(object sender, MouseEventArgs e)
        {
            IncreaseFontButton.FontSize -= 2.5;
            if (darkmode)
            {
                IncreaseFontButton.Foreground = Brushes.White;
            }

        }

        private void DecreaseFontButton_MouseEnter(object sender, MouseEventArgs e)
        {
            DecreaseFontButton.FontSize += 2.5;
            if (darkmode)
            {
                DecreaseFontButton.Foreground = Brushes.White;
            }
        }

        private void DecreaseFontButton_MouseLeave(object sender, MouseEventArgs e)
        {
            DecreaseFontButton.FontSize -= 2.5;
            if (darkmode)
            {
                DecreaseFontButton.Foreground = Brushes.White;
            }

        }

        private void CutButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CutButton.FontSize += 2.5;
            if (darkmode)
            {
                CutButton.Foreground = Brushes.White;
            }
        }

        private void CutButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CutButton.FontSize -= 2.5;
            if (darkmode)
            {
                CutButton.Foreground = Brushes.White;
            }

        }

        private void CopyButton_MouseEnter(object sender, MouseEventArgs e)
        {
            CopyButton.FontSize += 2.5;
            if (darkmode)
            {
                CopyButton.Foreground = Brushes.White;
            }
        }

        private void CopyButton_MouseLeave(object sender, MouseEventArgs e)
        {
            CopyButton.FontSize -= 2.5;
            if (darkmode)
            {
                CopyButton.Foreground = Brushes.White;
            }

        }

        private void PasteButton_MouseEnter(object sender, MouseEventArgs e)
        {
            PasteButton.FontSize += 2.5;
            if (darkmode)
            {
                PasteButton.Foreground = Brushes.White;
            }
        }

        private void PasteButton_MouseLeave(object sender, MouseEventArgs e)
        {
            PasteButton.FontSize -= 2.5;
            if (darkmode)
            {
                PasteButton.Foreground = Brushes.White;
            }

        }

        private void StatsButton_MouseEnter(object sender, MouseEventArgs e)
        {
            StatsButton.FontSize += 2.5;
            if (darkmode)
            {
                StatsButton.Foreground = Brushes.White;
            }
        }

        private void StatsButton_MouseLeave(object sender, MouseEventArgs e)
        {
            StatsButton.FontSize -= 2.5;
            if (darkmode)
            {
                StatsButton.Foreground = Brushes.White;
            }

        }



        private void CloseApplication_MouseEnter(object sender, MouseEventArgs e)
        {
            CloseApplication.FontSize += 2.5;
            CloseApplication.Background = Brushes.Red;
        }

        private void CloseApplication_MouseLeave(object sender, MouseEventArgs e)
        {
            CloseApplication.FontSize -= 2.5;
            if(darkmode)
            {
                CloseApplication.Background = darkgray;
            }
            else
            {
                CloseApplication.Background = Brushes.White;
            }
        }

        private void MaximizeApplication_MouseEnter(object sender, MouseEventArgs e)
        {
            MaximizeApplication.FontSize += 2.5;
        }

        private void MaximizeApplication_MouseLeave(object sender, MouseEventArgs e)
        {
            MaximizeApplication.FontSize -= 2.5;
        }

        private void MinimizeApplication_MouseEnter(object sender, MouseEventArgs e)
        {
            MinimizeApplication.FontSize += 2.5;

        }

        private void MinimizeApplication_MouseLeave(object sender, MouseEventArgs e)
        {
            MinimizeApplication.FontSize -= 2.5;

        }



    }
}
