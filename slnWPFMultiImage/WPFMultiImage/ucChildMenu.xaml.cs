﻿using System;
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

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for ucChildMenu.xaml
    /// </summary>
    public partial class ucChildMenu : UserControl
    {
        public ucChildMenu()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            ChildCode.Close_Click(Window.GetWindow(this)); //Child window
        }

    }//class
}//namespace
