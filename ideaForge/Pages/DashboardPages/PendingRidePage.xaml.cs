using ideaForge.ViewModels;
using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    /// Interaction logic for CompletedRidePage.xaml
    /// </summary>
    public partial class PendingRidePage : UserControl
    {
        public PendingRidePage(Ride ride)
        {
            InitializeComponent();
            this.DataContext = new PendingRideViewModel();
            var vModel = (PendingRideViewModel)DataContext;
            vModel.RideById = ride;
            vModel.MissionName = ride.MissionName;
            double totalHours = (ride.endDate - ride.startDate).TotalHours;
            vModel.TotalRequestedTime1 = Math.Round(totalHours, 2); ;
            vModel.FlightDate1 = ride.startDate.ToString("dd/MM/yyyy");
            vModel.FlightTime1 = ride.startDate.ToString("hh:mm tt") + "-" + ride.endDate.ToString("hh:mm tt");//For Time
            vModel.Status1 = ride.status;
            vModel.StatusForUser1 = ride.status;
            vModel.Latitude1 = ride.originLatitude;
            vModel.Longitude1 = ride.originLongitude;
        }

        private void AutoCompleteComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnLongitude_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            Clipboard.SetText(txtLongitude.Text);
            obj.Background = new ImageBrush(GetImage("acceptIcon"));
            ///Images/CopyTextIcon.png
            btnLatitude.Background = new ImageBrush(GetImage("CopyTextIcon"));
        }

        private void btnLatitude_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            Clipboard.SetText(txtLatitude.Text);
            obj.Background = new ImageBrush(GetImage("acceptIcon"));
            btnLongitude.Background = new ImageBrush(GetImage("CopyTextIcon"));
        }
        BitmapImage GetImage(string path)
        {
            BitmapImage bit = new BitmapImage(new Uri(string.Format("pack://application:,,,/{0};component/Images/" + path + ".png", Assembly.GetExecutingAssembly().GetName().Name)));
            return bit;
        }
    }
}
