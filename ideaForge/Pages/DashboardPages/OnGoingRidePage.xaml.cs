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
    public partial class OnGoingRidePage : UserControl
    {
        public OnGoingRidePage(IdeaForge.Domain.Ride userData)
        {
            
     
            InitializeComponent();
            var vModel = (OnGoingRideViewModel)DataContext;
            vModel.RideById = userData;
            vModel.MissionName = userData.MissionName;
            double totalHours = (userData.endDate - userData.startDate).TotalHours;
            vModel.TotalRequestedTime1 = Math.Round(totalHours, 2); ;
            vModel.FlightDate1 = userData.startDate.ToString("dd/MM/yyyy");
           
            vModel.FlightTime1 = userData.startDate.ToString("hh:mm tt")+" - "+userData.endDate.ToString("hh:mm tt");//For Time
            vModel.StatusForUser1 = userData.status;
            vModel.Latitude1 = userData.originLatitude;
            vModel.Longtitude1 = userData.originLongitude;
            vModel.UAV1 = userData.UAVID;
            vModel.ControlKey1 = userData.ControlKey;
            vModel.SecretKey1 = userData.SecretKey;


        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            Clipboard.SetText(txt_Lattitude1.Text);
            //obj.Background = new ImageBrush(new BitmapImage(new Uri(@"/Images/acceptIcon.png", UriKind.Relative)));
            obj.Background = new ImageBrush(GEtIamge("acceptIcon"));
            //btnLongitude.Background = new ImageBrush(new BitmapImage(new Uri(@"/Images/CopyTextIcon.png", UriKind.Relative)));
            btnLongitude.Background = new ImageBrush(GEtIamge("CopyTextIcon"));
        }

        private void btnLongitude_Click(object sender, RoutedEventArgs e)
        {
            var obj = sender as Button;
            Clipboard.SetText(txtLongitude.Text);
            obj.Background = new ImageBrush(GEtIamge("acceptIcon"));
            ///Images/CopyTextIcon.png
             btnLatitude.Background = new ImageBrush(GEtIamge("CopyTextIcon"));
        }
        BitmapImage GEtIamge(string path)
        {
            BitmapImage bit = new BitmapImage(new Uri(string.Format("pack://application:,,,/{0};component/Images/" + path + ".png", Assembly.GetExecutingAssembly().GetName().Name)));
            return bit;
        }
    }
}
