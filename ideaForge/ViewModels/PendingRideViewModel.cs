using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace ideaForge.ViewModels
{
    public class PendingRideViewModel : ViewModelBase
    {
        private RideById _rideById;

        public RideById RideById
        {
            get { return _rideById; }
            set
            {
                _rideById = value;
                OnPropertyChanged(nameof(RideById));
            }
        }
       
        public PendingRideViewModel()
        {
            var result = RideById;
        }

        #region Commands
        private readonly DelegateCommand _saveChanges_Comand;
        public ICommand SaveChanges_Comand => _saveChanges_Comand;

        private readonly DelegateCommand _cancelChanges_Comand;
        public ICommand CancelChanges_Comand=> _cancelChanges_Comand;
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

    }
}
