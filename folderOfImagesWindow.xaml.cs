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
    /// Interaction logic for folderOfImagesWindow.xaml
    /// </summary>
    public partial class folderOfImagesWindow : Window
    {
        public string imageFolder;
        int numberOfFiles;
        int imageNumber;
        string[] filesInFolder;

        public folderOfImagesWindow()
        {
            InitializeComponent();

            imageFolder = variables.currentSubDirectorySelected + "//";


            filesInFolder = Directory.GetFiles(variables.currentSubDirectorySelected);
            numberOfFiles = filesInFolder.Length;


            imageNumber = 5;
            try
            {
                display();
            }
            catch (IndexOutOfRangeException)
            {
                notificationsWindow notificationsWindow = new notificationsWindow();
                notificationsWindow.textBlockMessage.Text = "Oops! No images in this Folder!";
                notificationsWindow.Show();

                // this was causing the problem in the following scenario
                // 1) open empty folder 2) go out of range 3) control is retained normally but not going to main screen
                // 1) open empty folder 2) open another folder 3) control not retained and not going to main screen
                //variables.currentWindow = variables.previousCurrentWindow;
                this.Close();
            }
        }

        public void display()
        {
            keepImageNumberInsideRange();

            string imagePath = imageFolder + System.IO.Path.GetFileName( filesInFolder[imageNumber] );
            try
            {
                var uri = new Uri(imagePath);
                // if the image is found (name is correct)
                var bitmap = new BitmapImage(uri);
                image.Source = bitmap;
            }
            catch (FileNotFoundException)
            {
                notificationsWindow notificationsWindow = new notificationsWindow();
                notificationsWindow.textBlockMessage.Text = "Oops! No images in this Folder!";
                notificationsWindow.Show();

                variables.currentWindow = variables.previousCurrentWindow;
                this.Close();
            }
            catch (IndexOutOfRangeException)
            {
                notificationsWindow notificationsWindow = new notificationsWindow();
                notificationsWindow.textBlockMessage.Text = "Oops! No images in this Folder!";
                notificationsWindow.Show();

                variables.currentWindow = variables.previousCurrentWindow;
                this.Close();
            }
        }

        public void keepImageNumberInsideRange()
        {
            if (imageNumber>= numberOfFiles)
            {
                imageNumber = 0;
            }
            else if (imageNumber < 0)
            {
                imageNumber = numberOfFiles - 1;
            }
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            imageNumber += 1;
            display();
        }

        private void buttonPrevious_Click(object sender, RoutedEventArgs e)
        {
            imageNumber -= 1;
            display();
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
