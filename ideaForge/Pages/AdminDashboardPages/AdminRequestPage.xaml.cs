using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using log4net;
using System;
using System.Collections.Generic;
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
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace ideaForge.Pages.AdminDashboardPages
{
    /// <summary>
    /// Interaction logic for AdminRequestPage.xaml
    /// </summary>
    public partial class AdminRequestPage : UserControl
    {
        public AdminRequestPage()
        {
            InitializeComponent();
        }
        BackgroundWorker worker = new BackgroundWorker();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
        private void DataGrid_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var grid = (DataGrid)sender;
            var selectedRecord = (UserGetdata)grid.CurrentItem;
            var vModel = (AdminRequestViewModel)this.DataContext;
            vModel.IsBusy = true;
            if (selectedRecord != null)
            {
                var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                UserGetdata rideDetails = new UserGetdata();
                rideDetails = selectedRecord;//
                var context = (DashboardViewModel)dashboard.DataContext;
                context.PageName = $"User Request - User Id: {selectedRecord.ID}";
                context.CurrentPage.Content = new userListViewPage(rideDetails);
                context.BackButtonVisibility = Visibility.Visible;



            }
            vModel.IsBusy = false;
        }
        //private async void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        //{
        //    var grid = (DataGrid)sender;
        //    var selectedRecord = (UserGetdata)grid.CurrentItem;
        //    var vModel = (AdminRequestViewModel)this.DataContext;
        //    vModel.IsBusy = true;
        //    if (selectedRecord != null)
        //    {
        //        var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
        //        UserGetdata rideDetails = new UserGetdata();
        //        rideDetails = selectedRecord;//
        //        var context = (DashboardViewModel)dashboard.DataContext;
        //        context.CurrentPage.Content = new userListViewPage(rideDetails);


        //    }   
        //    vModel.IsBusy = false;

        //}

        private Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }
        private CornerRadius ConvertBorderRadius(string radius)
        {
            var converter = new System.Windows.CornerRadiusConverter();
            var result = (CornerRadius)converter.ConvertFromString(radius);
            return result;
        }
        private Thickness ConvertBorderThickness(string border)
        {
            var converter = new System.Windows.ThicknessConverter();
            var result = (Thickness)converter.ConvertFromString(border);
            return result;
        }

        private async void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var tabControl = (TabControl)sender;
            var selectedTab = tabControl.SelectedItem as TabItem;
            var name = selectedTab.Header;
            if (name != null)
            {
                if (name.Equals("User Requests"))
                {
                    var vModel = DataContext as AdminRequestViewModel;
                    await vModel.GetUserDataUserapp();
                    vModel.IsBusy = false;
                }
                if (name.Equals("Pilot Requests"))
                {
                    var vModel = DataContext as AdminRequestViewModel;
                    await vModel.GetUserDataPilotapp();
                    vModel.IsBusy = false;
                    worker.DoWork += worker_DoWork;
                    worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                    worker.ProgressChanged += Worker_ProgressChanged;
                    worker.WorkerReportsProgress = true;
                    worker.WorkerSupportsCancellation = true;
                    if (!worker.IsBusy)
                        worker.RunWorkerAsync();
                }
            }
            
        }
        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;
            if (e.Cancelled)
            {
                return;
            }


        }
        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userObject = e.UserState;
            int percentage = e.ProgressPercentage;
        }
        private async void worker_DoWork(object sender, DoWorkEventArgs e)
        {

            await this.Dispatcher.Invoke(async () =>
            {
                if (!Global.isStoped)
                {
                    var parentWindow = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    if (parentWindow != null)
                    {
                        var menu = parentWindow.Content as MahApps.Metro.Controls.HamburgerMenu;
                        if (menu != null)
                        {
                            var selectedItem = (MahApps.Metro.Controls.HamburgerMenuItem)menu.SelectedItem;

                            if (selectedItem.Label != "Requests")
                            {
                                log.Info("BackgroundWorker is  Closed");
                                worker.CancelAsync();
                                Global.isStoped = true;
                                e.Cancel = true;

                                return;

                            }
                            if (selectedItem.Label == "Requests")
                            {
                                var selectedTab = rTabControl.SelectedItem as TabItem;
                                var name = selectedTab.Header;
                                if (!name.Equals("Today's Requests"))
                                {
                                    log.Info("BackgroundWorker is  Closed");
                                    Global.isStoped = true;
                                    worker.CancelAsync();
                                    e.Cancel = true;
                                    return;
                                }
                                else
                                {
                                    await Task.Delay(TimeSpan.FromSeconds(30));


                                    try
                                    {


                                        var vModel = DataContext as RequestViewModel;
                                        var status = await vModel.GetTodaysRequest("");
                                        log.Info("Today is Request Updating Frequently");
                                        if (!status)
                                        {
                                            log.Info("Today Request not working.");
                                            e.Cancel = true;
                                            Global.isStoped = true;
                                            worker.CancelAsync();
                                            return;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        log.Error(ex.Message, ex);
                                        e.Cancel = true;
                                        Global.isStoped = true;
                                        worker.CancelAsync();
                                        return;
                                    }




                                    if (!worker.IsBusy)
                                        worker.RunWorkerAsync();
                                }












                            }
                        }
                    }
                }
                else
                {
                    if (worker.IsBusy == true && worker.WorkerSupportsCancellation == true)
                    {
                        worker.CancelAsync();
                    }

                    //On DoWork event (UI layer):
                    if ((worker.CancellationPending == true))
                    {
                        e.Cancel = true;
                    }
                }

            });


            //vModel.GetTodaysRequest("").Wait();
        }
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        
        //private ImageSource ConvertImageSource(string source)
        //{
        //    var converter = new ImageSourceConverter();
        //    var location = Directory.GetCurrentDirectory();
        //    var imgAddress = location + source;

        //    var result = (ImageSource)converter.ConvertFromString(imgAddress);
        //    return result;
        //}
    }
}

    

