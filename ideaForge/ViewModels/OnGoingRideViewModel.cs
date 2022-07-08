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
    public class OnGoingRideViewModel : ViewModelBase
    {
        #region Commands

      



        #endregion
        private RideById _rideById;

        public RideById RideById
        {
            get { return _rideById; }
            set { _rideById = value;
                OnPropertyChanged(nameof(RideById));
            }
        }

        public OnGoingRideViewModel()
        {
            var result = RideById;
           
        }

        private int _missionType1;

        public int MissionType1
        {
            get { return _missionType1; }
            set
            {
                _missionType1 = value;
                OnPropertyChanged(nameof(MissionType1));
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
