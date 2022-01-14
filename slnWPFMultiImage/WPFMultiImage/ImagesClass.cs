using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media.Imaging;

namespace WPFMultiImage
{
    public class ImagesClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private static Visibility _imagesloaded;
        public static Visibility ImagesLoaded
        {
            get
            {
                return _imagesloaded;
            }
            set
            {
                if (value != _imagesloaded)
                {
                    _imagesloaded = value;
                    //NotifyPropertyChanged();
                }
            }
        }//ImagesLoaded

        private FileInfo _fileinfo;
        public FileInfo FileInfo
        {
            get
            {
                return _fileinfo;
            }
            set
            {
                if (value != _fileinfo)
                {
                    _fileinfo = value;
                    NotifyPropertyChanged();

                }
            }
        }

        private string _filetype;
        public string FileType
        {
            get
            {
                return _filetype;
            }
            set
            {
                if (value != _filetype)
                {
                    _filetype = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapImage _bitmapImage;

        public BitmapImage BitmapImage
        {
            get
            {
                return _bitmapImage;
            }
            set
            {
                if (value != _bitmapImage)
                {
                    value = _bitmapImage;
                    NotifyPropertyChanged();
                }
            }
        }

        private BitmapPalette _bitmappalette;

        public BitmapPalette BitmapPalette
        {
            get
            {
                return _bitmappalette;
            }
            set
            {
                if (value != _bitmappalette)
                {
                    value = _bitmappalette;
                    NotifyPropertyChanged();
}
            }
        }

        private BitmapMetadata _bitmapmetadata;

        public BitmapMetadata BitmapMetadata
        {
            get
            {
                return _bitmapmetadata;
            }
            set
            {
                if (value != _bitmapmetadata)
                {
                    value = _bitmapmetadata;
                    NotifyPropertyChanged();
                }
            }
        }




    }//class
}//namespace
