﻿using System;
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
    /// Interaction logic for imageWindow.xaml
    /// </summary>
    public partial class imageWindow : Window
    {


        public imageWindow()
        {
            InitializeComponent();

            variables.displayImage(this, image);

        }

        private void buttonGoBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();

        }
    }
}
