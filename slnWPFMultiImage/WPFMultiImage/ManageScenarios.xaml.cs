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

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for ManageScenarios.xaml
    /// </summary>
    public partial class ManageScenarios : Window
    {
        public string ManageScenariosAction;
        public ManageScenarios()
        {
            InitializeComponent();

            ManageScenariosAction = "Cancel"; //default

        }

        private void ClearAndOpen_Click(object sender, RoutedEventArgs e)
        {
            ManageScenariosAction = "ClearAndOpen";
            this.Close();
        }

        private void Append_Click(object sender, RoutedEventArgs e)
        {
            ManageScenariosAction = "Append";
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ManageScenariosAction = "Cancel";
            this.Close();

        }

    }//class
}//namespace
