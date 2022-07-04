using MahApps.Metro.Controls;
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
using System.Windows.Shapes;

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : MetroWindow
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            string menuItem = ((MahApps.Metro.Controls.HamburgerMenuItem)args.InvokedItem).Label;

            if(menuItem == "Profile")
            {
                this.HamburgerMenuControl.Content = new Pages.ProfilePage();
            }
            else
            {
                this.HamburgerMenuControl.Content = new Pages.DashboardPages.CompletedRidePage();
            }

           
        }
    }
}
