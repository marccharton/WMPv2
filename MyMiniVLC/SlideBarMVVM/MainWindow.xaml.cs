﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using wmp2;
using Microsoft.Win32;

namespace SlideBarMVVM
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void WMPWindow_Initialized(object sender, EventArgs e)
        {
            MessageBox.Show("Coucou INIT");
        }

        private void WMPWindow_Loaded(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Coucou LOAD");
        }
    }
}
