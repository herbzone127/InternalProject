using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using log4net;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
        public BackgroundWorker worker = null;
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        RequestViewModel viewmodel;
        int count = 0;
        DateTime? lastTime =null;
        Brush brush = (Brush)new System.Windows.Media.BrushConverter().ConvertFromString("#91c84f");
        public Requests()
        {
            InitializeComponent();
           Global.isStoped = false;
            viewmodel = this.DataContext as RequestViewModel;
            viewmodel.CloseFilter += Viewmodel_CloseFilter;
            dpstart.SelectedDate = DateTime.Now.AddDays(-7);
            dpend.SelectedDate = DateTime.Now;
            worker = new BackgroundWorker();
            worker.WorkerSupportsCancellation = true;
            worker.WorkerReportsProgress = false;
            worker.DoWork += worker_DoWork;
         
            worker.RunWorkerCompleted += worker_RunWorkerCompleted;
        }

        private void worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            
        }

        private void Viewmodel_CloseFilter(object sender, EventArgs e)
        {
            filteroptions.Visibility = Visibility.Hidden;
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
                if (rideDetails != null)
                {
                    if (rideDetails.status)
                    {
                        var dashboard = Application.Current.Windows.OfType<Dashboard>().FirstOrDefault();
                        //Window parentWindow = Window.GetWindow(this);
                        //var dashboard = (Dashboard)parentWindow;
                        var context = (DashboardViewModel)dashboard.DataContext;
                        if (selectedRecord.statusID.Equals(1))
                        {
                            context.statusBorder = Visibility.Visible;
                            context.statusBorderBackground = ConvertColor("#DEECFF");
                            //dashboard.statusImage.Source = ConvertImageSource("/Images/pendingIcon.png");
                            context.StatusLogo = "/Images/pendingIcon.png";
                            context.statusBorderCornerRadius = ConvertBorderRadius("6");
                            context.statusLabel = "Pending";
                            context.statusLabelForeground = ConvertColor("#3398D8");
                            context.statusBorderBorderThickness = ConvertBorderThickness("1");
                            context.statusBorderBorderBrush = ConvertColor("#3398D8");
                            context.PageName = $"Booking Id:{selectedRecord.id}";
                            context.CurrentPage.Content = new PendingRidePage(rideDetails.userData);
                            context.BackButtonVisibility = Visibility.Visible;
                        }
                        if (selectedRecord.statusID.Equals(2))
                        {
                            context.statusBorder = Visibility.Visible;
                            context.statusBorderBackground = ConvertColor("#FFF3D9");
                            //dashboard.Header.
                            //BitmapImage bitmap = new BitmapImage();
                            //bitmap.BeginInit();
                            //bitmap.UriSource = new Uri("/Images/pendingIcon.png");
                            //bitmap.EndInit();
                            //dashboard.statusImage.Source = bitmap;
                            context.StatusLogo = "/Images/ongoingIcon.png";
                            context.statusBorderCornerRadius = ConvertBorderRadius("6");
                            context.statusLabel = "Ongoing";
                            context.statusLabelForeground = ConvertColor("#F98926");
                            context.statusBorderBorderThickness = ConvertBorderThickness("1");
                            context.statusBorderBorderBrush = ConvertColor("#FFF3D9");
                            context.PageName = $"Booking Id:{selectedRecord.id}";
                            context.CurrentPage.Content = new OnGoingRidePage(rideDetails.userData);
                            context.BackButtonVisibility = Visibility.Visible;
                        }

                        if (selectedRecord.statusID.Equals(5))
                        {
                            context.statusBorder = Visibility.Visible;
                            context.statusBorderBackground = ConvertColor("#E8F4D9");
                            context.StatusLogo = "/Images/completedIcon.png";
                            context.statusBorderCornerRadius = ConvertBorderRadius("6");
                            context.statusLabel = "Completed";
                            context.statusLabelForeground = ConvertColor("#91C84F");
                            context.statusBorderBorderThickness = ConvertBorderThickness("1");
                            context.statusBorderBorderBrush = ConvertColor("#9ACB5D");
                            context.PageName = $"Booking Id:{selectedRecord.id}";

                            context.CurrentPage.Content = new CompletedRidePage(rideDetails.userData);
                            context.BackButtonVisibility = Visibility.Visible;
                        }
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
                if (name.Equals("All Requests"))
                {
                    var vModel = DataContext as RequestViewModel;
                    await vModel.GetAllRequest("");
                    worker.CancelAsync();

                }
                if (name.Equals("Today's Requests"))
                {
                    var vModel = DataContext as RequestViewModel;
                    await vModel.GetTodaysRequest("");
                  
                    if (!worker.IsBusy)
                        worker.RunWorkerAsync();
                }
            }
        }
        private  void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
          
            if (e.Cancelled)
            {
                return;
            }
           
            else
            {
                //if (!worker.IsBusy)
                //{
                //  Task.Delay(TimeSpan.FromSeconds(30)).ContinueWith( (Task t) => {

                //      if (t.IsCompleted)
                //      {
                //          worker.RunWorkerAsync();
                //      }
                  
                //  });
                //}
                 
                //lblStatus.Foreground = Brushes.Green;
                //lblStatus.Text = "Done... Calc result: " + e.Result;
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
                                if (selectedItem == null) return;
                                if (selectedItem.Label != "Requests")
                                {
                                    log.Info("BackgroundWorker is Closed");
                                    worker.CancelAsync();
                                    Global.isStoped = true;

                                    e.Cancel = true;
                                    worker.CancelAsync();
                                    return;




                                }
                                if (worker.CancellationPending == true)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                                var vModel = DataContext as RequestViewModel;

                                var status = await vModel.GetTodaysRequest("");
                                if (status)
                                {

                                }

                                // Thread.Sleep(TimeSpan.FromSeconds(30));





                                log.Info("Today is Request Updating Frequently");
                            }
                        }
                    


                }).ContinueWith((Task t) =>
                {
                    if (t.IsCompleted)
                    {

                        try
                        {
                            if(!worker.CancellationPending)
                            {
                                Thread.Sleep(TimeSpan.FromSeconds(30));
                                worker.RunWorkerAsync();
                            }

                           
                        }
                        catch (Exception)
                        {

                         
                        }
                       

                        
                    }
                   

                });
            
           

                   
                   

                
                
            
           
         


            //vModel.GetTodaysRequest("").Wait();
        }
        private void DataGrid_MouseEnter(object sender, MouseEventArgs e)
        {

        }

        private void moreoption_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            MoreselectOption.Visibility = (MoreselectOption.Visibility == Visibility.Visible) ? Visibility.Hidden : Visibility.Visible;
        }

        private void Close_Filter(object sender, MouseButtonEventArgs e)
        {
          
            filteroptions.Visibility = Visibility.Hidden;
        }

        private void open_filter(object sender, MouseButtonEventArgs e)
        {
            
           
            filteroptions.Visibility = Visibility.Visible;
        }

        private void mousemove_search(object sender, MouseEventArgs e)
        {
            bordersearhbox.Visibility = Visibility.Visible;
        }

        private void UserControl_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.OriginalSource is Border)
            {
                if ((Border)e.OriginalSource == searchborder)
                {
                    bordersearhbox.Visibility = Visibility.Visible;
                    searchborder.Visibility = Visibility.Hidden;
                }

            }
            else if (e.OriginalSource is Image)
            {
                if ((Image)e.OriginalSource == imgsearch)
                {
                    bordersearhbox.Visibility = Visibility.Visible;
                    searchborder.Visibility = Visibility.Hidden;
                }
            }
            else if (tbsearch.IsFocused == false)
            {
                bordersearhbox.Visibility = Visibility.Hidden; 
                searchborder.Visibility = Visibility.Visible;
            }
            e.Handled = true;
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            FocusManager.SetFocusedElement(FocusManager.GetFocusScope(tbsearch), null);
            Keyboard.ClearFocus();
            tbsearch.Text = ""; ;

            //bordersearhbox.Visibility = Visibility.Hidden;

        }

        private void tbsearch_LostFocus(object sender, RoutedEventArgs e)
        {
            searchborder.Visibility = Visibility.Visible;

        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                if (e.OriginalSource.GetType() == typeof(RadioButton))
                {
                    RadioButton radiobutton = (RadioButton)sender;
                    int selectedview = Convert.ToInt16(radiobutton.Tag);
                    viewmodel.ShowCount = selectedview;
                    viewmodel.FilterShowAllRequest();
                    viewmodel.FilterShowTodayRequest();

                }
            }
            catch (Exception ex)
            {
            }
        }

        private void RadioButton_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                RequestViewModel vm = this.DataContext as RequestViewModel;
                vm.ClearFilters();
            }
            catch (Exception ex)
            { 
            }
        }
        private void searchLocation_TextChange(object sender, TextChangedEventArgs e)
        {
            viewmodel.FiltersData.SearchLocation = tbsearchLocation.Text;
        }

        ImageSource GEtIamge(string path)
        {
            BitmapImage bit = new BitmapImage(new Uri(string.Format("pack://application:,,,/{0};component/Images/" + path+".png", Assembly.GetExecutingAssembly().GetName().Name)));
            return bit;
        }
        private void borderongoing_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Border selectedBorder = (Border)sender;
            Label textselected = (Label)FindName("textstatus" + selectedBorder.Tag);
            Image imgicon = (Image)FindName("imgbdr" + selectedBorder.Tag);
            int selectedtagindex = Convert.ToInt32(selectedBorder.Tag);
            if (selectedBorder.BorderBrush == brush)
            {
                viewmodel.FiltersData.SelectedStatus.Remove((Models.Status)selectedtagindex-1);
                selectedBorder.BorderBrush = Brushes.Gray;
                textselected.Foreground = Brushes.Gray;
                imgicon.Source = GEtIamge( "bdr" + selectedBorder.Tag);
            }
            else
            {
                viewmodel.FiltersData.SelectedStatus.Add((Models.Status)selectedtagindex-1);
                selectedBorder.BorderBrush = brush;
                textselected.Foreground = brush;
                imgicon.Source = GEtIamge("bdr" + selectedBorder.Tag+"_green");

            }
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            dpstart.Focus();

            dpstart.IsDropDownOpen = true;
        }

        private void tbsearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            viewmodel.SearchText(tbsearch.Text.ToLower());
        }

        private void dpstart_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dpstart != null )
                if (dpstart.SelectedDate != null)
                {
                   if(dpstart.SelectedDate.Value.Date<= DateTime.Now.Date)
                    {
                        if (dpend.SelectedDate != null)
                        {
                            if (dpstart.SelectedDate.Value.Date > dpend.SelectedDate.Value.Date)
                            {
                                ideaForge.Popups.MessageBox.ShowError("To date can not be less than From date");
                                return;
                            }
                        }
                 
                        viewmodel.FiltersData.StartDate = (DateTime)dpstart.SelectedDate;
                        btnApplyFilter.Visibility = Visibility.Visible;  
                        //var vModel = DataContext as RequestViewModel;

                    }
                    else
                    {
                        viewmodel.FiltersData.StartDate = null;
                        btnApplyFilter.Visibility = Visibility.Hidden;
                        ideaForge.Popups.MessageBox.ShowError("From date can not be greater than To date");
                    }
                   
                }
              
        }

        private void dpend_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dpend!=null)
                if (dpend.SelectedDate != null)
                {
                    if (dpstart.SelectedDate.Value.Date <= dpend.SelectedDate.Value.Date)
                    {
                        btnApplyFilter.Visibility = Visibility.Visible;
                        viewmodel.FiltersData.EndDate = (DateTime)dpend.SelectedDate;
                    }
                    else
                    {
                        viewmodel.FiltersData.StartDate = null;
                        btnApplyFilter.Visibility = Visibility.Hidden;
                        ideaForge.Popups.MessageBox.ShowError("To date can not be less than From date");
                    }
                
                }
                  

        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //var tbl = tbl_TodayRequest;
            //var booking_Id = tbl.Columns[0];
            //BookingIcon.Visibility = Visibility.Hidden;
            //dtIcon.Visibility = Visibility.Visible;


        }

        private void Grid_MouseLeftButtonDown_1(object sender, MouseButtonEventArgs e)
        {
            //var tbl = tbl_TodayRequest;
            //var dateTime = tbl.Columns[3];
            //dtIcon.Visibility = Visibility.Hidden;
            //BookingIcon.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeftButtonDown_2(object sender, MouseButtonEventArgs e)
        {
            //var tbl = tbl_AllRequest;
            //var booking_Id = tbl.Columns[0];
            ////BookingIcon1.Visibility = Visibility.Hidden;
            //dtIcon1.Visibility = Visibility.Visible;
        }

        private void Grid_MouseLeftButtonDown_3(object sender, MouseButtonEventArgs e)
        {
            //var tbl = tbl_TodayRequest;
            //var dateTime = tbl.Columns[3];
            //dtIcon1.Visibility = Visibility.Hidden;
           //BookingIcon1.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                    int selectedview = 7;
                    viewmodel.ShowCount = selectedview;
                    viewmodel.FilterShowAllRequest();
                    viewmodel.FilterShowTodayRequest();
            }
            catch (Exception ex)
            {
            }
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
