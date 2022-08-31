using ClosedXML.Excel;
using ideaForge.Models;
using ideaForge.Pages.DashboardPages;
using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MahApps.Metro.Controls;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            btnExcel.Visibility = Visibility.Hidden;
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
            if (value.Label== "Report")
            {

                btnExcel.Visibility= Visibility.Visible;


            }
            else
            {
                btnExcel.Visibility = Visibility.Hidden;
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
            if (vModel.PageName.Contains("Booking"))
            {
                var selectedRecord = vModel.CurrentPage.Content as ReportCompletedPage;  
                if (selectedRecord != null)
                {
                    vModel.CurrentPage.Content = new ReportsData(selectedRecord._ride);
                    vModel.PageName = "Report Details";
                    backButton.Visibility = Visibility.Visible;
                }
                else
                {
                    vModel.CurrentPage.Content = new Requests();
                    vModel.PageName = "Requests";
                }
                
            }else
            if (vModel.PageName == "User Management Detail")
            {
                vModel.CurrentPage.Content = new UserManagementPage();
                vModel.PageName = "User Management";
            }
            else if (vModel.PageName== "Report Details")
            {
                vModel.CurrentPage.Content = new Reports();
                vModel.PageName = "Reports";
            }
            else
            {
                vModel.CurrentPage.Content = new Requests();
                vModel.PageName = "Requests";
            }
            
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

        private void btnExcel_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.SaveFileDialog saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            saveFileDialog.Filter = "Excel |*.xlsx";
            var dialog = saveFileDialog.ShowDialog();
            if (dialog== System.Windows.Forms.DialogResult.OK)
            {
                var vModel = (DashboardViewModel)this.DataContext;
                Global.isStoped = true;

                var pageName = vModel.PageName;
                if (pageName == "Reports")
                {
                    var result = (Reports)vModel.CurrentPage.Content;
                    var rModel = (ReportsViewModel)result.DataContext;
                    List<ReportsToExcel> lst = new List<ReportsToExcel>();
                    rModel.RidesAcceptedByUsers.ToList().ForEach(u => {
                        lst.Add(new ReportsToExcel{PilotName=u.userName,FilghtAccepted=u.TotalAcceptedRidesByUser,FlightRejected=u.TotalRejectedRidesByUser });
                    });
                    string fileName = "reports.xlsx";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var dt = IdeaForge.Core.ListtoDataTableConverter.ToDataTable(lst);
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            //Return xlsx Excel File  
                            File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                            ideaForge.Popups.MessageBox.ShowSuccess("File Download"," Successfully");
                        }
                    }
                }
                if (pageName == "Report Details")
                {
                    var result = (ReportsData)vModel.CurrentPage.Content;
                    var rModel = (ReportsDataViewModel)result.DataContext;
                    List<ReportDetailsToExcel> lst = new List<ReportDetailsToExcel>();
                    rModel.RidesAcceptedByUsers.ToList().ForEach(u => {
                        var model = new ReportDetailsToExcel
                        {
                            BookingId = u.id,
                            ContactNo = u.contactNo,
                            UserName = u.userName,
                            DateTime = u.startDate.ToShortDateString(),
                            StartTime = u.startDate.ToShortTimeString(),
                            EndTime = u.endDate.ToShortTimeString(),
                            IFDock = u.location,


                        };
                        if(u.statusID==2|| u.statusID == 5)
                        {
                            model.Status = "Accepted";
                        }
                        else
                        {
                            model.Status = "Rejected";
                        }
                        lst.Add(model); 
                    });
                    string fileName = "reports.xlsx";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        var dt = IdeaForge.Core.ListtoDataTableConverter.ToDataTable(lst);
                        wb.Worksheets.Add(dt);
                        using (MemoryStream stream = new MemoryStream())
                        {
                            wb.SaveAs(stream);
                            //Return xlsx Excel File  
                            File.WriteAllBytes(saveFileDialog.FileName, stream.ToArray());
                            ideaForge.Popups.MessageBox.ShowSuccess("File Download", " Successfully");
                        }
                    }
                }
            }
             
          
        }

        private void CityComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(cityComboBox.Text))
            {
                cityWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                cityWaterMark.Visibility = Visibility.Hidden;
            }
        }

        private void cityComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vModel = this.DataContext as DashboardViewModel;
            var currentPage = vModel.CurrentPage.Content as UserManagementPage;
            if(currentPage != null)
            {
                var userViewModel = currentPage.DataContext as UserManagementPageViewModel;
                userViewModel.GetReportsByUser(vModel.SelectedCity?.city_Name?.ToLower()?.Trim()).ConfigureAwait(false);
                //userViewModel.RidesAcceptedByUsers = new ObservableCollection<IdeaForge.Domain.RequestData>();
            }
           
        }
    }
}
