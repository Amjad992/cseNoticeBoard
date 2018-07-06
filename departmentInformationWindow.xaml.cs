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
    /// Interaction logic for departmentInformationWindow.xaml
    /// </summary>
    public partial class departmentInformationWindow : Window
    {
        public static int fileNumber;
        //it was .txt when it was displaying file's text
        string thisWindowextension = ".jpg";
        int tabsAvailable;
        string currentFilePath;
        //string contentFileText;



        public departmentInformationWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);


            setNumberOfTabs("this");
            setTextOfUpperButtons();

            fileNumber = 1;

            prepareContentFile();
            // no need for this any more 
            //displayContentFile();
            variables.displayImage(this, middleImage);
        }

        private void setNumberOfTabs(string path)
        {
            if (path == "this")
            {
                int numberOfFiles = controlFile.getNumberOfLineInControlFile();

                if (numberOfFiles < 1)
                { }
                // -- TODO -- //
                //add an expression later to make sure that it do not open if there is no directories
                else if (numberOfFiles < 5)
                    tabsAvailable = numberOfFiles;
                else
                    tabsAvailable = 4;
            }
            else
            {

            }
        }

        private void setTextOfUpperButtons()
        {
            TextBlock[] temp = { textBlockTab1, textBlockTab2, textBlockTab3, textBlockTab4 };
            clearTextOfTextBlockTabs();

            for (int i = 1; i <= tabsAvailable; ++i)
            {
                temp[i - 1].Text = controlFile.getTabTitle(i);
            }
        }

        private void clearTextOfTextBlockTabs()
        {
            textBlockTab1.Text = "";
            textBlockTab2.Text = "";
            textBlockTab3.Text = "";
            textBlockTab4.Text = "";
        }

        private void prepareContentFile()
        {
            //only first line was used when it was displaying file's text, second line is for image part only
            currentFilePath = variables.requiredResourcePath + controlFile.getFileName(fileNumber) + thisWindowextension;
            variables.imagePath = currentFilePath;
        }

        private void displayContentFile()
        {
            ////this was when it was displaying file content
            //contentFileText = System.IO.File.ReadAllText(currentFilePath);
            //middleTextBlock.Text = contentFileText;
            variables.displayImage(this, middleImage);
        }

        private void buttonTab_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;

            int temp = Convert.ToInt32(senderButton.Content.ToString());
            fileNumber = temp;

            changeTabDisplayed();
        }

        private void changeTabDisplayed()
        {
            prepareContentFile();
            displayContentFile();


            #region maintain the blue background behnind the buttons
            hideAlltextBackgrounds();
            if (fileNumber == 1)
            {
                textBackgroundFourthTab.Opacity = 100;
            }
            else if (fileNumber == 2)
            {
                textBackgroundThirdTab.Opacity = 100;
            }
            else if (fileNumber == 3)
            {
                textBackgroundSecondTab.Opacity = 100;
            }
            else if (fileNumber == 4)
            {
                textBackgroundFirstTab.Opacity = 100;
            }
            #endregion
        }

        private void hideAlltextBackgrounds()
        {
            textBackgroundFourthTab.Opacity = 0;
            textBackgroundThirdTab.Opacity = 0;
            textBackgroundSecondTab.Opacity = 0;
            textBackgroundFirstTab.Opacity = 0;
        }
        #region moving between windows
        private void buttonMain_Click(object sender, RoutedEventArgs e)
        {
            variables.setResourcesPath("announcements\\");
            variables.prepareWindowControlFile();

            MainWindow.annoucementTimeInterval.Start();

            this.Close();
        }
        private void buttonEnlarge_Click(object sender, RoutedEventArgs e)
        {
            imageWindow imageWindow = new imageWindow();
            variables.previousCurrentWindow = variables.currentWindow;
            variables.currentWindow = imageWindow;
            imageWindow.Show();

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
