using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Windows;

using Microsoft.Win32;

namespace WPFMultiImage
{
    public static class FileDialogImage
    {
        public static string[] FileNames; //Array of files just selected in Open

        public static string ChosenFile; //Filename chosen in save
        public static string ChosenExt; //Extension chosen in save

        public static bool OpenFileDialogImage()
        {
            bool retval = false;

            OpenFileDialog ofd = new();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = "All Files (*.*)|*.*|Image Files (*.bmp;*.gif;*.ico;*.jpeg;*.jpg;*.png;*.tif;*.tiff)|*.bmp;*.gif;*.ico;*.jpeg;*.jpg;*.png,*.tif;*.tiff";
            ofd.Multiselect = true;
            ofd.Title = "Open Image Files";


            if (ofd.ShowDialog() == true)
            {
                FileNames = ofd.FileNames;
                retval = true; //if ShowDialog is false or null return false
            }

            return retval;
        }

        public static bool SaveFileDialogImage(ImagesClass i)
        {
            bool retval = false;

            SaveFileDialog sfd = new();
            sfd.Title = "Save Image File";

            string fullname = i.FileInfo.FullName;
            string pathonly = Path.GetDirectoryName(fullname);
            string fnonly = Path.GetFileNameWithoutExtension(fullname);
            string fndialog = Path.Combine(pathonly,fnonly);
            sfd.FileName = fndialog;
            
            sfd.Filter = "BMP (*.bmp)|*.bmp|GIF (*.gif)|*.gif|JPG (*.jpg)|*.jpg|PNG (*.png)|*.png|TIF (*.tif)|*.tif";
            sfd.DefaultExt = i.FileType;
            sfd.FilterIndex = i.SaveFilterIndex;
            sfd.AddExtension = true;

            if (sfd.ShowDialog() == true)
            {
                ChosenFile = sfd.FileName;
                ChosenExt = sfd.DefaultExt;
                retval = true; //if ShowDialog is false or null return false
            }

            return retval;

        }

    }//class
}//namespace
