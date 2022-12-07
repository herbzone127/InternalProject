using ideaForge.ViewModels;
using IdeaForge.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ideaForge.Pages.AdminDashboardPages
{
    /// <summary>
    /// Interaction logic for userListViewPage.xaml
    /// </summary>
    public partial class userListViewPage : UserControl
    {
        public userListViewPage(UserGetdata data)
        {
            InitializeComponent();
            this.DataContext = new AdminUserListViewModel();
            var vModel = (AdminUserListViewModel)DataContext;
            vModel.getUserData = data;
            vModel.requestDate = data.Addedon.ToString("dd MMMM yyyy");
            vModel.requestTime = data.Addedon.ToString("hh:mm tt");
        }
    }
}
