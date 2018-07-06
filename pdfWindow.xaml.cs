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
    /// Interaction logic for pdfWindow.xaml
    /// </summary>
    public partial class pdfWindow : Window
    {
        public pdfWindow()
        {
            InitializeComponent();

            Visibility hidden = System.Windows.Visibility.Hidden;
            Visibility visible = System.Windows.Visibility.Visible;

            if (variables.chairmanMessageDisplayed)
            {
                canvasGoToMaps.Visibility = hidden;
                textGoToMaps.Visibility = hidden;
                buttonMap.Visibility = hidden;

                // reset this variable for future use
                variables.chairmanMessageDisplayed = false;
            }
            else
            {
                canvasGoToMaps.Visibility = visible;
                textGoToMaps.Visibility = visible;
                buttonMap.Visibility = visible;
            }



            var uc = new userControlPdf(variables.pdfPath);
            this.windowsFormHostPdf.Child = uc;
        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            // if true then closing will take us to main window and for that control file and timer should be setted
            if (variables.chairmanMessageDisplayed == true)
            {
                variables.setResourcesPath("announcements\\");
                variables.prepareWindowControlFile();

                MainWindow.annoucementTimeInterval.Start();

            }
            this.Close();
        }

        private void buttonMap_Click(object sender, RoutedEventArgs e)
        {
            variables.teacherInfoWindow.Close();
            variables.openPage("departmentInformation");
            this.Close();

        }
    }
}
