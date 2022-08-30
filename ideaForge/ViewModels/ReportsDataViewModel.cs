using ideaForge.Pages.DashboardPages;
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
    public class ReportsDataViewModel : ViewModelBase
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPilotRequestServices _pilotRequestServices
      => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        private ObservableCollection<RequestData> _ridesAcceptedByUsers;

        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;

        public ObservableCollection<RequestData> RidesAcceptedByUsers
        {
            get { return _ridesAcceptedByUsers; }
            set
            {
                _ridesAcceptedByUsers = value;
                OnPropertyChanged(nameof(RidesAcceptedByUsers));
            }
        }

        public ReportsDataViewModel()
        {
           
            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
        }

        public async Task GetReportsByUser(string recordsByUserId)
        {
            //      
            var requests = await _pilotRequestServices.GetAllRequest("");
            if (requests != null)
            {
                if (requests.status)
                {
                    var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()
                    && u.addedBy == recordsByUserId
                    && (u.statusID==2 || u.statusID==4 || u.statusID==5)
                    ).ToList();
                    requests.userData.ForEach(u => {

                        if (u.statusID == 2)
                        {
                            //OnGoing
                            u.status = "Accepted";
                            u.color = ConvertColor("#E8F4D9");
                            u.TextColor = ConvertColor("#91C84F");
                            u.StatusImage = "/Images/completedIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Visible;

                        }
                        if (u.statusID == 4)
                        {
                            //Rejected
                            u.color = ConvertColor("#FFDFDF");
                            u.TextColor = ConvertColor("#D42424");
                            u.StatusImage = "/Images/rejectedIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Visible;
                        }
                        if (u.statusID == 5)
                        {
                            u.status = "Accepted";
                            //Completed
                            u.color = ConvertColor("#E8F4D9");
                            u.TextColor = ConvertColor("#91C84F");
                            u.StatusImage = "/Images/completedIcon.png";
                            u.IsVisibleButton = Visibility.Hidden;
                            u.ViewButtonVisible = Visibility.Visible;
                        }

                    });
                   
                    RidesAcceptedByUsers = new ObservableCollection<RequestData>(requests.userData);
                }
            }
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
                    if (rideDetails.status)
                    {
                        var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                        if (dashboard != null)
                        {
                            var context = (DashboardViewModel)dashboard.DataContext;
                            dashboard.backButton.Visibility = Visibility.Visible;
                            dashboard.btnExcel.Visibility = Visibility.Hidden;
                            context.PageName = $"Booking Id: {selectedRecord.id}";
                            context.CurrentPage.Content = new ReportCompletedPage(rideDetails.userData);
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

        private string _getReportByUserId;

        public string GetReportByUserId
        {
            get { return _getReportByUserId; }
            set
            {
                _getReportByUserId = value;
                OnPropertyChanged(nameof(GetReportByUserId));
            }
        }
        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
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
    }
}
