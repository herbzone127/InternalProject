﻿using ideaForge.ViewModels;
using IdeaForge.Domain;
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

namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : UserControl
    {
      
        public Requests()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }

        private void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = (DataGrid)sender;
            var selectedRecord = (RequestData)grid.CurrentItem;
           
            Window parentWindow = Window.GetWindow(this);
            var dashboard = (Dashboard)parentWindow;
            var context = (DashboardViewModel)dashboard.DataContext;
            if (selectedRecord.statusID.Equals(1))
            {
              
                context.CurrentPage.Content = new PendingRidePage();
            }
            if (selectedRecord.statusID.Equals(2))
            {

                context.CurrentPage.Content = new OnGoingRidePage();
            }

            if (selectedRecord.statusID.Equals(5))
            {

                context.CurrentPage.Content = new CompletedRidePage();
            }


        }
    }
}
