using ideaForge.Pages;
using ideaForge.Pages.AdminDashboardPages;
using ideaForge.Pages.DashboardPages;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MahApps.Metro.Controls;
using MonkeyCache.FileStore;
using IdeaForge.Service.IGenericServices;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows.Controls;
using System.Windows.Input;
using UserControl = System.Windows.Controls.UserControl;
using MessageBox = ideaForge.Popups.MessageBox;
using log4net;
using Microsoft.Extensions.DependencyInjection;
using MapControl;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows;
using System.Windows.Media;

namespace ideaForge.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {
        #region Services
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public IPilotRequestServices _pilotRequestServices
         => App.serviceProvider.GetRequiredService<IPilotRequestServices>();
        public IRegisterService _registerService
        => App.serviceProvider.GetRequiredService<IRegisterService>();
        #endregion

        public string ApplicationId { get; set; }

        /// <summary>
        ///  All Properties are here
        /// </summary>
        #region Properties
        private string _markerIcon;

        public string MarkerIcon
        {
            get { return _markerIcon; }
            set { _markerIcon = value;
            OnPropertyChanged(nameof(MarkerIcon)); 
            }
        }


        #region Navbar Properties
        private Visibility _backvisible = Visibility.Hidden, _btnexcelvisibility = Visibility.Hidden, _cityWaterMark = Visibility.Hidden, _cityWaterMarkk = Visibility.Hidden, _statusBorder = Visibility.Hidden, _DashBoardDataStackPanel = Visibility.Hidden, 
                           _headerBarReport = Visibility.Hidden, _headerBar = Visibility.Visible, _datePicker = Visibility.Hidden, _btnExcell = Visibility.Hidden, _btnExcel = Visibility.Hidden, _CityComboBox = Visibility.Hidden;
        private string _cityComboBoxText, _statusLabel;
        public Visibility BackButtonVisibility
        {
            get { return _backvisible; }
            set {
                _backvisible = value;
                OnPropertyChanged(nameof(BackButtonVisibility));
            }
        }
        public Visibility BtnExcelVisibility 
        {
            get => _btnexcelvisibility;
            set 
            {
                _btnexcelvisibility = value;
                OnPropertyChanged(nameof(BtnExcelVisibility));
            }
        }
        public Visibility cityWaterMark
        {
            get => _cityWaterMark;
            set
            {
                _cityWaterMark = value;
                OnPropertyChanged(nameof(cityWaterMark));
            }
        }
        public Visibility DashBoardDataStackPanel
        {
            get => _DashBoardDataStackPanel;
            set
            {
                _DashBoardDataStackPanel = value;
                OnPropertyChanged(nameof(DashBoardDataStackPanel));
            }
        }
        public Visibility datePicker
        {
            get => _datePicker;
            set
            {
                _datePicker = value;
                OnPropertyChanged(nameof(datePicker));
            }
        }
        public Visibility btnExcell
        {
            get => _btnExcell;
            set
            {
                _btnExcell = value;
                OnPropertyChanged(nameof(btnExcell));
            }
        }
        public Visibility headerBar
        {
            get => _headerBar;
            set
            {
                _headerBar = value;
                OnPropertyChanged(nameof(headerBar));
            }
        }
        public Visibility btnExcel
        {
            get => _btnExcel;
            set
            {
                _btnExcel = value;
                OnPropertyChanged(nameof(btnExcel));
            }
        }        
        public Visibility headerBarReport
        {
            get => _headerBarReport;
            set
            {
                _headerBarReport = value;
                OnPropertyChanged(nameof(headerBarReport));
            }
        }
        public Visibility cityWaterMarkk
        {
            get => _cityWaterMarkk;
            set
            {
                _cityWaterMarkk = value;
                OnPropertyChanged(nameof(cityWaterMarkk));
            }
        }
        public Visibility statusBorder
        {
            get => _statusBorder;
            set
            {
                _statusBorder = value;
                OnPropertyChanged(nameof(statusBorder));
            }
        }

        public Visibility CityComboBox
        {
            get => _CityComboBox;
            set
            {
                _CityComboBox = value;
                OnPropertyChanged(nameof(CityComboBox));
            }
        }

