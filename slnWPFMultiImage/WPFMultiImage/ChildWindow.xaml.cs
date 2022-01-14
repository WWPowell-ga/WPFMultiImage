using System;
using System.Collections.Generic;
using System.Linq;
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

using System.IO;

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for ChildWindow.xaml
    /// </summary>
    public partial class ChildWindow : Window
    {
        string FileType; //Type of graphics file as determined by extension
        

        //public ChildWindow()
        //{
        //    InitializeComponent();
        //}

        public ChildWindow(string fn)
        {
            InitializeComponent();

            FileInfo fi = new FileInfo(fn);

            this.Title = fi.FullName;
            this.ToolTip = fi.FullName; //So you can see name of file when window minimized
            string ext = fi.Extension;

            switch (ext)
            {
                case ".bmp":
                    FileType = "BMP"; break;
                case ".gif":
                    FileType = "GIF"; break;
                case ".ico":
                    FileType = "ICO"; break;
                case ".jpg":
                case ".jpeg":
                    FileType = "JPG"; break;
                case ".png":
                    FileType = "PNG"; break;
                case ".tif":
                case ".tiff":
                    FileType = "GIF"; break;
                default:
                    FileType = ""; //This is a programmer error!
                    break;
            }

            RefreshImage(fi);
            RefreshStats(fi);
        }

        public void RefreshStats(FileInfo info)
        {
            this.FullName.Content = info.FullName;
            this.CreationTime.Content = info.CreationTime.ToString("F"); //Full datetime longtime
            this.LastWriteTime.Content = info.LastWriteTime.ToString("F");
            this.Size.Content = info.Length.ToString();            
        }

        private void RefreshImage(FileInfo info)
        {
            BitmapImage bi = new(); 

            bi.BeginInit();
            bi.UriSource = new Uri(info.FullName);

            bi.EndInit();

            theImg.Source = bi;

            //stats
            this.PixelSize.Content = bi.PixelWidth.ToString() + " x " + bi.PixelHeight.ToString();
            this.InchSize.Content = (bi.Width / 96).ToString() + " x " + (bi.Height / 96).ToString();
            this.DPISize.Content = bi.DpiX.ToString() + " x " + bi.DpiY.ToString();
            
            this.PixelFormat.Content = bi.Format.ToString();
            
            if (true) //Gif or tiff
            { //BitmapPalette
            }

            List<string> HasMetaData = new List<String>(new string[] { "GIF", "JPG", "PNG", "TIF" });
            if (HasMetaData.Any(x => FileType.Contains(x)))
            {
                try
                {
                    BitmapMetadata bmd = new(FileType);

                    this.metadataApplicationName.Content = bmd.ApplicationName;
                    //this.Author.Content = bmd.Author.ToString();
                    //this.CameraManufacturer.Content = bmd.CameraManufacturer;
                    //this.CameraModel.Content = bmd.CameraModel;
                    //this.Comment.Content = bmd.Comment;
                    //this.Copyright.Content = bmd.Copyright;
                    //this.DateTaken.Content = bmd.DateTaken;
                    //this.Format.Content = bmd.Format;
                    //this.Keywords.Content = bmd.Format;
                    //this.LocationChanged.Content = bmd.Location;
                    //this.Rating.Content = bmd.Rating.ToString(); //0 thru 5
                    //this.Subject.Content = bmd.Subject;
                    //this.metadataTitle.Content = bmd.Title;
                }
                catch //do nothing if codec does not support metadata properly
                {

                }
            }     
        }//RefreshImage

    }//class
}//namespace
