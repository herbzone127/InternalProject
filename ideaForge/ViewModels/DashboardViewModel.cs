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
            try
            {
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
                        Command = new DelegateCommand(CanExecuteUserManagementPage),
                        Glyph = "/Images/nounProfile.png",
                        Label = "User Management"
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
                else //Admin Pages 
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
                        Command = new DelegateCommand(CanExecuteUserManagementPage),
                        Glyph = "/Images/nounProfile.png",
                        Label = "User Management"
                    });
                    #endregion
                }
            }
            var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
            PageLocation = selectedCity?.city_Name;
            CurrentPage = new System.Windows.Controls.UserControl();
            if (Global.RoleID == 2)
            {
                PageName = "IF Dock";
                CurrentPage.Content = new IFDockPage();
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

            _selectionCommand = new DelegateCommand(CanExecuteUserManagementPageWithData);
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
        private readonly DelegateCommand _selectionCommand;
        public ICommand SelectionChangeCityCombo => _selectionCommand;
        #endregion
        /// <summary>
        /// Command Methods 
        /// </summary>

        #region CommandMethods
        private void CanExecuteIFDockMenu(object obj)
        {
            if (Global.RoleID == 2)
            {
                IsBusy = true;
                PageName = "IF Dock";
                CurrentPage.Content = new IFDockPage();
                IsBusy = false;
            }
            if (Global.RoleID == 3)
            {
                var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                IsBusy = true;
                if (dashboard != null)
                {
                    dashboard.DashBoardDataStackPanel.Visibility = System.Windows.Visibility.Visible;
                    dashboard.CityComboBox.Visibility = System.Windows.Visibility.Hidden;
                }
                PageName = "Admin IF Dock";
                CurrentPage.Content = new AdminIFDockPage();
                IsBusy = false;
            }
        }
        private void CanExecuteRequestPage(object obj)
        {
            IsBusy = true;
            PageName = "Request";
            CurrentPage.Content = new Requests();
            IsBusy = false;
        }
        private void CanExecuteIFProfilePage(object obj)
        {
            IsBusy = true;
            PageName = "Profile";
            CurrentPage.Content = new ProfilePage();
            IsBusy = false;
        }
        private void CanExecuteReportsPage(object obj)
        {
            IsBusy = true;
            PageName = "Reports";
            CurrentPage.Content = new Reports();
            IsBusy = false;
        }
        private void CanExecuteUserManagementPage(object obj)
        {
            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            IsBusy = true;
            if (dashboard != null)
            {
                dashboard.DashBoardDataStackPanel.Visibility = System.Windows.Visibility.Hidden;
                dashboard.CityComboBox.Visibility = System.Windows.Visibility.Visible;
            }
            PageName = "User Management";
            CurrentPage.Content = new UserManagementPage("");
            IsBusy = false;
        }
        private void CanExecuteUserManagementPageWithData(object obj)
        {
            var selectedCityName = SelectedCity;
            var dashboard = App.Current.Windows.OfType<Dashboard>().FirstOrDefault();
            IsBusy = true;
            if (dashboard != null)
            {
                dashboard.DashBoardDataStackPanel.Visibility = System.Windows.Visibility.Hidden;
                dashboard.CityComboBox.Visibility = System.Windows.Visibility.Visible;
            }
            PageName = "User Management";
            CurrentPage.Content = new UserManagementPage(selectedCityName.ToString());
            IsBusy = false;
        }
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

                SelectedCity = selectedCity;


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
