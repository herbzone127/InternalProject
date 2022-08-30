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

namespace ideaForge.ViewModels
{
    public class ReportsViewModel: ViewModelBase
    {
        public IPilotRequestServices _pilotRequestServices
      => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        private ObservableCollection<RequestData> _ridesAcceptedByUsers;

        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;

        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


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
            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
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
                    requests.userData.ForEach(u =>
                    {
                        u.TotalAcceptedRidesByUser = requests.userData.Where(x => x.addedBy == u.addedBy && (x.statusID == 2 || x.statusID==5)).Count();
                        u.TotalRejectedRidesByUser = requests.userData.Where(x => x.addedBy == u.addedBy && x.statusID == 4).Count();
                    });
                    var distinct = requests.userData.GroupBy(u => u.addedBy).Select(x => x.First());
                    RidesAcceptedByUsers = new ObservableCollection<RequestData>(distinct);
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
                            dashboard.btnExcel.Visibility = System.Windows.Visibility.Visible;
                            dashboard.backButton.Visibility = System.Windows.Visibility.Visible;
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
