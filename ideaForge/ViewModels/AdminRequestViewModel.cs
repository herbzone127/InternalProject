using ideaForge.Pages.AdminDashboardPages;
using ideaForge.Popups;
using IdeaForge.Domain;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using MessageBox = ideaForge.Popups.MessageBox;

namespace ideaForge.ViewModels
{
    public class AdminRequestViewModel : ViewModelBase
    {

        #region Services
        public IAdminRequestPage _adminService
         => App.serviceProvider.GetRequiredService<IAdminRequestPage>();
        #endregion

        #region Commands
        //RegisterCommand
        private readonly DelegateCommand _AcceptedCommand;
        public ICommand AcceptedCommand => _AcceptedCommand;
        
        private readonly DelegateCommand _RejectedCommand;
        public ICommand RejectedCommand => _RejectedCommand;
        private readonly DelegateCommand _viewCommand;
        public ICommand ViewCommand => _viewCommand;
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
        #region ObservableCollections
        private ObservableCollection<UserGetdata> _getAllUserapp;

        public ObservableCollection<UserGetdata> GetAllUserapp
        {
            get { return _getAllUserapp; }
            set
            {
                _getAllUserapp = value;
                OnPropertyChanged(nameof(GetAllUserapp));

            }
        }

        private ObservableCollection<UserGetdata> _getAllUserPilot;

        public ObservableCollection<UserGetdata> GetAllUserappPilot
        {
            get { return _getAllUserPilot; }
            set
            {
                _getAllUserPilot = value;
                OnPropertyChanged(nameof(GetAllUserappPilot));

            }
        }
        private Visibility _popupVisiblity;
        public Visibility popupVisiblity
        {
            get { return _popupVisiblity; }
            set
            {
                _popupVisiblity = value;
                OnPropertyChanged(nameof(popupVisiblity));
            }
        }
         

    private bool _locatioNotAvalable;
    
    public bool locatioNotAvalable
    {
        get { return _locatioNotAvalable; }
        set
        {
            _locatioNotAvalable = value;
            OnPropertyChanged(nameof(locatioNotAvalable));
        }
    }
    
    private bool _ifDocksInactive;
    
    public bool ifDocksInactive
    {
        get { return _ifDocksInactive; }
        set
        {
                _ifDocksInactive = value;
            OnPropertyChanged(nameof(ifDocksInactive));
        }
    }
    private UserGetdata _selecteduser;

        public UserGetdata Selecteduser
        {
            get { return _selecteduser; }
            set
            {
                _selecteduser = value;
                OnPropertyChanged(nameof(Selecteduser));
            }
        }

        private UserGetdata _userData;

        public UserGetdata userData
        {
            get { return _userData; }
            set
            {
                _userData = value;
                OnPropertyChanged(nameof(userData));
                OnTodayRequestSelect(userData);
            }
        }
        #endregion
        private void OnTodayRequestSelect(UserGetdata userData)
        {
            Application.Current.Properties["userData"] = userData;

        }
        public AdminRequestViewModel()
        {
            //GetUserData();
            _AcceptedCommand = new DelegateCommand(AcceptedCommandCanExecute);
            _RejectedCommand = new DelegateCommand(RejectedCommandCanExecute);
        }

