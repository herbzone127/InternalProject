using MahApps.Metro.Controls;
using MonkeyCache.FileStore;
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
           

           
        }

        private  void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(trayProfile.Visibility == Visibility.Visible)
            trayProfile.Visibility = Visibility.Hidden;
           else if (trayProfile.Visibility == Visibility.Hidden)
                trayProfile.Visibility = Visibility.Visible;
        }

        private void logout_MouseLeftDownUp(object sender, MouseButtonEventArgs e)
        {
            Barrel.Current.EmptyAll();
            trayProfile.Visibility = Visibility.Hidden;
        }
    }
}
