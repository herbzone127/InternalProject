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

        private readonly DelegateCommand _missionType;
        public ICommand MissionType => _missionType;
        private readonly DelegateCommand _totalRequestedTime;
        public ICommand TotalRequestedTime => _totalRequestedTime;
        private readonly DelegateCommand _flightDate;
        public ICommand FlightDate => _flightDate;
        private readonly DelegateCommand _flightTime;
        public ICommand FlightTime => _flightTime;
        private readonly DelegateCommand _statusForUser;
        public ICommand StatusForUser => _statusForUser;
        private readonly DelegateCommand _userRating;
        public ICommand UserRating => _userRating;
        private readonly DelegateCommand _userFeedBack;
        public ICommand UserFeedBack => _userFeedBack;
        private readonly DelegateCommand _uAVId;
        public ICommand UAVId => _uAVId;
        private readonly DelegateCommand _controlKey;
        public ICommand ControlKey => _controlKey;



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


       
    }
}
