using ideaForge.Pages.AdminDashboardPages;
using ideaForge.ViewModels;
using IdeaForge.Service.IGenericServices;
using Microsoft.Extensions.DependencyInjection;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ideaForge.Popups.MessageBox;
using Application = System.Windows.Application;

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for rejectPopupPage.xaml
    /// </summary>
    public partial class rejectPopupPage : Window
    {
        int userId = 0;
        bool isRejectRequest;
        public static string RejectReason;
        #region Services
        public IAdminRequestPage _adminService
         => App.serviceProvider.GetRequiredService<IAdminRequestPage>();
        #endregion
        public rejectPopupPage(int Id, bool IsRejectRequest)
        {
            InitializeComponent();
            if (IsRejectRequest)
            {
                userrequesttext.Content = "Flight request for";
            }
            isRejectRequest = IsRejectRequest;
            userId = Id;
          
            userIdReject.Content = "User ID " + Id; 
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

        static rejectPopupPage cDockAreaPopup;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public static DialogResult Show(int id, bool IsRejectRequest=false)
        {
            try
            {

                
                cDockAreaPopup = new rejectPopupPage(id, IsRejectRequest);
                cDockAreaPopup.DataContext = new AdminRequestViewModel();
                cDockAreaPopup.btnContinue.Content = "Continue";
                //Login = new Window();
                //Login = Application.Current.MainWindow;
               

                cDockAreaPopup.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            Close();
            
            //Application.Current.Shutdown();
            //this.Close();
        }

        private async void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(locatioNotAvail.IsChecked == false && ifDocksInactive.IsChecked == false)
                {
                    validate.Visibility = Visibility.Visible;
                    validate.Height = 30;
                }
                else
                {
                    if (isRejectRequest == false)
                    {
                        var result = await _adminService.isActive(false, userId);
                        if (result.status)
                        {
                            Close();
                            MessageBox.ShowSuccessful("User request for User ID " + userId + " rejected.", "");
                            var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                           
                            var context = (DashboardViewModel)dashboard.DataContext;
                            context.CurrentPage = new AdminRequestPage();
                            context.PageName = "Registration";
                            context.statusBorder = Visibility.Hidden;
                            context.BackButtonVisibility = Visibility.Hidden;
                            //Global.isStoped = false;
                        }
                    }
                    else
                    {
                        RejectReason = "";
                        if(locatioNotAvail.IsChecked == true)     RejectReason += "Location not available";
                        if (ifDocksInactive.IsChecked == true)    RejectReason += " And " + "IF Docks inactive";
                        if (!string.IsNullOrEmpty(Conmments.Text)) RejectReason += " And " + Conmments.Text;
                        result = System.Windows.Forms.DialogResult.Yes;
                        Close(); 
                    }
                }
            }
            catch (Exception ex)
            {
               MessageBox.ShowError(ex.Message);
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }
        private void cLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //lblError.Visibility = Visibility.Hidden;
        }

        private void locatioNotAvail_Checked(object sender, RoutedEventArgs e)
        {
            validate.Visibility = Visibility.Hidden;
            validate.Height = 0;
        }

        private void ifDocksInactive_Checked(object sender, RoutedEventArgs e)
        {
            validate.Visibility = Visibility.Hidden;
            validate.Height = 0;
        }
    }
}
