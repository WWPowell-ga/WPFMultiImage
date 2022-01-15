using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Win32;
using System.Windows;

namespace WPFMultiImage
{
    public static class FileDialogImage
    {
        public static string[] FileNames; //Array of files just selected
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

    }//class
}//namespace
