using MahApps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Application = System.Windows.Application;

namespace ideaForge.Popups
{
    /// <summary>
    /// Interaction logic for CMessageBox.xaml
    /// </summary>
    public partial class MessageBox : Window
    {
        public MessageBox()
        {
            InitializeComponent();
        }
        static MessageBox cMessageBox;
        static DialogResult result = System.Windows.Forms.DialogResult.No;
        public CMessageTitle messageTitle { get; set; }
        static Window LoginPage { get; set; }
        public enum CMessageButton
        {
            Ok,
            Yes,
            No,
            Cancel,
            Confirm

        }
        public string GetTitle(CMessageTitle value)
        {
            return Enum.GetName(typeof(CMessageTitle), value);
        }
        public string GetButtonText(CMessageButton value)
        {
            return Enum.GetName(typeof(CMessageButton), value);
        }

        public enum CMessageTitle
        {
            Error,
            Info,
            Warning,
            Confirm
        }
        public static DialogResult Show(string message, CMessageTitle title, CMessageButton okButton,string coloredMessage)
        {
            cMessageBox = new MessageBox();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(okButton);
            //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);
            cMessageBox.lblColorMessage.Text = coloredMessage;
            LoginPage = new Window();
            LoginPage = Application.Current.MainWindow;
            //LoginPage.Background = ConvertColor("#000000");
            //LoginPage.Opacity = 0.1;
            //LoginPage.AllowsTransparency = true;
            LoginPage.Effect = new BlurEffect() { RenderingBias = RenderingBias.Quality, KernelType = KernelType.Gaussian, Radius = 10};
            //cMessageBox.Effect =new DropShadowEffect() { Color = Colors.Black, Opacity=100, ShadowDepth=0,BlurRadius=100,  };
            //icon
            switch (title)
            {
                case CMessageTitle.Error:
                    //cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    //cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/ErrorBack.png", UriKind.Relative));
                    cMessageBox.btnOk.Background = ConvertColor("#91C84F");
                 
                    break;
                case CMessageTitle.Info:
                    //cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    //cMessageBox.msgLogo.Foreground = Brushes.Blue;
                    //cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    //cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Warning:
                    //cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    //cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    //cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    //cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Confirm:
                    cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/SuccessBack.png",UriKind.Relative));
                    cMessageBox.btnOk.Background= ConvertColor("#91C84F");
            
                    //cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    //cMessageBox.msgLogo.Foreground = Brushes.Gray;

                    break;
            }
            cMessageBox.ShowDialog();
            return result;
        }
        public static DialogResult ShowError(string message)
        {
            cMessageBox = new MessageBox();
            LoginPage = new Window();
            LoginPage = Application.Current.MainWindow;
            LoginPage.Effect = new BlurEffect();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(CMessageButton.Ok);
            //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(CMessageTitle.Error);
            LoginPage = cMessageBox.Parent as Login;
            LoginPage.Effect = new BlurEffect();
            cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/ErrorBack.png", UriKind.Relative));
            cMessageBox.btnOk.Background = ConvertColor("#91C84F");
            cMessageBox.ShowDialog();
            return result;
        }
        public static DialogResult ShowSuccess(string message, string coloredMessage)
        {
            cMessageBox = new MessageBox();
            cMessageBox.btnOk.Content = cMessageBox.GetButtonText(CMessageButton.Ok);
            LoginPage = new Window();
            LoginPage = Application.Current.MainWindow;
            LoginPage.Effect = new BlurEffect();
            //cMessageBox.btnCancel.Content = cMessageBox.GetButtonText(noButton);
            cMessageBox.txtMessage.Text = message;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(CMessageTitle.Confirm);
            cMessageBox.lblColorMessage.Text = coloredMessage;
            LoginPage = cMessageBox.Parent as Login;
            LoginPage.Effect = new BlurEffect();
            cMessageBox.msgLogo.Source = new BitmapImage(new Uri(@"/Images/SuccessBack.png", UriKind.Relative));
            cMessageBox.btnOk.Background = ConvertColor("#91C84F");
            cMessageBox.ShowDialog();
            return result;
        }
        private void BntOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            Border border = new Border();

            cMessageBox.Close();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 0;
            growAnimation.To = 1;
            storyboard.Children.Add(growAnimation);
           
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);

            storyboard.Begin();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            LoginPage.Effect = null;
            //LoginPage.AllowsTransparency = false;
            Closing -= Window_Closing;
            e.Cancel = true;
            Storyboard storyboard = new Storyboard();

            ScaleTransform scale = new ScaleTransform(1.0, 1.0);
            this.RenderTransformOrigin = new Point(0.5, 0.5);
            this.RenderTransform = scale;

            DoubleAnimation growAnimation = new DoubleAnimation();
            growAnimation.Duration = TimeSpan.FromMilliseconds(300);
            growAnimation.From = 1;
            growAnimation.To = 0;
            storyboard.Children.Add(growAnimation);
      
            Storyboard.SetTargetProperty(growAnimation, new PropertyPath("RenderTransform.ScaleX"));
            Storyboard.SetTarget(growAnimation, this);
            growAnimation.Completed += GrowAnimation_Completed;
       
            storyboard.Begin();
        }

        private void GrowAnimation_Completed(object sender, EventArgs e)
        {
            this.Close();
        }
        private static Brush ConvertColor(string color)
        {
            var converter = new System.Windows.Media.BrushConverter();
            var brush = (Brush)converter.ConvertFromString(color);
            return brush;
        }

        private void btnPopupClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}