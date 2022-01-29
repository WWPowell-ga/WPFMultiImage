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
        ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        public CarouselClass Carousel { get; set; }
        public Dictionary<string,ImagesClass> Images = new Dictionary<string, ImagesClass>();

        private ImagesClass oneImage;

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
                rm.GetString("Index") + " " + lb.SelectedIndex.ToString() + "    " +
                rm.GetString("Red") + " " + (ci as ColorInfo).Color.R + "    " +
                rm.GetString("Green") + " " + (ci as ColorInfo).Color.G + "    " +
                rm.GetString("Blue") + " " + (ci as ColorInfo).Color.B + "    " +
                rm.GetString("Alpha") + " " + (ci as ColorInfo).Color.A;
            
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
                Carousel.AddToFileList(FileDialogImage.FileNames);  //Assuming since I just pick them that they are there

                if (Carousel.Current == -1)
                    Carousel.ZeroCurrent();  //images loaded

                DisplayCurrentImageToScreen();
            }
        }

        private void About_Click(object sender, RoutedEventArgs e)
        {
            About wind = new();
            wind.ShowDialog();
        }

        private void OpenScenario_Click(object sender, RoutedEventArgs e)
        {
            Carousel.ReadAScenario();

            if (Carousel.Current == -1)
                Carousel.ZeroCurrent();  //images loaded

            DisplayCurrentImageToScreen();
        }

        private void SaveScenario_Click(object sender, RoutedEventArgs e)
        {
            Carousel.WriteAScenario(); 
        }

        //== Toolbar clicks

        private void Previous_Click(object sender, RoutedEventArgs e)
        {
            Carousel.PreviousCurrent();
            DisplayCurrentImageToScreen();
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            Carousel.NextCurrent();
            DisplayCurrentImageToScreen();
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

        private void SaveImage_Click(object sender, RoutedEventArgs e) //save current image
        {
            oneImage = new();
            oneImage.SaveImage(Images[Carousel.CurrentFile()]);
        }

        private void CloseImage_Click(object sender, RoutedEventArgs e)
        {
            RemoveImageFromFileList(Carousel.CurrentFile()); 
            DisplayCurrentImageToScreen();
        }

        private void CloseAll_Click(object sender, RoutedEventArgs e)
        {
            int imgcnt = Carousel.FileList.Count;

            for (int i=0; i < imgcnt; i++)
                RemoveImageFromFileList(Carousel.CurrentFile()); //Moves Current

            DisplayCurrentImageToScreen(); //Handles ImageGrid
        }

        private void SaveMultiImage_Click(object sender, RoutedEventArgs e) //save current image
        {
            oneImage = new();
            oneImage.SaveMultiImage(Images);
        }
        //================================================================================================

        private void RemoveImageFromFileList(string FileWhatGoes) //Changes Current
        {
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
            }
            else
            {
                Carousel.BadCurrent(); //Nothing to Display
            }
        }//RemoveImageFromFileList

        private void DisplayCurrentImageToScreen()
        { 
            //If there is nothing to display get out
            if (Carousel.Current == -1)
            {
                ImageGrid.Visibility = Visibility.Hidden;
                return; //this would end recursion
            }

            //Update WhereAmI on Toolbar in case images have been added to FileList
            WhereAmI.Content = Carousel.WhereAmI();

            string file2display = Carousel.CurrentFile();

            //If displayed image is same as current image, there's nothing to do
            if (Carousel.Displayed == file2display)
                return;

            ////Does file in FileList still exist
            //if (!(File.Exists(file2display)))
            //{
            //    MessageBox.Show(file2display + " " + rm.GetString("CannotBeFound"), "", MessageBoxButton.OK);
            //    RemoveImageFromFileList(file2display); //This move Current
            //    DisplayCurrentImageToScreen();  //RECURSION!
            //}

            oneImage = new();

            if (!(Images.ContainsKey(file2display))) //load img into dictionary if need be
            {
                Mouse.OverrideCursor = Cursors.Wait;  //Worry circle?
                Images[file2display] = oneImage.LoadImage(file2display); //Lazy loading, does not overwrite previous load no err
                Mouse.OverrideCursor = null;
            }

            //Make ImageGrid visible if need be
            if (ImageGrid.Visibility == Visibility.Hidden)
                ImageGrid.Visibility = Visibility.Visible;

            //Make image itself visible if need be
            if (Carousel.Displayed != file2display)
            {
                RefreshScreen(Images[file2display]);
                Carousel.Displayed = file2display;
            }

        }//DisplayCurrentImageToScreen

        private void RefreshScreen(ImagesClass img)
        {
            //Update the xaml

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

            PixelFormat.Content = img.BitmapImage.Format.ToString();
            PixelFormatDefinition.Text = PixelFormatDef.Defintion(img.BitmapImage.Format.ToString().ToUpper());

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
