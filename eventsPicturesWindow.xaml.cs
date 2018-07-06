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
    /// Interaction logic for eventsPicturesWindow.xaml
    /// </summary>
    public partial class eventsPicturesWindow : Window
    {
        public static int directoryNumber;
        int directoriesAvailable;

        public eventsPicturesWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);

            setNumberOfDirectories("this");
            setTextOfUpperButtons();

            directoryNumber = 1;

            setCurrentDirectoryPath(directoryNumber);
            displayFolders();
        }


        private void setNumberOfDirectories(string path)
        {
            if (path == "this")
            {
                int folderNumbers = controlFile.getNumberOfLineInControlFile();

                if (folderNumbers < 1)
                {
                }
                // -- TODO -- //
                //add an expression later to make sure that it does not open if there is no directories
                else if (folderNumbers < 5)
                    directoriesAvailable = folderNumbers;
                else
                    directoriesAvailable = 4;
            }
            else
            {

            }
        }
        private void setCurrentDirectoryPath(int directoryNumber)
        {
            variables.currentDirectorySelected = variables.requiredResourcePath + 
                controlFile.getDirectoryName(directoryNumber);
        }
        private void displayFolders()
        {
            //textBlockFolder1;
            //canvasEnvlope1;
            Visibility visible = System.Windows.Visibility.Visible;


            TextBlock[] textBlockFolder = { textBlockFolder1, textBlockFolder2, textBlockFolder3,
                                 textBlockFolder4, textBlockFolder5, textBlockFolder6,
                                 textBlockFolder7, textBlockFolder8};
            Canvas[] canvas = { canvasEnvlope1, canvasEnvlope2, canvasEnvlope3, canvasEnvlope4,
                                canvasEnvlope5, canvasEnvlope6, canvasEnvlope7, canvasEnvlope8};
            Button[] button = { buttonFolder1, buttonFolder2, buttonFolder3, buttonFolder4,
                                buttonFolder5, buttonFolder6, buttonFolder7, buttonFolder8};
            clearTextOfTextBlockFoldersAndHideCanvasesAndButton();

            string[] subdirectoryEntries = Directory.GetDirectories(variables.currentDirectorySelected);
            int i = 0;
            string temp;
            foreach (string subdirectory in subdirectoryEntries)
            {
                // + 2 to delete the 2 // characters
                temp = getDirectoryNameFromDirectoryPath(subdirectory);
                // commented because I decided to make all the names with one size, it won't look nice with different values
                //if (temp.Length > 12)
                {
                    textBlockFolder[i].FontSize = 18;
                }
                // The number 103 is decided by manual testing until it looks good
                int maxLengthOfName = 103;
                if (temp.Length> maxLengthOfName)
                {
                    int count = temp.Length - maxLengthOfName;
                    int x = count;
                    temp = temp.Remove(maxLengthOfName, count);
                }
                textBlockFolder[i].Text = temp;
                canvas[i].Visibility = visible;
                button[i].Visibility = visible;
                ++i;
            }

        }


        private void setTextOfUpperButtons()
        {
            TextBlock[] temp = { textBlockTab1, textBlockTab2, textBlockTab3, textBlockTab4 };
            clearTextOfTextBlockTabs();

            for (int i = 1; i <= directoriesAvailable; ++i)
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
        private void clearTextOfTextBlockFoldersAndHideCanvasesAndButton()
        {
            Visibility hidden = System.Windows.Visibility.Hidden;

            textBlockFolder1.Text = "";
            textBlockFolder2.Text = "";
            textBlockFolder3.Text = "";
            textBlockFolder4.Text = "";
            textBlockFolder5.Text = "";
            textBlockFolder6.Text = "";
            textBlockFolder7.Text = "";
            textBlockFolder8.Text = "";

            canvasEnvlope1.Visibility = hidden;
            canvasEnvlope2.Visibility = hidden;
            canvasEnvlope3.Visibility = hidden;
            canvasEnvlope4.Visibility = hidden;
            canvasEnvlope5.Visibility = hidden;
            canvasEnvlope6.Visibility = hidden;
            canvasEnvlope7.Visibility = hidden;
            canvasEnvlope8.Visibility = hidden;

            buttonFolder1.Visibility = hidden;
            buttonFolder2.Visibility = hidden;
            buttonFolder3.Visibility = hidden;
            buttonFolder4.Visibility = hidden;
            buttonFolder5.Visibility = hidden;
            buttonFolder6.Visibility = hidden;
            buttonFolder7.Visibility = hidden;
            buttonFolder8.Visibility = hidden;
        }
        private string getDirectoryNameFromDirectoryPath(string path)
        {
            string temp = path.Remove(0, variables.currentDirectorySelected.Length + 1);
            return temp;
        }
        private void buttonFolder_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;
            string temp = senderButton.Content.ToString();
            int folderNumberClicked = Convert.ToInt32(temp);

            int i = 1;
            string[] subdirectoryEntries = Directory.GetDirectories(variables.currentDirectorySelected);
            foreach (string sub in subdirectoryEntries)
            {
                if (folderNumberClicked == i)
                    variables.currentSubDirectorySelected = sub;
                
                    ++i;
            }

            try
            {
                displayImagesInFolder();
            }
            catch (InvalidOperationException)
            {

            }
        }
        private void displayImagesInFolder()
        {
            folderOfImagesWindow folderOfImagesWindow = new folderOfImagesWindow();
            variables.previousCurrentWindow = variables.currentWindow;
            variables.currentWindow = folderOfImagesWindow;
            //try
            //{
            folderOfImagesWindow.Show();
            //}
            //catch (IndexOutOfRangeException)
            //{

            //}
        }
        private void buttonTab_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;

            int temp = Convert.ToInt32(senderButton.Content.ToString());
            directoryNumber = temp;

            changeSetOfFolders();
        }
        private void hideAlltextBackgrounds()
        {
            textBackgroundFourthTab.Opacity = 0;
            textBackgroundThirdTab.Opacity = 0;
            textBackgroundSecondTab.Opacity = 0;
            textBackgroundFirstTab.Opacity = 0;
        }
        private void changeSetOfFolders()
        {
            setCurrentDirectoryPath(directoryNumber);
            displayFolders();

            #region maintain the blue background behnind the buttons
            hideAlltextBackgrounds();
            if (directoryNumber == 1)
            {
                textBackgroundFourthTab.Opacity = 100;
            }
            else if (directoryNumber == 2)
            {
                textBackgroundThirdTab.Opacity = 100;
            }
            else if (directoryNumber == 3)
            {
                textBackgroundSecondTab.Opacity = 100;
            }
            else if (directoryNumber == 4)
            {
                textBackgroundFirstTab.Opacity = 100;
            }
            #endregion
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

