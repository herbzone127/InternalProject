﻿using DocumentFormat.OpenXml.Drawing.Charts;
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
    public class CompleteRideViewModel : ViewModelBase
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
        #region Commands

        private readonly DelegateCommand _TextCopyLatitude_Comand;
        public ICommand TextCopyLatitude_Comand => _TextCopyLatitude_Comand;

        private readonly DelegateCommand _TextCopyLONGITUDE_Comand;
        public ICommand TextCopyLONGITUDE_Comand => _TextCopyLONGITUDE_Comand;

        private readonly DelegateCommand _TextCopyUAV_ID_Comand;
        public ICommand TextCopyUAV_ID_Comand => _TextCopyUAV_ID_Comand;
        
        private readonly DelegateCommand _TextCopySECRET_KEY_Comand;
        public ICommand TextCopySECRET_KEY_Comand => _TextCopySECRET_KEY_Comand;
        
        private readonly DelegateCommand _TextCopyCONTROL_KEY_Comand;
        public ICommand TextCopyCONTROL_KEY_Comand => _TextCopyCONTROL_KEY_Comand;

        private readonly DelegateCommand _SaveChanges_Comand;
        public ICommand SaveChanges_Comand => _SaveChanges_Comand;
        
       

        private readonly DelegateCommand _Image_Rating5_Comand;
        public ICommand Image_Rating5_Comand => _Image_Rating5_Comand;

        private readonly DelegateCommand _Image_Rating4_Comand;
        public ICommand Image_Rating4_Comand => _Image_Rating4_Comand;

        private readonly DelegateCommand _Image_Rating3_Comand;
        public ICommand Image_Rating3_Comand => _Image_Rating3_Comand;

        private readonly DelegateCommand _Image_Rating2_Comand;
        public ICommand Image_Rating2_Comand => _Image_Rating2_Comand;

        private readonly DelegateCommand _Image_Rating1_Comand;
        public ICommand Image_Rating1_Comand => _Image_Rating1_Comand;



        #endregion
        #region Constructor
        public CompleteRideViewModel()
        {
            Global.isStoped = true;

            var result = RideById;
            //_TextCopyLatitude_Comand = new DelegateCommand(TextCopyLatitude_ComandExecut);
            _TextCopyLONGITUDE_Comand = new DelegateCommand(TextCopyLONGITUDE_ComandExecut);
            _TextCopyUAV_ID_Comand = new DelegateCommand(TextCopyUAV_ID_ComandExecut);
            _TextCopyCONTROL_KEY_Comand = new DelegateCommand(TextCopyCONTROL_KEY_ComandExecut);
            _TextCopySECRET_KEY_Comand = new DelegateCommand(TextCopySECRET_KEY_ComandExecut);
            _SaveChanges_Comand = new DelegateCommand(SaveChanges_ComandExecut);
       
            _Image_Rating1_Comand = new DelegateCommand(Image_Rating1_ComandExecut);
            _Image_Rating2_Comand = new DelegateCommand(Image_Rating2_ComandExecut);
            _Image_Rating3_Comand = new DelegateCommand(Image_Rating3_ComandExecut);
            _Image_Rating4_Comand = new DelegateCommand(Image_Rating4_ComandExecut);
            _Image_Rating5_Comand = new DelegateCommand(Image_Rating5_ComandExecut);
            RestStarRating();
            //GetUserFeedback(RideById.id).ConfigureAwait(false);
            _saveChanges_Command = new DelegateCommand(CanExecuteSaveChanges);
            _cancelChanges_Command = new DelegateCommand(CanExecuteCancelChanges);

           
        }

        #endregion

        private async void CanExecuteSaveChanges(object obj)
        {
            IsBusy = true;


            var result = await _pilotRequestServices.AddUpdatePilotFeedback(new FlightFeedback
            {
                pioltRating = Rating_Num,
                rideId = RideById.id,
                userId = RideById.userID,
                feedbackComments = ""

            }) ;
            if (result.status)
            {
                MessageBox.ShowSuccess("Ride update","Successful.");
            }
            else
            {
                MessageBox.Show(result.message, CMessageTitle.Error, CMessageButton.Ok, "");;
            }
            IsBusy = false;
        }


        private void CanExecuteCancelChanges(object obj)
        {
            var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            Global.isStoped = false;
         
            var context = (DashboardViewModel)dashboard.DataContext;
            context.CurrentPage = new Requests();
            context.PageName = "Requests";
            context.statusBorder = Visibility.Hidden;
            context.BackButtonVisibility = Visibility.Hidden;

        }

        #region properties2
        private string _Image_Rating1;

        public string Image_Rating1
        {
            get { return _Image_Rating1; }
            set
            {
                _Image_Rating1 = value;
                OnPropertyChanged(nameof(Image_Rating1));
            }
        }

        private string _Image_Rating2;

        public string Image_Rating2
        {
            get { return _Image_Rating2; }
            set
            {
                _Image_Rating2 = value;
                OnPropertyChanged(nameof(Image_Rating2));
            }
        }

        private string _Image_Rating3;

        public string Image_Rating3
        {
            get { return _Image_Rating3; }
            set
            {
                _Image_Rating3 = value;
                OnPropertyChanged(nameof(Image_Rating3));
            }
        }

        private string _Image_Rating4;

        public string Image_Rating4
        {
            get { return _Image_Rating4; }
            set
            {
                _Image_Rating4 = value;
                OnPropertyChanged(nameof(Image_Rating4));
            }
        }

        private string _Image_Rating5;

        public string Image_Rating5
        {
            get { return _Image_Rating5; }
            set
            {
                _Image_Rating5 = value;
                OnPropertyChanged(nameof(Image_Rating5));
            }
        }

        private string _Latitude;

        public string Latitude
        {
            get { return _Latitude; }
            set
            {
                _Latitude = value;
                OnPropertyChanged(nameof(Latitude));
            }
        }


        private int _Rating_Num;
        public int Rating_Num
        {
            get { return _Rating_Num; }
            set
            {
                _Rating_Num = value;
                OnPropertyChanged(nameof(Rating_Num));
            }
        }
        #endregion


        #region UserFeedback Star Properties 

        private string _UserFeedback_Image_Rating1;
        public string UserFeedback_Image_Rating1
        {
            get { return _UserFeedback_Image_Rating1; }
            set
            {
                _UserFeedback_Image_Rating1 = value;
                OnPropertyChanged(nameof(UserFeedback_Image_Rating1));
            }
        }
        private string _UserFeedback_Image_Rating2;
        public string UserFeedback_Image_Rating2
        {
            get { return _UserFeedback_Image_Rating2; }
            set
            {
                _UserFeedback_Image_Rating2 = value;
                OnPropertyChanged(nameof(UserFeedback_Image_Rating2));
            }
        }
        private string _UserFeedback_Image_Rating3;
        public string UserFeedback_Image_Rating3
        {
            get { return _UserFeedback_Image_Rating3; }
            set
            {
                _UserFeedback_Image_Rating3 = value;
                OnPropertyChanged(nameof(UserFeedback_Image_Rating3));
            }
        }
        private string _UserFeedback_Image_Rating4;
        public string UserFeedback_Image_Rating4
        {
            get { return _UserFeedback_Image_Rating4; }
            set
            {
                _UserFeedback_Image_Rating4 = value;
                OnPropertyChanged(nameof(UserFeedback_Image_Rating4));
            }
        }
        private string _UserFeedback_Image_Rating5;
        public string UserFeedback_Image_Rating5
        {
            get { return _UserFeedback_Image_Rating5; }
            set
            {
                _UserFeedback_Image_Rating5 = value;
                OnPropertyChanged(nameof(UserFeedback_Image_Rating5));
            }
        }
        #endregion

        public void TextCopyLatitude_ComandExecut(object obj)
        {
           
            Clipboard.SetDataObject(Latitude);
        }
        public void TextCopyLONGITUDE_ComandExecut(object obj)
        {

        }
        public void TextCopyUAV_ID_ComandExecut(object obj)
        {

        }
        public void TextCopyCONTROL_KEY_ComandExecut(object obj)
        {

        }
        public void TextCopySECRET_KEY_ComandExecut(object obj)
        {

        }
        public void SaveChanges_ComandExecut(object obj)
        {
            CanExecuteSaveChanges(obj);
        }
    
        public void Image_Rating1_ComandExecut(object obj)
        {
            Image_Rating1 = "/Images/FeedBackYellowStar.png";
            Image_Rating2 = "/Images/StarFealedBackplain.png";
            Image_Rating3 = "/Images/StarFealedBackplain.png";
            Image_Rating4 = "/Images/StarFealedBackplain.png";
            Image_Rating5 = "/Images/StarFealedBackplain.png";
            Rating_Num = 1;
        }
        public void Image_Rating2_ComandExecut(object obj)
        {
            Image_Rating1 = "/Images/FeedBackYellowStar.png";
            Image_Rating2 = "/Images/FeedBackYellowStar.png";
            Image_Rating3 = "/Images/StarFealedBackplain.png";
            Image_Rating4 = "/Images/StarFealedBackplain.png";
            Image_Rating5 = "/Images/StarFealedBackplain.png";
            Rating_Num = 2;
        }
        public void Image_Rating3_ComandExecut(object obj)
        {
            Image_Rating1 = "/Images/FeedBackYellowStar.png";
            Image_Rating2 = "/Images/FeedBackYellowStar.png";
            Image_Rating3 = "/Images/FeedBackYellowStar.png";
            Image_Rating4 = "/Images/StarFealedBackplain.png";
            Image_Rating5 = "/Images/StarFealedBackplain.png";
            Rating_Num = 3;
        }
        public void Image_Rating4_ComandExecut(object obj)
        {
            Image_Rating1 = "/Images/FeedBackYellowStar.png";
            Image_Rating2 = "/Images/FeedBackYellowStar.png";
            Image_Rating3 = "/Images/FeedBackYellowStar.png";
            Image_Rating4 = "/Images/FeedBackYellowStar.png";
            Image_Rating5 = "/Images/StarFealedBackplain.png"; 
            Rating_Num = 4;
        }
        public void Image_Rating5_ComandExecut(object obj)
        {
            Image_Rating1 = "/Images/FeedBackYellowStar.png";
            Image_Rating2 = "/Images/FeedBackYellowStar.png";
            Image_Rating3 = "/Images/FeedBackYellowStar.png";
            Image_Rating4 = "/Images/FeedBackYellowStar.png";
            Image_Rating5 = "/Images/FeedBackYellowStar.png";
            Rating_Num = 5;
        }
        public void RestStarRating()
        {
            Image_Rating1 = "/Images/StarFealedBackplain.png";
            Image_Rating2 = "/Images/StarFealedBackplain.png";
            Image_Rating3 = "/Images/StarFealedBackplain.png";
            Image_Rating4 = "/Images/StarFealedBackplain.png";
            Image_Rating5 = "/Images/StarFealedBackplain.png";
            Rating_Num = 0;
        }

        #region properties
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
        private double _totalRequestedTime;

        public double TotalRequestedTime
        {
            get { return _totalRequestedTime; }
            set
            {
                _totalRequestedTime = value;
                OnPropertyChanged(nameof(TotalRequestedTime));
            }
        }

        private string _flightDate;

        public string FlightDate
        {
            get { return _flightDate; }
            set
            {
                _flightDate = value;
                OnPropertyChanged(nameof(FlightDate));
            }
        }

        private string _flightTime;

        public string FlightTime
        {
            get { return _flightTime; }
            set
            {
                _flightTime = value;
                OnPropertyChanged(nameof(FlightTime));
            }
        }

        private string _statusForUser;

        public string StatusForUser
        {
            get { return _statusForUser; }
            set
            {
                _statusForUser = value;
                OnPropertyChanged(nameof(StatusForUser));
            }
        }

        private string _feedbackComments;

        public string feedbackComments
        {
            get { return _feedbackComments; }
            set
            {
                _feedbackComments = value;
                OnPropertyChanged(nameof(feedbackComments));
            }
        }

        private string _userFeedBack;

        public string UserFeedBack
        {
            get { return _userFeedBack; }
            set
            {
                _userFeedBack = value;
                OnPropertyChanged(nameof(UserFeedBack));
            }
        }


        private string _uavId;

        public string UAVId
        {
            get { return _uavId; }
            set
            {
                _uavId = value;
                OnPropertyChanged(nameof(UAVId));
            }
        }

      

        private string _controlKey;

        public string ControlKey
        {
            get { return _controlKey; }
            set
            {
                _controlKey = value;
                OnPropertyChanged(nameof(ControlKey));
            }
        }



        private string _secretKey;

        public string SecretKey
        {
            get { return _secretKey; }
            set
            {
                _secretKey = value;
                OnPropertyChanged(nameof(SecretKey));
            }
        }

        private bool _inFlightService;

        public bool inFlightService
        {
            get { return _inFlightService; }
            set
            {
                _inFlightService = value;
                OnPropertyChanged(nameof(inFlightService));
            }
        }
        private bool _flightControl;

        public bool flightControl
        {
            get { return _flightControl; }
            set
            {
                _flightControl = value;
                OnPropertyChanged(nameof(flightControl));
            }
        }
        private bool _communication;

        public bool communication
        {
            get { return _communication; }
            set
            {
                _communication = value;
                OnPropertyChanged(nameof(communication));
            }
        }

        public Ride SelectedRequest { get;  set; }
        public string Longitude { get;  set; }

        #endregion

        #region ApiMethods

        public async Task<FlightFeedback> GetPilotFeeback(int rideId)
        {
            var request = await _pilotRequestServices.GetPilotFeeback(rideId);
            if (request.status)
            {
                if (request.userData != null)
                {
                    switch (request.userData.pioltRating)
                    {
                        case 1:
                            Image_Rating1_Comand.Execute(new object());
                            break;
                        case 2:
                            Image_Rating2_Comand.Execute(new object());
                            break;
                        case 3:
                            Image_Rating3_Comand.Execute(new object());
                            break;
                        case 4:
                            Image_Rating4_Comand.Execute(new object());
                            break;
                        case 5:
                            Image_Rating5_Comand.Execute(new object());
                            break;
                    }
                   // RideById = request.userData;
                }
            }
            return null;
        }

        public async Task<UserFeedback> GetUserFeedback(int rideId)
        {
            var request = await _pilotRequestServices.GetUserFeedbackByRideId(rideId);
            if (request.status)
            {
                if (request.userData != null)
                {
                    var result = request.userData.FirstOrDefault();
                    int ratinguser = 0;
                    if (result != null)
                    {
                        UserFeedBack = result?.Comments;
                        ratinguser = result.Rating;
                    }
                    if (ratinguser == 1)
                    {
                        UserFeedback_Image_Rating1 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating2 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating3 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating4 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating5 = "/Images/StarFealedBackplain.png";
                    }
                    else if (ratinguser == 2)
                    {
                        UserFeedback_Image_Rating1 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating2 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating3 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating4 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating5 = "/Images/StarFealedBackplain.png";
                    }
                    else if (ratinguser == 3)
                    {
                        UserFeedback_Image_Rating1 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating2 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating3 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating4 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating5 = "/Images/StarFealedBackplain.png";
                    }
                    else if (ratinguser == 4)
                    {
                        UserFeedback_Image_Rating1 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating2 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating3 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating4 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating5 = "/Images/StarFealedBackplain.png";
                    }
                    else if (ratinguser == 5)
                    {
                        UserFeedback_Image_Rating1 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating2 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating3 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating4 = "/Images/FeedBackYellowStar.png";
                        UserFeedback_Image_Rating5 = "/Images/FeedBackYellowStar.png";
                    }
                    else
                    {
                        UserFeedback_Image_Rating1 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating2 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating3 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating4 = "/Images/StarFealedBackplain.png";
                        UserFeedback_Image_Rating5 = "/Images/StarFealedBackplain.png";
                    }
                    return result;
                }
            }
            else
            {
                MessageBox.ShowError(request.message);
            }
            return null;
        }

        public async Task<FlightFeedback> GetFlightFeedback(int rideId)
        {
            var request = await _pilotRequestServices.GetFlightFeedbackByRideId(rideId);
            if (request != null)
            {
                if (request.status)
                {
                    if (request.userData != null)
                    {
                        var result = request.userData;

                        feedbackComments = result.feedbackComments;
                        if (inFlightService == true)
                        {
                            inFlightService = result.inFlightService;
                        }
                        else
                        {
                            inFlightService = result.inFlightService;
                        }
                        if (flightControl == true)
                        {
                            flightControl = result.flightControl;
                        }
                        else
                        {
                            flightControl = result.flightControl;
                        }
                        if (communication == true)
                        {
                            communication = result.communication;
                        }
                        else
                        {
                            communication = result.communication;
                        }
                        return result;
                    }
                }
            }
      
            return null;
        }
        #endregion

    }
}
