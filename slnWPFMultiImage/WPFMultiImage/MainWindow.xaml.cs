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
using System.Reflection;
using System.Resources;

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public CarouselClass Carousel { get; set; }
        public Dictionary<string,ImagesClass> Images = new Dictionary<string, ImagesClass>();
        ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        public MainWindow()
        {
            InitializeComponent();

            DataContext = this;
            ImageGrid.Visibility = Visibility.Hidden;

            InitCarousel();

            InitToolbar();
        }

        private void InitCarousel()
        {
            Carousel = new();
            Carousel.FileList = new();
            Carousel.ZeroCurrent();
            Carousel.Displayed = "";
        }

        private void InitToolbar()
        {
            HideShowStats.Content = rm.GetString("HideBtn");
            HideShowStats.ToolTip = rm.GetString("HideBtnTooltip");
        }

        private void lstboxColors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = sender as ListBox;

            // display the Index and RBGA of the selected color
            object ci = lb.SelectedItem;

            string colores = "";

            if (lb.SelectedItem is null)
                colores = rm.GetString("lstClickOnForDetails");
            else
                colores =
                "Index: " + lb.SelectedIndex.ToString() +
                "    Red " + (ci as ColorInfo).Color.R +
                "    Green " + (ci as ColorInfo).Color.G +
                "    Blue " + (ci as ColorInfo).Color.B +
                "    Alpha " + (ci as ColorInfo).Color.A;
            
            txtblkRGBNumbers.Content = colores;
        }

        //== Menu clicks

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

                WhereAmI.Content = Carousel.WhereAmI(); //More files got loaded even if image doesn't change
            }
        }

        //== Toolbar clicks

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            Carousel.PreviousCurrent();
            DisplayImage();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Carousel.NextCurrent();
            DisplayImage();
        }

        private void HideShow_Click(object sender, RoutedEventArgs e)
        {
            if ((string)HideShowStats.Content == rm.GetString("HideBtn"))
            {
                statsandpalette.Visibility = Visibility.Collapsed;
                HideShowStats.Content = rm.GetString("ShowBtn");
                HideShowStats.ToolTip = rm.GetString("ShowBtnTooltip");
            }
            else
            {
                statsandpalette.Visibility = Visibility.Visible;
                HideShowStats.Content = rm.GetString("HideBtn");
                HideShowStats.ToolTip = rm.GetString("HideBtnTooltip");
            }
        }

        private void SaveImage_Click(object sender, RoutedEventArgs e)
        {
            string File2Save = (string)Carousel.FileList[Carousel.Current];

            if (FileDialogImage.SaveFileDialogImage(Images[File2Save]) == true)
            {
                switch (FileDialogImage.ChosenExt)
                {
                    case "BMP":
                        {
                            SaveBitmaps.BMPSave(FileDialogImage.ChosenFile,Images[File2Save]);
                            break;
                        }
                    case "GIF":
                        {
                            SaveBitmaps.GIFSave(FileDialogImage.ChosenFile, Images[File2Save]);
                            break;
                        }
                    case "JPG":
                        {
                            SaveBitmaps.JPGSave(FileDialogImage.ChosenFile, Images[File2Save]);
                            break;
                        }
                    case "PNG":
                        {
                            SaveBitmaps.PNGSave(FileDialogImage.ChosenFile, Images[File2Save]);
                            break;
                        }
                    case "TIF":
                        {
                            SaveBitmaps.TIFSave(FileDialogImage.ChosenFile, Images[File2Save]);
                            break;
                        }
                    default:
                        break;
                }//switch
            }//if savedialog is OK

        }//SaveImage_Click

        private void CloseImage_Click(object sender, RoutedEventArgs e)
        {
            string FileWhatGoes = (string)Carousel.FileList[Carousel.Current];

            //Remove current from dictionary
            Images.Remove(FileWhatGoes);

            //Remove from filelist
            Carousel.FileList.Remove(FileWhatGoes);

            //Are there any files left? If so go to the next one and display
            if (Carousel.FileList.Count > 0) 
            {
                //Current now points to the NEXT image
                //If we delete last image move current to 0
                if (Carousel.Current == Carousel.FileList.Count) //We deleted last one and Current is WRONG
                    Carousel.ZeroCurrent();

                DisplayImage();
            }
            else //If not hide ImageGrid
            {
                ImageGrid.Visibility = Visibility.Hidden;
            }
        }

       //================================================================================================

        private void DisplayImage()
        {
            string file2display = (string)Carousel.FileList[Carousel.Current];

            //Might already be visible
            if (ImageGrid.Visibility == Visibility.Hidden)
                ImageGrid.Visibility = Visibility.Visible;

            //Might already be in the dictionary
            if (!(Images.ContainsKey(file2display)))
            {
                ImagesClass oneImage = new();

                Mouse.OverrideCursor = Cursors.Wait;  //Worry circle?
                Images[file2display] = oneImage.LoadImage(file2display); //Lazy loading, does not overwrite previous load no err
                Mouse.OverrideCursor = null;
            }

            //Might already be displayed
            if (Carousel.Displayed != file2display)
            {
                RefreshScreen(Images[file2display]);
                
                Carousel.Displayed = file2display;     
            }
        }//DisplayImage

        private void RefreshScreen(ImagesClass img)
        {
            //Update the xaml

            //Toolbar
            WhereAmI.Content = Carousel.WhereAmI();

            //Image
            theImg.Source = img.BitmapImage;

            //stats
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
                Rating.Content = (rating == -1) ? rm.GetString("NotSupported") : img.Rating().ToString(); //0 thru 5

                Subject.Content = img.Subject();
                metadataTitle.Content = img.metadataTitle();

                BitsPerPixel.Content = img.BPP().ToString();
                Masks.Content = img.Masks();
            }//Metadata

            //palette
            img.GetColorList();
            lstboxColors.ItemsSource = img.ColorList;

            if (img.ColorList.Count > 0)
            {
                PaletteCount.Visibility = Visibility.Visible;
                txtblkRGBNumbers.Visibility = Visibility.Visible;
                lstboxColors.Visibility = Visibility.Visible;

                PaletteTitle.Content = rm.GetString("PaletteTitle");
                PaletteCount.Content = rm.GetString("PaletteCount") + " " +img.ColorList.Count();
                txtblkRGBNumbers.Content = rm.GetString("lstClickOnForDetails");
            }
            else //no palette
            {
                PaletteCount.Visibility = Visibility.Collapsed;
                txtblkRGBNumbers.Visibility = Visibility.Collapsed;
                lstboxColors.Visibility = Visibility.Collapsed;

                PaletteTitle.Content = rm.GetString("NoPaletteTitle");
            }
            
        }//RefreshScreen
    
    }//class
}//namespace
