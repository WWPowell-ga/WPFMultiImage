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

using System.Reflection;
using System.Resources;

namespace WPFMultiImage
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {
        ResourceManager rm = new ResourceManager("WPFMultiImage.Properties.Resources", Assembly.GetExecutingAssembly());

        public About()
        {
            InitializeComponent();

            this.Title = rm.GetString("AboutBoxWindowTitle");
            this.AboutBy.Content = rm.GetString("AboutBy");

            GetAssemblyInfoForAbout();
        }

        private void GetAssemblyInfoForAbout()
        {
            // Get the AssemblyInfo class.
            ReadAssemblyInfo info = new ReadAssemblyInfo();

            // Display the values.
            //titleTextBox.Text = info.Title;
            TitleContent.Content = info.Title;

            //fileVersionTextBox.Text = info.FileVersion;
            fileVersionContent.Content = info.FileVersion;

            //descriptionTextBox.Text = info.Description;
            descriptionContent.Text = info.Description;

            //companyTextBox.Text = info.Company;
            companyContent.Content = info.Company;

            //productTextBox.Text = info.Product;
            //copyrightTextBox.Text = info.Copyright;
            //trademarkTextBox.Text = info.Trademark;
            //assemblyVersionTextBox.Text = info.AssemblyVersion;

            //guidTextBox.Text = info.Guid;
            //neutralLanguageTextBox.Text = info.NeutralLanguage;
            //comVisibleTextBox.Text = info.IsComVisible.ToString();
        }

    }//class
}//namespace
