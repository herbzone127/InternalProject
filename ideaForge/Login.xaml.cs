using IdeaForge.Core.Utilities;
using log4net;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

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
            //Global.LoginState = this.WindowState;
            //log4net.Config.XmlConfigurator.Configure();
        }

        private void loginWindow_SizeChanged(object sender, System.Windows.SizeChangedEventArgs e)
        {
            //Global.LoginState = this.WindowState;
            //if(this.WindowState== System.Windows.WindowState.Maximized)
            //{
            //    loginImage.Margin= new Thickness(0, 80, 0, 80);
            //}
            //if (this.WindowState == System.Windows.WindowState.Maximized)
            //{
            //    loginImage.Margin = new Thickness(0, 40, 0, 40);
            //}
        }

        private void StackPanel_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {

        }

        private void loginWindow_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            try
            {
                if (e.Source is Button)
                {
                    Button sp = (Button)(e.Source);
                    ChangeColor(sp.Name == "btnBack");
                }
                else if (e.Source is Image)
                {
                    Image img = (Image)e.Source;
                    ChangeColor(img.Name == "btnBackImage");
                }
                else if (e.Source is TextBlock)
                {
                    TextBlock tb = (TextBlock)e.Source;
                    ChangeColor(tb.Name == "tbback");
                }
                else
                {
                    ChangeColor(false);
                }
            }
            catch (Exception ex)
            { 
            
            }
        }

        void ChangeColor(bool isOver)
        {
            try
            {
                var converter = new ImageSourceConverter();
                string path = isOver ? "backArrow.png" : "graypng.png";
                var result = (ImageSource)converter.ConvertFromString("pack://application:,,,/Images/" + path);
                btnBackImage.Source = result;
                if (isOver)
                {
                    tbback.Foreground = Brushes.Black;
                }
                else
                {
                    tbback.Foreground = Brushes.LightGray;

                }
            }
            catch (Exception)
            { 
            
            }
        }

    }
}
