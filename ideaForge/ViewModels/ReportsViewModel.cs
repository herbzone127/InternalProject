using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using ideaForge.Pages.DashboardPages;
using MonkeyCache.FileStore;
using System.Windows.Input;
using log4net;
using ideaForge.Models;
using System.Globalization;

namespace ideaForge.ViewModels
{
    public class ReportsViewModel: ViewModelBase
    {
        public IPilotRequestServices _pilotRequestServices
      => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        private ObservableCollection<RequestData> _ridesAcceptedByUsers;
        public RequestFilter FiltersData;
        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;

        private readonly DelegateCommand _applyfilter;
        public ICommand ApplyFilter => _applyfilter;

        private readonly DelegateCommand _clearfilter;
        public ICommand ClearFilter => _clearfilter;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        private RequestData _SelectedRideByPilot;

        public RequestData SelectedRideByPilot
        {
            get { return _SelectedRideByPilot; }
            set { _SelectedRideByPilot = value; 
            OnPropertyChanged(nameof(SelectedRideByPilot)); 
            }
        }

        public List<RequestData> tempData;
        public ObservableCollection<RequestData> RidesAcceptedByUsers
        {
            get { return _ridesAcceptedByUsers; }
            set { _ridesAcceptedByUsers = value;
                OnPropertyChanged(nameof(RidesAcceptedByUsers));
            }
        }

        public ReportsViewModel()
        {
            GetReportsByUser().ConfigureAwait(false);
            tempData = new List<RequestData>();
            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
            _applyfilter = new DelegateCommand(ApplyFilterCommand);
            _clearfilter = new DelegateCommand(DoClearFilter);
            FiltersData = new RequestFilter();
            FiltersData.SelectedStatus = new List<Status>();
        }

        public event EventHandler CloseFilter;

        private void DoClearFilter(object obj)
        {
            if (tempData != null)
                RidesAcceptedByUsers = new ObservableCollection<RequestData>(tempData);

           
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

        void FilterAll(ObservableCollection<RequestData> temprequestdata)
        {
            var newCultureInfo = new CultureInfo("hi-IN");

            if (temprequestdata.Count <= 0) return;
            List<RequestData> tempdata = new List<RequestData>();

            DateTime now = DateTime.Parse(FiltersData.StartDate.ToString(), newCultureInfo);
            tempdata = temprequestdata.Where(x =>
             GetParseDateTime(x.startDate) >= GetParseDateTime((DateTime)FiltersData.StartDate)
            && GetParseDateTime(x.endDate) <= GetParseDateTime((DateTime)FiltersData.EndDate)).ToList();

            if (!string.IsNullOrEmpty(FiltersData.SearchLocation))
            {
                tempdata = tempdata.Where(x => x.location.ToLower().Contains(FiltersData.SearchLocation.ToLower())).ToList();
            }

            List<RequestData> tempStatus = new List<RequestData>();
            foreach (Status item in FiltersData.SelectedStatus)
            {
                tempStatus.AddRange(tempdata.Where(x => x.status.ToLower() == Enum.GetName(typeof(Status), item).ToLower()).ToList());
            }
            if (FiltersData.SelectedStatus.Count > 0) tempdata = tempStatus;

            RidesAcceptedByUsers = new ObservableCollection<RequestData>(tempdata);

        }


        public async void ApplyFilterCommand(object obj)
        {
            if (tempData != null) FilterAll(new ObservableCollection<RequestData>(tempData));
          
            if (CloseFilter != null)
            {
                CloseFilter(new object(), new EventArgs());
            }
        }

        public async Task GetReportsByUser()
        {
            IsBusy = true;
            var requests = await _pilotRequestServices.GetAllRequest("");
            if (requests != null)
            {
                if (requests.status)
                {
                    var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()).ToList();
                    int count = 1;
                    requests.userData.ForEach(u =>
                    {
                       
                        u.TotalAcceptedRidesByUser = requests.userData.Where(x => x.updateBy == u.updateBy && (x.statusID == 2 || x.statusID == 3 || x.statusID==5 || u.statusID== 7)).Count();
                        u.TotalRejectedRidesByUser = requests.userData.Where(x => x.updateBy == u.updateBy && x.statusID == 4).Count();
                    });
                    var distinct = requests.userData.GroupBy(u => u.updateBy).Select(x => x.First());
                    distinct.ToList().ForEach(u => u.SR = count++);
                    RidesAcceptedByUsers = new ObservableCollection<RequestData>(distinct);
                    tempData =new List<RequestData>(distinct);
                }
            }
            IsBusy = false;
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
                            DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                            vm.btnExcel = System.Windows.Visibility.Visible;
                            vm.BackButtonVisibility = System.Windows.Visibility.Visible;
                            var context = (DashboardViewModel)dashboard.DataContext;
                            context.PageName = "Report Details";
                            context.CurrentPage.Content = new ReportsData(rideDetails.userData);
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

        internal void ClearFilters()
        {
            RidesAcceptedByUsers = new ObservableCollection<RequestData>(tempData);
        }

        internal void FilterReport(int selectedview)
        {
            var lst = tempData.Take(selectedview).ToList();
            RidesAcceptedByUsers = new ObservableCollection<RequestData>(lst);
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
    }
}
