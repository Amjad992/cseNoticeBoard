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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Kinect;
using Coding4Fun.Kinect.Wpf;
using Coding4Fun.Kinect;
using System.Runtime.InteropServices;
using System.Windows.Controls.Primitives;

// change the counter of dealy before starting the application
// change the window loaded if disconnected
// change timer of the mouse stuck and hover
// change the angle of the device

// I shortend the activation and deactivation period, test or return to 2.5 sec in notification window
namespace cseNoticeBoard
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Actual notice board app
        //object thisObject;
        ///////// -------- Variables related to the announcements slider -------- /////////
        // this will be used to change the fill property of an ellipse with the changing of announcement in slider
        Ellipse currentSelected;
        int currentAnnouncementIndex;
        public static DispatcherTimer annoucementTimeInterval = new DispatcherTimer();
        ///////// ---------------------------------------------------------------------- /////////
        #endregion

        #region Controlling mouse through Microsoft Kinect parts

        #region DLL Files Import

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        static extern bool SetCursorPos(int x, int y);

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_MIDDLEDOWN = 0x0020;
        private const int MOUSEEVENTF_MIDDLEUP = 0x0040;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out POINT lpPoint);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int X;
            public int Y;

            public POINT(int x, int y)
            {
                this.X = x;
                this.Y = y;
            }
        }

        #endregion

        #region Kinect Variables
        private KinectSensor sensor;
        private Skeleton[] skeletons = null;
        private int skeletonId = -1;
        private Skeleton activeSkeleton;


        private bool clickedAlready = false;
        private bool skeletonSelected = false;
        DispatcherTimer kinectTimer = new DispatcherTimer();
        DispatcherTimer mouseStuckTimer = new DispatcherTimer();
        DispatcherTimer programStuckTimer = new DispatcherTimer();
        DispatcherTimer clickingTimer = new DispatcherTimer();

        private bool justDeactivated = false;
        // if I wanted not to close the window when someone crosses
        //private bool someoneCrossedInFrontOfUser = false;

        double minX = -0.39;    //-0.27 // -0.39
        double maxX = 0.45;     // 0.33
        double minZ = 0.8;  // 0.8
        double maxZ = 2;    // 1.6

        // mouser variables
        int mouseX, mouseY;

        // to check when mouse stuck
        int stuckMouseX, stuckMouseY;

        // will contain the position of mouse, if after sometime (clickingTimer) it didn't go far from this position
        // then a click will be activated
        // basically a click will happen if a user stopped the cursor for sometime
        int clickMouseX, clickMouseY;

        Joint ScaledJoint;

        


        Point rightHandPoint;
        Point leftHandPoint;
        Point leftElbowPoint;
        

        #endregion



        #endregion

        public MainWindow()
        {
            InitializeComponent();

            setWindowVariables();
        }

        #region Actual notice board app

        public void setWindowVariables()
        {
            variables.currentWindow = this;

            variables.setLogoImage(this, logoImage);

            // set the attention image
            var uri = new Uri(variables.appResourcesPath + "\\attention.png");
            var bitmap = new BitmapImage(uri);
            attentionImage.Source = bitmap;


            // prepare the control file and its path to be assigned to values in variables class
            // needed for announcement class
            variables.setResourcesPath("announcements\\");
            variables.prepareWindowControlFile();

            ///////// -------- realted to the announcements slider -------- /////////
            currentSelected = ellipse1;
            currentSelected.Fill = new SolidColorBrush(Colors.Blue);

            currentAnnouncementIndex = 1;
            displayAnnouncementSummary();

            annoucementTimeInterval.Interval = TimeSpan.FromMilliseconds(5000);
            annoucementTimeInterval.Tick += announcementTimeTick;
            annoucementTimeInterval.Start();
            ///////// ---------------------------------------------------- /////////
        }
        private void announcementTimeTick(object sender, EventArgs e)
        {
            // the priority index of the announcement ordered by its line in the file
            if (currentAnnouncementIndex < 7)
                currentAnnouncementIndex += 1;
            else
                currentAnnouncementIndex = 1;
            displayAnnouncementSummary();
        }
        private void displayAnnouncementSummary ()
        {

            // some properties value to be used later for the small elipses changes during slider
            SolidColorBrush blue = new SolidColorBrush(Colors.Blue);
            SolidColorBrush transparent = new SolidColorBrush(Colors.Transparent);
            Visibility visible = System.Windows.Visibility.Visible;
            Visibility hidden = System.Windows.Visibility.Hidden;

            // if a line has no data ellipse will be hidden
            #region hide ellipses that are not needed
            if (controlFile.getFileDescriptionMainWindow(1) != "") { ellipse1.Visibility = visible; }
            else { ellipse1.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(2) != "") { ellipse2.Visibility = visible; }
            else { ellipse2.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(3) != "") { ellipse3.Visibility = visible; }
            else { ellipse3.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(4) != "") { ellipse4.Visibility = visible; }
            else { ellipse4.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(5) != "") { ellipse5.Visibility = visible; }
            else { ellipse5.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(6) != "") { ellipse6.Visibility = visible; }
            else { ellipse6.Visibility = hidden; }
            if (controlFile.getFileDescriptionMainWindow(7) != "") { ellipse7.Visibility = visible; }
            else { ellipse7.Visibility = hidden; }
            #endregion

            // the main functionality of the ellipse, only one instance will be explained and rest follow same paradigm
            #region fill only the ellipse corresponding to announcement index
            // if the current announcement line is 1 then it should be displayed if present in file
            // else make the counter equal zero
            if (currentAnnouncementIndex == 1)
            {
                // if an ellipse is visible that means that announcement line is present, that was checked in
                // previous block when we set up the value of visibility
                if (ellipse1.Visibility == visible)
                {
                    // make current as visible which is still the previous ecllipse
                    currentSelected.Fill = transparent;
                    // since we reach here, that means there is announcement so display its summary
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(1);
                    // change the current ecllipse to reflect the index ecllipse then fill it with blue
                    currentSelected = ellipse1;
                    currentSelected.Fill = blue;
                }
                else
                    // if it is not visible then no announcement line, means no point to keep incrementing
                    // the counter
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 2)
            {
                if (ellipse2.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(2);
                    currentSelected = ellipse2;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 3)
            {
                if (ellipse3.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(3);
                    currentSelected = ellipse3;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 4)
            {
                if (ellipse4.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(4);
                    currentSelected = ellipse4;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 5)
            {
                if (ellipse5.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(5);
                    currentSelected = ellipse5;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 6)
            {
                if (ellipse6.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(6);
                    currentSelected = ellipse6;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            else if (currentAnnouncementIndex == 7)
            {
                if (ellipse7.Visibility == visible)
                {
                    currentSelected.Fill = transparent;
                    textBlockAnnouncementSlider.Text = controlFile.getFileDescriptionMainWindow(7);
                    currentSelected = ellipse7;
                    currentSelected.Fill = blue;
                }
                else
                    currentAnnouncementIndex = 0;
            }
            #endregion

        }
        private void buttonReadMore_Click(object sender, RoutedEventArgs e)
        {
            AnnouncementsWindow.lineNumber = currentAnnouncementIndex;
            variables.goToAnnouncementFromSlide = true;

            variables.openPage("announcements");
        }
        private void buttonNextAnnoucement_Click(object sender, RoutedEventArgs e)
        {
            annoucementTimeInterval.Stop();

            if (currentAnnouncementIndex < 7)
                currentAnnouncementIndex += 1;
            else
                currentAnnouncementIndex = 1;

            displayAnnouncementSummary();

            annoucementTimeInterval.Start();
        }
        private void buttonPreviousAnnoucement_Click(object sender, RoutedEventArgs e)
        {
            annoucementTimeInterval.Stop();

            if (currentAnnouncementIndex > 1)
                currentAnnouncementIndex -= 1;
            else
                currentAnnouncementIndex = 7;

            displayAnnouncementSummary();

            annoucementTimeInterval.Start();

        }
        public static void correctAnnouncementSliderContent ()
        {
            annoucementTimeInterval.Stop();
            annoucementTimeInterval.Start();

        }

        #region moving between windows

        private void buttonMain_Click(object sender, RoutedEventArgs e)
        {
        }
        private void buttonAnnouncements_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("announcements");
        }
        private void buttonTeachersInfo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("teachersInfo");
        }
        private void buttonTimeTables_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("timeTables");
        }
        private void buttonDepartmentInformation_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("departmentInformation");
        }
        private void buttonEventsPictures_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("eventsPictures");
        }
        private void buttonCredits_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.annoucementTimeInterval.Stop();

            variables.openPage("credits");
        }
        #endregion

        #endregion

        #region Controlling mouse through Microsoft Kinect parts

        #region Window Loaded Event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Kinect Part////////////////////////////////////////////////////////////////////
            // Check if Kinect is connected to the system or not

            // Timer to stop Tracking for a second when deactivating the control mode
            kinectTimer.Interval = TimeSpan.FromSeconds(1);
            kinectTimer.Tick += timeTick;

            // To deal with the event of someone crossing and the detection stuck with the sign of mouse stuck
            // retain control in 2.5 seconds
            mouseStuckTimer.Interval = TimeSpan.FromMilliseconds(2500);
            mouseStuckTimer.Tick += mouseStuckTimerTimeTick;

            // when a cursor is in the same area for sometime that means the hand is stopped and a click should be activated 
            clickingTimer.Interval = TimeSpan.FromSeconds(1);
            clickingTimer.Tick += clickingTimerTimeTick;


            // To deal with the event of the program stucking, when no change in control mode happens for 3 minutes
            // then restart
            programStuckTimer.Interval = TimeSpan.FromMinutes(3);
            programStuckTimer.Tick += programStuckTimerTimeTick;
            // it has to be started at the first run
            programStuckTimer.Start();



            turnKinectOn();
            //////////////////////////////////////////////////////////////////////////////////
        }
        private void turnKinectOn ()
        {
            if (KinectSensor.KinectSensors.Count > 0)
            {
                this.sensor = KinectSensor.KinectSensors[0];
                this.StartSensor();
            }
            else
            {
                MessageBox.Show("No Kinect sensor is connected with system! Please inform server room!");

                // Un-comment this if you plan to stop it if Kinect is not connected
                //Window_Loaded(this, e);
                //this.Close();
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            turnKinectOff();
        }
        private void turnKinectOff ()
        {
            this.StopSensor();
        }
        #endregion

        #region Kinect Functions
        private void StartSensor()
        {
            if (this.sensor != null && !this.sensor.IsRunning)
            {
                this.sensor.Start();

                var parameters = new TransformSmoothParameters
                {
                    Smoothing = 0.5f,
                    Correction = 0.3f, // 0.5f
                    Prediction = 0.5f,
                    JitterRadius = 0.05f,
                    MaxDeviationRadius = 0.04f
                };

                // tilting the device when used in the department to calibrate it
                // I don't need this
                //this.sensor.ElevationAngle = 0;

                this.sensor.SkeletonStream.Enable(parameters);
                this.sensor.SkeletonFrameReady += sensor_SkeletonFrameReady;
            }
        }
        private void StopSensor()
        {
            if (this.sensor != null && this.sensor.IsRunning)
            {
                this.sensor.Stop();
            }
        }
        private void sensor_SkeletonFrameReady(object sender, SkeletonFrameReadyEventArgs e)
        {
            // If controller was deactivated less than one second before, it won't track any thing
            if (justDeactivated == true)
            {
                return;
            }

            using (SkeletonFrame skeletonFrame = e.OpenSkeletonFrame())
            {
                // if the frame has data then copy to skeletons array
                if (skeletonFrame != null)
                {
                    skeletons = new Skeleton[skeletonFrame.SkeletonArrayLength];
                    skeletonFrame.CopySkeletonDataTo(skeletons);
                }
                // if skeletons array is null then go out
                if (skeletons == null)
                    return;


                // if a skeleton is already given control then ignore others
                if (skeletonSelected)
                {
                    foreach (Skeleton skeleton in skeletons)
                    {
                        // set the activeSkeleton as the one that holds the skeletonId stored previously
                        if (skeleton.TrackingId == skeletonId)
                        {
                            activeSkeleton = skeleton;
                        }
                    }                 

                    // if the skeleton went out of range then they will lose his status as active
                    if (activeSkeleton.Position.X < minX || activeSkeleton.Position.X > maxX
                        || activeSkeleton.Position.Z < minZ || activeSkeleton.Position.Z > maxZ)
                    {
                        deactivateActions();

                        return;
                    }


                    leftHandPoint = this.ScalePosition(activeSkeleton.Joints[JointType.HandLeft].Position);
                    leftElbowPoint = this.ScalePosition(activeSkeleton.Joints[JointType.ElbowLeft].Position);
                    rightHandPoint = this.ScalePosition(activeSkeleton.Joints[JointType.HandRight].Position);


                    // move mouse
                    actionWhenTracked();

                    // This is to deal with the event of someone crossing in front of the sensor when you are using
                    if (activeSkeleton.TrackingState == SkeletonTrackingState.NotTracked || 
                        activeSkeleton.Joints[JointType.Spine].TrackingState == JointTrackingState.NotTracked )
                    {
                        deactivateActions();

                        // if I wanted not to close the window when someone crosses
                        //someoneCrossedInFrontOfUser = true;
                    }


                    if (activeSkeleton == null)
                    {
                        deactivateActions();
                    }

                }

                // if no user is active then go through all skeletons to detect new users
                else
                {
                    foreach (Skeleton skeleton in skeletons)
                    {

                        // skeleton not tracked then do nothing
                        if (skeleton.TrackingState == SkeletonTrackingState.NotTracked) ;
                        // skeleton not in a specific range in terms of x and z, then ignore user and return
                        else if (skeleton.Position.X < minX || skeleton.Position.X > maxX
                            || skeleton.Position.Z < minZ || skeleton.Position.Z > maxZ)
                        {
                            skeleton.TrackingState = SkeletonTrackingState.PositionOnly;
                        }
                        else
                        {
                            // if you reach this point, the person is standing a certain distance from the Kinect.  
                            // Not too close, not too far away and he is also in front of sensor.

                            // mark him as a tracked skeleton to be detected and interacted with
                            skeleton.TrackingState = SkeletonTrackingState.Tracked;

                            //// Automatic activation as long as you are in the activation position
                            skeletonSelected = true;
                            skeletonId = skeleton.TrackingId;
                            activateActions();

                        }
                    }
                }
            }
        }
        private void actionWhenTracked()
        {
            this.moveCursor();

            //if (clickedAlready == false && this.isItClick())
            //{
            //    clickedAlready = true;

            //    LeftMouseHold();
            //    LeftMouseRelease();

            //}
            //else 
            if (clickedAlready == true && this.isItRelease())
            {
                clickedAlready = false;
            }
        }
        private void moveCursor()
        {
            ScaledJoint = activeSkeleton.Joints[JointType.HandRight].ScaleTo(1280, 960, .25f, .25f);

            mouseX = Convert.ToInt32(ScaledJoint.Position.X);
            mouseY = Convert.ToInt32(ScaledJoint.Position.Y);

            SetCursorPos(mouseX, mouseY);

            System.Threading.Thread.Sleep(10);
        }
        private bool isItClick()
        {
            if (leftHandPoint.Y < leftElbowPoint.Y)
                return true;
            else
                return false;
        }
        private bool isItRelease()
        {
            if (leftHandPoint.Y < leftElbowPoint.Y)
                return false;
            else
                return true;
        }
        private Point ScalePosition(SkeletonPoint skeletonPoint)
        {
            DepthImagePoint depthPoint = this.sensor.CoordinateMapper.MapSkeletonPointToDepthPoint(skeletonPoint,
                DepthImageFormat.Resolution80x60Fps30);
            return new Point(depthPoint.X, depthPoint.Y);
        }
        private void LeftMouseHold()
        {
            mouse_event(MOUSEEVENTF_LEFTDOWN, mouseX, mouseY, 0, 0);
        }
        private void LeftMouseRelease()
        {
            mouse_event(MOUSEEVENTF_LEFTUP, mouseX, mouseY, 0, 0);
        }

        private void activateActions ()
        {

            // For Instructions
            //firstLineOfInstruction.Content = "- Move Mouse with your right hand!";
            //secondLineOfInstruction.Content = "- Click with your left arm (slowly rise it up then  down)!";
            //thirdLineOfInstruction.Content = "- If mouse shakes, Change position (slightly left or right)!";

            notificationsWindow notificationsWindow = new notificationsWindow();
            notificationsWindow.textBlockMessage.Text = "Control activated. Read instruction Please!";
            notificationsWindow.ShowDialog();

            // this timer to get out of a stuck if the detection stopped (indicated by no movement of cursor)
            mouseStuckTimer.Start();
            stuckMouseX = mouseX;
            stuckMouseY = mouseY;

            // this timer to click if the cursor stopped for sometime
            clickingTimer.Start();
            clickMouseX = mouseX;
            clickMouseY = mouseY;

            // since a control change happened under the specified time, then just restart the timer, waiting for next change
            programStuckTimer.Stop();
            programStuckTimer.Start();
        }

        private void deactivateActions ()
        {
            justDeactivated = true;
            skeletonId = -1;
            skeletonSelected = false;
            kinectTimer.Start();


            // For Instructions
            //firstLineOfInstruction.Content = "- Stand in the box drawn on the ground to control!";
            //secondLineOfInstruction.Content = "- Only one user at a time!";
            //thirdLineOfInstruction.Content = "- If no response, Please inform server room!";


            notificationsWindow notificationsWindow = new notificationsWindow();
            notificationsWindow.textBlockMessage.Text = "Control De-Activated. Thanks for using!";

            //// if deactivation was due to going out of box and not some guy crossing
            //// then close all previous windows, set cursor on corner, and intilize the main window again
            //if (someoneCrossedInFrontOfUser == false)
            //{
                if (variables.previousCurrentWindow != null)
                    variables.previousCurrentWindow.Close();
                if (variables.currentWindow != this)
                    variables.currentWindow.Close();

                SetCursorPos(0, 0);
                this.InitializeComponent();
                setWindowVariables();
            //}          

            notificationsWindow.ShowDialog();           

            mouseStuckTimer.Stop();


            // since a control change happened under the specified time, then just restart the timer, waiting for next change
            programStuckTimer.Stop();
            programStuckTimer.Start();
        }

        private void timeTick(object sender, EventArgs e)
        {
            justDeactivated = false;
            kinectTimer.Stop();
        }

        private void mouseStuckTimerTimeTick(object sender, EventArgs e)
        {
            if (mouseX == stuckMouseX && mouseY == stuckMouseY)
            {
                deactivateActions();

                // if I wanted not to close the window when someone crosses
                //someoneCrossedInFrontOfUser = true;
            }
            else
            {
                stuckMouseX = mouseX;
                stuckMouseY = mouseY;
            }

        }

        private void clickingTimerTimeTick(object sender, EventArgs e)
        {
            int differenceInX = System.Math.Abs(mouseX - clickMouseX) ;
            int differenceInY = System.Math.Abs(mouseY - clickMouseY) ;

            int rangeOfX = 60;
            int rangeOfY = 60;

            if ( differenceInX < rangeOfX && differenceInY < rangeOfY)
            {
                clickedAlready = true;

                LeftMouseHold();
                LeftMouseRelease();

                clickingTimer.Stop();
                clickingTimer.Start();
            }
            else
            {
                clickMouseX = mouseX;
                clickMouseY = mouseY;
            }

        }

        private void programStuckTimerTimeTick(object sender, EventArgs e)
        {
            restartApplication();
        }

        private void restartApplication()
        {
            System.Windows.Forms.Application.Restart();

            System.Windows.Application.Current.Shutdown();
        }

        #endregion


        #endregion
    }
}
