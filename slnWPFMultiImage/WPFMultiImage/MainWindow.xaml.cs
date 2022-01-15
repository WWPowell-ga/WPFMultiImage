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
using System.Windows.Navigation;
using System.Windows.Shapes;

using System.IO;

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CarouselClass Carousel { get; set; }
        public Dictionary<string,ImagesClass> Images = new Dictionary<string, ImagesClass>();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            ImageGrid.Visibility = Visibility.Hidden;

            InitCarousel();
        }

        private void InitCarousel()
        {
            Carousel = new();
            //Carousel.ImagesLoaded = Visibility.Hidden;
            Carousel.FileList = new();
            Carousel.InitCurrent();
            Carousel.Displayed = "";
        }

        //public void DisplayAnImage()
        //{
        //    if (ImageGrid.Visibility == Visibility.Hidden)
        //    {
        //        ImageIndex = 0;
        //        DisplayOneImage(ImageIndex);
        //    }
        //}

        //public void DisplayOneImage(int i)
        //{
        //    FileInfo fi = new FileInfo(FileList[i]);

        //    this.Title = fi.FullName;
        //    this.ToolTip = fi.FullName; //So you can see name of file when window minimized
        //    string ext = fi.Extension;

        //    

        //    RefreshImage(fi);
        //    RefreshStats(fi);
        //}//DisplayOneImage

        //public void RefreshStats(FileInfo info)
        //{
        //    this.FullName.Content = info.FullName;
        //    this.CreationTime.Content = info.CreationTime.ToString("F"); //Full datetime longtime
        //    this.LastWriteTime.Content = info.LastWriteTime.ToString("F");
        //    this.Size.Content = info.Length.ToString();
        //}

        //private void RefreshImage(FileInfo info)
        //{
        //    BitmapImage bi = new();

        //    bi.BeginInit();
        //    bi.UriSource = new Uri(info.FullName);

        //    bi.EndInit();

        //    theImg.Source = bi;

        //    //stats
        //    this.PixelSize.Content = bi.PixelWidth.ToString() + " x " + bi.PixelHeight.ToString();
        //    this.InchSize.Content = (bi.Width / 96).ToString() + " x " + (bi.Height / 96).ToString();
        //    this.DPISize.Content = bi.DpiX.ToString() + " x " + bi.DpiY.ToString();

        //    this.PixelFormat.Content = bi.Format.ToString();

        //    if (true) //Gif or tiff
        //    { //BitmapPalette
        //    }

        //    List<string> HasMetaData = new List<String>(new string[] { "GIF", "JPG", "PNG", "TIF" });
        //    if (HasMetaData.Any(x => FileType.Contains(x)))
        //    {
        //        try
        //        {
        //            BitmapMetadata bmd = new(FileType);

        //            this.metadataApplicationName.Content = bmd.ApplicationName;
        //            //this.Author.Content = bmd.Author.ToString();
        //            //this.CameraManufacturer.Content = bmd.CameraManufacturer;
        //            //this.CameraModel.Content = bmd.CameraModel;
        //            //this.Comment.Content = bmd.Comment;
        //            //this.Copyright.Content = bmd.Copyright;
        //            //this.DateTaken.Content = bmd.DateTaken;
        //            //this.Format.Content = bmd.Format;
        //            //this.Keywords.Content = bmd.Format;
        //            //this.LocationChanged.Content = bmd.Location;
        //            //this.Rating.Content = bmd.Rating.ToString(); //0 thru 5
        //            //this.Subject.Content = bmd.Subject;
        //            //this.metadataTitle.Content = bmd.Title;
        //        }
        //        catch //do nothing if codec does not support metadata properly
        //        {

        //        }
        //    }
        //}//RefreshImage

        ////== Menu clicks

        public void Exit_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            if (FileDialogImage.OpenFileDialogImage()) //FileList is filled out
            {
                Carousel.AddToFileList(FileDialogImage.FileNames);

                DisplayImage();
            }
        }

        private void DisplayImage()
        {
            string fileinquestion = (string)Carousel.FileList[Carousel.Current];

            //Might already be visible
            //if (Carousel.ImagesLoaded != Visibility.Visible)
            //    Carousel.ImagesLoaded = Visibility.Visible;

            if (ImageGrid.Visibility == Visibility.Hidden)
                ImageGrid.Visibility = Visibility.Visible;

            //Might already be in the dictionary
            if (!(Images.ContainsKey(fileinquestion)))
            {
                ImagesClass oneImage = new();
                Images[fileinquestion] = oneImage.LoadImage(fileinquestion); //Lazy loading
            }

            //Might already be displayed
            if (Carousel.Displayed != fileinquestion)
            {
                RefreshScreen(Images[fileinquestion]);
                
                Carousel.Displayed = fileinquestion;
            }
        }//DisplayImage

        private void RefreshScreen(ImagesClass img)
        {
            //Update the xaml
            
            theImg.Source = img.BitmapImage;

            FullName.Content = img.FileInfo.FullName;
            CreationTime.Content = img.FileInfo.CreationTime.ToString("F"); //Full datetime longtime
            LastWriteTime.Content = img.FileInfo.LastWriteTime.ToString("F");
            Size.Content = img.FileInfo.Length.ToString();

            PixelSize.Content = img.PixelSize();
            InchSize.Content = img.InchSize();
            DPISize.Content = img.DPISize();
            PixelFormat.Content = img.BitmapImage.Format.ToString();

            if (!(img.BitmapMetadata is null))
            {
                
                metadataApplicationName.Content = img.ApplicationName();
                Author.Content = img.Author();
                CameraManufacturer.Content = img.CameraManufacturer();
                CameraModel.Content = img.CameraModel();
                Comment.Content = img.Comment();
                Copyright.Content = img.Copyright();
                DateTaken.Content = img.DateTaken();
                Format.Content = img.Format();
                Keywords.Content = img.Keywords();
                Location.Content = img.Location();

                int rating = img.Rating();
                Rating.Content = (rating == -1) ? "<not supported>" : img.Rating().ToString(); //0 thru 5

                Subject.Content = img.Subject();
                metadataTitle.Content = img.metadataTitle();

                BitsPerPixel.Content = img.BPP().ToString();
                Masks.Content = img.Masks();
            }

        }

    }//class
}//namespace
