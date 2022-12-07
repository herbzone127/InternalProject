using ideaForge.ViewModels;
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
    public partial class CompletedRidePage : UserControl
    {
        public IdeaForge.Domain.Ride _ride;
        public CompletedRidePage(IdeaForge.Domain.Ride rideDetails)
        {
            InitializeComponent();
            this.DataContext = new  CompleteRideViewModel();
            var vModel = (CompleteRideViewModel)DataContext;
            vModel.MissionName = rideDetails.MissionName;
            double totalHours = (rideDetails.endDate - rideDetails.startDate).TotalHours;
            vModel.TotalRequestedTime = Math.Round(totalHours, 2); ;
            vModel.FlightDate = rideDetails.startDate.ToString("dd/MM/yyyy");
            vModel.FlightTime = rideDetails.startDate.ToString("hh:mm tt").ToLower() + " - " + rideDetails.endDate.ToString("hh:mm tt").ToLower();//For Time
            vModel.StatusForUser = rideDetails.status;
            vModel.UserFeedBack = (string)rideDetails.comments;
            vModel.UAVId = rideDetails.UAVID;
            vModel.Latitude = rideDetails.originLatitude;
            vModel.Longitude = rideDetails.originLongitude;
            vModel.RideById = rideDetails;
            vModel.GetUserFeedback(rideDetails.id).ConfigureAwait(false);
            vModel.GetPilotFeeback(rideDetails.id).ConfigureAwait(false);
            vModel.GetFlightFeedback(rideDetails.id).ConfigureAwait(false);
            _ride =rideDetails;
            //vModel.ControlKey = rideDetails.ControlKey;
            //vModel.SecretKey = rideDetails.SecretKey;
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
