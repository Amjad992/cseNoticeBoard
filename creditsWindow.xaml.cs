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

namespace cseNoticeBoard
{
    /// <summary>
    /// Interaction logic for creditsWindow.xaml
    /// </summary>
    public partial class creditsWindow : Window
    {
        //string imagePath;
        public creditsWindow()
        {
            InitializeComponent();

            variables.imagePath = variables.requiredResourcePath + "Dr. Usman.jpg";
            variables.displayImage(this, imageAdvisor);


            variables.imagePath = variables.requiredResourcePath + "Amjad.jpg";
            variables.displayImage(this, imageAmjad);

            variables.imagePath = variables.requiredResourcePath + "Yousef.jpg";
            variables.displayImage(this, imageYousef);

            variables.imagePath = variables.requiredResourcePath + "Fraidoon.jpg";
            variables.displayImage(this, imageFraidoon);

            variables.imagePath = variables.requiredResourcePath + "Faizan.jpg";
            variables.displayImage(this, imageFaizan);

        }

        #region moving between windows
        private void buttonMain_Click(object sender, RoutedEventArgs e)
        {
            variables.setResourcesPath("announcements\\");
            variables.prepareWindowControlFile();

            MainWindow.annoucementTimeInterval.Start();

            this.Close();
        }
        private void buttonAnnouncements_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("announcements");
            this.Close();
        }
        private void buttonTeachersInfo_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("teachersInfo");
            this.Close();
        }
        private void buttonTimeTables_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("timeTables");
            this.Close();
        }
        private void buttonDepartmentInformation_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("departmentInformation");
            this.Close();
        }
        private void buttonEventsPictures_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("eventsPictures");
            this.Close();
        }
        private void buttonCredits_Click(object sender, RoutedEventArgs e)
        {
            variables.openPage("credits");
            this.Close();
        }
        #endregion

    }
}
