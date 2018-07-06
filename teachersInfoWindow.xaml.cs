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
    /// Interaction logic for teachersInfoWindow.xaml
    /// </summary>
    public partial class teachersInfoWindow : Window
    {
        public teachersInfoWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);          
        }
        private void buttonChairman_Click(object sender, RoutedEventArgs e)
        {
            openAcademicPage("chairmanPage");
        }
        private void buttonProfessors_Click(object sender, RoutedEventArgs e)
        {
            openAcademicPage("professorsAndAssociates\\");
        }
        private void buttonAssociateProfessors_Click(object sender, RoutedEventArgs e)
        {
            openAcademicPage("professorsAndAssociates\\");
        }
        private void buttonAssistantProfessors_Click(object sender, RoutedEventArgs e)
        {
            openAcademicPage("assistantsAndLecturers\\");
        }
        private void buttonLecturers_Click(object sender, RoutedEventArgs e)
        {
            openAcademicPage("assistantsAndLecturers\\");
        }
        private void openAcademicPage(string page)
        {   
            if (page == "chairmanPage")
            {
                displayChairmanPage();
            }
            else
            {
                variables.teacherInfoWindow = this;

                variables.openPage(page);
                this.Close();
            }        
        }

        private void displayChairmanPage()
        {
            //This was when chairman button displaying chariman CV, now it will display his message
            //int chairmanLine = 0;
            //int lineCount = File.ReadAllLines(variables.controlFilePath).Length;

            //for (int i=1; i<lineCount; ++i)
            //{
            //    if (controlFile.checkIfChairman(i))
            //    {
            //        chairmanLine = i;
            //        i = lineCount;
            //    }              
            //}

            //variables.preparePdfFile(chairmanLine);

            variables.pdfPath = variables.requiredResourcePath + "chairman.pdf";
            variables.chairmanMessageDisplayed = true;

            variables.teacherInfoWindow = this;
            pdfWindow pdfWindow = new pdfWindow();
            variables.previousCurrentWindow = variables.currentWindow;
            variables.currentWindow = pdfWindow;
            pdfWindow.Show();

            this.Close();
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