        Brush _statusBorderBackground,_statusLabelForeground, _statusBorderBorderBrush;
        public Brush statusBorderBackground 
        {
            get => _statusBorderBackground;
            set
            {
                _statusBorderBackground = value;
                OnPropertyChanged(nameof(statusBorderBackground));
            }
        }

        public Brush statusLabelForeground
        {
            get => _statusLabelForeground;
            set
            {
                _statusLabelForeground = value;
                OnPropertyChanged(nameof(statusLabelForeground));
            }

        }

        public Brush statusBorderBorderBrush 
        {
            get => _statusBorderBorderBrush;
            set 
            {
                _statusBorderBorderBrush = value;
                OnPropertyChanged(nameof(statusBorderBorderBrush));
            }
        }


        Thickness _statusBorderBorderThickness;
        CornerRadius _statusBorderCornerRadius;

        public CornerRadius statusBorderCornerRadius 
        {
            get => _statusBorderCornerRadius;
            set 
            {
                _statusBorderCornerRadius = value;
                OnPropertyChanged(nameof(statusBorderCornerRadius));
            }
        }

        public Thickness statusBorderBorderThickness
        {
            get => _statusBorderBorderThickness;
            set 
            {
                _statusBorderBorderThickness = value;
                OnPropertyChanged(nameof(statusBorderBorderThickness));
            }
        }

        public string statusLabel
        {
            get => _statusLabel;
            set
            {
                _statusLabel = value;
                OnPropertyChanged(nameof(statusLabel));
            }
        }
        public string cityComboBoxText
        {
            get => _cityComboBoxText;
            set 
            {
                _cityComboBoxText = value;
                OnPropertyChanged(nameof(cityComboBoxText));
            }
        }


        #endregion






