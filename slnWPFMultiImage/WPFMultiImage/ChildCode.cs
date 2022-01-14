using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows;

namespace WPFMultiImage
{
    public static class ChildCode
    {
        public static void Close_Click(DependencyObject dobj)
        {
            Window child = (Window)dobj;
            child.Owner = null;
            child.ShowInTaskbar = true;
            child.Close();
        }


    }//class
}//namespace
