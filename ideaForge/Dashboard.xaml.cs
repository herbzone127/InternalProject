using ideaForge.Pages.DashboardPages;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using MahApps.Metro.Controls;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
      
        static string pg=string.Empty;
        public Dashboard()
        {
            InitializeComponent();
            this.WindowState = Global.LoginState;
            var login = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if (login != null)
            {
                login.Close();
            }
            
        }
   
        private  void HamburgerMenuControl_OnItemInvoked(object sender, HamburgerMenuItemInvokedEventArgs args)
        {
            statusBorder.Visibility=Visibility.Hidden;
            backButton.Visibility=Visibility.Hidden;
            var menu = (HamburgerMenu)sender;
            var value =(HamburgerMenuGlyphItem) menu.SelectedItem;
            pg = value.Label;
            if (value.Label== "Requests")
            {
               
             
                
            }
            else
            {
                
                //worker.CancelAsync();
                
            }
           

        }

      

        private  void Grid_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if(trayProfile.Visibility == Visibility.Visible)
            {

                //trayProfileImage.Source = new BitmapImage(new Uri(@"/Images/down-arrow.png", UriKind.Relative));
                trayProfile.Visibility = Visibility.Hidden;
            }
            
           else if (trayProfile.Visibility == Visibility.Hidden)
            {
                //trayProfileImage.Source = new BitmapImage(new Uri(@"/Images/iconUp.png", UriKind.Relative));
                trayProfile.Visibility = Visibility.Visible;
                
            }
               
        }

        private void logout_MouseLeftDownUp(object sender, MouseButtonEventArgs e)
        {
            //Barrel.Current.EmptyAll();
            trayProfile.Visibility = Visibility.Hidden;
            Global.IsIFDockStatus = false;
            Barrel.Current.EmptyAll();
            var previousLogin = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if (previousLogin != null)
            {
                previousLogin.Effect = null;
              var dashboard  = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                dashboard.Close();
                previousLogin.WindowState=dashboard.WindowState;
                previousLogin.Show();
            }
            else
            {
                var login = new Login();
                var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                dashboard.Close();
                login.WindowState = dashboard.WindowState;
                login.Show();
            }

            
          
          
        }

        private void currentPg_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        
         
        }

    

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            statusBorder.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            var vModel = (DashboardViewModel)this.DataContext;
            Global.isStoped = false;
            vModel.CurrentPage.Content = new Requests();
            vModel.PageName = "Requests";
        }

        private void loginWindow_Closing(object sender, CancelEventArgs e)
        {
            
           // CloseAllWindows();
            //App.Current.Shutdown();
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            if (trayProfile.Visibility == Visibility.Visible)
            {

                //trayProfileImage.Source = new BitmapImage(new Uri(@"/Images/down-arrow.png", UriKind.Relative));
                trayProfile.Visibility = Visibility.Hidden;
            }

            else if (trayProfile.Visibility == Visibility.Hidden)
            {
                //trayProfileImage.Source = new BitmapImage(new Uri(@"/Images/iconUp.png", UriKind.Relative));
                trayProfile.Visibility = Visibility.Visible;

            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            trayProfile.Visibility = Visibility.Hidden;
        }
    }
}
