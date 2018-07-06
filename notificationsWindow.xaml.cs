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
using System.Windows.Threading;

namespace cseNoticeBoard
{
    /// <summary>
    /// Interaction logic for notificationsWindow.xaml
    /// </summary>
    public partial class notificationsWindow : Window
    {
        private DispatcherTimer closingTimeInterval = new DispatcherTimer();

        public notificationsWindow()
        {
            InitializeComponent();

            closingTimeInterval.Interval = TimeSpan.FromMilliseconds(1000);
            closingTimeInterval.Tick += closingTimeTick;
            closingTimeInterval.Start();


        }

        private void closingTimeTick(object sender, EventArgs e)
        {
            closingTimeInterval.Stop();
            closingTimeInterval.Tick -= closingTimeTick;
            this.Close();

        }
    }
}
