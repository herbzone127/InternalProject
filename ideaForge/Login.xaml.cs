using IdeaForge.Core.Utilities;
using log4net;
using MahApps.Metro.Controls;
using System.Windows;

namespace ideaForge
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public Login()
        {
            InitializeComponent();
            Global.LoginState = this.WindowState;
            log4net.Config.XmlConfigurator.Configure();
        }

        private void loginWindow_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Global.LoginState = this.WindowState;
            //if(this.WindowState== System.Windows.WindowState.Maximized)
            //{
            //    loginImage.Margin= new Thickness(0, 80, 0, 80);
            //}
            //if (this.WindowState == System.Windows.WindowState.Maximized)
            //{
            //    loginImage.Margin = new Thickness(0, 40, 0, 40);
            //}
        }
    }
}
