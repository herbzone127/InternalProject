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
    /// Interaction logic for CompletedRidePage.xaml
    /// </summary>
    public partial class PendingRidePage : UserControl
    {
        public PendingRidePage(RideById ride)
        {
            InitializeComponent();
            this.DataContext = new PendingRideViewModel();
            var vModel = (PendingRideViewModel)DataContext;
            vModel.MissionType = ride.missionType;
            double totalHours = (ride.endDate - ride.startDate).TotalHours;
            vModel.TotalRequestedTime1 = Math.Round(totalHours, 2); ;
            vModel.FlightDate1 = ride.startDate.ToShortDateString();

            vModel.FlightTime1 = ride.startDate.ToString("hh:mm:ss tt") + "-" + ride.endDate.ToString("hh:mm:ss tt");//For Time
            vModel.Status1 = ride.status;
            vModel.StatusForUser1 = ride.status;
            vModel.Latitude1 = ride.latitude;
            vModel.Longitude1 = ride.longitude;
        }
    }
}
