using ideaForge.Models;
using ideaForge.Pages.DashboardPages;
using ideaForge.Popups;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using IdeaForge.Services;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using MessageBox = ideaForge.Popups.MessageBox;

namespace ideaForge.ViewModels
{
    public class RequestViewModel : ViewModelBase
    {
        /// <summary>
        /// Services
        /// </summary>
        #region Services
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        public IGoogleMapsApiService _googleMapsApiService
        => App.serviceProvider.GetRequiredService<IGoogleMapsApiService>();
        #endregion
        public int rideId { get; set; }
        private RequestData _selectedAllRequest;

        public RequestData SelectedAllRequest
        {
            get { return _selectedAllRequest; }
            set { _selectedAllRequest = value;
                OnPropertyChanged(nameof(SelectedAllRequest));
            }
        }
        public RequestFilter FiltersData;
        public int ShowCount = 0;

       
        #region Commands
        //RegisterCommand
        private readonly DelegateCommand _AcceptedCommand;
        public ICommand AcceptedCommand => _AcceptedCommand;
        private readonly DelegateCommand _RejectedCommand;
        public ICommand RejectedCommand => _RejectedCommand;
        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;

        private readonly DelegateCommand _applyfilter;
        public ICommand ApplyFilter => _applyfilter;

        private readonly DelegateCommand _clearfilter;
        public ICommand ClearFilter => _clearfilter;
        #endregion

        #region Constructur
        public RequestViewModel()
        {
            _AcceptedCommand = new DelegateCommand(AcceptedCommandCanExecute);
            _RejectedCommand = new DelegateCommand(RejectedCommandCanExecute);
            AllRequests = new ObservableCollection<RequestData>();
            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
            _applyfilter = new DelegateCommand(ApplyFilterCommand);
            _clearfilter = new DelegateCommand(DoClearFilter);
            FiltersData = new RequestFilter();
            FiltersData.SelectedStatus = new List<Status>();
            //GetTodaysRequest("");
            //Task.Factory.StartNew(async() => {

            //  await  GetAllRequest("");
            //  await  GetTodaysRequest("");
            //} ).ConfigureAwait(false);

            
          
          
        }

        private void DoClearFilter(object obj)
        {
            //var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            //if (dashboard != null)
            //{
            //    var dVModel = dashboard.DataContext as DashboardViewModel;
            //    if (dVModel != null)
            //    {
            //        var requesPage = dVModel.CurrentPage.Content as Requests;
            //        requesPage.worker.RunWorkerAsync();

            //    }
            //}
            if (tempTodayRequest!=null)
            TodaysRequests = new ObservableCollection<RequestData>(tempTodayRequest);
            
            if(tempallRequest!=null)
            AllRequests = new ObservableCollection<RequestData>(tempallRequest);

            if (CloseFilter != null)
            {
                CloseFilter(new object(), new EventArgs());
            }
        }

        public void FilterShowTodayRequest()
        {
           TodaysRequests = new ObservableCollection<RequestData>(tempTodayRequest.Take(ShowCount));
        }

        public void FilterShowAllRequest()
        {
            AllRequests = new ObservableCollection<RequestData>(tempallRequest.Take(ShowCount));
        }

        public void ClearFilters()
        {
            TodaysRequests = new ObservableCollection<RequestData>(tempTodayRequest);
            AllRequests = new ObservableCollection<RequestData>(tempallRequest);
           
        }
        public event EventHandler CloseFilter;

       
        public async void ApplyFilterCommand(object obj)
        {
            if(tempallRequest!=null)     FilterAll(new ObservableCollection<RequestData>(tempallRequest), true);
            if(tempTodayRequest!=null)   FilterAll(new ObservableCollection<RequestData>(tempTodayRequest), false);
            if (CloseFilter != null)
            {
                CloseFilter(new object(), new EventArgs());
            }
        }


