using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ideaForge.ViewModels
{
    public class UserManagementPageViewModel : ViewModelBase
    {
        public IPilotRequestServices _pilotRequestServices => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        private ObservableCollection<RequestData> _ridesAcceptedByUsers;
        public UserManagementPageViewModel()
        {
            GetReportsByUser().ConfigureAwait(false);

        }

        public ObservableCollection<RequestData> RidesAcceptedByUsers
        {
            get { return _ridesAcceptedByUsers; }
            set
            {
                _ridesAcceptedByUsers = value;
                OnPropertyChanged(nameof(RidesAcceptedByUsers));
            }
        }


        public async Task GetReportsByUser(string selectedCityName)
        {//1245
            IsBusy = true;
            var requests = await _pilotRequestServices.GetAllRequest("");
            if (requests != null)
            {
                if (requests.status)
                {
                    //var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                    requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCity?.city_Name?.ToLower()?.Trim()).ToList();
                    requests.userData.ForEach(u =>
                    {
                        u.TotalAcceptedRidesByUser = requests.userData.Where(x => x.addedBy == u.addedBy && (x.statusID == 2 )).Count();
                        u.TotalRejectedRidesByUser = requests.userData.Where(x => x.addedBy == u.addedBy && x.statusID == 4).Count();
                        u.TotalServiceByUser= requests.userData.Where(x => x.addedBy == u.addedBy && x.statusID == 5).Count();
                        u.TotalRequestedByUser = requests.userData.Where(x => x.addedBy == u.addedBy && x.statusID == 1).Count();
                    });
                    var distinct = requests.userData.GroupBy(u => u.addedBy).Select(x => x.First());
                    RidesAcceptedByUsers = new ObservableCollection<RequestData>(distinct);
                }
            }
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

    }
}
