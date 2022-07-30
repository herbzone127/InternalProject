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
        BackgroundWorker worker = new BackgroundWorker();
        static string pg=string.Empty;
        public Dashboard()
        {
            InitializeComponent();
            this.WindowState = Global.LoginState;
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
               
                worker.DoWork += worker_DoWork;
              
                worker.RunWorkerCompleted += worker_RunWorkerCompleted;
                worker.ProgressChanged += Worker_ProgressChanged;
                worker.WorkerReportsProgress=true;  
                worker.WorkerSupportsCancellation = true;
                worker.RunWorkerAsync();
            }
            else
            {
                
                //worker.CancelAsync();
                
            }
           

        }

        private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            object userObject = e.UserState;
            int percentage = e.ProgressPercentage;
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
            var login = new Login();
            login.Show();
            this.Close();
        }

        private void currentPg_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
        
         
        }

        private void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            var bgWorker = (BackgroundWorker)sender;
            if (e.Cancelled)
            {
                return;
            }
          
           
        }

        private void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (pg!= "Requests")
            {
                e.Cancel = true;
               
            }
            else
            {
                Task.Run(async () =>
                {
                  
                        
                      await Task.Delay(TimeSpan.FromSeconds(30)).ContinueWith(async(task) => {
                          if (task.IsCompleted)
                          {
                              await this.Dispatcher.Invoke(async () =>
                              {
                                  try
                                  {
                                      var model = (DashboardViewModel)this.DataContext;
                                      var rPage = (Requests)model.CurrentPage.Content;
                                      var ucModel = (RequestViewModel)rPage.DataContext;
                                      await ucModel.GetTodaysRequest("");
                                  }
                                  catch 
                                  {

                                      e.Cancel = true;
                                  }
                                  
                              
                                  
                              });
                              worker.RunWorkerAsync();
                          }
                      
                     
                      
                         
                    });

                });
          
                
            }
           
         
             //vModel.GetTodaysRequest("").Wait();
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {
            statusBorder.Visibility = Visibility.Hidden;
            backButton.Visibility = Visibility.Hidden;
            var vModel = (DashboardViewModel)this.DataContext;
            vModel.CurrentPage.Content = new Requests();
            vModel.PageName = "Requests";
        }

        private void loginWindow_Closing(object sender, CancelEventArgs e)
        {
           // CloseAllWindows();
            App.Current.Shutdown();
        }
        private void CloseAllWindows()
        {
            for (int intCounter = App.Current.Windows.Count - 1; intCounter >= 0; intCounter--)
                App.Current.Windows[intCounter].Close();
        }
    }
}
