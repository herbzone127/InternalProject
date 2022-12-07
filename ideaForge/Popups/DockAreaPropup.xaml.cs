using ideaForge.ViewModels;
using IdeaForge.Core.Utilities;
using IdeaForge.Domain;
using MonkeyCache.FileStore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static ideaForge.Popups.MessageBox;
using Application = System.Windows.Application;

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for DockAreaPropup.xaml
    /// </summary>
    public partial class DockAreaPopup : System.Windows.Controls.UserControl
    {
        public DockAreaPopup()
        {
            InitializeComponent();
           /// new ErrorMessageBox("").ShowDialog();
        }

        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

        static DockAreaPopup cDockAreaPopup;
        static Window Login { get; set; }
        static DialogResult result = System.Windows.Forms.DialogResult.No;

        public static DialogResult Show()
        {
            try
            {
                cDockAreaPopup = new DockAreaPopup();
                cDockAreaPopup.DataContext = new IFDockViewModel();
                cDockAreaPopup.btnContinue.Content = "Continue";
                //Login = new Window();
                //Login = Application.Current.MainWindow;
                var selectedCity = Barrel.Current.Get<UserDatum>("SelectedLocation");
                if (selectedCity != null)
                {
                    var vModel = cDockAreaPopup.DataContext as IFDockViewModel;
                    vModel.SelectedCity = selectedCity;
                    vModel.SelectedLocationId = selectedCity.id;
                    //cDockAreaPopup.cLocation.SelectedItem = selectedCity;
                }
                var loginWindow = Application.Current.Windows.OfType<Login>().FirstOrDefault();
                if (loginWindow != null)
                {
                    loginWindow.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
                }

                //cDockAreaPopup.ShowDialog();
               
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            //Close();
            var loginWindow = Application.Current.Windows.OfType<Login>().FirstOrDefault();
            if(loginWindow != null)
            {
                Barrel.Current.EmptyAll();
                if (loginWindow.Effect != null)
                {
                    loginWindow.Effect = null;
                }
                loginWindow.Show();
            }
            else
            {
                var login = new Login();
                login.Show();
            }
           //Application.Current.Shutdown();
          //this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
           
            try
            {
              var selectedITem=  cLocation.SelectedItem as UserDatum;
                if(selectedITem != null)
                {
                    lblError.Visibility = Visibility.Hidden;

                    Barrel.Current.Add<UserDatum>("SelectedLocation", selectedITem, TimeSpan.FromHours(5));
                    result = System.Windows.Forms.DialogResult.Yes;
                    var dashboard = new Dashboard();

                    //cDockAreaPopup.Close();
                    var loginWindow = App.Current.Windows.OfType<Login>().FirstOrDefault();
                    if (loginWindow is Login)
                    {
                        if (loginWindow.Effect != null)
                            loginWindow.Effect = null;
                        loginWindow.Close();
                    }

                    var vModel = this.DataContext as IFDockViewModel;
                    if (vModel != null)
                    {
                        //vModel.IsActive = IsActive;
                        dashboard.Show();
                    }
                        
                    

                }
                else
                {
                    
                    lblError.Visibility = Visibility.Visible;
                }
                
            }
            catch (Exception ex)
            {

                //throw;
            }
          

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }

        private void cLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            lblError.Visibility = Visibility.Hidden;
        }

        private void cLocation_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(cLocation.Text))
            {
                cLocation.Text = "";
            }
            if (string.IsNullOrEmpty(cLocation.Text))
            {
                cityWaterMark.Visibility = Visibility.Visible;
            }
            else
            {
                cityWaterMark.Visibility = Visibility.Hidden;
            }
        }

        private void cLocation_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if(e.Key== Key.Enter)
            {
                btnContinue_Click(sender, e);
            }
        }
    }
}
