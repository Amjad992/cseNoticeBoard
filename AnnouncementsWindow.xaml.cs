using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for AnnouncementsWindow.xaml
    /// </summary>
    public partial class AnnouncementsWindow : Window
    {
        public static int lineNumber;
        string thisWindowextension = ".jpg";

        int announcementAvailable;


        public AnnouncementsWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);

            setNumberOfAnnouncements();

            // later will need this to subtract image name from that line
            if (variables.goToAnnouncementFromSlide == false)
                lineNumber = 1;

            variables.prepareImageFile(lineNumber, thisWindowextension);
            variables.displayImage(this, imageAnnouncements);
        }

        private void setNumberOfAnnouncements()
        {
            // this - 1 because it is calculating the lines with guidance line which
            announcementAvailable = File.ReadLines(variables.controlFilePath).Count() - 1;            
        }
        private void buttonNextAnnoucement_Click(object sender, RoutedEventArgs e)
        {
            lineNumber += 1;

            //string name = controlFile.getFileName(lineNumber);
            if (lineNumber > announcementAvailable)
                lineNumber = 1;

            variables.prepareImageFile(lineNumber, ".jpg");
            variables.displayImage(this, imageAnnouncements);
        }
        private void buttonPreviousAnnoucement_Click(object sender, RoutedEventArgs e)
        {
            lineNumber -= 1;
            if (lineNumber < 1)
                lineNumber = announcementAvailable;

            variables.prepareImageFile(lineNumber, ".jpg");
            variables.displayImage(this, imageAnnouncements);
        }

        private void buttonEnlarge_Click(object sender, RoutedEventArgs e)
        {
            variables.prepareImageFile(lineNumber, thisWindowextension);
            imageWindow imageWindow = new imageWindow();
            variables.previousCurrentWindow = variables.currentWindow;
            variables.currentWindow = imageWindow;
            imageWindow.Show();

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
