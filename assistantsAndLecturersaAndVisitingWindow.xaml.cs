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
    /// Interaction logic for assistantsAndLecturersWindow.xaml
    /// </summary>
    public partial class assistantsAndLecturersAndVisitingWindow : Window
    {
        public static int numberOfTeachers;
        public static int requiredTeacherLineNumber;
        int numberOfAssistants = 0;
        public assistantsAndLecturersAndVisitingWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);

            numberOfTeachers = controlFile.getNumberOfLineInControlFile();


            string test;
            List<Button> assistantObject1 = new List<Button>();
            List<Button> assistantObject2 = new List<Button>();
            List<Button> lecturerObject = new List<Button>();
            List<Button> visitingObject = new List<Button>();


            for (int i = 1; i <= numberOfTeachers; ++i)
            {
                test = controlFile.getTeacherPositionTeacherWindow(i).ToLower();

                if (test == "assistant" && numberOfAssistants < 14)
                {
                    Button temp = new Button() { Content = controlFile.getTeacherNameTeachersInfoWindow(i) };
                    #region button style
                    temp.FontSize = 20;
                    temp.Foreground = new SolidColorBrush(Colors.Purple);
                    temp.FontWeight = FontWeights.Bold;
                    temp.Opacity = 0.5;
                    temp.Background = new SolidColorBrush(Colors.Transparent);
                    temp.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    #endregion button style

                    temp.Click += (s, e) => { buttonTeacherObject(temp.Content.ToString()); };

                    assistantObject1.Add(temp);

                    numberOfAssistants += 1;
                }
                else if (test == "assistant")
                {
                    Button temp = new Button() { Content = controlFile.getTeacherNameTeachersInfoWindow(i) };
                    #region button style
                    temp.FontSize = 20;
                    temp.Foreground = new SolidColorBrush(Colors.Purple);
                    temp.FontWeight = FontWeights.Bold;
                    temp.Opacity = 0.5;
                    temp.Background = new SolidColorBrush(Colors.Transparent);
                    temp.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    #endregion button style
                    temp.Click += (s, e) => { buttonTeacherObject(temp.Content.ToString()); };

                    assistantObject2.Add(temp);
                }
                else if (test == "lecturers")
                {
                    Button temp = new Button() { Content = controlFile.getTeacherNameTeachersInfoWindow(i) };
                    #region button style
                    temp.FontSize = 20;
                    temp.Foreground = new SolidColorBrush(Colors.Purple);
                    temp.FontWeight = FontWeights.Bold;
                    temp.Opacity = 0.5;
                    temp.Background = new SolidColorBrush(Colors.Transparent);
                    temp.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    #endregion button style
                    temp.Click += (s, e) => { buttonTeacherObject(temp.Content.ToString()); };

                    lecturerObject.Add(temp);
                }
                else if (test == "visiting")
                {
                    Button temp = new Button() { Content = controlFile.getTeacherNameTeachersInfoWindow(i) };
                    #region button style
                    temp.FontSize = 20;
                    temp.Foreground = new SolidColorBrush(Colors.Purple);
                    temp.FontWeight = FontWeights.Bold;
                    temp.Opacity = 0.5;
                    temp.Background = new SolidColorBrush(Colors.Transparent);
                    temp.BorderBrush = new SolidColorBrush(Colors.Transparent);
                    #endregion button style
                    temp.Click += (s, e) => { buttonTeacherObject(temp.Content.ToString()); };

                    visitingObject.Add(temp);
                }


            }

            listBoxAssistants1.ItemsSource = assistantObject1;
            listBoxAssistants2.ItemsSource = assistantObject2;
            listBoxLecturers.ItemsSource = lecturerObject;
            listBoxVisitings.ItemsSource = visitingObject;
        }

        private void buttonTeacherObject(string teacherName)
        {
            variables.teacherInfoWindow = this;

            requiredTeacherLineNumber = controlFile.getLineNumberTeacherWindow(teacherName);

            variables.preparePdfFile(requiredTeacherLineNumber);

            pdfWindow pdfWindow = new pdfWindow();
            variables.previousCurrentWindow = variables.currentWindow;
            variables.currentWindow = pdfWindow;
            pdfWindow.Show();

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
