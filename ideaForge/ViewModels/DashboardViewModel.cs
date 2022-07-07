using ideaForge.Pages;
using ideaForge.Pages.DashboardPages;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace ideaForge.ViewModels
{
    public class DashboardViewModel : ViewModelBase
    {

        /// <summary>
        ///  All Properties are here
        /// </summary>
        #region Properties
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
        #endregion
        #region Construtor
        public DashboardViewModel()
        {
            PageName = "IF Dock";
            var user = Barrel.Current.Get<UserOTP>(UrlHelper.pilotOTPURl);
            if (user != null)
            {
                if (string.IsNullOrEmpty(user.image))
                    ProfileImage = "/Images/profile.png";
                else
                ProfileImage =UrlHelper.baseURL+ user.image;
                UserName= user.name;
            }
            CurrentPage = new UserControl();
            CurrentPage.Content = new IFDockPage();
            _IFDockMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);

            //CurrentPage = new RequestsPage();
            _RequestMenuCommand = new DelegateCommand(CanExecuteRequestPage);

            //CurrentPage = new ProfilePage();
            _ProfileMenuCommand = new DelegateCommand(CanExecuteIFProfilePage);
            //_ProfileMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);
            
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
        #endregion
        /// <summary>
        /// Command Methods 
        /// </summary>
      
        #region CommandMethods
        private void CanExecuteIFDockMenu(object obj)
        {
            IsBusy = true; 
            PageName = "IF Dock";
            CurrentPage.Content = new IFDockPage();
            IsBusy = false; 
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
        #endregion
    }
}
