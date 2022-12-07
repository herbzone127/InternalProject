using ideaForge.Pages.AdminDashboardPages;
using ideaForge.Popups;
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
using MessageBox = ideaForge.Popups.MessageBox;


namespace ideaForge.ViewModels
{
    public class AdminUserListViewModel : ViewModelBase
    {
        #region Services
        public IAdminRequestPage _adminService
         => App.serviceProvider.GetRequiredService<IAdminRequestPage>();
        #endregion
        private UserGetdata _getUserData;

        public UserGetdata getUserData
        {
            get { return _getUserData; }
            set
            {
                _getUserData = value;
                OnPropertyChanged(nameof(getUserData));

            }
        }

        private string _requestDate;
        public string requestDate
        {
            get { return _requestDate; }
            set
            {
                _requestDate = value;
                OnPropertyChanged(nameof(requestDate));

            }
        }
        private string _requestTime;

        public string requestTime
        {
            get { return _requestTime; }
            set
            {
                _requestTime = value;
                OnPropertyChanged(nameof(requestTime));

            }
        }

        private readonly DelegateCommand _saveChanges_Command;
        public ICommand SaveChanges_Command => _saveChanges_Command;

        private readonly DelegateCommand _cancelChanges_Command;
        public ICommand CancelChanges_Command => _cancelChanges_Command;
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
        public AdminUserListViewModel()
        {
            _saveChanges_Command = new DelegateCommand(AcceptedCommandCanExecute);
            _cancelChanges_Command = new DelegateCommand(RejectedCommandCanExecute);
        }

        private async void AcceptedCommandCanExecute(object obj)
        {
            IsBusy = true;
            var model = getUserData;
            try
            {
                var result = await _adminService.isActive(true, model.ID);
                if (result.status)
                {
                    IsBusy = false;
                    MessageBox.ShowSuccessful("User request for User ID " + model.ID + " accepted.", "");
                    var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                    vm.statusBorder = Visibility.Hidden;
                    vm.BackButtonVisibility = Visibility.Hidden;
                    //Global.isStoped = false;
                    var context = (DashboardViewModel)dashboard.DataContext;
                    context.CurrentPage = new AdminRequestPage();
                    context.PageName = "Registration";
                }
                else
                {

                }
                IsBusy = false;
            }
            catch (Exception ex)
            {
                IsBusy = false;
                MessageBox.ShowError(ex.Message);
            }
        }
        public void ShowDashboard(int id)
        {
            var dialogYes = rejectPopupPage.Show(id);
        }
        private void RejectedCommandCanExecute(object obj)
        {
            try
            {
                var model = getUserData;
                ShowDashboard(model.ID);
                //IsBusy = true;
                //var model = getUserData;
                //try
                //{
                //    var result = await _adminService.isActive(false, model.ID);
                //    if (result.status)
                //    {
                //        IsBusy = false;
                //        MessageBox.ShowSuccessful("User request for User ID " + model.ID + " rejected.", "");
                //        var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();

                //        dashboard.statusBorder.Visibility = Visibility.Hidden;
                //        dashboard.backButton.Visibility = Visibility.Hidden;
                //        //Global.isStoped = false;
                //        var context = (DashboardViewModel)dashboard.DataContext;
                //        context.CurrentPage = new AdminRequestPage();
                //        context.PageName = "Registration";
                //    }
                //    else
                //    {

                //    }
                //    IsBusy = false;
                //}
                //catch (Exception ex)
                //{
                //    IsBusy = false;
                //    MessageBox.ShowError(ex.Message);
                //}
            }
            catch (Exception ex)
            {

            }
        }
    }
}
