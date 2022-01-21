using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Windows;

namespace WPFMultiImage
{
    public class CarouselClass : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        private int _current;
        public int Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (value != _current)
                {
                    _current = value;
                    //NotifyPropertyChanged();
                }
            }
        }

        private ArrayList _filelist;
        public ArrayList FileList
        {
            get
            {
                return _filelist;
            }
            set
            {
                if (value != _filelist)
                {
                    _filelist = value;
                    //NotifyPropertyChanged();
                }
            }
        }

        private string _displayed;
        public string Displayed
        {
            get
            {
                return _displayed;
            }
            set
            {
                if (value != _displayed)
                {
                    _displayed = value;
                    //NotifyPropertyChanged();
                }
            }
        }

        //================================================================================================

        public void AddToFileList(string[] morefiles)
        {

            foreach (string fn in morefiles)
            {
                if (!(FileList.Contains(fn)))
                    FileList.Add(fn);
            }
        }

        //=================================================================================================

        public void ZeroCurrent()
        {
            this.Current = 0;
        }

        public void PreviousCurrent()
        {
            this.Current = this.Current - 1;

            if (this.Current < 0)
                this.Current = this.FileList.Count-1;

        }

        public void NextCurrent()
        {

            this.Current = this.Current + 1;

            if (this.Current == this.FileList.Count)
                this.Current = 0;
        }

        public string WhereAmI()
        {
            return (this.Current + 1).ToString() + " " + rm.GetString("xOFy") + " " + this.FileList.Count.ToString();
        }
    }//class
}//namespace
