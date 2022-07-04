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
    internal class DashboardViewModel:ViewModelBase
    {
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
            CurrentPage = new IFDockPage();
        }

    }
}
