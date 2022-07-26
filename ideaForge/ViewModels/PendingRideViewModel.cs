using ideaForge.Pages.DashboardPages;
using ideaForge.Popups;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ideaForge.ViewModels
{
    public class PendingRideViewModel : ViewModelBase
    {

        #region Services
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();

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
        private Ride _rideById;

        public Ride RideById
        {
            get { return _rideById; }
            set
            {
                _rideById = value;
                OnPropertyChanged(nameof(RideById));
            }
        }
        #region Constructor
        public PendingRideViewModel()
        {
            var result = RideById;
            _saveChanges_Command = new DelegateCommand(CanExecuteSaveChanges);
            _cancelChanges_Command = new DelegateCommand(CanExecuteCancelChanges);

            GetAllStatuses().ConfigureAwait(false);
        }

        #endregion

        private async void CanExecuteSaveChanges(object obj)
        {
            IsBusy = true;
            if (RideStatusId == 2)
            {
                RideById.SecretKey = GenerateRandomCryptographicKey(16);
                RideById.ControlKey = GenerateRandomCryptographicKey(8);
            }
            
            RideById.statusID = RideStatusId;
          var result =await  _pilotRequestServices.UpdateRideByPilot(RideById);
            if (result.status)
            {
                new SuccessMessageBox().Show("Ride update ","Successful.");
            }
            else
            {
                new ErrorMessageBox().Show(result.message);
            }
            IsBusy = false;
        }
       

        private void CanExecuteCancelChanges(object obj)
        {
            Window parentWindow = Window.GetWindow(Application.Current.MainWindow);
            var dashboard = (Dashboard)parentWindow;
            var context = (DashboardViewModel)dashboard.DataContext;
            context.CurrentPage = new Requests();
            context.PageName = "Requests";
        }
       
    

        private readonly DelegateCommand _saveChanges_Command;
        public ICommand SaveChanges_Command => _saveChanges_Command;
       
             private readonly DelegateCommand _cancelChanges_Command;
        public ICommand CancelChanges_Command => _cancelChanges_Command;
        #region Commands


        #endregion
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
       
              private string _status1;

        public string Status1
        {
            get { return _status1; }
            set
            {
                _status1 = value;
                OnPropertyChanged(nameof(Status1));
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

        private String _longtitude1;

        public String Longitude1
        {
            get { return _longtitude1; }
            set
            {
                _longtitude1 = value;
                OnPropertyChanged(nameof(Longitude1));
            }
        }

        private ObservableCollection<RideStatus> _rideStatuses;

        public ObservableCollection<RideStatus> RideStatuses
        {
            get { return _rideStatuses; }
            set { _rideStatuses = value;
                OnPropertyChanged(nameof(RideStatuses));
            }
        }
        private int _rideStatusId;

        public int RideStatusId
        {
            get { return _rideStatusId; }
            set { _rideStatusId = value;
            OnPropertyChanged(nameof(RideStatusId));
            }
        }



        //Control Key 8 Length ---- Secret Key  16 Length
        public string GenerateRandomCryptographicKey(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }

        public async Task GetAllStatuses()
        {
            try
            {
              var result = await  _pilotRequestServices.GetAllStatuses();
               
                    RideStatuses = new ObservableCollection<RideStatus>(result);
                
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
