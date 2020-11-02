﻿using ShakeShack_Kiosk.Controls;
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

namespace ShakeShack_Kiosk
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            titleControl.OnNavigateToHome += TitleControl_OnNavigateToHome; ;
        }

        private void TitleControl_OnNavigateToHome(object sender, EventArgs e)
        {
            while (frPage.CanGoBack)
            {
                frPage.GoBack();
            }
        }
    }
}