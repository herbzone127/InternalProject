using ideaForge.Pages.DashboardPages;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace ideaForge.ViewModels
{
    public class UserManagementDetailPageViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IAdminRequestServices _pilotRequestServices => App.serviceProvider.GetRequiredService<IAdminRequestServices>();

        public UserManagementDetailPageViewModel()
        {
            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
            //var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
            GetReportsByUser().ConfigureAwait(false);
        }

        private async void ViewCommandCanExecute(object obj)
        {
            IsBusy = true;
            var selectedRecord = obj as RequestData;
            try
            {
                if (selectedRecord != null)
                {
                    var rideDetails = await GetRideById(selectedRecord.id);
                    if (rideDetails != null) {

                        if (rideDetails.status)
                        {
                            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                            if (dashboard != null)
                            {
                                var context = (DashboardViewModel)dashboard.DataContext;
                                context.BackButtonVisibility = Visibility.Visible;
                                context.btnExcel = Visibility.Visible;
                                context.PageName = $"Booking Id: {selectedRecord.id}";
                                context.CurrentPage.Content = new ReportCompletedPage(rideDetails.userData);
                            }
                        }
                    }
                
                }
            }
            catch (Exception ex)
            {
                log.Error(ex.Message, ex);
            }
            //var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            //if (dashboard != null)
            //{
            //    var context = (DashboardViewModel)dashboard.DataContext;
            //    context.CurrentPage.Content = new ReportsData();
            //}
            IsBusy = false;
        }


        #region Command
        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;
        #endregion

        private ObservableCollection<RequestData> _SelectedUserRides;

        public ObservableCollection<RequestData> SelectedUserRides
        {
            get { return _SelectedUserRides; }
            set
            {
                _SelectedUserRides = value;
                OnPropertyChanged(nameof(SelectedUserRides));
            }
        }
        private RequestData _selectedRequest;

        public RequestData SelectedRequest
        {
            get { return _selectedRequest; 
            }
            set { _selectedRequest = value;

                OnPropertyChanged("SelectedRequest");
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
        public int StatusID { get;  set; }

        public async Task GetReportsByUser()
        {


            IsBusy = true;
            var requests = await _pilotRequestServices.GetAllRequest("");
            if (requests != null)
            {
                if (requests.status)
                {
                    //var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    //var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == SelectedRequest.city?.ToLower()?.Trim()
                    && 
                    u.statusID==StatusID
                    &&
                   ( u.addedBy == SelectedRequest.addedBy)
                    ).ToList();
                    requests.userData.ForEach(u => {
                        if (u.statusID == 1)
                        {
                            //Pending
                            u.color = ConvertColor("#DEECFF");
                            u.TextColor = ConvertColor("#3398D8");
                            u.StatusImage = "/Images/pendingIcon.png";

                            if (Global.IsIFDockStatus && u.city == SelectedRequest.city?.ToLower()?.Trim())
                            {
                                u.IsVisibleButton = Visibility.Visible;
                            }
                            else
                            {
                                u.IsVisibleButton = Visibility.Hidden;
                            }
                            u.ViewButtonVisible = Visibility.Visible;
                        }
                        if (u.statusID == 2)
                        {
                            //OnGoing
                            u.color = ConvertColor("#FFF3D9");
                            u.TextColor = ConvertColor("#F98926");
                            u.StatusImage = "/Images/ongoingIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Hidden;

                        }
                        if (u.statusID == 3)
                        {
                            //UpComming
                            u.color = ConvertColor("#EEE2FF");
                            u.TextColor = ConvertColor("#9F52FF");
                            u.StatusImage = "/Images/upcomingIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Hidden;
                        }
                        if (u.statusID == 4)
                        {
                            //Rejected
                            u.color = ConvertColor("#FFDFDF");
                            u.TextColor = ConvertColor("#D42424");
                            u.StatusImage = "/Images/rejectedIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Hidden;
                        }
                        if (u.statusID == 5)
                        {
                            //Completed
                            u.color = ConvertColor("#E8F4D9");
                            u.TextColor = ConvertColor("#91C84F");
                            u.StatusImage = "/Images/completedIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Visible;
                        }
                    });
                    SelectedUserRides = new ObservableCollection<RequestData>(requests.userData);
                }

            }
            IsBusy = false;
        }
        public async Task<RideResponse> GetRideById(int status)
        {
            try
            {
                var result = await _pilotRequestServices.GetRideById(status);
                return result;
            }
            catch (Exception ex)
            {

                log.Error(ex.Message, ex);
            }
            return null;
        }
        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
    }
}
