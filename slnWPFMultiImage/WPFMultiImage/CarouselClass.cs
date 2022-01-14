using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFMultiImage
{
    class CarouselClass : INotifyPropertyChanged
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
    

    }//class
}//namespace
