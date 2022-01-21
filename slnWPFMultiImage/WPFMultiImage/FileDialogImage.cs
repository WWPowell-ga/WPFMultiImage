﻿using System;
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
            Window dialogPositioningWindow = PositionFileDialogWindow();

            if (ofd.ShowDialog() == true)
            {
                FileNames = ofd.FileNames;
                retval = true; //if ShowDialog is false or null return false
            }
            dialogPositioningWindow.Close();

            return retval;
        }

        // This works but definitely isn't ideal. Can use DLL imports if a
        // cleaner solution is needed. (https://stackoverflow.com/a/10489660)
        private static Window PositionFileDialogWindow()
        {
            //https://stackoverflow.com/a/17390866
            // Open an invisible window to set the position of the FileDialog.
            // OpenFileDialog is positioned in the upper-left corner
            // of the last shown window (dialogPositioningWindow)
            Window dialogPositioningWindow = new Window
            {
                // Cannot control size of filedialog window so we have to guess where
                // the center of the screen is without DLL imports.
                // These values determine the top left position of filedialog
                Left = System.Windows.SystemParameters.PrimaryScreenWidth / 4.5,
                Top = System.Windows.SystemParameters.PrimaryScreenHeight / 5,
                Width = 0,
                Height = 0,
                WindowStyle = WindowStyle.None,
                ResizeMode = ResizeMode.NoResize,
                ShowInTaskbar = false
            };
            dialogPositioningWindow.Show();
            // if the window is closed before the filedialog opens, it
            // doesn't position the filedialog window

            //dialogPositioningWindow.Close();

            // Fix this: returning the window so that the method caller
            // can close the window. Do we use a timer instead?
            return dialogPositioningWindow;
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

            Window dialogPositioningWindow = PositionFileDialogWindow();
            if (sfd.ShowDialog() == true)
            {
                ChosenFile = sfd.FileName;
                ChosenExt = sfd.DefaultExt;
                retval = true; //if ShowDialog is false or null return false
            }
            dialogPositioningWindow.Close();

            return retval;

        }

    }//class
}//namespace
