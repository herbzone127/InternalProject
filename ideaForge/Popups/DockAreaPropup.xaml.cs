﻿using System;
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

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for DockAreaPropup.xaml
    /// </summary>
    public partial class DockAreaPopup : Window
    {
        public DockAreaPopup()
        {
            InitializeComponent();
           /// new ErrorMessageBox("").ShowDialog();
        }

        public void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {

            Application.Current.MainWindow = new Dashboard();
            Application.Current.MainWindow.Show();
            this.Close();
            var window = (Login)Application.Current.Windows[0];
            window.Close();

        }

        
    }
}
