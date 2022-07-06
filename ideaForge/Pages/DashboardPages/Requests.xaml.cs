using ideaForge.ViewModels;
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
                dashboard.statusBorder.Visibility = Visibility.Visible;
                dashboard.statusBorder.Background = ConvertColor("#FFF4DB");
                //dashboard.Header.
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                bitmap.EndInit();
                dashboard.statusImage.Source = bitmap; 
                dashboard.statusLabel.Content = "Pending";
                dashboard.statusBorder.BorderBrush = ConvertColor("#FFF4DB");
                context.PageName = $"Booking Id:{selectedRecord.id}";
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

        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
    }
}
