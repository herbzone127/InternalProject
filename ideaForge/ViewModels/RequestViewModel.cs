using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ideaForge.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        /// <summary>
        /// Services
        /// </summary>
        #region Services
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();

     

        #endregion


        public RequestViewModel()
        {
            GetTodaysRequest("");
            GetAllRequest("");

        }
        #region ObservableCollections
        private ObservableCollection<RequestData> _todaysRequests;

        public ObservableCollection<RequestData> TodaysRequests
        {
            get { return _todaysRequests; }
            set
            {
                _todaysRequests = value;
                OnPropertyChanged(nameof(TodaysRequests));
               
            }
        }

        private void OnTodayRequestSelect(RequestData todayRequest)
        {
            Application.Current.Properties["todayRequest"] = todayRequest;

        }

        private ObservableCollection<RequestData> _allRequest;

        public ObservableCollection<RequestData> AllRequests
        {
            get { return _allRequest; }
            set
            {
                _allRequest = value;
                OnPropertyChanged(nameof(RequestData));
            }
        }

        private ObservableCollection<RideById> _rideById;

        public ObservableCollection<RideById> RideById
        {
            get { return _rideById; }
            set
            {
                _rideById = value;
                OnPropertyChanged(nameof(RideById));
            }
        }
        #endregion

        #region Properties
        private RequestData _todayRequest;

        public RequestData TodayRequest
        {
            get { return _todayRequest; }
            set { _todayRequest = value;
                OnPropertyChanged(nameof(TodayRequest));
                OnTodayRequestSelect(TodayRequest);
            }
        }
        #endregion

        #region Methods
        public async void GetTodaysRequest(string status)
        {
            try
            {
                var requests = await _pilotRequestServices.GetTodaysRequest(status);
                requests.userData.ForEach(u => {
                    if (u.statusID == 1)
                    {
                        //Pending
                        u.color = ConvertColor("#FFF4DB");
                        u.TextColor = ConvertColor("#FFC540");
                    }
                    if (u.statusID == 2)
                    {
                        //OnGoing
                        u.color = ConvertColor("#F98926");

                    }
                    if (u.statusID == 3)
                    {
                        //UpComming
                        u.color = ConvertColor("#000000");
                    }
                    if (u.statusID == 4)
                    {
                        //Rejected
                        u.color = ConvertColor("#D42424"); ;
                    }
                    if (u.statusID == 5)
                    {
                        //Completed
                        u.color = ConvertColor("#3398D8");
                    }
                    if (u.statusID == 6)
                    {
                        //Cancel
                        u.color = u.color = ConvertColor("#A9ABB1");
                    }
                    if (u.statusID == 7)
                    {
                        //EndFlight
                        u.color = u.color = ConvertColor("#000000");
                    }
                });
                TodaysRequests = new ObservableCollection<RequestData>(requests.userData);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public async void GetAllRequest(string status)
        {

            try
            {
                var requests = await _pilotRequestServices.GetAllRequest(status);
                requests.userData.ForEach(u => { 
                    if (u.statusID == 1) {
                        //Pending
                        u.color = ConvertColor("#FFC540");
                    }
                    if (u.statusID == 2)
                    {
                        //OnGoing
                        u.color = ConvertColor("#F98926"); 
                    }
                    if (u.statusID == 3)
                    {
                        //UpComming
                        u.color = ConvertColor("#000000"); 
                    }
                    if (u.statusID == 4)
                    {
                        //Rejected
                        u.color = ConvertColor("#D42424"); ;
                    }
                    if (u.statusID == 5)
                    {
                        //Completed
                        u.color = ConvertColor("#3398D8"); 
                    }
                    if (u.statusID == 6)
                    {
                        //Cancel
                        u.color = u.color = ConvertColor("#000000");
                    }
                    if (u.statusID == 7)
                    {
                        //EndFlight
                        u.color = u.color = ConvertColor("#000000");
                    }
                });
                AllRequests = new ObservableCollection<RequestData>(requests.userData);
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }

        }
        public async Task<RideById>  GetRideById(int status)
        {
            var result= await _pilotRequestServices.GetRideById(status);
            return result.userData;
        }

        #endregion
        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        } 

    }
}
