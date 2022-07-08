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
    public partial class OnGoingRidePage : UserControl
    {
        public OnGoingRidePage(IdeaForge.Domain.RideById userData)
        {
            
     
            InitializeComponent();
            var vModel = (OnGoingRideViewModel)DataContext;
            vModel.MissionName = userData.missionName;
            double totalHours = (userData.endDate - userData.startDate).TotalHours;
            vModel.TotalRequestedTime1 = Math.Round(totalHours, 2); ;
            vModel.FlightDate1 = userData.startDate.ToShortDateString();

            vModel.FlightTime1 = userData.startDate.ToString("hh:mm:ss tt")+"-"+userData.endDate.ToString("hh:mm:ss tt");//For Time
            vModel.StatusForUser1 = userData.status;
            vModel.Latitude1 = userData.latitude;
            vModel.Longtitude1 = userData.longitude;
            //vModel.UAV1 = userData.UAVID;
            //vModel.ControlKey1 = userData.ControlKey;
            //vModel.SecretKey1 = userData.SecretKey;


        }
    }
}
