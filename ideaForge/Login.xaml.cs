using IdeaForge.Core.Utilities;
using MahApps.Metro.Controls;


namespace ideaForge
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : MetroWindow
    {
        public Login()
        {
            InitializeComponent();
            Global.LoginState = this.WindowState;
        }

        private void loginWindow_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            Global.LoginState = this.WindowState;
        }
    }
}