        DateTime GetParseDateTime(DateTime dt)
        {
            var newCultureInfo = new CultureInfo("hi-IN");
            DateTime checkdate = DateTime.Parse(dt.ToString(), newCultureInfo);
            return checkdate;
        }
        void FilterAll(ObservableCollection<RequestData> temprequestdata, bool IsAll)
        {
            //var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            //if (dashboard != null)
            //{
            //    var dVModel = dashboard.DataContext as DashboardViewModel;
            //    if (dVModel != null)
            //    {
            //        var requesPage = dVModel.CurrentPage.Content as Requests;
            //        requesPage.worker.CancelAsync();

            //    }
            //}
            if (temprequestdata.Count <= 0) return;
            List<RequestData> tempdata = new List<RequestData>();
            if(FiltersData.StartDate!=null && FiltersData.EndDate!=null)
            {
                            tempdata = temprequestdata.Where(x =>
             GetParseDateTime(x.startDate.Date) >= GetParseDateTime((DateTime)FiltersData.StartDate)
            && GetParseDateTime(x.endDate.Date) <= GetParseDateTime((DateTime)FiltersData.EndDate)).ToList();
            }

  
            if (!string.IsNullOrEmpty(FiltersData.SearchLocation))
            {
                tempdata = tempdata.Where(x => x.location.ToLower().Contains(FiltersData.SearchLocation.ToLower())).ToList();
            }

            //var d = tempdata.FirstOrDefault(x => x.id == 1088);

            List<RequestData> tempStatus = new List<RequestData>();
            foreach (Status item in FiltersData.SelectedStatus)
            {
                var status = Enum.GetName(typeof(Status),item).ToLower();
                tempdata.ForEach(u => { 
                    var newStatus= u.status.Replace(" ", "").ToLower();
                    if (newStatus == status)
                    {
                        tempStatus.Add(u);
                    }
                
                });
              //  tempStatus.AddRange(tempdata.Where(x => x.status.Replace(" ", "").ToLower() == status).ToList()); 
            }
            if (FiltersData.SelectedStatus.Count > 0)
            {
                tempdata = tempStatus;
            }
            

            if (IsAll)
            {

                AllRequests = new ObservableCollection<RequestData>(tempdata); 
            }
            else
            {
                TodaysRequests = new ObservableCollection<RequestData>(tempdata);
            }
        }

        #endregion
        private async void ViewCommandCanExecute(object obj)
        {
            try
            {
                var selectedRecord = obj as RequestData;
                IsBusy = true;
                if (selectedRecord != null)
                {
                    var rideDetails = await GetRideById(selectedRecord.id);
                    if (rideDetails.status)
                    {


                        var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                        if (dashboard != null)
                        {
                            var context = (DashboardViewModel)dashboard.DataContext;
                            if (selectedRecord.statusID.Equals(1))
                            {
                                context.statusBorder = Visibility.Visible;
                                context.statusBorderBackground = ConvertColor("#DEECFF");
                                //dashboard.statusImage.Source = ConvertImageSource("/Images/pendingIcon.png");
                                context.StatusLogo = "/Images/pendingIcon.png";
                                context.statusBorderCornerRadius = ConvertBorderRadius("6");
                                context.statusLabel = "Pending";
                                context.statusLabelForeground = ConvertColor("#3398D8");
                                context.statusBorderBorderThickness = ConvertBorderThickness("1");
                                context.statusBorderBorderBrush = ConvertColor("#3398D8");
                                context.PageName = $"Booking ID : {selectedRecord.id}";
                                context.CurrentPage.Content = new PendingRidePage(rideDetails.userData);
                                context.BackButtonVisibility = Visibility.Visible;
                            }
                            if (selectedRecord.statusID.Equals(2))
                            {
                                context.statusBorder = Visibility.Visible;
                                context.statusBorderBackground = ConvertColor("#FFF3D9");
                                //dashboard.Header.
                                //BitmapImage bitmap = new BitmapImage();
                                //bitmap.BeginInit();
                                //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                                //bitmap.EndInit();
                                //dashboard.statusImage.Source = bitmap;
                                context.StatusLogo = "/Images/ongoingIcon.png";
                                context.statusBorderCornerRadius = ConvertBorderRadius("6");
                                context.statusLabel = "Ongoing";
                                context.statusLabelForeground = ConvertColor("#F98926");
                                context.statusBorderBorderThickness = ConvertBorderThickness("1");
                                context.statusBorderBorderBrush = ConvertColor("#FFF3D9");
                                context.PageName = $"Booking ID : {selectedRecord.id}";
                                context.CurrentPage.Content = new OnGoingRidePage(rideDetails.userData);
                                context.BackButtonVisibility = Visibility.Visible;
                                
                            }

                            if (selectedRecord.statusID.Equals(5))
                            {
                                context.statusBorder = Visibility.Visible;
                                context.statusBorderBackground = ConvertColor("#E8F4D9");
                                context.StatusLogo = "/Images/completedIcon.png";
                                context.statusBorderCornerRadius = ConvertBorderRadius("6");
                                context.statusLabel = "Completed";
                                context.statusLabelForeground = ConvertColor("#91C84F");
                                context.statusBorderBorderThickness = ConvertBorderThickness("1");
                                context.statusBorderBorderBrush = ConvertColor("#E8F4D9");
                                context.PageName = $"Booking ID : {selectedRecord.id}";

                                context.CurrentPage.Content = new CompletedRidePage(rideDetails.userData);
                                context.BackButtonVisibility = Visibility.Visible;
                            }
                        }

                    }

                }

                IsBusy = false;
            }
            catch (Exception ex)
            {

               log.Error(ex.Message,ex);
            }
          
        }
        private async void RejectedCommandCanExecute(object obj)
        {
            try
            {
                IsBusy = true;
                var model = (RequestData)obj;
                var dialogYes = rejectPopupPage.Show(model.id, true);

                if (dialogYes == System.Windows.Forms.DialogResult.Yes)
                {
                    string rejectreason = rejectPopupPage.RejectReason;
                    var result = await GetStatusChangesResponse(false, model.id, rejectreason);
                    if (result)
                    {
                        MessageBox.ShowSuccess("Selected Record Rejected ", "Successfully.");

                        await GetAllRequest("");
                        await GetTodaysRequest("");

                    }
                }
                IsBusy = false;
            }
            catch (Exception ex)
            {

                log.Error(ex.Message,ex);
            }
         
        }

