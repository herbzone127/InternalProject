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
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                //bitmap.EndInit();
                //dashboard.statusImage.Source = bitmap;
                dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                dashboard.statusLabel.Content = "Pending";
                dashboard.statusLabel.Foreground = ConvertColor("#FFC540");
                dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                dashboard.statusBorder.BorderBrush = ConvertColor("#FFC540");
                context.PageName = $"Booking Id:{selectedRecord.id}";
                context.CurrentPage.Content = new PendingRidePage();
                dashboard.backButton.Visibility = Visibility.Visible;
            }
            if (selectedRecord.statusID.Equals(2))
            {
                dashboard.statusBorder.Visibility = Visibility.Visible;
                dashboard.statusBorder.Background = ConvertColor("#FFF3D9");
                //dashboard.Header.
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                //bitmap.EndInit();
                //dashboard.statusImage.Source = bitmap;
                dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                dashboard.statusLabel.Content = "Ongoing";
                dashboard.statusLabel.Foreground = ConvertColor("#F98926");
                dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                dashboard.statusBorder.BorderBrush = ConvertColor("#F98926");
                context.PageName = $"Booking Id:{selectedRecord.id}";
                context.CurrentPage.Content = new OnGoingRidePage();
                dashboard.backButton.Visibility = Visibility.Visible;
            }

            if (selectedRecord.statusID.Equals(5))
            {
                dashboard.statusBorder.Visibility = Visibility.Visible;
                dashboard.statusBorder.Background = ConvertColor("#DEECFF");
                //dashboard.Header.
                //BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                //bitmap.EndInit();
                //dashboard.statusImage.Source = bitmap;
                dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                dashboard.statusLabel.Content = "Completed";
                dashboard.statusLabel.Foreground = ConvertColor("#3398D8");
                dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                dashboard.statusBorder.BorderBrush = ConvertColor("#3398D8");
                context.PageName = $"Booking Id:{selectedRecord.id}";
                context.CurrentPage.Content = new OnGoingRidePage();
                context.CurrentPage.Content = new CompletedRidePage();
                dashboard.backButton.Visibility = Visibility.Visible;
            }


        }

        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
        private CornerRadius ConvertBorderRadius(string radius)
        {
            var converter = new System.Windows.CornerRadiusConverter();
            var result = (CornerRadius)converter.ConvertFromString(radius);
            return result;
        }
        private Thickness ConvertBorderThickness(string border)
        {
            var converter = new System.Windows.ThicknessConverter();
            var result = (Thickness)converter.ConvertFromString(border);
            return result;
        }

    }
}
