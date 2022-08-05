using ideaForge.ViewModels;
using IdeaForge.Domain;
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

namespace ideaForge.Pages.DashboardPages
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : UserControl
    {
        BackgroundWorker worker = new BackgroundWorker();
        public Requests()
        {
            InitializeComponent();
        }

        private void DataGrid_Sorting(object sender, DataGridSortingEventArgs e)
        {

        }
       
        private async void DataGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var grid = (DataGrid)sender;
            var selectedRecord = (RequestData)grid.CurrentItem;
            var vModel =(RequestViewModel) this.DataContext;
            vModel.IsBusy = true;
            if(selectedRecord != null)
            {
                var rideDetails = await vModel.GetRideById(selectedRecord.id);
                if (rideDetails.status)
                {
                    var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                    //Window parentWindow = Window.GetWindow(this);
                    //var dashboard = (Dashboard)parentWindow;
                    var context = (DashboardViewModel)dashboard.DataContext;
                    if (selectedRecord.statusID.Equals(1))
                    {
                        dashboard.statusBorder.Visibility = Visibility.Visible;
                        dashboard.statusBorder.Background = ConvertColor("#DEECFF");
                        //dashboard.statusImage.Source = ConvertImageSource("/Images/pendingIcon.png");
                        context.StatusLogo = "/Images/pendingIcon.png";
                        dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                        dashboard.statusLabel.Content = "Pending";
                        dashboard.statusLabel.Foreground = ConvertColor("#3398D8");
                        dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                        dashboard.statusBorder.BorderBrush = ConvertColor("#3398D8");
                        context.PageName = $"Booking Id:{selectedRecord.id}";
                        context.CurrentPage.Content = new PendingRidePage(rideDetails.userData);
                        dashboard.backButton.Visibility = Visibility.Visible;
                    }
                    if (selectedRecord.statusID.Equals(2))
                    {
                        dashboard.statusBorder.Visibility = Visibility.Visible;
                        dashboard.statusBorder.Background = ConvertColor("#FFF3D9");
                        //dashboard.Header.
                        //BitmapImage bitmap = new BitmapImage();
                        //bitmap.BeginInit();
                        //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                        //bitmap.EndInit();
                        //dashboard.statusImage.Source = bitmap;
                        context.StatusLogo = "/Images/ongoingIcon.png";
                        dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                        dashboard.statusLabel.Content = "Ongoing";
                        dashboard.statusLabel.Foreground = ConvertColor("#F98926");
                        dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                        dashboard.statusBorder.BorderBrush = ConvertColor("#FFF3D9");
                        context.PageName = $"Booking Id:{selectedRecord.id}";
                        context.CurrentPage.Content = new OnGoingRidePage(rideDetails.userData);
                        dashboard.backButton.Visibility = Visibility.Visible;
                    }

                    if (selectedRecord.statusID.Equals(5))
                    {
                        dashboard.statusBorder.Visibility = Visibility.Visible;
                        dashboard.statusBorder.Background = ConvertColor("#E8F4D9");
                        context.StatusLogo = "/Images/completedIcon.png";
                        dashboard.statusBorder.CornerRadius = ConvertBorderRadius("6");
                        dashboard.statusLabel.Content = "Completed";
                        dashboard.statusLabel.Foreground = ConvertColor("#91C84F");
                        dashboard.statusBorder.BorderThickness = ConvertBorderThickness("1");
                        dashboard.statusBorder.BorderBrush = ConvertColor("#E8F4D9");
                        context.PageName = $"Booking Id:{selectedRecord.id}";

                        context.CurrentPage.Content = new CompletedRidePage(rideDetails.userData);
                        dashboard.backButton.Visibility = Visibility.Visible;
                    }
                }

            }

            vModel.IsBusy = false;

        }

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
            var tabControl =(TabControl) sender;
            var selectedTab = tabControl.SelectedItem as TabItem;
            var name = selectedTab.Header;
            if (name != null)
            {
                if(name.Equals("All Requests"))
                {
                    var vModel = DataContext as RequestViewModel;
                await vModel.GetAllRequest("");

                }
                if (name.Equals("Today's Requests"))
                {
                    var vModel = DataContext as RequestViewModel;
                    await vModel.GetTodaysRequest("");
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
            var parentWindow = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                if (parentWindow != null)
                {
                    var menu = parentWindow.Content as MahApps.Metro.Controls.HamburgerMenu;
                    if (menu != null)
                    {
                        var selectedItem = (MahApps.Metro.Controls.HamburgerMenuItem)menu.SelectedItem;

                        if (selectedItem.Label!= "Requests")
                        {
                            worker.CancelAsync();
                            e.Cancel = true;
                            return;

                        }
                        if(selectedItem.Label== "Requests")
                        {
                            var selectedTab = rTabControl.SelectedItem as TabItem;
                            var name = selectedTab.Header;
                         if(!name.Equals( "Today's Requests"))
                            {
                                worker.CancelAsync();
                                e.Cancel = true;
                                return;
                            }
                            else
                            {
                                       await   Task.Delay(TimeSpan.FromSeconds(30));
                                

                                        try
                                        {


                                    var vModel = DataContext as RequestViewModel;
                                    var status= await vModel.GetTodaysRequest("");
                                            if (!status)
                                            {
                                                e.Cancel = true;
                                                worker.CancelAsync();
                                                return;
                                            }
                                            
                                        }
                                        catch(Exception ex)
                                        {

                                            e.Cancel = true;
                                            worker.CancelAsync();
                                            return;
                                        }




                                        if (!worker.IsBusy)
                                            worker.RunWorkerAsync();
                                    }






                            
                            

                           


                        }
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