        public async Task GetUserDataUserapp()
        {
            try
            {
                var list = await _adminService.GetAllUser();
                if(list != null)
                {
                    if (list.status)
                    {
                        int count = 0;
                        List<UserGetdata> getList = new List<UserGetdata>();
                        foreach(var item in list.userData)
                        {
                            UserGetdata user = new UserGetdata();
                          
                            user.ID = item.ID;
                            user.Name = item.Name;
                            user.Email = item.Email;
                            user.LanguageID = item.LanguageID;
                            user.ContactNo = item.ContactNo;
                            user.OrganizationName = item.OrganizationName;
                            user.DepartmentName = item.DepartmentName;
                            user.City = item.City;
                            user.Addedon = item.Addedon;
                            user.Addedondate = item.Addedon.ToString("dd MMMM yyyy");
                            user.Addedontime = item.Addedon.ToString("hh:mm tt");
                            user.RoleID = item.RoleID;
                            user.IsActive = item.IsActive;
                            user.IsApproved = item.IsApproved;
                            user.Image = item.Image;
                            user.IsVisibleButton = Visibility.Visible;
                            user.ViewButtonVisible = Visibility.Hidden;
                            getList.Add(user);
                        }
                        var result = getList.Where(x => x.RoleID == 1 && x.IsApproved != false).OrderByDescending(u => u.Addedon).ToList();
                        result.ForEach(u => {count= count + 1; u.SRNO = count;  });
                        GetAllUserapp = new ObservableCollection<UserGetdata>(result);

                        //GetAllUserappPilot = new ObservableCollection<UserGetdata>(list.userData.Where(x => x.RoleID == 2).ToList());
                        IsBusy = false;
                        
                    }
                }
            }
            catch(Exception ex)
            {

            }
        }
        public async Task GetUserDataPilotapp()
        {
            try
            {
                int count = 0;
                var list = await _adminService.GetAllUser();
                if(list != null) 
                { 
                    if (list.status)
                    {
                        List<UserGetdata> getList = new List<UserGetdata>();
                        foreach (var item in list.userData)
                        {
                            UserGetdata user = new UserGetdata();
                            user.ID = item.ID;
                            user.Name = item.Name;
                            user.Email = item.Email;
                            user.LanguageID = item.LanguageID;
                            user.ContactNo = item.ContactNo;
                            user.OrganizationName = item.OrganizationName;
                            user.DepartmentName = item.DepartmentName;
                            user.City = item.City;
                            user.Addedon = item.Addedon;
                            user.Addedondate = item.Addedon.ToString("dd MMMM yyyy");
                            user.Addedontime = item.Addedon.ToString("hh:mm tt");
                            user.RoleID = item.RoleID;
                            user.IsActive = item.IsActive;
                            user.IsApproved = item.IsApproved;
                            user.Image = item.Image;
                            user.IsVisibleButton = Visibility.Visible;
                            user.ViewButtonVisible = Visibility.Hidden;
                            getList.Add(user);
                        }
                        var result = getList.Where(x => x.RoleID == 2 && x.IsApproved != false).OrderByDescending(u=>u.Addedon).ToList();
                        result.ForEach(x => { count = count + 1; x.SRNO = count; });
                        GetAllUserappPilot = new ObservableCollection<UserGetdata>(result);
                        IsBusy = false;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void AcceptedCommandCanExecute(object obj)
        {
            IsBusy = true;
            var model = (UserGetdata)obj;
            try
            {
               var result = await _adminService.isActive(true, model.ID);
                if(result.status)
                {
                    IsBusy = false;
                    MessageBox.ShowSuccessful("User request for User ID " + model.ID + " accepted.", "");
                    var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                   
                    //Global.isStoped = false;
                    if(model.RoleID== 2)
                        GetAllUserappPilot.Remove(model);
                    if (model.RoleID == 1)
                        GetAllUserapp.Remove(model);
                    var context = (DashboardViewModel)dashboard.DataContext;
                    //context.CurrentPage = new AdminRequestPage();
                    context.PageName = "Registration";
                    context.statusBorder = Visibility.Hidden;
                    context.BackButtonVisibility = Visibility.Hidden;
                }
                else
                {
                    IsBusy = false;
                }
                IsBusy = false;
            }
            catch(Exception ex)
            {
                IsBusy = false;
                MessageBox.ShowError(ex.Message);
            }
        }
        public void ShowDashboard(int id)
        {
            var dialogYes = rejectPopupPage.Show(id);
        }
        private async void RejectedCommandCanExecute(object obj)
        {
            try
            {

                var model = (UserGetdata)obj;
                ShowDashboard(model.ID);
                IsBusy = true;
                //var model = (UserGetdata)obj;
                try
                {
                    var result = await _adminService.isActive(false, model.ID);
                    if (result.status)
                    {
                        IsBusy = false;
                        MessageBox.ShowSuccessful("User request for User ID " + model.ID + " rejected.", "");
                        var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                      
                        //Global.isStoped = false;
                        if (model.RoleID == 2)
                            GetAllUserappPilot.Remove(model);
                        if (model.RoleID == 1)
                            GetAllUserapp.Remove(model);
                        var context = (DashboardViewModel)dashboard.DataContext;
                        context.statusBorder = Visibility.Hidden;
                        context.BackButtonVisibility = Visibility.Hidden;
                        context.CurrentPage = new AdminRequestPage();
                        context.PageName = "Registration";
                    }
                    else
                    {
                        IsBusy = false;
                    }
                    IsBusy = false;
                }
                catch (Exception ex)
                {
                    IsBusy = false;
                    MessageBox.ShowError(ex.Message);
                }
            }
            catch (Exception ex)
            {
                IsBusy = false;
            }
        }
    }
}
