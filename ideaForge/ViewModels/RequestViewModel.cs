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

            //GetTodaysRequest("");
            //Task.Factory.StartNew(async() => {

            //  await  GetAllRequest("");
            //  await  GetTodaysRequest("");
            //} ).ConfigureAwait(false);

      
          GetAllRequest("").ConfigureAwait(false);
          GetTodaysRequest("").ConfigureAwait(false);



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
                OnPropertyChanged(nameof(AllRequests));
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

        private ObservableCollection<StatusChanges> _statusChanges;

        public ObservableCollection<StatusChanges> StatusChanges
        {
            get { return _statusChanges; }
            set
            {
                _statusChanges = value;
                OnPropertyChanged(nameof(StatusChanges));
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

        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        #endregion

        #region Methods
        public async Task GetTodaysRequest(string status)
        {
            IsBusy = true;
            try
            {
                var requests = await _pilotRequestServices.GetTodaysRequest(status);
                if (requests.status)
                {
                    requests.userData.ForEach(u => {
                        if (u.statusID == 1)
                        {
                            //Pending
                            u.color = ConvertColor("#FFF4DB");
                            u.TextColor = ConvertColor("#FFC540");
                            u.StatusImage = "/Images/pendingIcon.png";
                        }
                        if (u.statusID == 2)
                        {
                            //OnGoing
                            u.color = ConvertColor("#F98926");
                            u.TextColor = ConvertColor("#F98926");
                            u.StatusImage = "/Images/ongoingIcon.png";

                        }
                        if (u.statusID == 3)
                        {
                            //UpComming
                            u.color = ConvertColor("#000000");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 4)
                        {
                            //Rejected
                            u.color = ConvertColor("#D42424");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 5)
                        {
                            //Completed
                            u.color = ConvertColor("#3398D8");
                            u.TextColor = ConvertColor("#3398D8");
                            u.StatusImage = "/Images/CompleteRideIcon.png";
                        }
                        if (u.statusID == 6)
                        {
                            //Cancel
                            u.color = ConvertColor("#A9ABB1");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 7)
                        {
                            //EndFlight
                            u.color = ConvertColor("#000000");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                    });
                    TodaysRequests = new ObservableCollection<RequestData>(requests.userData);
                }
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            IsBusy = false;

        }
        public async Task GetAllRequest(string status)
        {
            IsBusy = true;
            try
            {
                var requests = await _pilotRequestServices.GetAllRequest(status);
                if (requests.status)
                {
                    requests.userData.ForEach(u => {
                        if (u.statusID == 1)
                        {
                            //Pending
                            u.color = ConvertColor("#FFF4DB");
                            u.TextColor = ConvertColor("#FFC540");
                            u.StatusImage = "/Images/pendingIcon.png";
                        }
                        if (u.statusID == 2)
                        {
                            //OnGoing
                            u.color = ConvertColor("#F98926");
                            u.TextColor = ConvertColor("#F98926");
                            u.StatusImage = "/Images/ongoingIcon.png";

                        }
                        if (u.statusID == 3)
                        {
                            //UpComming
                            u.color = ConvertColor("#000000");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 4)
                        {
                            //Rejected
                            u.color = ConvertColor("#D42424");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 5)
                        {
                            //Completed
                            u.color = ConvertColor("#3398D8");
                            u.TextColor = ConvertColor("#3398D8");
                            u.StatusImage = "/Images/CompleteRideIcon.png";
                        }
                        if (u.statusID == 6)
                        {
                            //Cancel
                            u.color = ConvertColor("#A9ABB1");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                        if (u.statusID == 7)
                        {
                            //EndFlight
                            u.color = ConvertColor("#000000");
                            u.TextColor = ConvertColor("#FFFFFF");
                        }
                    });
                    AllRequests = new ObservableCollection<RequestData>(requests.userData);
                }
             
              
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
            IsBusy = false;
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
