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

        private void CanExecuteIFDockMenu(object obj)
        {
            IsBusy = true;
            CurrentPage.Content = new IFDockPage();
            IsBusy = false;
        }
        private void CanExecuteRequestPage(object obj)
        {
            IsBusy = true;
            CurrentPage.Content = new Requests();
            IsBusy = false;
        }
        private void CanExecuteIFProfilePage(object obj)
        {
            IsBusy = true;
            CurrentPage.Content = new ProfilePage();
            IsBusy = false;
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

        public DashboardViewModel()
        {
            CurrentPage = new UserControl();
            CurrentPage.Content = new IFDockPage();
            _IFDockMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);

            //CurrentPage = new RequestsPage();
            _RequestMenuCommand = new DelegateCommand(CanExecuteRequestPage);

            //CurrentPage = new ProfilePage();
            _ProfileMenuCommand = new DelegateCommand(CanExecuteIFProfilePage);
            //_ProfileMenuCommand = new DelegateCommand(CanExecuteIFDockMenu);
            
        }

        private readonly DelegateCommand _IFDockMenuCommand;
        public ICommand IFDockMenuCommand => _IFDockMenuCommand;

        private readonly DelegateCommand _RequestMenuCommand;
        public ICommand RequestMenuCommand => _RequestMenuCommand;

        private readonly DelegateCommand _ReportMenuCommand;
        public ICommand ReportMenuCommand => _ReportMenuCommand;

        private readonly DelegateCommand _ProfileMenuCommand;
        public ICommand ProfileMenuCommand => _ProfileMenuCommand;
    }
}
