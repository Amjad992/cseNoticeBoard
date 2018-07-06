using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;

namespace cseNoticeBoard
{
    class variables
    {
        ///////// -------- required path strings to be used according to need to -------- /////////
        // the path of the application where the exe files exist, resources file must be place at same folder
        public static string appResourcesPath = Directory.GetCurrentDirectory() + "\\resources\\";
        // will be set as appResourcesPath + resourceFolderName before the openning of a window using the function 
        // prepareWindowControlFile. ex : prepareWindowControlFile ("announcements\\");
        public static string requiredResourcePath;
        // this is a string that will not be changed and contains the name of the control files
        public static string controlFileString = "controlFile.txt";
        // this will be the compination of requiredResourcePath + controlFileString
        public static string controlFilePath;
        // this will be the compination of requiredResourcePath + imageName from control File + extension
        // this will be set using the function prepareImage
        public static string imagePath;
        public static string pdfPath;

        // this is for eventPicturesWindow only
        public static string currentDirectorySelected;
        public static string currentSubDirectorySelected;

        // used to recongnize if the pdf file displayed in pdfWindow is chairmanMessage or not
        // useful in navigation after closing and when map is clicked
        public static bool chairmanMessageDisplayed;
        public static Window teacherInfoWindow;

        public static string notificationMessage = "error occured";

        public static Window currentWindow;
        public static Window previousCurrentWindow;


        ///////// ---------------------------------------------------------------------- /////////

        // will be set as true if announcement in slider is clicked and image displayed will be deciding according
        // to this either default image which is first or a specific image
        public static bool goToAnnouncementFromSlide = false;

        public static void openPage (string resource)
        {
            if (resource == "announcements")
            {
                setResourcesPath("announcements\\");
                prepareWindowControlFile();
                AnnouncementsWindow announcements = new AnnouncementsWindow();
                currentWindow = announcements;
                announcements.Show();
            }
            else if (resource == "teachersInfo")
            {
                setResourcesPath("teachersInfo\\");
                prepareWindowControlFile();
                teachersInfoWindow teachersInfo = new teachersInfoWindow();
                currentWindow = teachersInfo;
                teachersInfo.Show();
            }
            else if (resource == "timeTables")
            {
                setResourcesPath("timeTables\\");
                prepareWindowControlFile();
                timeTablesWindow timeTables = new timeTablesWindow();
                currentWindow = timeTables;
                timeTables.Show();
            }
            else if (resource == "departmentInformation")
            {
                setResourcesPath("departmentMaps\\");
                prepareWindowControlFile();
                departmentInformationWindow departmentInformation = new departmentInformationWindow();
                currentWindow = departmentInformation;
                departmentInformation.Show();
            }
            else if (resource == "eventsPictures")
            {
                setResourcesPath("eventPictures\\");
                prepareWindowControlFile();
                eventsPicturesWindow eventsPictures = new eventsPicturesWindow();
                currentWindow = eventsPictures;
                eventsPictures.Show();
            }
            else if (resource == "credits")
            {
                setResourcesPath("credits\\");
                // there is no need to prepare control file
                creditsWindow credits = new creditsWindow();
                currentWindow = credits;
                credits.Show();
            }
            // in case the call for the buttons inside the teachersInfoWindow, no need to set resourcePath
            else if (resource == "professorsAndAssociates\\")
            {
                professorsAndAssociateWindow professorsAndAssociateWindow = new professorsAndAssociateWindow();
                currentWindow = professorsAndAssociateWindow;
                professorsAndAssociateWindow.Show();
            }
            else if (resource == "assistantsAndLecturers\\")
            {
                assistantsAndLecturersAndVisitingWindow assistantsAndLecturersAndVisitingWindow = new assistantsAndLecturersAndVisitingWindow();
                currentWindow = assistantsAndLecturersAndVisitingWindow;
                assistantsAndLecturersAndVisitingWindow.Show();
            }



        }
        public static void setResourcesPath(string resourceTitle)
        {
            requiredResourcePath = appResourcesPath + resourceTitle;
        }
        public static void setControlFilePath ()
        {
            controlFilePath = requiredResourcePath + controlFileString;
        }
        public static void prepareWindowControlFile()
        {
            setControlFilePath();
            controlFile.setTextAsCurrent();
        }
        public static void prepareImageFile (int lineNumber, string extension)
        {
            imagePath = requiredResourcePath + controlFile.getFileName(lineNumber) + extension;
        }
        public static void displayImage(Window window, Image image)
        {
            // the uri of the image
            try
            {
                var uri = new Uri(imagePath);
                // if the image is found (name is correct)
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            catch (FileNotFoundException)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                window.Close();
            }
        }
        public static void setLogoImage(Window window, Image image)
        {
            try
            {
                var uri = new Uri(appResourcesPath + "\\uetLogo.png");
                // if the image is found (name is correct)
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            catch (FileNotFoundException)
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                window.Close();
            }
        }

        public static void preparePdfFile(int lineNumber)
        {
            pdfPath = requiredResourcePath + controlFile.getFileName(lineNumber) + ".pdf";
        }

    }
}