        private DateTime _date;
        public DateTime date_selected
        {
            get { return _date; }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(date_selected));
            }
        }
        private string _pageLocation;

        public string PageLocation
        {
            get { return _pageLocation; }
            set { _pageLocation = value; }
        }

        private string _statusLogo;

        public string StatusLogo
        {
            get { return _statusLogo; }
            set { _statusLogo = value;
                OnPropertyChanged(nameof(StatusLogo));
            }
        }

        private string _profileImage;

        public string ProfileImage
        {
            get { return _profileImage; }
            set { _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }
        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value;
            OnPropertyChanged(nameof(UserName));
            }
        }
        private bool _isBusy;

        public bool IsBusy
        {
            get { return _isBusy; }
            set { _isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }
        private Visibility _isSearchBarVisible;

        public Visibility IsSearchBarVisible
        {
            get { return _isSearchBarVisible; }
            set { _isSearchBarVisible = value;
                OnPropertyChanged(nameof(IsSearchBarVisible));
            }
        }

        private string _pageName;

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value;
                OnPropertyChanged(nameof(PageName));
            }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        private UserControl _currentPage;

        public UserControl CurrentPage
        {
            get { return _currentPage; }
            set { _currentPage = value;
            OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public UserDatum _selectedCity = new UserDatum();
        public UserDatum SelectedCity
        {
            get { return _selectedCity; }
            set
            {
                _selectedCity = value; OnPropertyChanged(nameof(SelectedCity));

            }
        }

        private int _selectedLocationId;

        public int SelectedLocationId
        {
            get { return _selectedLocationId; }
            set
            {
                _selectedLocationId = value;
                OnPropertyChanged(nameof(SelectedLocationId));
            }
        }

        private string _keyPressUpdateReports;

        public string KeyPressUpdateReports
        {
            get { return _keyPressUpdateReports; }
            set { _keyPressUpdateReports = value;
                OnPropertyChanged(nameof(KeyPressUpdateReports));
            }
        }

        #endregion
        #region Observable Collections
        private ObservableCollection<HamburgerMenuGlyphItem> _menuItems;

        public ObservableCollection<HamburgerMenuGlyphItem> MenuItems
        {
            get { return _menuItems; }
            set { _menuItems = value; }
        }

        #endregion
        #region Construtor
        public DashboardViewModel()
        {
            MarkerIcon = "/Images/markerIcon.png";
            try
            {
                date_selected = DateTime.Now;
                GetCityList().ConfigureAwait(false);
                  

            }
            catch (Exception)
            {

                throw;
            }

            //  CloseAllWindows();
            //ApplicationId = Barrel.ApplicationId;
            PageName = "IF Dock";
            var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.image))
                    ProfileImage = "/Images/profile.png";
                else
                ProfileImage =UrlHelper.baseURL+ user.image;
                UserName= user.name;

                if(user.roleID == 2)
                {
                    if (user.isActive)
                    {
                        #region MenuItems
                        MenuItems = new ObservableCollection<HamburgerMenuGlyphItem>();

                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteIFDockMenu),
                            Glyph = "/Images/anchor.png",
                            Label = "IF Dock"
                        });
                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteRequestPage),
                            Glyph = "/Images/request.png",
                            Label = "Requests"
                        });

                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteReportsPage),
                            Glyph = "/Images/nounReport.png",
                            Label = "Report"
                        });
                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteIFProfilePage),
                            Glyph = "/Images/nounProfile.png",
                            Label = "Profile"
                        });
                        #endregion
                    }
                    else
                    {
                        MenuItems = new ObservableCollection<HamburgerMenuGlyphItem>();
                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteIFDockMenu),
                            Glyph = "/Images/iconAdminIFDock.png",
                            Label = "Home"
                        });
                        MenuItems.Add(new HamburgerMenuGlyphItem
                        {
                            Command = new DelegateCommand(CanExecuteIFProfilePage),
                            Glyph = "/Images/nounProfile.png",
                            Label = "Profile"
                        });
                        MarkerIcon = "";
                    }

                }
                else //Admin Pages 
                {

                    #region MenuItems
                    MenuItems = new ObservableCollection<HamburgerMenuGlyphItem>();

                    MenuItems.Add(new HamburgerMenuGlyphItem
                    {
                        Command = new DelegateCommand(CanExecuteIFDockMenu),
                        Glyph = "/Images/iconAdminIFDock.png",
                        Label = "IF Dock"
                    });
                    MenuItems.Add(new HamburgerMenuGlyphItem
                    {
                        Command = new DelegateCommand(CanExecuteRequestPage),
                        Glyph = "/Images/request.png",
                        Label = "Registration"
                    });
                 
                    MenuItems.Add(new HamburgerMenuGlyphItem
                    {
                        Command = new DelegateCommand(CanExecuteUserManagementPage),
                        Glyph = "/Images/iconAdminUserManagement.png",
                        Label = "User Management"
                    });
                    MenuItems.Add(new HamburgerMenuGlyphItem
                    {
                        Command = new DelegateCommand(CanExecuteReportsPage),
                        Glyph = "/Images/iconAdminReport.png",
                        Label = "Report"
                    });
                    MenuItems.Add(new HamburgerMenuGlyphItem
                    {
                        Command = new DelegateCommand(CanExecuteIFProfilePage),
                        Glyph = "/Images/iconAdminProfile.png",
                        Label = "Profile"
                    });
                    #endregion
                }
            }
            CurrentPage = new System.Windows.Controls.UserControl();
            if (Global.inactive || Global.RoleID == 3)
            {
                var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                PageLocation = selectedCity?.city_Name;
                
                if (Global.RoleID == 2)
                {
                    PageName = "IF Dock";
                    CurrentPage.Content = new Requests();
                }
                if (Global.RoleID == 3)
                {
                    PageName = "Admin IF Dock";
                    CurrentPage.Content = new AdminIFDockPage();
                }
                _IFDockMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);

                //CurrentPage = new RequestsPage();
                _RequestMenuCommand = new DelegateCommand(CanExecuteRequestPage);

                //CurrentPage = new ProfilePage();
                _ProfileMenuCommand = new DelegateCommand(CanExecuteIFProfilePage);
                //_ProfileMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);

                _ReportMenuCommand = new DelegateCommand(CanExecuteReportsPage);

                _UserManagementPage = new DelegateCommand(CanExecuteUserManagementPage);
            }
            else
            {
                PageName = "IF Dock";
                CurrentPage.Content = new InactivePage();
                _ProfileMenuCommand = new DelegateCommand(CanExecuteIFProfilePage);
            }
            

            //_selectionCommand = new DelegateCommand(CanExecuteUserManagementPageWithData);
        }
        #endregion
        /// <summary>
        /// Commands
        /// </summary>
        #region Commands
        private readonly DelegateCommand _IFDockMenuCommand;
        public ICommand IFDockMenuCommand => _IFDockMenuCommand;

        private readonly DelegateCommand _RequestMenuCommand;
        public ICommand RequestMenuCommand => _RequestMenuCommand;

        private readonly DelegateCommand _ReportMenuCommand;
        public ICommand ReportMenuCommand => _ReportMenuCommand;

        private readonly DelegateCommand _ProfileMenuCommand;
        public ICommand ProfileMenuCommand => _ProfileMenuCommand;
        private readonly DelegateCommand _UserManagementPage;
        public ICommand UserManagementPage => _UserManagementPage;
        //private readonly DelegateCommand _selectionCommand;
        //public ICommand SelectionChangeCityCombo => _selectionCommand;
        #endregion
        /// <summary>
        /// Command Methods 
        /// </summary>

        #region CommandMethods
        private void CanExecuteIFDockMenu(object obj)
        {
            if (Global.RoleID == 2)
            {
                headerBar = Visibility.Visible;
                if (Global.inactive)
                {
                    var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    IsBusy = true;
                    if (dashboard != null)
                    {
                        DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                        vm.DashBoardDataStackPanel = System.Windows.Visibility.Visible;
                        vm.CityComboBox = System.Windows.Visibility.Hidden;
                    }
                    PageName = "IF Dock";
                    CurrentPage.Content = new IFDockPage();
                    IsBusy = false;
                }
                else
                {
                    var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    IsBusy = true;
                    if (dashboard != null)
                    {
                        DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                        vm.DashBoardDataStackPanel = System.Windows.Visibility.Visible;
                        vm.CityComboBox = System.Windows.Visibility.Hidden;
                    }
                    PageName = "IF Dock";
                    CurrentPage.Content = new InactivePage();
                    IsBusy = false;
                }
                
            }
            if (Global.RoleID == 3)
            {
                var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                IsBusy = true;
                if (dashboard != null)
                {
                    DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;

                    vm.DashBoardDataStackPanel = System.Windows.Visibility.Visible;
                    vm.CityComboBox = System.Windows.Visibility.Hidden;
                    vm.headerBar = System.Windows.Visibility.Visible;
                    vm.headerBarReport = System.Windows.Visibility.Hidden;
                }
                PageName = "Admin IF Dock";
                CurrentPage.Content = new AdminIFDockPage();
                IsBusy = false;
            }
        }
        private void CanExecuteRequestPage(object obj)
        {
            if(Global.RoleID == 2)
            {
                IsBusy = true;
                PageName = "Request";
                CurrentPage.Content = new Requests();
                IsBusy = false;
            }
            if(Global.RoleID == 3)
            {
                var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                IsBusy = true;
                if (dashboard != null)
                {
                    DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                    vm.DashBoardDataStackPanel = System.Windows.Visibility.Visible;
                    vm.CityComboBox = System.Windows.Visibility.Hidden;
                    vm.headerBar = System.Windows.Visibility.Visible;
                    vm.headerBarReport = System.Windows.Visibility.Hidden;
                }
                PageName = "Registration";
                CurrentPage.Content = new AdminRequestPage();
                IsBusy = false;
            }
        }
        private void CanExecuteIFProfilePage(object obj)
        {
            IsBusy = true;
            PageName = "Profile";
            ProfilePage page = new ProfilePage();
            ProfilePageViewModels vm = page.DataContext as ProfilePageViewModels;
            vm.UpdateProfile += Vm_UpdateProfile;
            CurrentPage.Content = page;
            IsBusy = false;
        }

        private void Vm_UpdateProfile(object sender, EventArgs e)
        {
            try
            {
                UserDataProfile newprofile = (UserDataProfile)sender;
                if (string.IsNullOrEmpty(newprofile.Image))
                    ProfileImage = "/Images/profile.png";
                else
                    ProfileImage = UrlHelper.baseURL + newprofile.Image;

                UserName = newprofile.name;
                UserOTP otp = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
                otp.name = UserName;
                otp.image = newprofile.Image;
                otp.organizationName = newprofile.organizationName;
                otp.departmentName = newprofile.departmentName;
                otp.languageID = Convert.ToInt32(newprofile.languageID);
            
                Barrel.Current.Add<UserOTP>(UrlHelper.pilotOTPURl, otp, TimeSpan.FromDays(365));

            }
            catch (Exception ex)
            {

            }
        }

        private void CanExecuteReportsPage(object obj)
        {
            if (Global.RoleID == 2)
            {
                IsBusy = true;
                PageName = "Reports";
                CurrentPage.Content = new Reports();
                IsBusy = false;
            }
            if (Global.RoleID == 3)
            {
                var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                IsBusy = true;
                if (dashboard != null)
                {
                    DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                    vm.DashBoardDataStackPanel = System.Windows.Visibility.Visible;
                    vm.headerBar = System.Windows.Visibility.Hidden;
                    vm.headerBarReport = System.Windows.Visibility.Visible;
                    vm.CityComboBox = System.Windows.Visibility.Visible;
                    vm.datePicker = System.Windows.Visibility.Visible;
                    vm.btnExcell = System.Windows.Visibility.Visible;
                }
                var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");

                SelectedCity = selectedCity;
                PageName = "Reports";
                CurrentPage.Content = new AdminReportPage();
                IsBusy = false;
            }

        }
        private void CanExecuteUserManagementPage(object obj)
        {
            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            IsBusy = true;
            if (dashboard != null)
            {
                DashboardViewModel vm = (DashboardViewModel)dashboard.DataContext;
                vm.DashBoardDataStackPanel = System.Windows.Visibility.Hidden;
                vm.CityComboBox = System.Windows.Visibility.Visible;
                vm.headerBar = System.Windows.Visibility.Visible;
                vm.headerBarReport = System.Windows.Visibility.Hidden;
            }
            var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");

            SelectedCity = selectedCity;
            PageName = "User Management";
            CurrentPage.Content = new UserManagementPage();
            IsBusy = false;
        }
        //public void CanExecuteUserManagementPageWithData()
        //{
        //    var selectedCityName = SelectedCity;
        //    var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
        //    IsBusy = true;
        //    if (dashboard != null)
        //    {
        //        dashboard.DashBoardDataStackPanel.Visibility = System.Windows.Visibility.Hidden;
        //        dashboard.CityComboBox.Visibility = System.Windows.Visibility.Visible;
        //    }
        //    PageName = "User Management";
        //    CurrentPage.Content = new UserManagementPage(selectedCityName.ToString());
        //    IsBusy = false;
        //}
        #endregion

        #region ObservableCollectionList
        private ObservableCollection<UserDatum> _cityList;

        public ObservableCollection<UserDatum> CityList
        {
            get { return _cityList; }
            set
            {
                _cityList = value;
                OnPropertyChanged(nameof(CityList));
            }
        }
        #endregion

        #region ApiMethods
        public async Task GetCityList()
        {
            try
            {
                var userDatumCities = await _registerService.GetCityList();
                userDatumCities.userData.ForEach(u => u.position = new Location(Convert.ToDouble(u.Latitude), Convert.ToDouble(u.Longitude)));
                CityList = new ObservableCollection<UserDatum>(userDatumCities.userData);
                var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                if (selectedCity == null)
                {
                    Barrel.Current.Add<UserDatum>("SelectedLocation", CityList[0], TimeSpan.FromHours(5));
                }

                //SelectedCity = selectedCity;

                
            }
            catch (Exception ex)
            {

                MessageBox.ShowError(ex.Message);
            }
           
        }
        #endregion
        private void CloseAllWindows()
        {
            //for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
            //App.Current.Windows[intCounter].Hide();
            if (App.Current.Windows[0] is Login)
            {
                //App.Current.Windows[0].Close();
                //App.Current.MainWindow = App.Current.Windows[App.Current.Windows.Count-1];
                //App.Current.MainWindow.Show();
            }
        }
    }
}
