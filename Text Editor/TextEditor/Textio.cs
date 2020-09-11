using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TextEditor
{
    class Textio
    {
        //string to save file name and path so every file save doenst need to ask where to save.
        private string fileName = string.Empty;
        public string OpenFile(string text)
        {
            
            string filePath = string.Empty;
            
            OpenFileDialog openFileDialog = new OpenFileDialog();

            //If there is no open file, the open file dialog will goto to the users' home directory , if there is a open file it will goto the location of the last opened file.
            if(fileName==string.Empty)
            {
                openFileDialog.InitialDirectory = Environment.GetEnvironmentVariable("USERPROFILE");
            }
            else
            {
                openFileDialog.InitialDirectory = Path.GetDirectoryName(fileName);
            }
            openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            openFileDialog.FilterIndex = 2;
            openFileDialog.RestoreDirectory = true;
            Nullable<bool> result = openFileDialog.ShowDialog();
            if(result??false)
            {
                fileName = openFileDialog.FileName;
                string retstr = string.Empty;
                string[] filelines = File.ReadAllLines(fileName);
                for(int i = 0; i<filelines.Length;i++)
                {
                    retstr += filelines[i];
                    retstr += "\n";

                }
                return retstr;
            }
            else
            {
                return text;
            }

        }


        public bool SaveFile(String text)
        {
            try
            {
                //throw new System.IO.PathTooLongException();
                if (fileName != string.Empty)
                {
                    File.WriteAllText(fileName, text);
                }
                else
                {
                    SaveFileDialog dialog = new SaveFileDialog()
                    {
                        Filter = "Text Files(*.txt)|*.txt|all(*.*)|*"
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        File.WriteAllText(dialog.FileName, text);
                        fileName = dialog.FileName;
                        return true;
                    }
                    return false;
                }
                return true;
            }
            //Catch common exceptions
            catch(System.IO.PathTooLongException)
            {
                MessageBox.Show("Path is too long","Error");
                return false;
            }
            catch (System.UnauthorizedAccessException)
            {
                MessageBox.Show("Permission denied", "Error");
                return false;
            }
            catch (DriveNotFoundException)
            {
                MessageBox.Show("Drive not found", "Error");
                return false;
            }
            //Handler for all other exceptions
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            

        }

        public bool SaveAs(string text)
        {
            SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|all(*.*)|*"
            };
            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, text);
                fileName = dialog.FileName;
                return true;
            }
            return false;
        }

        public string getFileName()
        {
            if(fileName!=string.Empty)
            {
                return Path.GetFileName(fileName);
            }
            return string.Empty;
        }

        public long getFileSize()
        {
            if(fileName!=string.Empty)
            {
                return new System.IO.FileInfo(fileName).Length;
            }
            return 0;
        }

        public string getSha256()
        {
            try
            {
                if (fileName != string.Empty)
                {
                    FileStream fileStream;
                    SHA256 sha = SHA256Managed.Create();
                    fileStream = new FileStream(fileName, FileMode.Open);
                    fileStream.Position = 0;
                    byte[] hashvalue = sha.ComputeHash(fileStream);
                    return BitConverter.ToString(hashvalue).Replace("-", string.Empty);
                }
                return string.Empty;
            }
            catch(Exception)
            {
                return "Error calculating SHA256";
            }
        }
    }
}
