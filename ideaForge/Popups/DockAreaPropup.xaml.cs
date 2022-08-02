using System;
using System.Collections.Generic;
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
    public partial class DockAreaPopup : Window
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
            cDockAreaPopup = new DockAreaPopup();
            cDockAreaPopup.btnContinue.Content = "Continue";
            Login = new Window();
            Login = Application.Current.MainWindow;
            
            Login.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10 };
            cDockAreaPopup.ShowDialog();
            return result;
        }

        public void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
           //Application.Current.Shutdown();
            //this.Close();
        }

        private void btnContinue_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //var dashboard= new Dashboard();
                //dashboard.Show();
                //this.Close();
                //if (Application.Current.Windows[0] is Login)
                //{
                //    var window = Application.Current.Windows[0] as Login;
                //    if (window is Login)
                //    {
                //        window.Close();
                //    }

                //}
                Login.Effect = null;
                result = System.Windows.Forms.DialogResult.Yes;
                var dashboard = new Dashboard();
               
                cDockAreaPopup.Close();
              var loginWindow=  App.Current.Windows.OfType<Login>().FirstOrDefault();
                if (loginWindow is Login)
                {
                    loginWindow.Close();
                }
                dashboard.Show();
            }
            catch (Exception)
            {

                throw;
            }
          

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Application.Current.Shutdown();
        }
    }
}
