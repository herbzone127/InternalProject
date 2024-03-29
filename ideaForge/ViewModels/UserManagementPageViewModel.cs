﻿using DocumentFormat.OpenXml.Office2010.CustomUI;
using ideaForge.Pages.AdminDashboardPages;
using ideaForge.Pages.DashboardPages;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using UserControl = System.Windows.Controls.UserControl;

namespace ideaForge.ViewModels
{
    public class UserManagementPageViewModel : ViewModelBase
    {
        public IAdminRequestServices _pilotRequestServices => App.serviceProvider.GetRequiredService<IAdminRequestServices>();
        private ObservableCollection<RequestData> _ridesAcceptedByUsers;

        private UserControl _currentPage;

        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;


        private RequestData _SelectedRideByUser;

        public RequestData SelectedRideByUser
        {
            get { return _SelectedRideByUser; }
            set { _SelectedRideByUser = value; 
            OnPropertyChanged(nameof(SelectedRideByUser));  
            }
        }


        public UserControl CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }
        public UserManagementPageViewModel()
        {
            var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");

            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;

            if (dashboard != null)
            {
                var cityData = vm.CityList;
                var index = cityData.FirstOrDefault(u => u.id == selectedCity.id);
                vm.SelectedCity = index;
            }
            GetReportsByUser(selectedCity?.city_Name?.ToLower()?.Trim()).ConfigureAwait(false);

            _viewCommand = new DelegateCommand(ViewCommandCanExecute);
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
        {
            IsBusy = true;
            var requests = await _pilotRequestServices.GetAllRidesByPilotStatus(selectedCityName);
            if (requests != null)
            {
                if (requests.status)
                {
                    //var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                 
                    requests.userData = requests.userData.Where(u => u.city?.ToLower()?.Trim() == selectedCityName).ToList();
                    
                    requests.userData.ForEach(u =>
                    
                    {
                        u.TotalAcceptedRidesByUser = requests.userData.Where(x => x.updateBy == u.updateBy && (x.statusID == 2 || x.statusID== 5 || x.statusID==7)).Count();
                        u.TotalRejectedRidesByUser = requests.userData.Where(x => x.updateBy == u.updateBy && x.statusID == 4).Count();
                        u.TotalServiceByUser= requests.userData.Where(x => x.updateBy == u.updateBy && x.statusID == 5 || x.statusID==7).Count();
                        u.TotalRequestedByUser = requests.userData.Where(x => x.updateBy == u.addedBy && x.statusID == 1).Count();
                    });
                    var distinct = requests.userData.GroupBy(u => u.updateBy).Select(x => x.First());
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

        private async void ViewCommandCanExecute(object obj)
        {
            var selectedRecord=SelectedRideByUser;
            int statusID = Convert.ToInt32(obj);
            IsBusy = true;
            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            if (dashboard != null)
            {
             var context = (DashboardViewModel)dashboard.DataContext;
             context.PageName = "User Management Detail";
                context.btnExcel = System.Windows.Visibility.Visible;
             var detailPage= new UserManagementDetailPage();
             var vModel= detailPage.DataContext as UserManagementDetailPageViewModel;
             vModel.SelectedRequest = selectedRecord;
             vModel.StatusID = statusID;
             context.CurrentPage.Content = detailPage;
                context.BackButtonVisibility = Visibility.Visible;
                  
            }
                    
                
            
            IsBusy = false;
        }
    }
}