        private async void AcceptedCommandCanExecute(object obj)
        {
            if (Global.IsIFDockStatus)
            {
                IsBusy = true;
                var model = (RequestData)obj;
                var requests = await _pilotRequestServices.GetTodaysRequest("");
                if (requests?.userData != null)
                {

                    var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    int count = requests.userData.Where(u => (u.statusID == 2 || u.statusID == 3) && u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()).Count();
                    if (count > 0)
                    {
                        MessageBox.ShowError("Only one flight can be in Ongoing status at a time");
                    }
                    else
                    if (model.statusID == 1)
                    {

                        var result = await GetStatusChangesResponse(true, model.id, "accepted");
                        if (result)
                        {
                            MessageBox.ShowSuccess("Selected record accepted ", "successfully.");
                            await GetAllRequest("");
                            await GetTodaysRequest("");


                        }
                        else
                        {
                            MessageBox.ShowError("Record is not updated");
                        }


                    }
                    else
                    {
                        MessageBox.ShowError("Only pending rides can be accepted");
                    }
                    IsBusy = false;
                }
            }
            else
            {
                MessageBox.ShowError("Flight & Drone/iF Dock Down");
            }
          
        }


     


        #region ObservableCollections
        private ObservableCollection<RequestData> _todaysRequests;
        private ObservableCollection<RequestData> tempTodayRequest;
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
        private ObservableCollection<RequestData> tempallRequest;
        public ObservableCollection<RequestData> AllRequests
        {
            get { return _allRequest; }
            set
            {
                _allRequest = value;
                OnPropertyChanged(nameof(AllRequests));
            }
        }

        private ObservableCollection<Ride> _rideById;

