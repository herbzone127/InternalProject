using ideaForge.Pages.DashboardPages;
using ideaForge.Popups;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ideaForge.Popups.MessageBox;
using MessageBox = ideaForge.Popups.MessageBox;

namespace ideaForge.ViewModels
{
    public class OnGoingRideViewModel : ViewModelBase
    {
        #region Services
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();

        #endregion
        #region Commands

        private readonly DelegateCommand _saveChanges_Command;
        public ICommand SaveChanges_Command => _saveChanges_Command;

        private readonly DelegateCommand _cancelChanges_Command;
        public ICommand CancelChanges_Command => _cancelChanges_Command;



        #endregion
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
        private bool _CameraError;

        public bool CameraError
        {
            get { return _CameraError; }
            set { _CameraError = value;
                OnPropertyChanged(nameof(CameraError));
            }
        }
        private bool _TechnicalError;

        public bool TechnicalError
        {
            get { return _TechnicalError; }
            set { _TechnicalError = value;
                OnPropertyChanged(nameof(TechnicalError));
            }
        }
        private bool _CommunicationLoss;

        public bool CommunicationLoss
        {
            get { return _CommunicationLoss; }
            set { _CommunicationLoss = value;
                OnPropertyChanged(nameof(CommunicationLoss));
            }
        }
        private bool _BadWeather;

        public bool BadWeather
        {
            get { return _BadWeather; }
            set { _BadWeather = value;
                OnPropertyChanged(nameof(BadWeather));
            }
        }


        private Ride _rideById;

        public Ride RideById
        {
            get { return _rideById; }
            set { _rideById = value;
                OnPropertyChanged(nameof(RideById));
            }
        }
        #region Constructor
        public OnGoingRideViewModel()
        {
            Global.isStoped = true;
            _saveChanges_Command = new DelegateCommand(CanExecuteSaveChanges);
            _cancelChanges_Command = new DelegateCommand(CanExecuteCancelChanges);

        }

#endregion

        private async void CanExecuteSaveChanges(object obj)
        {
            IsBusy = true;

            var flightStatus =await _pilotRequestServices.AddUpdatePilotStatus(new FlightStatus
            {
                badWeather = BadWeather,
                cameraError = CameraError,
                communicationLoss = CommunicationLoss,
                technicalError = TechnicalError,
                rideId = RideById.id,
                userId = RideById.userID
            }) ;
            if (flightStatus.status)
            {
                if(BadWeather || CameraError|| CommunicationLoss || TechnicalError)
                {
                    RideById.statusID = 7;
                }
               
                var result = await _pilotRequestServices.UpdateRideByPilot(RideById);
                if (result.status)
                {

                    MessageBox.ShowSuccess("Ride update ","Successful.");
                    var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    var context = (DashboardViewModel)dashboard.DataContext;
                    context.CurrentPage = new Requests();
                    context.PageName = "Requests";
                }
                else
                {
                    MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");
                }
            }
            
            IsBusy = false;
        }


        private void CanExecuteCancelChanges(object obj)
        {
            var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
            Global.isStoped = false;
            vm.statusBorder = Visibility.Hidden;
            vm.BackButtonVisibility = Visibility.Hidden;
            var context = (DashboardViewModel)dashboard.DataContext;
           
            context.CurrentPage = new Requests();
            context.PageName = "Requests";
        }
        private String _missionName;

        public String MissionName
        {
            get { return _missionName; }
            set
            {
                _missionName = value;
                OnPropertyChanged(nameof(MissionName));
            }
        }
        private double _totalRequestedTime1;

        public double TotalRequestedTime1
        {
            get { return _totalRequestedTime1; }
            set
            {
                _totalRequestedTime1 = value;
                OnPropertyChanged(nameof(TotalRequestedTime1));
            }
        }

        private string _flightDate1;

        public string FlightDate1
        {
            get { return _flightDate1; }
            set
            {
                _flightDate1 = value;
                OnPropertyChanged(nameof(FlightDate1));
            }
        }


        private string _flightTime1;

        public string FlightTime1
        {
            get { return _flightTime1; }
            set
            {
                _flightTime1 = value;
                OnPropertyChanged(nameof(FlightTime1));
            }
        }

        private String _statusForUser1;

        public String StatusForUser1
        {
            get { return _statusForUser1; }
            set
            {
                _statusForUser1 = value;
                OnPropertyChanged(nameof(StatusForUser1));
            }
        }
        
        private String _latitude1;

        public String Latitude1
        {
            get { return _latitude1; }
            set
            {
                _latitude1 = value;
                OnPropertyChanged(nameof(Latitude1));
            }
        }
        private String _longtitude1;

        public String Longtitude1
        {
            get { return _longtitude1; }
            set
            {
                _longtitude1 = value;
                OnPropertyChanged(nameof(Longtitude1));
            }
        }

        private String _uav1;

        public String UAV1
        {
            get { return _uav1; }
            set
            {
                _uav1 = value;
                OnPropertyChanged(nameof(UAV1));
            }
        }
        

        private String _controlKey1;

        public String ControlKey1
        {
            get { return _controlKey1; }
            set
            {
                _controlKey1 = value;
                OnPropertyChanged(nameof(ControlKey1));
            }
        }
       
        private String _secretKey1;

        public String SecretKey1
        {
            get { return  _secretKey1; }
            set
            {
                _secretKey1 = value;
                OnPropertyChanged(nameof(SecretKey1));
            }
        }
    }
}
