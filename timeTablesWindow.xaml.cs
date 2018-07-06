using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace cseNoticeBoard
{
    /// <summary>
    /// Interaction logic for timeTablesWindow.xaml
    /// </summary>
    public partial class timeTablesWindow : Window
    {
        public static int tableNumber;
        string thisWindowextension = ".jpg";

        // will contain 3 if first years students still didn't arrived, otherwise will contain 4
        int tablesAvailable;



        public timeTablesWindow()
        {
            InitializeComponent();
            variables.setLogoImage(this, logoImage);

            setNumberOfTables();
            setTextOfButtons();

            tableNumber = 1;

            variables.prepareImageFile(tableNumber, thisWindowextension);
            variables.displayImage(this, imageTable);
        }

        private void setNumberOfTables()
        {
            // when first years students still didn't arrived, they will be no table in the control file
            if (controlFile.getFileName(4) == "")
                tablesAvailable = 3;
            else
                tablesAvailable = 4;                
        }
        private void setTextOfButtons()
        {
            Visibility hidden = System.Windows.Visibility.Hidden;

            textBlockFourthYear.Text = controlFile.getSessionNumberTimeTablesWindow(1);
            textBlockThirdYear.Text = controlFile.getSessionNumberTimeTablesWindow(2);
            textBLockSecondYear.Text = controlFile.getSessionNumberTimeTablesWindow(3);
            if (tablesAvailable == 4)
            {
                textBlockFirstYear.Text = controlFile.getSessionNumberTimeTablesWindow(4);
            }
            // means juniors still didn't arrive
            else
            {
                textBlockFirstYear.Text = "";
                textBackgroundFirstYear.Visibility = hidden;

            }

        }

        private void buttonNextAnnoucement_Click(object sender, RoutedEventArgs e)
        {          
            tableNumber += 1;

            if (tableNumber > tablesAvailable)
                tableNumber = 1;

            displayTable(tableNumber);
        }

        private void buttonPreviousAnnoucement_Click(object sender, RoutedEventArgs e)
        {
            tableNumber -= 1;
            if (tableNumber < 1)
                tableNumber = tablesAvailable;

            displayTable(tableNumber);
        }

        private void displayTable(int tableNumber)
        {

            variables.prepareImageFile(tableNumber, thisWindowextension);
            variables.displayImage(this, imageTable);

            #region maintain the blue background behnind the buttons
            hideAlltextBackgrounds();
            if (tableNumber == 1)
            {
                textBackgroundFourthYear.Opacity = 100;
            }
            else if (tableNumber == 2)
            {
                textBackgroundThirdYear.Opacity = 100;
            }
            else if (tableNumber == 3)
            {
                textBackgroundSecondYear.Opacity = 100;
            }
            else if (tableNumber == 4)
            {
                textBackgroundFirstYear.Opacity = 100;
            }
            #endregion
        }

        private void buttonTimeTable_Click(object sender, RoutedEventArgs e)
        {
            Button senderButton = sender as Button;      
            tableNumber = Convert.ToInt32(senderButton.Content);

            displayTable(tableNumber);
        }

        private void hideAlltextBackgrounds()
        {
            textBackgroundFourthYear.Opacity = 0;
            textBackgroundThirdYear.Opacity = 0;
            textBackgroundSecondYear.Opacity = 0;
            textBackgroundFirstYear.Opacity = 0;
        }

        private void buttonEnlarge_Click(object sender, RoutedEventArgs e)
        {
            variables.prepareImageFile(tableNumber, thisWindowextension);
            imageWindow imageWindow = new imageWindow();
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

        //public void generalButton_MouseEnter(object sender, MouseEventArgs e)
        //{
        //    MainWindow.mouserHoverTimer.Start();
        //    MainWindow.hoveredButton = ((Button)sender);

        //}

        //public void generalButton_MouseLeave(object sender, MouseEventArgs e)
        //{
        //    MainWindow.hoveredButton.RaiseEvent(new RoutedEventArgs(ButtonBase.ClickEvent));
        //    MainWindow.mouserHoverTimer.Stop();
        //}

    }
}
