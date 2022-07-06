using ideaForge.Pages;
using ideaForge.Pages.DashboardPages;
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
            PageName = "IF Dock";
            CurrentPage.Content = new IFDockPage();
        }
        private void CanExecuteRequestPage(object obj)
        {
            PageName = "Request";
            CurrentPage.Content = new Requests();
        }
        private void CanExecuteIFProfilePage(object obj)
        {
            PageName = "Profile";
            CurrentPage.Content = new ProfilePage();
        }
        #endregion
    }
}
