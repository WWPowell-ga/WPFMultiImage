using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;
using System.Reflection;
using System.Resources;
using System.Windows;

using Microsoft.Win32;

namespace WPFMultiImage
{
    public static class FileDialogImage
    {
        static ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        public static string[] FileNames; //Array of files just selected in Open - images or scenarios

        public static string ChosenFile; //Filename chosen in save - image or scenario
        public static string ChosenExt; //Extension chosen in save - image or scenario

        public static bool OpenFileDialogImage()
        {
            bool retval = false;

            OpenFileDialog ofd = new();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            ofd.Filter = rm.GetString("ofdFilter");
            ofd.Multiselect = true;
            ofd.Title = rm.GetString("OpenImageFiles");   

            Window dialogPositioningWindow = PositionFileDialogWindow();

            if (ofd.ShowDialog() == true)
            {
                FileNames = ofd.FileNames;
                retval = true; //if ShowDialog is false or null return false
            }

            dialogPositioningWindow.Close();

            return retval;
        }

        public static bool SaveFileDialogImage(ImagesClass i)
        {
            bool retval = false;

            SaveFileDialog sfd = new();
            sfd.Title = rm.GetString("SaveImageFile");

            sfd.FileName = i.FileInfo.Name;
            
            sfd.Filter = rm.GetString("sfdFilter");
            sfd.DefaultExt = i.FileType;
            sfd.FilterIndex = i.SaveFilterIndex;
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;

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

        public static bool SaveFileDialogMulti()
        {
            bool retval = false;

            SaveFileDialog sfd = new();
            sfd.Title = rm.GetString("SaveImageFile");

            sfd.Filter = "TIF(*.TIF)|*.tif";
            sfd.DefaultExt = ".TIF";
            sfd.FilterIndex = 1;
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;

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

        //==

        public static bool OpenFileScenario()
        {
            bool retval = false;

            OpenFileDialog ofd = new();
            ofd.CheckFileExists = true;
            ofd.CheckPathExists = true;
            
            ofd.Multiselect = true;
            ofd.Title = rm.GetString("OpenFileScenario");
            ofd.Filter = rm.GetString("TextFiles");

            Window dialogPositioningWindow = PositionFileDialogWindow();

            if (ofd.ShowDialog() == true)
            {
                FileNames = ofd.FileNames;
                retval = true; //if ShowDialog is false or null return false
            }

            dialogPositioningWindow.Close();

            return retval;
        }

        public static bool SaveFileScenario()
        {
            bool retval = false;

            SaveFileDialog sfd = new();
            sfd.Title = rm.GetString("SaveFileScenario");

            sfd.FileName = "Scenario";
            sfd.Filter = rm.GetString("TextFiles"); 
            sfd.AddExtension = true;
            sfd.OverwritePrompt = true;

            Window dialogPositioningWindow = PositionFileDialogWindow();

            if (sfd.ShowDialog() == true)
            {
                ChosenFile = sfd.FileName;
                ChosenExt = "";
                retval = true; //if ShowDialog is false or null return false
            }

            dialogPositioningWindow.Close();

            return retval;
        }

        //==

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

    }//class
}//namespace
