using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Collections;
using System.ComponentModel;
using System.IO;
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

        public bool ImagesLoaded //Save Scenario enabled
        {
            get 
            {
                return (this.FileList.Count > 0);
            }
        }

        public CarouselClass()
        {
            this.FileList = new();
            this.BadCurrent();
            this.Displayed = "";
        }//default cons

        //===============================================================================================

        public string CurrentFile()
        {
            string ret = ""; ;

            if (this.Current >= 0)
                ret = (string)this.FileList[this.Current];

            return ret;
        }

        public void AddToFileList(string[] morefiles) //From string array to arraylist
        {

            foreach (string fn in morefiles)
            {
                if (!(FileList.Contains(fn)))
                    FileList.Add(fn);
            }
        }

        public void AddOneFileToFileList(string f)  //string is file name => add to arraylist
        {
            if (!FileList.Contains(f)) //throw away dups no message
                if (File.Exists(f))
                {
                    FileList.Add(f);
                }
                else
                {
                    MessageBox.Show(f + " " + rm.GetString("CannotBeFound"), "", MessageBoxButton.OK);
                }
        }

        public void ClearFileList()
        {
            FileList.Clear(); 
        }

        //==================================================================================================

        public void ReadAScenario()
        {
            if (FileDialogImage.OpenFileScenario())
            {
                if (FileList.Count > 0) //there are already images loaded - clear, append, or cancel?
                {
                    ManageScenarios ms = new();
                    ms.ShowDialog();

                    if (ms.ManageScenariosAction == "ClearAndOpen")
                    {
                        ClearFileList();
                    }

                    if (ms.ManageScenariosAction != "Cancel")  //"ClearAndOpen" && "Append"
                    {
                        LoadScenariosIntoFileList();
                    }
                    //else Cancel - do nothing
                }
                else //Nothing loaded => proceed
                {
                    LoadScenariosIntoFileList();
                }
            }//We opened one or more scenarios
        }//ReadAScenario

        public void WriteAScenario()
        {
            if (FileDialogImage.SaveFileScenario())
            {
                using (StreamWriter sw = File.CreateText(FileDialogImage.ChosenFile))
                {
                    foreach (string s in FileList)
                        sw.WriteLine(s);
                }
            }
        }//WriteAScenario

        private void LoadScenariosIntoFileList()
        {
            foreach (string scenariofn in FileDialogImage.FileNames)
            {
                string[] ListOFiles = File.ReadAllLines(scenariofn);

                foreach (string fn in ListOFiles)
                    AddOneFileToFileList(fn);          
            }
        }

        //=================================================================================================

        public void BadCurrent()
        {
            this.Current = -1; //no image currently displayed
        }
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