        public ObservableCollection<Ride> RideById
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
        public async Task<bool> GetTodaysRequest(string status)
        {
            //IsBusy = true;
            try
            {
                
                var requests = await _pilotRequestServices.GetTodaysRequest(status);
              
                if (requests != null)
                {
                    if (requests.status)
                    {
                        var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                        

                        requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()).ToList();
                        requests.userData.ForEach(async u => {
                         
                              

                            
                            if (u.statusID == 1)
                                {
                                    //Pending
                                    u.color = ConvertColor("#DEECFF");
                                    u.TextColor = ConvertColor("#3398D8");
                                    u.StatusImage = "/Images/pendingIcon.png";

                                    if (u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim())
                                    {
                                        u.IsAcceptButton = Visibility.Visible; u.IsCancelButton = Visibility.Visible;
                                    }
                                    else
                                    {
                                        u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    }

                                    u.ViewButtonVisible = Visibility.Visible;
                                }
                                if (u.statusID == 2)
                                {
                                    //OnGoing
                                    u.color = ConvertColor("#FFF3D9");
                                    u.TextColor = ConvertColor("#F98926");
                                    u.StatusImage = "/Images/ongoingIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    u.ViewButtonVisible = Visibility.Visible;

                                }
                                if (u.statusID == 3)
                                {
                                    //UpComming
                                    u.color = ConvertColor("#EEE2FF");
                                    u.TextColor = ConvertColor("#9F52FF");
                                    u.StatusImage = "/Images/upcomingIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden;
                                u.IsCancelButton = Visibility.Visible;
                                    u.ViewButtonVisible = Visibility.Hidden;
                                }
                                if (u.statusID == 4)
                                {
                                    //Rejected
                                    u.color = ConvertColor("#FFDFDF");
                                    u.TextColor = ConvertColor("#D42424");
                                    u.StatusImage = "/Images/rejectedIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    u.ViewButtonVisible = Visibility.Hidden;
                                }
                                if (u.statusID == 5)
                                {
                                    //Completed
                                    u.color = ConvertColor("#E8F4D9");
                                    u.TextColor = ConvertColor("#91C84F");
                                    u.StatusImage = "/Images/completedIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    u.ViewButtonVisible = Visibility.Visible;
                                }
                                if (u.statusID == 6)
                                {
                                    //Cancel
                                    u.color = ConvertColor("#FFF2F2");
                                    u.TextColor = ConvertColor("#D42424");
                                    u.StatusImage = "/Images/cancelledIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    u.ViewButtonVisible = Visibility.Hidden;
                                }
                                if (u.statusID == 7)
                                {
                                    //EndFlight
                                    u.color = ConvertColor("#FFDCEF");
                                    u.TextColor = ConvertColor("#C84F90");
                                    u.StatusImage = "/Images/endedIcon.png";
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                    u.ViewButtonVisible = Visibility.Hidden;
                            }
                            if (u.statusID == 8)
                            {
                                //EndFlight
                                u.color = ConvertColor("#FFDCEF");
                                u.TextColor = ConvertColor("#C84F90");
                                u.StatusImage = "/Images/endedIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }


                        });
                        //var dt = requests.userData;
                        //if (dt!= null)
                        //{
                        //  dt=  dt.Where(u=>u.location==Global.PilotLoggedLocation).ToList();
                        //    TodaysRequests = new ObservableCollection<RequestData>(dt);
                        //}
                        var data = requests.userData.OrderByDescending(x => x.updateOn).ThenBy(u => u.addedOn);

                        List<RequestData> tempdata = data.Where(x => x.statusID == 2).ToList();
                        tempdata.AddRange(data.Where(x => x.statusID != 2));
                        

                        TodaysRequests = new ObservableCollection<RequestData>(tempdata);
                        _ = TodaysRequests.OrderByDescending(u => u.updateOn).ThenBy(u => u.addedOn);
                        tempTodayRequest = new ObservableCollection<RequestData>(TodaysRequests);
                        
                        //Console.WriteLine("hello    " + DateTime.Now);
                        return true;
                    }
                }
            
                return false;
            }
            catch (Exception ex)
            {
                log.Error(ex.Message,ex);
                //MessageBox.ShowError(ex.Message);
                return false;
            }
            //IsBusy = false;
            return false;
        }

        public void SearchText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                if(tempTodayRequest!=null)  TodaysRequests = new ObservableCollection<RequestData>(tempTodayRequest);

