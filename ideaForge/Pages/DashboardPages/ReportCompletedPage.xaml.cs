using ideaForge.ViewModels;
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
    /// Interaction logic for CompletedRidePage.xaml
    /// </summary>
    public partial class ReportCompletedPage : UserControl
    {
        public IdeaForge.Domain.Ride _ride;
        public ReportCompletedPage(IdeaForge.Domain.Ride rideDetails)
        {
            InitializeComponent();
            //this.DataContext = new  CompleteRideViewModel();
            //var vModel = (CompleteRideViewModel)DataContext;
            this.DataContext = new ReportCompleteViewModel();
            var vModel=(ReportCompleteViewModel)DataContext;
            vModel.MissionName = rideDetails.MissionName;
            double totalHours = (rideDetails.endDate - rideDetails.startDate).TotalHours;
            vModel.TotalRequestedTime = Math.Round(totalHours, 2); ;
            vModel.FlightDate = rideDetails.startDate.ToString("dd/MM/yyyy hh:mm:ss tt");
            vModel.FlightTime = rideDetails.startDate.ToString("hh:mm:ss tt") + "-" + rideDetails.endDate.ToString("hh:mm:ss tt");//For Time
            vModel.StatusForUser = rideDetails.status;
            vModel.UserFeedBack = (string)rideDetails.comments;
            vModel.UAVId = rideDetails.UAVID;
            vModel.GetUserFeedBack(rideDetails.id).ConfigureAwait(false);
            vModel.GetPioltFeedBack(rideDetails.id).ConfigureAwait(false);
            _ride = rideDetails;
            //vModel.ControlKey = rideDetails.ControlKey;
            //vModel.SecretKey = rideDetails.SecretKey;
        }
    }
}