                if(tempallRequest!=null) AllRequests = new ObservableCollection<RequestData>(tempallRequest);

            }
            else
            {
                if (tempTodayRequest != null)
                    TodaysRequests = new ObservableCollection<RequestData>(tempTodayRequest.Where(x => x.userName.ToLower().Contains(text) || x.contactNo.Contains(text) || x.id.ToString().Contains(text)));

                if (tempallRequest != null)
                    AllRequests = new ObservableCollection<RequestData>(tempallRequest.Where(x => x.userName.ToLower().Contains(text) || x.contactNo.Contains(text) || x.id.ToString().Contains(text)));
            }
        }

        public async Task GetAllRequest(string status)
        {
            IsBusy = true;
            try
            {
                var requests = await _pilotRequestServices.GetAllRequest(status);
                if (requests != null)
                {
                    if (requests.status)
                    {
                        var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                        requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()).ToList();
                        requests.userData.ForEach(u => {
                            u.startDate.ToString("dd/MM/yyyy hh:mm:ss tt");
                            double totalHours = (u.startDate - DateTime.Now).TotalHours;

                            //if (totalHours <= -0.01 && totalHours >= -0.05 && u.statusID == 3)
                            //{
                            //    var result =  _pilotRequestServices.UpdateRideByPilot(new Ride
                            //    {
                            //        statusID = 2,
                            //        addedBy = u.addedBy,
                            //        addedOn = u.addedOn,
                            //        endDate = u.endDate,
                            //        id = u.id,
                            //        videoLink = u.videoLink,
                            //        originLatitude = u.originLatitude,
                            //        originLongitude = u.originLongitude,
                            //        userID = Convert.ToInt32(u.addedBy),
                            //        updateBy = Convert.ToString(Global.loginUserId),
                            //        updateOn = DateTime.UtcNow,
                            //        isActive = u.isActive,
                            //        requestID = u.requestID,
                            //        location = u.location,
                            //        requestOn = u.requestOn,
                            //        startDate = u.startDate,
                            //        status = "OnGoing",
                            //        SecretKey = GenerateRandomCryptographicKey(8),
                            //        ControlKey = GenerateRandomCryptographicKey(8),

                            //    }).ConfigureAwait(false);
                            //    u.statusID = 2;
                            //    u.color = ConvertColor("#FFF3D9");
                            //    u.TextColor = ConvertColor("#F98926");
                            //    u.StatusImage = "/Images/ongoingIcon.png";
                            //    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                            //    u.ViewButtonVisible = Visibility.Visible;

                            //}
                            //double endFlightHours = (u.endDate - DateTime.Now).TotalHours;
                            //if (endFlightHours <= -0.05 && u.statusID != 7 && u.statusID == 2)
                            //{
                            //    var result =  _pilotRequestServices.UpdateRideByPilot(new Ride
                            //    {
                            //        statusID = 5,
                            //        addedBy = u.addedBy,
                            //        addedOn = u.addedOn,
                            //        endDate = u.endDate,
                            //        id = u.id,
                            //        videoLink = u.videoLink,
                            //        originLatitude = u.originLatitude,
                            //        originLongitude = u.originLongitude,
                            //        userID = Convert.ToInt32(u.addedBy),
                            //        updateBy = Convert.ToString(Global.loginUserId),
                            //        updateOn = DateTime.UtcNow,
                            //        isActive = u.isActive,
                            //        requestID = u.requestID,
                            //        location = u.location,
                            //        requestOn = u.requestOn,
                            //        startDate = u.startDate,
                            //        status = "Completed",


                            //    }).ConfigureAwait(false);
                            //    u.statusID = 5;
                            //    u.color = ConvertColor("#E8F4D9");
                            //    u.TextColor = ConvertColor("#91C84F");
                            //    u.StatusImage = "/Images/completedIcon.png";
                            //    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                            //    u.ViewButtonVisible = Visibility.Visible;
                            //}
                            //if (endFlightHours <= -0.5 && u.statusID != 7 && u.statusID != 2 && u.statusID != 5)
                            //{
                            //     _pilotRequestServices.UpdateRideByPilot(new Ride
                            //    {
                            //        statusID = 8,
                            //        addedBy = u.addedBy,
                            //        addedOn = u.addedOn,
                            //        endDate = u.endDate,
                            //        id = u.id,
                            //        videoLink = u.videoLink,
                            //        originLatitude = u.originLatitude,
                            //        originLongitude = u.originLongitude,
                            //        userID = Convert.ToInt32(u.addedBy),
                            //        updateBy = Convert.ToString(Global.loginUserId),
                            //        updateOn = DateTime.UtcNow,
                            //        isActive = u.isActive,
                            //        requestID = u.requestID,
                            //        location = u.location,
                            //        requestOn = u.requestOn,
                            //        startDate = u.startDate,
                            //        status = u.status

                            //    }).ConfigureAwait(false);
                            //    u.color = ConvertColor("#FFDFDF");
                            //    u.TextColor = ConvertColor("#D42424");
                            //    u.StatusImage = "/Images/iconCanceled.png";
                            //    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                            //    u.ViewButtonVisible = Visibility.Hidden;
                            //}
                            if (u.statusID == 1)
                            {
                                //Pending
                                u.color = ConvertColor("#DEECFF");
                                u.TextColor = ConvertColor("#3398D8");
                                u.StatusImage = "/Images/pendingIcon.png";

                                if (u.city == selectedCity?.city_Name)
                                {
                                    u.IsAcceptButton = Visibility.Visible; u.IsCancelButton = Visibility.Visible;
                                }
                                else
                                {
                                    u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                }
                                u.ViewButtonVisible = Visibility.Visible;
                            }
                            if (u.statusID == 2)
                            {
                                //OnGoing
                                u.color = ConvertColor("#FFF3D9");
                                u.TextColor = ConvertColor("#F98926");
                                u.StatusImage = "/Images/ongoingIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Visible;

                            }
                            if (u.statusID == 3)
                            {
                                //UpComming
                                u.color = ConvertColor("#EEE2FF");
                                u.TextColor = ConvertColor("#9F52FF");
                                u.StatusImage = "/Images/upcomingIcon.png";
                                u.IsAcceptButton = Visibility.Hidden;
                                u.IsCancelButton = Visibility.Visible;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }
                            if (u.statusID == 4)
                            {
                                //Rejected
                                u.color = ConvertColor("#FFDFDF");
                                u.TextColor = ConvertColor("#D42424");
                                u.StatusImage = "/Images/rejectedIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }
                            if (u.statusID == 5)
                            {
                                //Completed
                                u.color = ConvertColor("#E8F4D9");
                                u.TextColor = ConvertColor("#91C84F");
                                u.StatusImage = "/Images/completedIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Visible;
                            }
                            if (u.statusID == 6)
                            {
                                //Cancel
                                u.color = ConvertColor("#FFF2F2");
                                u.TextColor = ConvertColor("#D42424");
                                u.StatusImage = "/Images/cancelledIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }
                            if (u.statusID == 7)
                            {
                                //EndFlight
                                u.color = ConvertColor("#FFDCEF");
                                u.TextColor = ConvertColor("#C84F90");
                                u.StatusImage = "/Images/endedIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }
                            if (u.statusID == 8)
                            {
                                //EndFlight
                                u.color = ConvertColor("#FFDCEF");
                                u.TextColor = ConvertColor("#C84F90");
                                u.StatusImage = "/Images/endedIcon.png";
                                u.IsAcceptButton = Visibility.Hidden; u.IsCancelButton = Visibility.Hidden;
                                u.ViewButtonVisible = Visibility.Hidden;
                            }
                        });

                        var data = requests.userData.OrderByDescending(x => x.updateOn).ThenBy(u=>u.addedOn);
                        List<RequestData> tempdata = data.Where(x => x.statusID == 2).ToList();
                        tempdata.AddRange(data.Where(x => x.statusID != 2));

                        AllRequests = new ObservableCollection<RequestData>(tempdata);
                        _=AllRequests.OrderByDescending(x => x.updateOn).ThenBy(u => u.addedOn);
                        tempallRequest = new ObservableCollection<RequestData>(AllRequests);
                    }
                }
              
             
              
            }
            catch (Exception ex)
            {
                log.Error(ex.Message,ex);
               //MessageBox.ShowError(ex.Message);
            }
            IsBusy = false;
        }
        public string GenerateRandomCryptographicKey(int keyLength)
        {
            RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            byte[] randomBytes = new byte[keyLength];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
        public async Task<RideResponse>  GetRideById(int status)
        {
            try
            {
                var result = await _pilotRequestServices.GetRideById(status);
                return result;
            }
            catch (Exception ex)
            {

                log.Error(ex.Message,ex);
            }
         return null;
        }

        public async Task<bool> GetStatusChangesResponse(bool isAccepted, int rideId, string reason)
        {
            try
            {
                bool result = false;
                var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                if (user != null)
                {
                     result = await _pilotRequestServices.GetStatusChangesResponse(isAccepted, rideId,user.id, reason);

                }
                

                return result;
            }
            catch (Exception ex)
            {

                log.Error(ex.Message,ex);
            }
            return false; 
        }

         
        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
     
        private CornerRadius ConvertBorderRadius(string radius)
        {
            var converter = new System.Windows.CornerRadiusConverter();
            var result = (CornerRadius)converter.ConvertFromString(radius);
            return result;
        }
        private Thickness ConvertBorderThickness(string border)
        {
            var converter = new System.Windows.ThicknessConverter();
            var result = (Thickness)converter.ConvertFromString(border);
            return result;
        }

        #endregion
    }
}
